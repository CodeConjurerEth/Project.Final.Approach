using System;
using GXPEngine;

public class MapScreen : GameObject
{
    private Vec2 _mousePos;

    public MapScreen()
	{
        Sprite background = new Sprite("mapSketch.png", false);
        AddChild(background);

        Player player = new Player();
        AddChild(player);

        HUD HUD = new HUD(this);
        AddChild(HUD);

        _myGame = (MyGame)game;
        _width = _myGame.width;
        _height = _myGame.height;

        AddInvisibleWalls();
    }

    void Update()
    {
        _mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Console.WriteLine(_mousePos);
    }

    public bool GetDayState()
    {
        return day;
    }

    private MyGame _myGame;
    private float _width, _height;
    private bool day = true;

    private void AddInvisibleWalls()
    {
        AddInvisibleFullLine(new Vec2(970, 0), new Vec2(1030, 130));
        AddInvisibleFullLine(new Vec2(1030, 130), new Vec2(1007, 160));
        AddInvisibleFullLine(new Vec2(1007, 160), new Vec2(1100, 215)); //bottom cap
        AddInvisibleFullLine(new Vec2(1100, 215), new Vec2(1200, 205));
        AddInvisibleFullLine(new Vec2(1200, 205), new Vec2(1180, 120));
        AddInvisibleFullLine(new Vec2(1180, 120), new Vec2(1020, 0));

        AddInvisibleFullLine(new Vec2(874, 261), new Vec2(736, 545));
        AddInvisibleFullLine(new Vec2(736, 545), new Vec2(490, 582));
        AddInvisibleFullLine(new Vec2(950, 305), new Vec2(795, 508));
        AddInvisibleFullLine(new Vec2(795, 508), new Vec2(846, 707));
        AddInvisibleFullLine(new Vec2(490, 582), new Vec2(846, 707)); //bottom cap
        AddInvisibleFullLine(new Vec2(874, 261), new Vec2(950, 305)); //top cap

        AddInvisibleFullLine(new Vec2(370, 679), new Vec2(280, 910));
        AddInvisibleFullLine(new Vec2(280, 910), new Vec2(821, 1080));
        AddInvisibleFullLine(new Vec2(821, 1080), new Vec2(805, 820));
        AddInvisibleFullLine(new Vec2(370, 679), new Vec2(805, 820)); //top cap
    }

    private void AddInvisibleFullLine(Vec2 start, Vec2 end)
    {
        LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
        LineSegment invLine = new LineSegment(end, start, 0xff00ff00, 4);
        _myGame.balls.Add(new Ball(0, start, new Vec2(0, 0), false));
        _myGame.balls.Add(new Ball(0, end, new Vec2(0, 0), false));

        //line.visible = false;
        //invLine.visible = false;

        AddChild(line);
        _myGame.lines.Add(line);
        AddChild(invLine);
        _myGame.lines.Add(invLine);
    }
}
