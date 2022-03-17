namespace Zoop;

using System.Numerics;
using static ColorEnum;
using Raylib_cs;

class Program
{
	public static void Main()
	{
		GravSystem system = new();
		Random rand = new();
		Renderer renderer = new();

		for (int i = 0; i < 100; i++)
		{
			system.Insert(new GravObject(
				rand.NextSingle() * 1000f,
				new Vector2(rand.NextSingle() * 500f, rand.NextSingle() * 500f),
				new Vector2(rand.NextSingle() * 0.1f, rand.NextSingle() * 0.1f),
				GetRandomColor()
				));
		}

		bool running = true;
		while (running)
		{
			system.Update();
			renderer.Render(system.CurrentState);
		}
	}
}
