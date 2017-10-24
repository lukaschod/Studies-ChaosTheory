using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Quaternion
{
	public Vector3 v;
	public float w;

	public float Magnitude { get { return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + w * w); } }

	public Quaternion(Vector3 v, float w) { this.v = v; this.w = w; }

	public override string ToString()
	{
		return string.Format("({0}, {1} {2}, {3})", v.x, v.y, v.z, w);
	}

	public static Quaternion Normalize(Quaternion value)
	{
		var distance = value.Magnitude;
		Debug.Assert(distance != 0);
		return new Quaternion(value.v / distance, value.w / distance);
	}

	public static Quaternion FromLookAt(Vector3 lookAt, float degress)
	{
		Quaternion o;

		float radians = degress * Mathf.Deg2Rad;
		float fi = -radians / 2;

		o.w = Mathf.Cos(fi);
		o.v = lookAt * Mathf.Sin(fi);

		o = Normalize(o);

		return o;
	}

	public static Quaternion Inverse(Quaternion value)
	{
		Debug.Assert(value.Magnitude == 1);
		return new Quaternion(-value.v, value.w);
	}

	public static Quaternion Multiply(Quaternion first, Quaternion second)
	{
		Quaternion o;

		o.w = first.w * second.w - Vector3.Dot(first.v, second.v);
		o.v = second.v * first.w + first.v * second.w + Vector3.Cross(first.v, second.v);

		return o;
	}

	public static Vector3 Rotate(Quaternion rotation, Vector3 point)
	{
		var inverseRotation = Inverse(rotation);
		var quatPoint = new Quaternion(point, 0);
		var transformedQuatPoint = Multiply(Multiply(inverseRotation, quatPoint), rotation);
		Debug.Assert(Mathf.Abs(transformedQuatPoint.w) < 0.00001);
		return transformedQuatPoint.v;
	}
}

public class TaskA : MonoBehaviour
{
	public Vector3 lookAt = new Vector3(3, 6, 2);
	public float degress = 60;

	void Start()
	{
		var rotation = Quaternion.FromLookAt(lookAt, degress);
		Rotate(rotation, new Vector3(6, 8, 0));
		Rotate(rotation, new Vector3(7, 12, 3));

	}

	private void Rotate(Quaternion rotation, Vector3 point)
	{
		Debug.LogFormat("Rotating point {0} by {1} = {2}", point, rotation, Quaternion.Rotate(rotation, point));
	}
}
