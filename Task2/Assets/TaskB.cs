using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector3
{
	public float x;
	public float y;
	public float z;

	public Vector3(float x, float y, float z) { this.x = x;  this.y = y; this.z = z; }

	public override string ToString()
	{
		return string.Format("({0}, {1} {2})", x, y, z);
	}

	public static float Dot(Vector3 first, Vector3 second)
	{
		return first.x * second.x + first.y * second.y + first.z * second.z;
	}

	public static Vector3 Cross(Vector3 first, Vector3 second)
	{
		Vector3 o;
		o.x = first.y * second.z - first.z * second.y;
		o.y = -(first.x * second.z - first.z * second.x);
		o.z = first.x * second.y - first.y * second.x;
		return o;
	}

	public static Vector3 operator*(Vector3 first, float second)
	{
		Vector3 o;
		o.x = first.x * second;
		o.y = first.y * second;
		o.z = first.z * second;
		return o;
	}

	public static Vector3 operator /(Vector3 first, float second)
	{
		Vector3 o;
		o.x = first.x / second;
		o.y = first.y / second;
		o.z = first.z / second;
		return o;
	}

	public static Vector3 operator+(Vector3 first, Vector3 second)
	{
		Vector3 o;
		o.x = first.x + second.x;
		o.y = first.y + second.y;
		o.z = first.z + second.z;
		return o;
	}

	public static Vector3 operator -(Vector3 first)
	{
		Vector3 o;
		o.x = -first.x;
		o.y = -first.y;
		o.z = -first.z;
		return o;
	}
}

public struct Matrix3x3
{
	public Vector3 first;
	public Vector3 second;
	public Vector3 third;

	public Matrix3x3(Matrix3x3 value)
	{
		first = value.first;
		second = value.second;
		third = value.third;
	}

	public static Matrix3x3 Transpose(Matrix3x3 value)
	{
		Matrix3x3 o;

		o.first = new Vector3(value.first.x, value.second.x, value.third.x);
		o.second = new Vector3(value.first.y, value.second.y, value.third.y);
		o.third = new Vector3(value.first.z, value.second.z, value.third.z);

		return o;
	}

	public static Matrix3x3 Multiply(Matrix3x3 first, Matrix3x3 second)
	{
		Matrix3x3 o;

		Matrix3x3 t = Transpose(second);
		o.first = new Vector3(Vector3.Dot(first.first, t.first), Vector3.Dot(first.first, t.second), Vector3.Dot(first.first, t.third));
		o.second = new Vector3(Vector3.Dot(first.second, t.first), Vector3.Dot(first.second, t.second), Vector3.Dot(first.second, t.third));
		o.third = new Vector3(Vector3.Dot(first.third, t.first), Vector3.Dot(first.third, t.second), Vector3.Dot(first.third, t.third));

		return o;
	}

	public static Vector3 Multiply(Matrix3x3 first, Vector3 second)
	{
		Vector3 o;

		o.x = Vector3.Dot(first.first, second);
		o.y = Vector3.Dot(first.second, second);
		o.z = Vector3.Dot(first.third, second);

		return o;
	}
}

public class TaskB : MonoBehaviour
{
	public float radius = 1;
	public float endPoint = 4;
	public float time = 2;
	public Material mat;

	void Update ()
	{
		float normalizedTime = Mathf.Min(1, Time.timeSinceLevelLoad / time);

		SetRollPosition(endPoint * normalizedTime);
	}

	private void SetRollPosition(float value)
	{
		Matrix3x3 currentTransform = CreateRollMatrix(value);
		mat.SetVector("_RollMatrixX", new Vector4(currentTransform.first.x, currentTransform.first.y, currentTransform.first.z, 0));
		mat.SetVector("_RollMatrixY", new Vector4(currentTransform.second.x, currentTransform.second.y, currentTransform.second.z, 0));
		mat.SetVector("_RollMatrixZ", new Vector4(currentTransform.third.x, currentTransform.third.y, currentTransform.third.z, 0));
	}

	private Matrix3x3 CreateRollMatrix(float value)
	{
		Matrix3x3 t;
		t.first = new Vector3(1, 0, value);
		t.second = new Vector3(0, 1, 0);
		t.third = new Vector3(0, 0, 1);

		float rotation = -value;
		Matrix3x3 r;
		r.first = new Vector3(Mathf.Cos(rotation), -Mathf.Sin(rotation), 0);
		r.second = new Vector3(Mathf.Sin(rotation), Mathf.Cos(rotation), 0);
		r.third = new Vector3(0, 0, 1);

		return Matrix3x3.Multiply(t, r);
	}
}
