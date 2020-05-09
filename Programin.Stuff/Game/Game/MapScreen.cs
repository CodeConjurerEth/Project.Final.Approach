using System;
using GXPEngine;

public class MapScreen : GameObject
{
    public MapScreen()
	{
        Sprite background = new Sprite("map0.png", false);
        background.scale = 0.5f; 
        AddChild(background);

        Player player = new Player();
        AddChild(player);

        _myGame = (MyGame)game;
        _width = _myGame.width;
        _height = _myGame.height;

        AddFullLine(new Vec2(_width / 2, 0), new Vec2(_width / 2, _height / 2));
    }

    private MyGame _myGame;
    private float _width, _height;

    private void AddFullLine(Vec2 start, Vec2 end)
    {
        LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
        LineSegment invLine = new LineSegment(start, end, 0xff00ff00, 4);
        _myGame.balls.Add(new Ball(0, start, new Vec2(0, 0), false));
        _myGame.balls.Add(new Ball(0, end, new Vec2(0, 0), false));

        AddChild(line);
        _myGame.lines.Add(line);
        AddChild(invLine);
        _myGame.lines.Add(invLine);
    }
}
