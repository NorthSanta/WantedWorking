using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct axisAngle
{
    public float x, y, z, w;

}
public class MyQuat : MonoBehaviour
{

    //Static Properties
    public static MyQuat identity = new MyQuat(0, 0, 0, 1);

    //Properties
    public float x, y, z, w;

    //Constructors
    public MyQuat()
    {
        x = 0;
        y = 0;
        z = 0;
        w = 0;
    }
    public MyQuat(float _x, float _y, float _z, float _w)
    {
        x = _x;
        y = _y;
        z = _z;
        w = _w;
    }
    public MyQuat(MyVec vec, float _w)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        w = _w;
    }
    public MyQuat(Quaternion quat)
    {
        x = quat.x;
        y = quat.y;
        z = quat.z;
        w = quat.w;
    }

    //Methods
    public void Normalize()
    {
        float magnitude = Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
        float scale = 1.0f / magnitude;
        x = x * scale;
        y = y * scale;
        z = z * scale;
        w = w * scale;
    }
    public MyQuat Inverse()
    {
        MyQuat p = new MyQuat();
        Normalize();
        p.x = -x;
        p.y = -y;
        p.z = -z;
        p.w = w;

        return p;
    }
    public axisAngle ToAxisAngle()
    {
        axisAngle axis = new axisAngle();
        Normalize();
        axis.w = 2 * Mathf.Acos(w);
        float s = Mathf.Sqrt(1 - (w * w));
        if (s < 0.01)
        {
            axis.x = x;
            axis.y = y;
            axis.z = z;
        }
        else
        {
            axis.x = x / s;
            axis.y = y / s;
            axis.z = z / s;
        }

        return axis;
    }

    //Static Methods
    public static MyQuat AngleAxis(float angle, ref MyVec axis)
    {
        if (axis.sqrMagnitude == 0.0f)
            return identity;

        MyQuat result = new MyQuat(0, 0, 0, 1);
        float radians = angle * Mathf.Deg2Rad;
        radians *= 0.5f;
        axis.Normalize();
        axis = axis * Mathf.Sin(radians);
        result.x = axis.x;
        result.y = axis.y;
        result.z = axis.z;
        result.w = Mathf.Cos(radians);

        return Normalize(result);
    }
    public static MyQuat FromAxisAngle(axisAngle axis)
    {
        MyQuat q = new MyQuat();
        q.w = Mathf.Cos(axis.w / 2);
        q.x = axis.x * Mathf.Sin(axis.w / 2);
        q.y = axis.y * Mathf.Sin(axis.w / 2);
        q.z = axis.z * Mathf.Sin(axis.w / 2);
        q.Normalize();

        return q;
    }
    public static MyQuat Normalize(MyQuat q)
    {
        MyQuat result = new MyQuat();
        float magnitude = Mathf.Sqrt((q.x * q.x) + (q.y * q.y) + (q.z * q.z) + (q.w * q.w));
        float scale = 1.0f / magnitude;
        result.x = q.x * scale;
        result.y = q.y * scale;
        result.z = q.z * scale;
        result.w = q.w * scale;

        return result;
    }

    //Operators
    public static MyQuat operator *(MyQuat q, MyQuat p)
    {
        MyQuat m = new MyQuat();
        m.w = (p.w * q.w - p.x * q.x - p.y * q.y - p.z * q.z);
        m.x = (p.w * q.x + p.x * q.w - p.y * q.z + p.z * q.y);
        m.y = (p.w * q.y + p.x * q.z + p.y * q.w - p.z * q.x);
        m.z = (p.w * q.z - p.x * q.y + p.y * q.x + p.z * q.w);
        m.Normalize();

        return m;
    }
}

