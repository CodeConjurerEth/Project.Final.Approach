using System;
using GXPEngine;

public class Player : Sprite
{
    //PUBLIC
    public Vec2 velocity;
    public Vec2 position;

    public Player() : base("car.png", true)
    {
        position.SetXY(500, 500);
        SetOrigin(width / 2, height / 2);

        _myGame = (MyGame)game;
        _speed = 1f;
        _maxVelocity = 7f;
        _frictionSpeed = 0.3f;
        _movingForward = false;
        _movingBackward = false;

        _lrPos = _myGame.width / 2;
        _lrSpeed = 5;
    }

    //PRIVATE
    private MyGame _myGame;
    private Vec2 _oldPosition;
    private float _mousePos;
    private float _prevMousePos;
    private float _speed;
    private float _maxVelocity;
    private float _frictionSpeed;
    private float _lrPos;
    private float _lrSpeed;
    private bool _movingForward;
    private bool _movingBackward;

    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;
        Vec2 finalUnitNormal = new Vec2(0, 0);
        GameObject finalGameObj = this;
        float finalColTime = 99;
        float radius = height / 2;
        for (int i = 0; i < myGame.GetNumberOfLines(); i++) //kinda works (circle hitbox)
        {
            LineSegment lineSegment = myGame.GetLine(i);
            Vec2 lineVec = lineSegment.end - lineSegment.start;
            Vec2 lineNormal = lineVec.Normal();
            Vec2 oldDifferenceVec = _oldPosition - lineSegment.start;
            Vec2 differenceVec = position - lineSegment.start;
            float differenceVecLength = differenceVec.Length();
            float projection = lineVec.Normalized().Dot(differenceVec);
            float blockDistance = Mathf.Sqrt(differenceVecLength * differenceVecLength - projection * projection);
            float colTime = 99;
            if (blockDistance < radius) // circle collision
            {
                float a = lineNormal.Dot(oldDifferenceVec) - radius;
                float b = -lineNormal.Dot(velocity);
                if (b >= 0)
                {
                    if (a >= 0)
                        colTime = a / b;
                    else if (a >= -radius)
                        colTime = 0;
                    if (colTime <= 1)
                    {
                        Vec2 pointOfImpact = _oldPosition + velocity * colTime;
                        Vec2 newDifferenceVec = pointOfImpact - lineSegment.start;
                        float distOnLine = lineVec.Normalized().Dot(newDifferenceVec);
                        if (distOnLine >= 0 && distOnLine <= lineVec.Length())
                            if (colTime < finalColTime)
                            {
                                finalColTime = colTime;
                                finalGameObj = lineSegment;
                                finalUnitNormal = lineNormal;
                            }
                    }
                }
            }
        }
        for (int i = 0; i < myGame.GetNumberOfBallMovers(); i++) //ball to ball collision
        {
            Ball mover = myGame.GetBallMover(i);
            Vec2 relativePosition = _oldPosition - mover.position;
            if (relativePosition.Length() < radius + mover.radius)
            {
                float a = velocity.Length() * velocity.Length();
                float b = 2 * velocity.Dot(relativePosition);
                float c = (relativePosition.Length() * relativePosition.Length()) - ((radius + mover.radius) * (radius + mover.radius));
                float colTime;
                if (c < 0)
                {
                    if (b < 0)
                    {
                        colTime = 0;
                        if (colTime <= finalColTime)
                        {
                            Vec2 pointOfImpact = _oldPosition + velocity * colTime;
                            Vec2 unitNormal = (pointOfImpact - mover.position).Normalized();
                            finalColTime = colTime;
                            finalGameObj = mover;
                            finalUnitNormal = unitNormal;
                        }
                    }
                    if (a < Mathf.Abs(0.00001f))
                    {
                        float delta = (b * b) - (4 * a * c);
                        if (delta >= 0)
                        {
                            colTime = (-b - Mathf.Sqrt(delta)) / (2 * a);
                            if (colTime >= 0 && colTime < 1)
                            {
                                Vec2 pointOfImpact = _oldPosition + velocity * colTime;
                                Vec2 unitNormal = (pointOfImpact - mover.position).Normalized();
                                finalColTime = colTime;
                                finalGameObj = mover;
                                finalUnitNormal = unitNormal;
                            }
                        }
                    }
                }
            }
        }
        if (finalColTime != 99)
            return new CollisionInfo(finalUnitNormal, finalGameObj, finalColTime);
        else
            return null;
    }

    private void ResolveCollision(CollisionInfo col)
    {
        if (col.other is LineSegment)
        {
            position = _oldPosition;
            velocity.SetXY(0, 0);
        }
        if (col.other is Ball)
        {
            position = _oldPosition;
            velocity.SetXY(0, 0);
        }
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
        float rotationValue = velocity.Length() / 3f;
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
        //HandleInput();
        HandleInputArrows();

        _oldPosition = position;
        Vec2 friction = (velocity.Normalized());
        velocity -= friction * _frictionSpeed;
        position += velocity;

        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
            if (firstCollision.timeOfImpact < Mathf.Abs(0.00001f))
            {
                _oldPosition = position;
                position += velocity;
                CollisionInfo secondCollision = FindEarliestCollision();
                if (secondCollision != null)
                {
                    ResolveCollision(secondCollision);
                }
            }
        }

        UpdateScreenPosition();
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }
}