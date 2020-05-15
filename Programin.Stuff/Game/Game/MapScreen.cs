using System;
using GXPEngine;

public class MapScreen : GameObject
{
    public MapScreen()
	{
        _myGame = (MyGame)game;

        if (_myGame.GetDayState())
            SetupChildren("mapDay.png");
        else
            SetupChildren("mapNight.png");
    }

    void Update()
    {
        if (_myGame.GetDayState())
        {
            CheckAnimalHitbox(new Vec2(430, 420), new Vec2(550, 490), 19);
            CheckAnimalHitbox(new Vec2(530, 340), new Vec2(585, 415), 4);
            CheckAnimalHitbox(new Vec2(640, 365), new Vec2(760, 500), 17);
            CheckAnimalHitbox(new Vec2(360, 30), new Vec2(490, 160), 18);
            CheckAnimalHitbox(new Vec2(980, 25), new Vec2(1100, 190), 9);
            CheckAnimalHitbox(new Vec2(960, 480), new Vec2(1065, 584), 3);
            CheckAnimalHitbox(new Vec2(945, 645), new Vec2(1005, 705), 7);
            CheckAnimalHitbox(new Vec2(1225, 195), new Vec2(1280, 250), 1);
            CheckAnimalHitbox(new Vec2(1505, 195), new Vec2(1635, 310), 20);
            CheckAnimalHitbox(new Vec2(1445, 455), new Vec2(1675, 640), 15);
            CheckAnimalHitbox(new Vec2(445, 750), new Vec2(625, 890), 16);
            CheckAnimalHitbox(new Vec2(330, 790), new Vec2(405, 890), 8);
            CheckAnimalHitbox(new Vec2(650, 800), new Vec2(730, 870), 5);
        }
        else
        {
            CheckAnimalHitbox(new Vec2(405, 820), new Vec2(720, 850), 2);
            CheckAnimalHitbox(new Vec2(910, 440), new Vec2(1010, 535), 14);
            CheckAnimalHitbox(new Vec2(740, 270), new Vec2(810, 350), 12);
            CheckAnimalHitbox(new Vec2(380, 30), new Vec2(490, 110), 6);
            CheckAnimalHitbox(new Vec2(950, 20), new Vec2(1180, 90), 11);
            CheckAnimalHitbox(new Vec2(1470, 150), new Vec2(1620, 220), 10);
            CheckAnimalHitbox(new Vec2(1170, 940), new Vec2(1320, 1000), 13);
        }
    }

    public void SetupChildren(string backgroundFilename)
    {
        _background = new Sprite(backgroundFilename, false);
        AddChild(_background);

        _player = new Player();
        _player.position.SetXY(170, 770);
        _player.rotation = -60f;
        AddChild(_player);

        _HUD = new HUD();
        AddChild(_HUD);

        AddInvisibleWalls();
    }

    public void DestroyChildren()
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

    private MyGame _myGame;
    private Sprite _background;
    private Player _player;
    private HUD _HUD;


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
        AddInvisibleFullLine(new Vec2(1150, 230), new Vec2(1180, 120));
        AddInvisibleFullLine(new Vec2(1180, 120), new Vec2(1020, 0));

        AddInvisibleFullLine(new Vec2(874, 261), new Vec2(736, 545));
        AddInvisibleFullLine(new Vec2(736, 545), new Vec2(490, 582));
        AddInvisibleFullLine(new Vec2(950, 305), new Vec2(795, 508));
        AddInvisibleFullLine(new Vec2(795, 508), new Vec2(846, 707));
        AddInvisibleFullLine(new Vec2(490, 582), new Vec2(846, 707)); //bottom cap
        AddInvisibleFullLine(new Vec2(874, 261), new Vec2(950, 305)); //top cap

        AddInvisibleFullLine(new Vec2(370, 679), new Vec2(805, 820)); //top cap
        AddInvisibleFullLine(new Vec2(370, 679), new Vec2(323, 722));
    }

    private void AddInvisibleFullLine(Vec2 start, Vec2 end)
    {
        LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
        LineSegment invLine = new LineSegment(end, start, 0xff00ff00, 4);
        _myGame.balls.Add(new Ball(0, start, new Vec2(0, 0), false));
        _myGame.balls.Add(new Ball(0, end, new Vec2(0, 0), false));

        line.visible = false;
        invLine.visible = false;

        AddChild(line);
        _myGame.lines.Add(line);
        AddChild(invLine);
        _myGame.lines.Add(invLine);
    }

}
