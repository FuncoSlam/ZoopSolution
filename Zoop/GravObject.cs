using System.Numerics;
using static Zoop.CustomMath;
using Raylib_cs;
using System.Diagnostics;

namespace Zoop;

internal struct GravObject
{

	public float Mass { get; init; }

	public Vector2 Position { get; set; }

	public Vector2 Velocity { get; set; }
	public Color Color { get; set; }
	public float Radius { get; }

	public GravObject(float Mass, Vector2 Position, Vector2 Velocity)
	{
		this.Mass = Mass;
		this.Position = Position;
		this.Velocity = Velocity;
		this.Color = Color.RAYWHITE;
		this.Radius = GetRadius(Mass);
	}

	public GravObject(float Mass, Vector2 Position, Vector2 Velocity, Color Color)
	{
		this.Mass = Mass;
		this.Position = Position;
		this.Velocity = Velocity;
		this.Color = Color;
		this.Radius = GetRadius(Mass);
	}

	public GravObject(List<GravObject> bodies)
	{
		this.Mass = 0;
		this.Position = new();
		this.Velocity = new();
		if (bodies.Count > 0)
		{
			GravObject largestBody = new();
			for (int i = 0; i < bodies.Count; i++)
			{
				if (bodies[i].Mass > largestBody.Mass)
				{
					largestBody = bodies[i];
				}
			}
			this.Color = largestBody.Color;
		}
		else
		{
			this.Color = Color.RAYWHITE;
		}

		for (int i = 0; i < bodies.Count; i++)
		{
			float massRatio = this.Mass / (this.Mass + bodies[i].Mass);
			this.Position = Lerp(bodies[i].Position, this.Position, massRatio);
			this.Velocity = Lerp(bodies[i].Velocity, this.Velocity, massRatio);
			this.Mass += bodies[i].Mass;
		}

		this.Radius = GetRadius(Mass);
	}

	private static float GetRadius(float Mass)
	{
		double temp = 3f * Mass / (4 * Math.PI);
		double radius = Math.Pow(temp, 1d / 3d);
		return (float)radius;
	}
}

