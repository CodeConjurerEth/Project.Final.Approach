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
            case 1: //thick knees
                _animalScreen = new AnimalScreen("savannaBG.png", "Thick_knees_colored.png", new Vec2(width * 3f / 4f, height/2), 0.5f, "Thick_Knees.wav", "Knees", 1);
                AddChild(_animalScreen);
                break;
            case 2: //Cheetah
                _animalScreen = new AnimalScreen("savannaBG.png", "Cheetah.png", new Vec2(width * 3f / 4f, height / 2), 0.5f, "Cheetah.wav", "African Elephant", 2);
                AddChild(_animalScreen);
                break;
            case 3: //Elephant
                _animalScreen = new AnimalScreen("savannaBG.png", "Elephant.png", new Vec2(width * 3f / 4f, height / 2), 0.5f, "African Elephant.wav", "African Elephant", 3);
                AddChild(_animalScreen);
                break;
            case 7: //Black rhino (night)
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Black_rhino.png", new Vec2(width * 1f / 2f, height / 2), 0.5f, "Black Rhino.wav", "Black Rhino", 2);
                AddChild(_animalScreen);
                break;
            case 8: //Wild boar (night)
                _animalScreen = new AnimalScreen("savanna_dust_day.png", "Common_warthog.png", new Vec2(width * 0.95f / 2f, height / 2), 0.4f, "Wild Boar.wav", "Wild Hog", 2);
                AddChild(_animalScreen);
                break;
            case 9: //ground hornbill
                _animalScreen = new AnimalScreen("savanna_river_day.png", "Ground Hornbill.png", new Vec2(width * 0.2f / 2f, height * 1.3f / 2f), 0.7f, "Southern Ground Hornbill.wav", "Southern Ground Hornbill", 2);
                AddChild(_animalScreen);
                break;
            case 10: //white backed vulture
                _animalScreen = new AnimalScreen("savannaBG.png", "White-backed_vulture.png", new Vec2(width * 0.3f / 2f, height * 0.9f / 2f), 0.6f, "White Backed Vulture.wav", "White Backed Vulture", 1);
                AddChild(_animalScreen);
                break;
            case 11: //tawny frog mouth
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Papuan_Frogmouth.png", new Vec2(width * 1.6f / 2f, height * 1.1f / 2f), 0.5f, "Tawny Frog-Mouth.wav", "Tawny frog mouth", 2);
                AddChild(_animalScreen);
                break;
            case 12: //Greater Adjutant Stork
                _animalScreen = new AnimalScreen("savanna_river_night.png", "Bucorvus.png", new Vec2(width * 1.6f / 2f, height * 0.6f / 2f), 0.3f, "Greater Adjutant Stork.wav", "Greater Adjutant Stork", 2);
                AddChild(_animalScreen);
                break;
            case 13: //Southern_masked_weaver
                _animalScreen = new AnimalScreen("savannaBG.png", "Southern_masked_weaver.png", new Vec2(width * 1.55f / 2f, height * 1f / 2f), 0.5f, "Southern Masked Weaver.wav", "Southern Masked Weaver", 1);
                AddChild(_animalScreen);
                break;
            case 14: //Saddle-billed_stork
                _animalScreen = new AnimalScreen("savanna_river_night.png", "Saddle-billed_stork.png", new Vec2(width * 0.13f / 2f, height * 1.2f / 2f), 0.6f, "Saddle Billed Stork.wav", "Saddle Billed Stork", 1);
                AddChild(_animalScreen);
                break;
            case 15: //Black Mamba
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Black_mamba.png", new Vec2(width * 1f / 2f, height * 1.2f / 2f), 0.3f, "Black Mambas.wav", "Black Mamba", 2);
                AddChild(_animalScreen);
                break;
            case 16: //Nile Crocodile
                _animalScreen = new AnimalScreen("savanna_river_night.png", "Crocodile.png", new Vec2(width * 1f / 2f, height * 0.88f / 2f), 0.35f, "Nile Crocodile.wav", "Nile Crocodile", 1);
                AddChild(_animalScreen);
                break;
            case 17: //Lion
                _animalScreen = new AnimalScreen("nightSavannaBG.png", "Lion.png", new Vec2(width * 1.5f / 2f, height * 1f / 2f), 0.4f, "Lion.wav", "Lion", 3);
                AddChild(_animalScreen);
                break;
            case 18: //Bushbaby
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Bushbaby.png", new Vec2(width * 1.7f / 2f, height * 0.9f / 2f), 0.4f, "Bushbaby.wav", "Bushbaby", 2);
                AddChild(_animalScreen);
                break;
            case 19: //Bushbaby
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Bushbaby.png", new Vec2(width * 1.7f / 2f, height * 0.9f / 2f), 0.4f, "Bushbaby.wav", "Bushbaby", 2);
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

        LoadScene(18);
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
           // Console.WriteLine(game.GetDiagnostics());
        }
	}

	static void Main() {
		new MyGame().Start();
	}
}