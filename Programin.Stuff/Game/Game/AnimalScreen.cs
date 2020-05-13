using System;
using System.Drawing;
using System.IO;
using System.Drawing.Text;
using GXPEngine;

public class AnimalScreen : GameObject
{
    public AnimalScreen(string backgroundFilename, string animalFilename, Vec2 animalXY, float animalScale, string animalSoundFilename, string textFilename, int textFileNumber)
    {
        _myGame = (MyGame)game;
        _width = _myGame.width;
        _height = _myGame.height;
        _backgroundFilename = backgroundFilename;
        _animalFilename = animalFilename;
        _animalXY = animalXY;
        _animalScale = animalScale;
        _animalSound = new Sound(animalSoundFilename);
        _textFilename = textFilename;
        _textNumber = 1;
        _maxTextFileNumber = textFileNumber;


        _soundChannel = new SoundChannel(0);
        _soundChannel.Volume = 1;

        SetFonts(35, 22);
        SetChildren(backgroundFilename, animalFilename, textFilename);

        if (!_myGame.GetVolumeState())
            SetVolumeButton("no_sound.png");
        else
        {
            SetVolumeButton("sound.png");
            _soundChannel = _animalSound.Play();
        }
    }

    void Update()
    {
        if (_textNumber < _maxTextFileNumber) // Forward Text button
            SetForwardTextButton();
        else if (_forwardTextButton != null)
        {
            _forwardTextButton.LateDestroy();
            _forwardTextButton = null;
        }

        if (_textNumber > 1) // Back Text button
            SetBackTextButton();
        else if (_backTextButton != null)
        {
            _backTextButton.LateDestroy();
            _backTextButton = null;
        }

        if (!_myGame.GetVolumeState()) //swich volume button;
        {
            _volumeButton.LateDestroy();
            SetVolumeButton("no_sound.png");
            if (_soundChannel.IsPlaying)
                _soundChannel.Stop();
        }
        else
        {
            _volumeButton.LateDestroy();
            SetVolumeButton("sound.png");
            if (!_soundChannel.IsPlaying)
                _soundChannel = _animalSound.Play();
        }

        HandleInput(); 
    }
    private Font _font;
    private Font _fontTitle;
    private PrivateFontCollection _fontCollection;
    private PrivateFontCollection _fontCollectionTitle;
    private MyGame _myGame;
    private Canvas _canvas;
    private Sound _animalSound;
    private SoundChannel _soundChannel;
    private Sprite _background;
    private Sprite _volumeButton;
    private Sprite _menuButton;
    private Sprite _backTextButton;
    private Sprite _forwardTextButton;
    private Sprite _animal;
    private Sprite _textbox;
    private Vec2 _animalXY;
    private string[] _txtLines;
    private readonly int _width, _height;
    private string _textFilename;
    private string _backgroundFilename;
    private string _animalFilename;
    private int _textNumber;
    private int _maxTextFileNumber;
    private float _animalScale;

