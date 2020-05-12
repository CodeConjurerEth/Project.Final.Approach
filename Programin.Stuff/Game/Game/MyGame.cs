using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    public List<Ball> balls;
    public List<LineSegment> lines;

    public bool GetVolumeState()
    {
        return _volume;
    }

    public void SwitchVolumeState()
    {
        if (_volume)
            _volume = false;
        else
            _volume = true;
    }

    public int GetNumberOfLines()
    {
        return lines.Count;
    }

    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < lines.Count)
        {
            return lines[index];
        }
        return null;
    }

    public int GetNumberOfBallMovers()
    {
        return balls.Count;
    }

    public Ball GetBallMover(int index)
    {
        if (index >= 0 && index < balls.Count)
        {
            return balls[index];
        }
        return null;
    }

    public void DrawLine(Vec2 start, Vec2 end) {
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

     public void LoadScene(int sceneNumber)
    {
        _startSceneNumber = sceneNumber;
        // remove previous scene:
        if (_mapScreen != null)
        {
            _mapScreen.LateDestroy();
        }
        if (_animalScreen != null)
        {
            _animalScreen.LateDestroy();
        }

        foreach (Ball mover in balls)
        {
            mover.Destroy();
        }
        balls.Clear();
        foreach (LineSegment line in lines)
        {
            line.Destroy();
        }
        lines.Clear();

        //start new scene
        switch (sceneNumber)
        {
            default:
                _mapScreen = new MapScreen();
                AddChild(_mapScreen);
                break;
            case 1:
                _animalScreen = new AnimalScreen("savannaBG.png");
                AddChild(_animalScreen);
                break;
        }
    }

    public MyGame() : base(1920, 1080, false, false)
    {
        balls = new List<Ball>();
        lines = new List<LineSegment>();

        targetFps = 60;
        _volume = true;

        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        LoadScene(_startSceneNumber);
    }

    // PRIVATE
    private int _startSceneNumber = 0;
    private bool _volume;

    private MapScreen _mapScreen;
    private AnimalScreen _animalScreen;
    private Canvas _lineContainer = null;

	void Update () {
        if (Input.GetKeyDown(Key.R)) {
            LoadScene(_startSceneNumber);
		}
	}

	static void Main() {
		new MyGame().Start();
	}
}