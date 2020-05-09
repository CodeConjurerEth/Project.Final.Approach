using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    public List<Ball> balls;
    public List<LineSegment> lines;

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

        //start new scene
        switch (sceneNumber)
        {
            default:
                MapScreen mapScreen = new MapScreen();
                AddChild(mapScreen);
                break;
        }
    }

    public MyGame() : base(1920, 1080, false, false)
    {
        balls = new List<Ball>();
        lines = new List<LineSegment>();

        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;
        LoadScene(_startSceneNumber);
    }

    // PRIVATE
    private bool _paused = false;
    private int _startSceneNumber = 0;

    private  Canvas _lineContainer = null;

	void Update () {
        targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;
        if (!_paused) {
            //StepThroughX();
		}
	}

	static void Main() {
		new MyGame().Start();
	}
}