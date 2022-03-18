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


		float positionVariance = 500f;
		float velocityVariance = 1f;
		for (int i = 0; i < 100; i++)
		{
			system.Insert(new GravObject(
				rand.NextSingle() * 1000f,
				new Vector2(rand.NextSingle() * positionVariance - positionVariance / 2f, rand.NextSingle() * positionVariance - positionVariance / 2f),
				new Vector2(rand.NextSingle() * velocityVariance - velocityVariance / 2f, rand.NextSingle() * velocityVariance - velocityVariance / 2f),
				GetRandomColor()
				));
		}

		system.TimeStep = 1f;
		bool running = true;
		while (running)
		{
			system.Update();
			renderer.Render(system.CurrentState);
		}
	}
}