    private void HandleInput()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        if (Input.GetMouseButtonDown(0))
        {
            Vec2 diffVectorBack = mousePos - new Vec2(_menuButton.x, _menuButton.y);
            if (diffVectorBack.Length() < _menuButton.width / 2)
            {
                if (_soundChannel.IsPlaying)
                    _soundChannel.Stop();
                _myGame.LoadScene(0);
            }
            Vec2 diffVectorVolume = mousePos - new Vec2(_volumeButton.x, _volumeButton.y);
            if (diffVectorVolume.Length() < _volumeButton.width / 2)
                _myGame.SwitchVolumeState();

            if (_backTextButton != null)
            {
                Vec2 diffVectorback = mousePos - new Vec2(_backTextButton.x, _backTextButton.y);
                if (diffVectorback.Length() < _backTextButton.width / 2)
                {
                    _textNumber--;
                    DestroyChildren();
                    SetChildren(_backgroundFilename, _animalFilename, _textFilename);
                }
            }
            if (_forwardTextButton != null)
            {
                Vec2 diffVectorForward = mousePos - new Vec2(_forwardTextButton.x, _forwardTextButton.y);
                if (diffVectorForward.Length() < _forwardTextButton.width / 2)
                {
                    _textNumber++;
                    DestroyChildren();
                    SetChildren(_backgroundFilename, _animalFilename, _textFilename);
                }
            }
        }
    }

    private void SetChildren(string backgroundFilename, string animalFilename, string textFilename)
    {
        _background = new Sprite(backgroundFilename);
        AddChild(_background);
        SetAnimal(animalFilename);
        SetTextBox();
        SetText(textFilename, _textNumber);
        SetMenuButton();
        SetBackTextButton();
        SetForwardTextButton();
    }

    private void DestroyChildren()
    {
        _background.LateDestroy();
        _background = null;
        _animal.LateDestroy();
        _animal = null;
        _textbox.LateDestroy();
        _textbox = null;
        _canvas.LateDestroy();
        _canvas = null;
        _menuButton.LateDestroy();
        _menuButton = null;
        if (_backTextButton != null)
        {
            _backTextButton.LateDestroy();
            _backTextButton = null;
        }
        if (_forwardTextButton != null)
        {
            _forwardTextButton.LateDestroy();
            _forwardTextButton = null;
        }
    }

    private void SetFonts(int titleSize, int contentSize)
    {
        _fontCollectionTitle = new PrivateFontCollection();
        _fontCollectionTitle.AddFontFile("FredokaOne-Regular.ttf");
        _fontCollection = new PrivateFontCollection();
        _fontCollection.AddFontFile("Poppins-Regular.ttf");
        _font = new Font(_fontCollection.Families[0], contentSize);
        _fontTitle = new Font(_fontCollectionTitle.Families[0], titleSize);
    }

    private void SetAnimal(string animalFilename)
    {
        _animal = new Sprite(animalFilename);
        _animal.SetOrigin(_animal.width / 2, _animal.height / 2);
        _animal.scale = _animalScale;
        _animal.SetXY(_animalXY.x, _animalXY.y);
        AddChild(_animal);
    }

    private void SetText(string textFilename, int textNumber)
    {
        _txtLines = File.ReadAllLines(textFilename + textNumber + ".txt");
        if (_canvas == null)
        {
            _canvas = new Canvas(_width, _height);
            AddChild(_canvas);
        }
        _canvas.graphics.Clear(new Color());
        _canvas.graphics.DrawString(_txtLines[0], _fontTitle, Brushes.Black, _width / 14, _height * 2.38f / 4);
        for (int i = 1; i < _txtLines.Length; i++) {
            _canvas.graphics.DrawString(_txtLines[i], _font, Brushes.Black, _width / 14, _height * 2.4f / 4 + (i + 1) * 30);
        }
    }

    private void SetTextBox()
    {
        _textbox = new Sprite("txt_box.png");
        _textbox.SetOrigin(_textbox.width / 2, _textbox.height);
        _textbox.SetXY(_width / 2, _height - 50f);
        _textbox.SetScaleXY(1f, 0.4f);
        AddChild(_textbox);
    }

    private void SetBackTextButton()
    {
        _backTextButton = new Sprite("backButton.png");
        _backTextButton.SetOrigin(_backTextButton.width / 2, _backTextButton.height / 2);
        _backTextButton.SetXY(_width / 25f, _height * 14f / 15f);
        _backTextButton.scale = 0.1f;
        AddChild(_backTextButton);
    }

    private void SetForwardTextButton()
    {
        _forwardTextButton = new Sprite("forwardButton.png");
        _forwardTextButton.SetOrigin(_forwardTextButton.width / 2, _forwardTextButton.height / 2);
        _forwardTextButton.SetXY(_width * 29f / 30f, _height * 14f / 15f);
        _forwardTextButton.scale = 0.1f;
        AddChild(_forwardTextButton);
    }

    private void SetMenuButton()
    {
        _menuButton = new Sprite("backbackButton.png");
        _menuButton.SetOrigin(_menuButton.width / 2, _menuButton.height / 2);
        _menuButton.SetXY(_width / 20f, _height / 15f);
        _menuButton.scale = 0.17f;
        AddChild(_menuButton);
    }

    private void SetVolumeButton(string fileName)
    {
        _volumeButton = new Sprite(fileName);
        _volumeButton.SetOrigin(_volumeButton.width / 2, _volumeButton.height / 2);
        _volumeButton.SetXY(_width * 3.8f / 4f, _height / 15f);
        _volumeButton.scale = 0.17f;
        AddChild(_volumeButton);
    }

}

