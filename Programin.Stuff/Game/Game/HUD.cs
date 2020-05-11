using System;
using System.Drawing;
using System.Drawing.Text;
using GXPEngine;

public class HUD : Canvas
{
    private Font _font;
    private PrivateFontCollection _fontCollection;
    private MapScreen _mapScreen;
    private Sprite dayNight;
    private float _width, _height;

    public HUD(MapScreen mapScreen) : base(1920, 1080, false)
    {
        _mapScreen = mapScreen;
        MyGame myGame = (MyGame)game;
        _width = myGame.width;
        _height = myGame.height;

        //_fontCollection = new PrivateFontCollection();
        // _fontCollection.AddFontFile("CarnevaleeFreakshow.ttf");
        //_font = new Font(_fontCollection.Families[0], 50);

        dayNight = new Sprite("Sun.png");
        dayNight.x = _width * 3 / 4;
        dayNight.y = _height / 4;
        AddChild(dayNight);
    }

    void Update()
    {
        if (_mapScreen.GetDayState())
            dayNight = new Sprite("Moon.png");
    }

    private void drawScore()
    {
        graphics.Clear(Color.Empty);
        graphics.DrawString("", _font, Brushes.White, 75, 50);
    }
}
