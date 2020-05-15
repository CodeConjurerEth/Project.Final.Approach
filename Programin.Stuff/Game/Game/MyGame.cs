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

    public bool GetDayState()
    {
        return _day;
    }

    public void SwitchDayState()
    {
        if (_day)
        {
            _day = false;
           _mapScreen.DestroyChildren();
            _mapScreen.SetupChildren("mapNight.png");
        }
        else
        {
            _day = true;
            _mapScreen.DestroyChildren();
            _mapScreen.SetupChildren("mapDay.png");
        }
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
            case 2: //Black rhino (night)
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Black_rhino.png", new Vec2(width * 1f / 2f, height / 2), 0.5f, "Black Rhino.wav", "Black Rhino", 2);
                AddChild(_animalScreen);
                break;
            case 3: //Wild boar
                _animalScreen = new AnimalScreen("savanna_dust_day.png", "Common_warthog.png", new Vec2(width * 0.95f / 2f, height / 2), 0.4f, "Wild Boar.wav", "Wild Hog", 2);
                AddChild(_animalScreen);
                break;
            case 4: //ground hornbill
                _animalScreen = new AnimalScreen("savanna_river_day.png", "Ground Hornbill.png", new Vec2(width * 0.2f / 2f, height * 1.3f / 2f), 0.7f, "Southern Ground Hornbill.wav", "Southern Ground Hornbill", 2);
                AddChild(_animalScreen);
                break;
            case 5: //white backed vulture
                _animalScreen = new AnimalScreen("savannaBG.png", "White-backed_vulture.png", new Vec2(width * 0.3f / 2f, height * 0.9f / 2f), 0.6f, "White Backed Vulture.wav", "White Backed Vulture", 1);
                AddChild(_animalScreen);
                break;
            case 6: //tawny frog mouth (night)
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Papuan_Frogmouth.png", new Vec2(width * 1.6f / 2f, height * 1.1f / 2f), 0.5f, "Tawny Frog-Mouth.wav", "Tawny frog mouth", 2);
                AddChild(_animalScreen);
                break;
            case 7: //Greater Adjutant Stork
                _animalScreen = new AnimalScreen("savanna_river_day.png", "Bucorvus.png", new Vec2(width * 1.6f / 2f, height * 0.6f / 2f), 0.4f, "Greater Adjutant Stork.wav", "Greater Adjutant Stork", 2);
                AddChild(_animalScreen);
                break;
            case 8: //Southern_masked_weaver
                _animalScreen = new AnimalScreen("savannaBG.png", "Southern_masked_weaver.png", new Vec2(width * 1.55f / 2f, height * 1f / 2f), 0.5f, "Southern Masked Weaver.wav", "Southern Masked Weaver", 1);
                AddChild(_animalScreen);
                break;
            case 9: //Saddle-billed_stork
                _animalScreen = new AnimalScreen("savanna_river_day.png", "Saddle-billed_stork.png", new Vec2(width * 0.13f / 2f, height * 1.2f / 2f), 0.6f, "Saddle Billed Stork.wav", "Saddle Billed Stork", 1);
                AddChild(_animalScreen);
                break;
            case 10: //Black Mamba (night)
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Black_mamba.png", new Vec2(width * 1f / 2f, height * 1.2f / 2f), 0.3f, "Black Mambas.wav", "Black Mamba", 2);
                AddChild(_animalScreen);
                break;
            case 11: //Nile Crocodile (night)
                _animalScreen = new AnimalScreen("savanna_river_night.png", "Crocodile.png", new Vec2(width * 1f / 2f, height * 0.88f / 2f), 0.35f, "Nile Crocodile.wav", "Nile Crocodile", 1);
                AddChild(_animalScreen);
                break;
            case 12: //Lion (night)
                _animalScreen = new AnimalScreen("nightSavannaBG.png", "Lion.png", new Vec2(width * 1.5f / 2f, height * 1f / 2f), 0.4f, "Lion.wav", "Lion", 3);
                AddChild(_animalScreen);
                break;
            case 13: //Bushbaby (night)
                _animalScreen = new AnimalScreen("savanna_dust_night.png", "Bushbaby.png", new Vec2(width * 1.6f / 2f, height * 0.9f / 2f), 0.4f, "Bushbaby.wav", "Bushbaby", 2);
                AddChild(_animalScreen);
                break;
            case 14: //African Wild Dogs (night)
                _animalScreen = new AnimalScreen("nightSavannaBG.png", "African_wild_dog.png", new Vec2(width * 1.5f / 2f, height * 1f / 2f), 0.45f, "African Wild Dogs.wav", "African Wild Dogs", 2);
                AddChild(_animalScreen);
                break;
            case 15: //Giraffe
                _animalScreen = new AnimalScreen("savanna_dust_day.png", "Giraffe.png", new Vec2(width * 0.1f / 2f, height * 1.1f / 2f), 0.5f, "Giraffe.wav", "Giraffe", 2);
                AddChild(_animalScreen);
                break;
            case 16: //Hippo
                _animalScreen = new AnimalScreen("savanna_river_day.png", "Hippo.png", new Vec2(width * 0.5f / 2f, height * 0.8f / 2f), 0.3f, "Hippo.wav", "Hippopotamus", 2);
                AddChild(_animalScreen);
                break;
            case 17: //Elephant
                _animalScreen = new AnimalScreen("savanna_dust_day.png", "Elephant.png", new Vec2(width * 1.7f / 2f, height / 2), 0.5f, "African Elephants.wav", "African Elephant", 3);
                AddChild(_animalScreen);
                break;
            case 18: //Gazelle
                _animalScreen = new AnimalScreen("savannaBG.png", "Gazelle.png", new Vec2(width * 1.5f / 2f, height * 0.95f / 2f), 0.4f, "Gazelle.wav", "Gazelle", 2);
                AddChild(_animalScreen);
                break;
            case 19: //Cheetah
                _animalScreen = new AnimalScreen("savannaBG.png", "Cheetah.png", new Vec2(width * 1.1f / 4f, height * 0.95f / 2f), 0.5f, "Cheetah.wav", "Cheetah", 2);
                AddChild(_animalScreen);
                break;
            case 20: //Zebra
                _animalScreen = new AnimalScreen("savanna_dust_day.png", "zebra.png", new Vec2(width * 0.8f / 2f, height * 0.95f / 2f), 0.5f, "Zebra.wav", "Zebra", 2);
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
        _day = true;

        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        LoadScene(21);
    }

    // PRIVATE
    private int _startSceneNumber = 0;
    private bool _volume;
    private bool _day;

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