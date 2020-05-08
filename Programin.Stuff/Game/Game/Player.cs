using System;
using GXPEngine;

public class Player : Sprite
{
    //PUBLIC
    public Vec2 velocity;
    public Vec2 position;

    //PRIVATE
    private MyGame _myGame;
    private float _mousePos;
    private float _prevMousePos;
    private float _speed;
    private float _maxVelocity;
    private float _frictionSpeed;
    private float _lrPos;
    private float _lrSpeed;
    private bool _movingForward;
    private bool _movingBackward;

    public Player() : base("square.png", true)
    {
        position.SetXY(500, 500);
        SetOrigin(width / 2, height / 2);

        _myGame = (MyGame)game;
        _speed = 1f;
        _maxVelocity = 7f;
        _frictionSpeed = 0.3f;
        _movingForward = false;
        _movingBackward = false;

        _lrPos = _myGame.width/2;
        _lrSpeed = 5;
    }

    void HandleInput()
    {
        _prevMousePos = _mousePos;
        _mousePos = Input.mouseX;
        Console.WriteLine("position: " + _lrPos + "             speed: " + _lrSpeed);
        //LIMIT POSITION LR BORDERS
        if (_lrPos > _myGame.width - this.width / 2)
            _lrPos = _myGame.width - this.width / 2;
        else if (_lrPos < this.width / 2)
            _lrPos = this.width / 2;

        if (Input.GetMouseButton(0))
        {
            _lrSpeed = Mathf.Abs(_prevMousePos - _mousePos) / 2.5f;
            if (_prevMousePos < _mousePos)
                _lrPos += _lrSpeed;
            if (_prevMousePos > _mousePos)
                _lrPos -= _lrSpeed;
        }
        //WELL, POS
        position.x = _lrPos;
    }

    void HandleInputArrows()
    { 
        Vec2 unitVector = Vec2.GetUnitVectorDeg(rotation);
        float rotationValue = velocity.Length() / 5f;
        if (Input.GetKey(Key.LEFT))
        {
            if (Input.GetKey(Key.UP) || _movingForward)
            {
                _movingForward = true;
                _movingBackward = false;
                rotation -= rotationValue;
                velocity.RotateDegrees(-rotationValue);
            }
            if (Input.GetKey(Key.DOWN) || _movingBackward)
            {
                _movingBackward = true;
                _movingForward = false;
                rotation += rotationValue;
                velocity.RotateDegrees(rotationValue);
            }

        }
        if (Input.GetKey(Key.RIGHT))
        {
            if (Input.GetKey(Key.UP) || _movingForward)
            {
                _movingForward = true;
                _movingBackward = false;
                rotation += rotationValue;
                velocity.RotateDegrees(rotationValue);
            }
            if (Input.GetKey(Key.DOWN) || _movingBackward)
            {
                _movingBackward = true;
                _movingForward = false;
                rotation -= rotationValue;
                velocity.RotateDegrees(-rotationValue);
            }
        }
        if (Input.GetKey(Key.UP))
        {
            if (velocity.Length() < _maxVelocity)
                velocity += unitVector * _speed;
        }
        if (Input.GetKey(Key.DOWN))
        {
            if (velocity.Length() < _maxVelocity)
                velocity -= unitVector * _speed;
        }
        else if (!Input.GetKey(Key.UP))
        {
            if (velocity.Length() > 1f)
            {
                Vec2 friction = (velocity.Normalized() * _frictionSpeed);
                velocity -= friction;
            }
            else if (velocity.x != 0 || velocity.y != 0)
            {
                velocity.SetXY(0, 0);
                _movingForward = false;
                _movingBackward = false;
            }
        }
    }

    void Update()
    {
       // HandleInput();
        HandleInputArrows();

        Vec2 friction = (velocity.Normalized());
        velocity -= friction * _frictionSpeed;
        position += velocity;

        UpdateScreenPosition();
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }
}