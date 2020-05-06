using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	
	public List<Ball> balls;
	public List<LineSegment> lines;
    public List<Block> blocks;

    public Level level;

	public int GetNumberOfLines() {
		return lines.Count;
	}

	public LineSegment GetLine(int index) {
		if (index >= 0 && index < lines.Count) {
			return lines [index];
		}
		return null;	
	}

    public int GetNumberOfBlockMovers()
    {
        return blocks.Count;
    }

    public Block GetBlockMover(int index)
    {
        if (index >= 0 && index < blocks.Count)
        {
            return blocks[index];
        }
        return null;
    }

    public int GetNumberOfBallMovers() {
		return balls.Count;
	}

	public Ball GetBallMover(int index) {
		if (index >= 0 && index < balls.Count) {
			return balls [index];
		}
		return null;
	}

    public void DrawLine(Vec2 start, Vec2 end) {
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}
    public MyGame() : base(1600, 900, false, false)
    {
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;

        balls = new List<Ball>();
        lines = new List<LineSegment>();
        blocks = new List<Block>();

        Vec2Test();
        Tutorial();
        LoadScene(_startSceneNumber);
    }

    // PRIVATE
    bool _paused = false;
    int _startSceneNumber = 0;

    Canvas _lineContainer = null;

    public void LoadScene(int sceneNumber)
    {
        _startSceneNumber = sceneNumber;
        // remove previous scene:
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
        foreach (Block block in blocks)
        {
            block.Destroy();
        }
        blocks.Clear();

        switch (sceneNumber)
        {
            default:
                Level pLevel = new Level();
                level = pLevel;
                AddChild(level);
                break;
        }
    }

	/****************************************************************************************/

	void HandleInput() {
        targetFps = Input.GetKey(Key.ENTER) ? 5 : 60;
        if (Input.GetKeyDown(Key.D))
        {
            Ball.drawDebugLine ^= false;
        }
        if (Input.GetKeyDown(Key.P))
        {
            _paused ^= true;
        }
        if (Input.GetKeyDown(Key.W))
        {
            Ball.wordy ^= true;
        }
        if (Input.GetKeyDown(Key.C))
        {
            _lineContainer.graphics.Clear(Color.Black);
        }
        if (Input.GetKeyDown(Key.R))
        {
            level.Destroy();
            LoadScene(_startSceneNumber);
        }
    }

	void StepThroughMovers() {
	    foreach (Ball mover in balls) {
			if (mover.moving) {
				mover.Step ();
			}
		}
        foreach (Block mover in blocks) {
            if (mover.moving){
                mover.Step();
            }
        }
    }

	void Update () {
        HandleInput();
		if (!_paused) {
			StepThroughMovers ();
		}
	}

	static void Main() {
		new MyGame().Start();
	}

    private void Vec2Test()
    {
        float result0 = Mathf.PI / 2f;
        Console.WriteLine("Deg2Rad ok?: " + (Vec2.Deg2Rad(90) == result0));

        float result1 = 90f;
        Console.WriteLine("Rad2Deg ok?: " + (Vec2.Rad2Deg(result0) == result1));

        Vec2 result2 = new Vec2(Mathf.Sqrt(0.5f), Mathf.Sqrt(0.5f));
        Vec2 test0 = Vec2.GetUnitVectorDeg(45f);
        Console.WriteLine("GetUnitVectorDeg ok?: " + (test0.x == result2.x && test0.y == result2.y));

        Vec2 test1 = Vec2.GetUnitVectorRad(Mathf.PI / 4f);
        Console.WriteLine("GetUnitVectorRad ok?: " + (test1.x == result2.x && test1.y == result2.y));

        Vec2 test2 = Vec2.GetRandomUnitVector();
        Console.WriteLine("GetRandomUnitVector ok?: " + (test2.Length() == 1f));

        test2.SetAngleDegrees(45f);
        float sqrtHalf = Mathf.Sqrt(0.5f);
        Console.WriteLine("SetAngleDegrees ok?: " + (test2.x == sqrtHalf && test2.y == sqrtHalf));

        test2 = Vec2.GetRandomUnitVector();
        test2.SetAngleRadians(Mathf.PI / 4f);
        Console.WriteLine("SetAngleRadians ok?: " + (test2.x == sqrtHalf && test2.y == sqrtHalf));

        Vec2 test3 = new Vec2(0, 1);
        Console.WriteLine("GetAngleRadians ok?: " + (test3.GetAngleRadians() == Mathf.PI / 2f));

        Console.WriteLine("GetAngleDegrees ok?: " + (test2.GetAngleDegrees() == 45f));

        test2.RotateDegrees(45f);
        Console.WriteLine("RotateDegrees ok?: " + (test2.GetAngleDegrees() == 90f));

        test2.RotateRadians(Mathf.PI / 4f);
        Console.WriteLine("RotateRadians ok? " + (test2.GetAngleDegrees() == 135f));

        Vec2 rotatePoint = new Vec2(1, 0);
        Vec2 test3copy = test3;
        test3.RotateAroundDegrees(rotatePoint, 90f);
        test3copy -= rotatePoint;
        test3copy.RotateDegrees(90f);
        test3copy += rotatePoint;
        Console.WriteLine("RotateAroundDegrees ok?: " + (test3.x < Mathf.Abs(0.0001f) && test3.x < Mathf.Abs(0.0001f) && test3.y == test3copy.y));

        Vec2 test4 = new Vec2(0, 1);
        Vec2 test4copy = test4;
        test4.RotateAroundRadians(rotatePoint, Mathf.PI / 2);
        test4copy -= rotatePoint;
        test4copy.RotateRadians(Mathf.PI / 2);
        test4copy += rotatePoint;
        Console.WriteLine("RotateAroundRadians ok?: " + (test4.x < Mathf.Abs(0.0001f) && test4copy.x < Mathf.Abs(0.0001f) && test4.y == test4copy.y));

        Vec2 testLength = new Vec2(10, 0);
        float Lengthresult = 10f;
        Console.WriteLine("Length ok?: " + (testLength.Length() == Lengthresult));

        Vec2 normalizeResult = new Vec2(1, 0);
        testLength.Normalize();
        Console.WriteLine("Normalize ok?: " + (testLength == normalizeResult));

        Vec2 testNormalized = new Vec2(10, 0);
        Console.WriteLine("Normalized ok?: " + (testNormalized.Normalized() == normalizeResult));

        float dotresult = 23f;
        Vec2 dotFirst = new Vec2(2, 3);
        Vec2 dotSecond = new Vec2(4, 5);
        Console.WriteLine("Dot ok?: " + (dotFirst.Dot(dotSecond) == dotresult));

        Vec2 normalResult = new Vec2(-2, 1);
        Vec2 normalTest = new Vec2(1, 2);
        normalTest.Normalize();
        normalResult.Normalize();
        Console.WriteLine("Normal ok?: " + (normalTest.Normal() == normalResult));

        Vec2 reflectResult = new Vec2(-1, 2);
        Vec2 reflectNormal = new Vec2(-1, 1);
        Vec2 reflectTestVelocity = new Vec2(1, 0);
        Console.WriteLine("Reflect ok?: " + (reflectTestVelocity.Reflect(reflectNormal, 1) == reflectResult));

        Console.WriteLine("");
    } 

    private void Tutorial()
    {
        Console.WriteLine("Move using the WASD keys");
        Console.WriteLine("Aim using the mouse");
        Console.WriteLine("Shoot a small ball that has bigger bounciness with the Z key");
        Console.WriteLine("Shoot a bigger ball that has smaller bounciness with the X key");
        Console.WriteLine("Skip your turn using spacebar key");
        Console.WriteLine("If one of the players gets hit the level resets");
    }
}