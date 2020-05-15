using GXPEngine;
using System;

public class HUD : Canvas
{
    public HUD() : base(1920, 1080, false)
    {
        _myGame = (MyGame)game;
        _width = _myGame.width;
        _height = _myGame.height;

        SetVolumeButton("sound.png");
        SetDayNightButton("Sun.png");
    }

    void Update()
    {
        _mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Console.WriteLine(_mousePos);
        Vec2 diffVectorDay = _mousePos - new Vec2(_dayNightButton.x, _dayNightButton.y);
        if (diffVectorDay.Length() < _dayNightButton.width / 2 && Input.GetMouseButtonDown(0))
            _myGame.SwitchDayState();

        if (!_myGame.GetDayState())
        {
            _dayNightButton.LateDestroy();
            SetDayNightButton("Moon.png");
        }
        else
        {
            _dayNightButton.LateDestroy();
            SetDayNightButton("Sun.png");
        }

        Vec2 diffVectorVolume = _mousePos - new Vec2(_volumeButton.x, _volumeButton.y);
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

    private Vec2 _mousePos;
    private MyGame _myGame;
    private Sprite _dayNightButton;
    private Sprite _volumeButton;
    private readonly float _width, _height;

    private void SetVolumeButton(string fileName)
    {
        _volumeButton = new Sprite(fileName);
        _volumeButton.SetOrigin(_volumeButton.width / 2, _volumeButton.height / 2);
        _volumeButton.SetXY(_width * 3.4f / 4f, _height / 15f);
        _volumeButton.scale = 0.17f;
        AddChild(_volumeButton);
    }

    private void SetDayNightButton(string fileName)
    {
        _dayNightButton = new Sprite(fileName);
        _dayNightButton.SetOrigin(_dayNightButton.width / 2, _dayNightButton.height / 2);
        _dayNightButton.SetXY(_width * 3.8f / 4f, _height / 15f);
        _dayNightButton.scale = 0.5f;
        AddChild(_dayNightButton);
    }
}
