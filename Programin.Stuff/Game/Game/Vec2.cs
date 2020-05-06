using System;
using GXPEngine;
using GXPEngine.Core;
public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }
    
    public Vec2(Vector2 vec)
    {
        x = vec.x;
        y = vec.y;
    }


    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public float Length()
    {
        return (float)Math.Sqrt((this.x * this.x) + (this.y * this.y));
    }

    public void Normalize()
    {
        float length = Length();
        if (length != 0)
        {
            this.x /= length;
            this.y /= length;
        }
    }

    public Vec2 Normalized()
    {
        Vec2 result = new Vec2(x, y);
        result.Normalize();
        return result;
    }

    public float Dot(Vec2 vector)
    {
        return x * vector.x + y * vector.y;
    }

    public Vec2 Normal()
    {
        float length = Length();
        if(length!=0)
            return new Vec2(-this.y / length, this.x / length);
        else
            return new Vec2(0, 0);
    }

    public Vec2 Reflect(Vec2 normal, float COE)
    {
        return COE * (this - 2 * (this.Dot(normal) * normal));
    }

    public static float Deg2Rad(float degrees)
    {
        return (degrees * Mathf.PI) / 180f;
    }

    public static float Rad2Deg(float radians)
    {
        return radians * 180 / Mathf.PI;
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        float radians = Deg2Rad(degrees);
        return GetUnitVectorRad(radians);
    }
 
    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public static Vec2 GetRandomUnitVector()
    {
        Random random = new Random();
        float randomDegrees = random.Next(360);
        return GetUnitVectorDeg(randomDegrees);
    }

    public void SetAngleDegrees(float degrees)
    {
        float radians = Deg2Rad(degrees);
        SetAngleRadians(radians);
    }

    public void SetAngleRadians(float radians)
    {
        float length = this.Length();
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        this.x = cos * length;
        this.y = sin * length;
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(this.y, this.x);
    }

    public void RotateDegrees(float degrees)
    {
        float radians = Deg2Rad(degrees);
        RotateRadians(radians);
    }

    public void RotateRadians(float radians)
    {
        float oldx = this.x;
        float oldy = this.y;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        this.x = oldx * cos - oldy * sin;
        this.y = oldx * sin + oldy * cos;  
    }

    public void RotateAroundDegrees(Vec2 origin, float degrees)
    {
        float radians = Deg2Rad(degrees);
        RotateAroundRadians(origin, radians);
    }

    public void RotateAroundRadians(Vec2 origin, float radians)
    {
        float oldx = this.x;
        float oldy = this.y;
        float transx = oldx - origin.x;
        float transy = oldy - origin.y;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        this.x = (transx * cos - transy * sin) + origin.x;
        this.y = (transx * sin + transy * cos) + origin.y;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, float scalar)
    {
        return new Vec2(left.x * scalar, left.y * scalar);
    }

    public static Vec2 operator *(float scalar, Vec2 right)
    {
        return new Vec2(scalar * right.x, scalar * right.y);
    }

    public static Vec2 operator *(Vec2 a, Vec2 b)
    {
        return new Vec2(a.x * b.x, a.y * b.y);
    }

    public static bool operator == (Vec2 a, Vec2 b)
    {
        if (a.x == b.x && a.y == b.y)
            return true;
        return false;
    }

    public static bool operator != (Vec2 a, Vec2 b)
    {
        if (a.x == b.x && a.y == b.y)
            return false;
        return true;
    }

    public static Vec2 operator / (Vec2 left, float scalar)
    {
        return new Vec2(left.x / scalar, left.y / scalar);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    /**
       void AddFullLine(Vec2 start, Vec2 end)
    {
        LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
        LineSegment invLine = new LineSegment(start, end, 0xff00ff00, 4);
        _movers.Add(new Ball(0, start, new Vec2(0, 0), false));
        _movers.Add(new Ball(0, end, new Vec2(0, 0), false));

        AddChild(line);
        _lines.Add(line);
        AddChild(invLine);
        _lines.Add(invLine);
    }
     */
}

