using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	
    public void DrawLine(Vec2 start, Vec2 end) {
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

    public MyGame() : base(1920, 1080, false, false)
    {
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;
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

        //start new scene
        switch (sceneNumber)
        {
            default:
                MapScreen mapScreen = new MapScreen();
                AddChild(mapScreen);
                break;
        }
    }

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