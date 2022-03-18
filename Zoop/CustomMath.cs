using System.Numerics;

namespace Zoop
{
	internal static class CustomMath
	{
		public static float Lerp(float firstFloat, float secondFloat, float by)
		{
			return firstFloat + (secondFloat - firstFloat) * by;
		}

		public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
		{
			float retX = Lerp(firstVector.X, secondVector.X, by);
			float retY = Lerp(firstVector.Y, secondVector.Y, by);
			return new Vector2(retX, retY);
		}
	}
}
