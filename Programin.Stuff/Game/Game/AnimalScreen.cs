using System;
using GXPEngine;

public class AnimalScreen : GameObject
{
    public AnimalScreen(string backgroundFilename)
    {
        _myGame = (MyGame)game;
        _width = _myGame.width;
        _height = _myGame.height;

        Sprite background = new Sprite(backgroundFilename, false);
        AddChild(background);

        SetTextBox();
        SetBackButton();

        if (!_myGame.GetVolumeState())
            SetVolumeButton("no_sound.png");
        else
            SetVolumeButton("sound.png");
    }

    void Update()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);

        Vec2 diffVectorBack = mousePos - new Vec2(_backButton.x, _backButton.y);
        if (diffVectorBack.Length() < _backButton.width / 2 && Input.GetMouseButtonDown(0))
            _myGame.LoadScene(0);

        Vec2 diffVectorVolume = mousePos - new Vec2(_volumeButton.x, _volumeButton.y);
        if (diffVectorVolume.Length() < _volumeButton.width / 2 && Input.GetMouseButtonDown(0))
            _myGame.SwitchVolumeState();

        if (!_myGame.GetVolumeState())
        {
            _volumeButton.LateDestroy();
            SetVolumeButton("no_sound.png");
        }
        else
        {
            _volumeButton.LateDestroy();
            SetVolumeButton("sound.png");
        }
    }

    private MyGame _myGame;
    private Sprite _volumeButton;
    private Sprite _backButton;
    private float _width, _height;

    private void SetTextBox()
    {
        Sprite textbox = new Sprite("txt_box.png");
        textbox.SetOrigin(textbox.width / 2, textbox.height);
        textbox.SetXY(_width / 2, _height - 50f);
        textbox.SetScaleXY(1.1f, 0.4f);
        AddChild(textbox);
    }

    private void SetBackButton()
    {
        _backButton = new Sprite("backButton.png");
        _backButton.SetOrigin(_backButton.width / 2, _backButton.height / 2);
        _backButton.SetXY(_width / 20f, _height / 15f);
        _backButton.scale = 0.17f;
        AddChild(_backButton);
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

