namespace Zoop;

using System.Numerics;
using static ColorEnum;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		GravSystem system = new();
		Random rand = new();
		Renderer renderer = new();

		int objectsToSpawn = 500;
		if (args.Length > 0)
		{
			if (int.TryParse(args[0], out int temp))
				objectsToSpawn = temp;
		}

		float positionVariance = 5000f;
		float velocityVariance = 2f;
		for (int i = 0; i < objectsToSpawn; i++)
		{
			system.Insert(new GravObject(
				rand.NextSingle() * 20000f,
				new Vector2(
					rand.NextSingle() * positionVariance - positionVariance / 2f, 
					rand.NextSingle() * positionVariance - positionVariance / 2f),
				new Vector2(
					rand.NextSingle() * velocityVariance - velocityVariance / 2f, 
					rand.NextSingle() * velocityVariance - velocityVariance / 2f),
				GetRandomColor()));
		}

		system.TimeStep = 1f;
		bool running = true;
		while (running)
		{
			system.Update();
			if (!renderer.Render(system.CurrentState))
				running = false;
		}
	}
}
