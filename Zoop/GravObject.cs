using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Zoop
{
	internal struct GravObject
	{

		public float Mass { get; init; }

		public Vector2 Position { get; set; }

		public Vector2 Velocity { get; set; }
		public Color Color { get; set; }

		private float radius = 0f;
		public float Radius { get => GetRadius(); }

		public GravObject(float Mass, Vector2 Position, Vector2 Velocity)
		{
			this.Mass = Mass;
			this.Position = Position;
			this.Velocity = Velocity;
			this.Color = Color.RAYWHITE;
		}

		public GravObject(float Mass, Vector2 Position, Vector2 Velocity, Color Color)
		{
			this.Mass = Mass;
			this.Position = Position;
			this.Velocity = Velocity;
			this.Color = Color;
		}

		private float GetRadius()
		{
			if (radius == 0f)
			{
				radius = (float)Math.Sqrt(3f * Mass / (4 * Math.PI));
			}
			return radius;
		}
	}
}
