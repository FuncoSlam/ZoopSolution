using System;
using System.Collections.Generic;
using System.Numerics;
using static Zoop.CustomMath;
using Raylib_cs;

namespace Zoop
{
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
			this.Radius = (float)Math.Pow(3f * Mass / (4 * Math.PI), 1/3);
		}

		public GravObject(float Mass, Vector2 Position, Vector2 Velocity, Color Color)
		{
			this.Mass = Mass;
			this.Position = Position;
			this.Velocity = Velocity;
			this.Color = Color;
			this.Radius = (float)Math.Sqrt(3f * Mass / (4 * Math.PI));
		}

		public GravObject(List<GravObject> bodies)
		{
			this.Mass = 0;
			this.Position = new();
			this.Velocity = new();
			if (bodies.Count > 0)
			{
				this.Color = bodies[0].Color;
			}
			else
			{
				this.Color = Color.RAYWHITE;
			}

			foreach (GravObject body in bodies)
			{
				float massRatio = this.Mass / body.Mass;
				this.Position = Lerp(body.Position, this.Position, massRatio);
				this.Velocity = Lerp(body.Velocity, this.Velocity, massRatio);
				this.Mass += body.Mass;
			}
			this.Radius = (float)Math.Sqrt(3f * Mass / (4 * Math.PI));
		}
	}
}
