using System;
using System.Collections.Generic;
using System.Drawing;

namespace Task1
{
	public struct Vector2
	{
		public float x;
		public float y;

		public float Magnitude { get { return (float)Math.Sqrt(x * x + y * y); } }

		public Vector2(float x, float y) { this.x = x;  this.y = y; }

		public static Vector2 operator-(Vector2 first, Vector2 second)
		{
			Vector2 o;
			o.x = first.x - second.x;
			o.y = first.y - second.y;
			return o;
		}
	}

	public class ImageDiameterSolver
	{
		private Image image;
		private List<Vector2> carcasPixels;

		public ImageDiameterSolver(string pathToImage)
		{
			carcasPixels = new List<Vector2>();
			image = Image.FromFile(pathToImage);
			FillCarcasPixels();
		}

		public float GetDiameter()
		{
			var maxDistance = 0.0f;

			// Use brute force to find the max distance between the pixels
			for (int i = 0; i < carcasPixels.Count; i++)
			{
				var first = carcasPixels[i];
				for (int j = i; j < carcasPixels.Count; j++)
				{
					var second = carcasPixels[j];

					var distance = (first - second).Magnitude;
					if (maxDistance < distance)
						maxDistance = distance;
				}
			}
			return maxDistance;
		}

		private void FillCarcasPixels()
		{
			carcasPixels.Clear();
			using (var bmp = new Bitmap(image))
			{
				for (int i = 0; i < bmp.Width; i++)
				{
					for (int j = 0; j < bmp.Height; j++)
					{
						var color = bmp.GetPixel(i, j);
						if (!IsBackgroundColor(color))
							carcasPixels.Add(new Vector2(i, j));
					}
				}
			}
		}

		private bool IsBackgroundColor(Color color)
		{
			return (color.R == 255 && color.G == 255 && color.B == 255) || color.A == 0;
		}
	}
}
