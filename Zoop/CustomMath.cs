using System.Numerics;

namespace Zoop
{
	internal static class CustomMath
	{
		public static float Lerp(float f1, float f2, float by)
		{
			return f1 + (f2 - f1) * by;
		}

		public static Vector2 Lerp(Vector2 vector1, Vector2 vector2, float by)
		{
			float retX = Lerp(vector1.X, vector2.X, by);
			float retY = Lerp(vector1.Y, vector2.Y, by);
			return new Vector2(retX, retY);
		}


	}
}
