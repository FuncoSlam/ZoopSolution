using Raylib_cs;

namespace Zoop
{
	internal static class ColorEnum
	{
		private static Random random = new();

		private static Color[] colors =
		{
			Color.BLACK,
			Color.RAYWHITE,
			Color.BLUE,
			Color.DARKBLUE,
			Color.DARKGRAY,
			Color.GREEN,
			Color.RED,
			Color.VIOLET,
			Color.GOLD
		};

		public static Color GetRandomColor()
		{
			return colors[(int)(random.NextSingle() * colors.Length)];
		}
	}
}
