using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVec
{
    //Static Properties
    public static MyVec back = new MyVec(0, 0, -1);
    public static MyVec forward = new MyVec(0, 0, 1);
    public static MyVec down = new MyVec(0, -1, 0);
    public static MyVec up = new MyVec(0, 1, 0);
    public static MyVec left = new MyVec(-1, 0, 0);
    public static MyVec right = new MyVec(1, 0, 0);
    public static MyVec zero = new MyVec(0, 0, 0);
    public static MyVec one = new MyVec(1, 1, 1);

    //Properties
    public float x, y, z;
    //public MyVector3 normalized;
    public float magnitude;
    public float sqrMagnitude;

    //Constructors
    public MyVec()
    {
        x = 1;
        y = 1;
        z = 1;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVec(float X, float Y)
    {
        x = X;
        y = Y;
        z = 0;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVec(float X, float Y, float Z)
    {
        x = X;
        y = Y;
        z = Z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVec(Vector3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }
    public MyVec(MyVec vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        magnitude = Mathf.Sqrt(x * x + y * y + z * z);
        sqrMagnitude = x * x + y * y + z * z;
        //normalized = new MyVector3(x / magnitude, y / magnitude, z / magnitude);
    }

    //Methods
    public void Set(float X, float Y, float Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    public void Normalize()
    {
        float num = magnitude;
        if (num > 1E-05f)
        {
            x /= num;
            y /= num;
            z /= num;
        }
        else
        {
            x = 0;
            y = 0;
            z = 0;
        }
    }

    //Static Methods
    public static float Angle(MyVec from, MyVec to)
    {
        //return Mathf.Acos(Dot(from,to) / (from.magnitude * to.magnitude));
        return Mathf.Acos(Mathf.Clamp(Dot(from, to) / (from.magnitude * to.magnitude), -1f, 1f)) * 57.29578f;
    }
    public static MyVec Cross(MyVec lhs, MyVec rhs)
    {
        return new MyVec(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
    }
    public static float Distance(MyVec lhs, MyVec rhs)
    {
        return (lhs - rhs).magnitude;
    }
    public static float Distance(Vector3 lhs, Vector3 rhs)
    {
        return (lhs - rhs).magnitude;
    }
    public static float Dot(MyVec lhs, MyVec rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }
    public static MyVec Normalize(MyVec value)
    {
        float mag = Mathf.Sqrt((value.x * value.x) + (value.y * value.y) + (value.z * value.z));
        //return new MyVector3(value.x / mag, value.y / mag, value.z / mag);
        if (mag > 1E-05f) return new MyVec(value.x / mag, value.y / mag, value.z / mag);
        else return zero;
    }
    public static MyVec Scale(MyVec lhs, MyVec rhs)
    {
        return new MyVec(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
    }

    //Operators
    public static bool operator ==(MyVec lhs, MyVec rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }
    public static bool operator !=(MyVec lhs, MyVec rhs)
    {
        return !(lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z);
    }
    public static MyVec operator -(MyVec lhs, MyVec rhs)
    {
        return new MyVec(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }
    public static MyVec operator +(MyVec lhs, MyVec rhs)
    {
        return new MyVec(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }
    public static MyVec operator *(MyVec lhs, float rhs)
    {
        return new MyVec(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static MyVec operator *(MyVec lhs, MyVec rhs)
    {
        return new MyVec(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
    }

    public static MyVec operator /(MyVec lhs, float rhs)
    {
        return new MyVec(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
    }
}
