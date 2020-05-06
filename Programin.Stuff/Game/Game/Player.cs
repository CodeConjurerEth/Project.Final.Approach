using System;
using GXPEngine;

public class Player : GameObject
{
    //PUBLIC
    public Vec2 velocity;
    public Vec2 position;
    public LineSegment line;

    public static float bounciness = 0.98f;
    public static Vec2 acceleration = new Vec2(0, 1);

    //PRIVATE
    private MyGame _myGame;
    private Ball _ball;

    private Vec2 _unitVectorRight;
    private Vec2 _unitVectorLeft;
    private Vec2 _unitVectorJump;
    private Vec2 mousePos;
    private Vec2 _delta;

    private int _radius;
    private int _right;

    private float _firstX;
    private float _firstY;
    private float _speed;
    private float _jumpSpeed;
    private float _maxVelocity;
    private float _frictionSpeed;
    private float _ballSpeed;

    private bool _grounded;
    private bool _thisPlayerTurn;

    public Player(Vec2 pPosition, int pRadius, bool pThisPlayerTurn)
    {
        _myGame = (MyGame)game;
        _ball = null;

        _firstX = pPosition.x;
        _firstY = pPosition.y;
        position = pPosition;
        _radius = pRadius;
        _thisPlayerTurn = pThisPlayerTurn;

        velocity.SetXY(0, 0);
        _grounded = true;
        _right = 1;
        _speed = 1f;
        _jumpSpeed = 30f;
        _maxVelocity = 7f;
        _frictionSpeed = 0.3f;
        _ballSpeed = 20f;

        _unitVectorRight = Vec2.GetUnitVectorRad(0);
        _unitVectorLeft.SetXY(-1, 0);
        _unitVectorJump.SetXY(0, -1);

        Ball head = new Ball(_radius, new Vec2(position.x - x, position.y -_radius - y), new Vec2(0, 0), false);
        Block body = new Block(_radius, new Vec2(position.x - x, position.y + _radius - y), new Vec2(0, 0), false);
        LineSegment aimLine = new LineSegment(new Vec2(position.x + _radius - x, position.y + _radius - y), (new Vec2(position.x + _radius - x + 100f, position.y + _radius - y)), 0xff00ff00, 4);
        AddChild(head);
        AddChild(body);
        AddChild(aimLine);
        this.line = aimLine;
        if (!_thisPlayerTurn)
            line.visible = false;
    }

    void Update()
    {
        HandleInput();
        StopAtBoundaries();
        drawAimLine();
        checkDead();
        limitOnScreenBalls();

        Vec2 friction = (velocity.Normalized() * _frictionSpeed);
        velocity -= friction;
        position += velocity;

        UpdateScreenPosition();
    }

    void checkDead()
    {
        for (int i = 0; i < _myGame.GetNumberOfBallMovers(); i++)
        {
            Ball mover = _myGame.GetBallMover(i);
            if (Mathf.Abs(mover.position.x - position.x) < _radius + mover.radius &&
                       Mathf.Abs(mover.position.y - position.y) < _radius * 2 + mover.radius)
            {
                _myGame.level.Destroy();
                _myGame.LoadScene(0);
            }
        }
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(Key.SPACE))
        {
            switchPlayers();
        }
        if (_thisPlayerTurn)
        {
            mousePos.SetXY(Input.mouseX, Input.mouseY);
            if (!Input.GetKey(Key.A) && !Input.GetKey(Key.D) && velocity.Length() < _frictionSpeed)
            {
                velocity.x = 0;
            }
            if (Input.GetKey(Key.A))
            {
                if (velocity.Length() < _maxVelocity)
                    velocity += _unitVectorLeft * _speed;
            }
            if (Input.GetKey(Key.D))
            {
                if (velocity.Length() < _maxVelocity)
                    velocity += _unitVectorRight * _speed;
            }
            if (Input.GetKey(Key.W))
            {
                if (_grounded)
                {
                    velocity += _unitVectorJump * _jumpSpeed;
                }
            }
            if (Input.GetKeyDown(Key.Z))
            {
                Ball smallBall = new Ball(7, new Vec2(_right * (2 * _radius) + position.x, _radius + position.y), (-1 * new Vec2(TransformPoint(_delta.x, _delta.y))).Normalized() * _ballSpeed);
                _ball = smallBall;
                smallBall.bounciness = 0.98f;
                parent.AddChild(smallBall);
                _myGame.balls.Add(smallBall);
            }
            if (Input.GetKeyDown(Key.X))
            {
                Ball bigBall = new Ball(25, new Vec2(_right * (2 * _radius) + position.x, _radius + position.y), (-1 * new Vec2(TransformPoint(_delta.x, _delta.y))).Normalized() * _ballSpeed);
                bigBall.bounciness = 0.9f;
                _ball = bigBall;
                parent.AddChild(bigBall);
                _myGame.balls.Add(bigBall);
            }
        }
        if (Input.GetKeyDown(Key.X))
        {
            switchPlayers();
        }
        if (Input.GetKeyDown(Key.Z))
        {
            switchPlayers();
        }
    }

    void limitOnScreenBalls()
    {
        if(_ball != null && _ball.velocity.Length() < 0.7f)
        {
            _myGame.balls.Remove(_ball);
            _ball.Destroy();
        }

        for (int i = 0; i < _myGame.GetNumberOfBallMovers() - 2; i++)
        {
            Ball mover = _myGame.balls[i];
            _myGame.balls.Remove(mover);
            mover.Destroy();
        }
    }

    void switchPlayers()
    {
        if (_thisPlayerTurn)
        {
            _thisPlayerTurn = false;
            line.visible = false;
        }
        else
        {
            _thisPlayerTurn = true;
            line.visible = true;
        }
    }

    void drawAimLine()
    {
        if (mousePos.x > position.x)
            _right = 1;
        else
            _right = -1;

        line.start = new Vec2(_right * _radius + _firstX, _radius + _firstY);
        _delta = line.start - mousePos;
        Vec2 startPoint = line.start - new Vec2(x, y) - velocity;
        //Vec2 endHundred = (startPoint + _delta).Normalized() * 100f;
        //line.end = startPoint - endHundred - _delta;
        line.end = startPoint - _delta;
    }

    void StopAtBoundaries()
    {
        if (position.y > _myGame.height - _radius * 2)
        {
            position.y = _myGame.height - _radius * 2;
            velocity.y = 0f;
            _grounded = true;
        }
        else
        {
            velocity += acceleration;
            _grounded = false;
        }
        if(position.x < _myGame.width / 2)
        {
            if (position.x < _radius)
            {
                position.x = _radius;
                velocity.x = 0;
            }
            if (position.x > _myGame.width /2 - _radius)
            {
                position.x = _myGame.width /2- _radius;
                velocity.x = 0;
            }
        }
        else
        {
            if(position.x < _myGame.width/2 + _radius)
            {
                position.x = _myGame.width / 2 + _radius;
                velocity.x = 0;
            }
            if (position.x > _myGame.width - _radius)
            {
                position.x = _myGame.width - _radius;
                velocity.x = 0;
            }
        }
    }

    void UpdateScreenPosition()
    {
        x = position.x - _firstX;
        y = position.y - _firstY;
    }
}

