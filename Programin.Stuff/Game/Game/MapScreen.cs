using System;
using GXPEngine;

public class MapScreen : GameObject
{
    public MapScreen()
	{
        _myGame = (MyGame)game;
        _day = true;

        SetupChildren("mapalmost.png");
    }

    public bool GetDayState()
    {
        return _day;
    }

    public void SwitchDayState()
    {
        if (_day)
        {
            _day = false;
            DestroyChildren();
            SetupChildren("mapSketchpng.png");
        }
        else
        {
            _day = true;
            DestroyChildren();
            SetupChildren("mapalmost.png");
        }
    }

    void Update()
    {
        if (_day)
        {
            CheckAnimalHitbox(new Vec2(430, 420), new Vec2(550, 490), 2);
            CheckAnimalHitbox(new Vec2(635, 380), new Vec2(705, 490), 3);
            CheckAnimalHitbox(new Vec2(735, 275), new Vec2(800, 345), 4);
            CheckAnimalHitbox(new Vec2(1175, 940), new Vec2(1325, 1080), 5);
            CheckAnimalHitbox(new Vec2(1485, 690), new Vec2(1620, 770), 6);
        }
    }

    private MyGame _myGame;
    private Sprite _background;
    private Player _player;
    private HUD _HUD;
    private bool _day;

    private void SetupChildren(string backgroundFilename)
    {
        _background = new Sprite(backgroundFilename, false);
        AddChild(_background);

        _player = new Player();
        _player.position.SetXY(170, 770);
        _player.rotation = -60f;
        AddChild(_player);

        _HUD = new HUD(this);
        AddChild(_HUD);

        AddInvisibleWalls();
    }

    private void DestroyChildren()
    {
        _background.LateDestroy();
        _background = null;
        _player.LateDestroy();
        _player = null;
        _HUD.LateDestroy();
        _HUD = null;
       
        foreach (Ball ball in _myGame.balls)
        {
            ball.Destroy();
        }
        _myGame.balls.Clear();
        foreach (LineSegment line in _myGame.lines)
        {
            line.Destroy();
        }
        _myGame.lines.Clear();
    }

    private void CheckAnimalHitbox(Vec2 topLeft, Vec2 bottomRight, int sceneNumber)
    {
        topLeft.x -= _player.height / 3;
        topLeft.y -= _player.height / 3;
        bottomRight.x += _player.height / 3;
        bottomRight.y += _player.height / 3;
        if (_player.position.x > topLeft.x && _player.position.x < bottomRight.x && _player.position.y > topLeft.y && _player.position.y < bottomRight.y)
            _myGame.LoadScene(sceneNumber);
    }

    private void AddInvisibleWalls()
    {
        AddInvisibleFullLine(new Vec2(970, 0), new Vec2(1030, 130));
        AddInvisibleFullLine(new Vec2(1030, 130), new Vec2(1007, 160));
       // AddInvisibleFullLine(new Vec2(1007, 160), new Vec2(1100, 215)); //bottom cap
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
        //AddInvisibleFullLine(new Vec2(370, 679), new Vec2(805, 820)); //top cap
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
