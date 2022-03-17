using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Zoop
{
	internal static class CustomMath
	{
		public static float Lerp(float firstFloat, float secondFloat, float by)
		{
			return firstFloat * (1 - by) + secondFloat * by;
		}

		public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
		{
			float retX = Lerp(firstVector.X, secondVector.X, by);
			float retY = Lerp(firstVector.Y, secondVector.Y, by);
			return new Vector2(retX, retY);
		}
	}
}
