using System;
using GXPEngine;

public class Ball : EasyDraw
{
	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public float bounciness = 0.98f;
	// For ease of testing / changing, we assume every ball has the same acceleration (gravity):
	public static Vec2 acceleration = new Vec2 (0, 0);

	public Vec2 velocity;
	public Vec2 position;

	public readonly int radius;
	public readonly bool moving;

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass {
		get {
			return radius * radius * _density;
		}
	}

	Vec2 _oldPosition;
	Arrow _velocityIndicator;

	float _density = 1;

    public Ball (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool moving=true) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		radius = pRadius;
		position = pPosition;
		velocity = pVelocity;
		this.moving = moving;

		position = pPosition;
		UpdateScreenPosition ();
		SetOrigin (radius, radius);

		Draw (230, 200, 0);

		_velocityIndicator = new Arrow(position, new Vec2(0,0), 10);
		AddChild(_velocityIndicator);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (radius, radius, 2*radius, 2*radius);
	}

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

    public void Step()
    {
        velocity += acceleration;
        _oldPosition = position;
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
        ShowDebugInfo();
    }

	CollisionInfo FindEarliestCollision() {
		MyGame myGame = (MyGame)game;	
        Vec2 finalUnitNormal = new Vec2(0, 0);
        GameObject finalGameObj = this;
        float finalColTime = 99;
        for (int i = 0; i < myGame.GetNumberOfBallMovers(); i++)
        {
            Ball mover = myGame.GetBallMover(i);
            if (mover != this)
            {
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
        }
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment lineSegment = myGame.GetLine(i);
            Vec2 lineVec = lineSegment.end - lineSegment.start;
            Vec2 lineNormal = lineVec.Normal();
            Vec2 oldDifferenceVec = _oldPosition - lineSegment.start;
            Vec2 differenceVec = position - lineSegment.start;
            float differenceVecLength = differenceVec.Length();
            float projection = lineVec.Normalized().Dot(differenceVec);
            float ballDistance = Mathf.Sqrt(differenceVecLength * differenceVecLength - projection * projection);
            float colTime = 99;
            if (ballDistance < radius)
            {
                float a = lineNormal.Dot(oldDifferenceVec) - radius;
                float b = -lineNormal.Dot(velocity);
                if (b >= 0)
                {
                    if(a>=0)
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
        if (finalColTime != 99)
            return new CollisionInfo(finalUnitNormal, finalGameObj, finalColTime);
        else
            return null;
      
	}

    void ResolveCollision(CollisionInfo col)
    {
        if (col.other is Ball)
        {
            Ball other = (Ball)col.other;
            Vec2 pointOfImpact = _oldPosition + velocity * col.timeOfImpact;
            Vec2 centerMass = (Mass * velocity + other.Mass * other.velocity) / (Mass + other.Mass);
            Vec2 relativeVel = velocity - other.velocity;
            Vec2 relativePos = pointOfImpact - other.position;

            if (relativeVel.Dot(relativePos) < 0)
            {
                position = pointOfImpact;
                velocity -= (1 + bounciness) * ((velocity - centerMass).Dot(col.normal) * col.normal);
                other.velocity -= (1 + bounciness) * ((other.velocity - centerMass).Dot(col.normal) * col.normal);
            }
        }
        if (col.other is LineSegment)
        {
            position = _oldPosition + velocity * col.timeOfImpact;
            velocity = velocity.Reflect(col.normal, bounciness);
        }
    }

	void ShowDebugInfo() {
		if (drawDebugLine) {
			((MyGame)game).DrawLine (_oldPosition, position);
		}
		_velocityIndicator.startPoint = position;
		_velocityIndicator.vector = velocity;
	}
}

