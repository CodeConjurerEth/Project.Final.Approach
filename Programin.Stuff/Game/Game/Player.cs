using System;
using GXPEngine;

public class Player : Sprite
{
    //PUBLIC
    public Vec2 velocity;
    public Vec2 position;

    float _speed;
    float _maxVelocity;
    float _frictionSpeed;
    bool _movingForward;
    bool _movingBackward;

    public Player() : base("square.png", true)
    {
        position.SetXY(500, 500);
        SetOrigin(width / 2, height / 2);

        _speed = 1f;
        _maxVelocity = 7f;
        _frictionSpeed = 0.3f;
        _movingForward = false;
        _movingBackward = false;
    }

    void HandleInput()
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
        HandleInput();

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

