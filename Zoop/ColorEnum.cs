using Raylib_cs;

namespace Zoop
{
	internal static class ColorEnum
	{
		private static Random random = new();

		private static Color[] colors =
		{
			Color.BLACK,
			Color.WHITE,
            Color.BLUE,
            Color.GREEN,
            Color.RED,
            Color.VIOLET,
            Color.YELLOW,
			Color.ORANGE,
			Color.PINK,
			Color.MAROON
        };

		public static Color GetRandomColor()
		{
			return colors[(int)(random.NextSingle() * colors.Length)];
		}
	}
}
