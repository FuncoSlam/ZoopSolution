using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Zoop;

internal class GravSystem
{
	private List<GravObject> currentState = new();
	public List<GravObject> CurrentState { get => currentState; }
	private List<GravObject> futureState = new();
	public List<GravObject> FutureState { get => futureState; }

	private float timeStep = 1f;
	public float TimeStep 
	{ 
		set => timeStep = value; 
		get => timeStep; 
	}

	public void Insert(GravObject body)
	{
		futureState.Add(body);
	}

	public void Update()
	{
		currentState = new(futureState);

		AlterVelocityOfObjects();
		MoveObjectsByVelocity();
		CombineCollisions();

		void AlterVelocityOfObjects()
		{
			for (int i = 0; i < currentState.Count; i++)
			{
				for (int j = 0; j < currentState.Count; j++)
				{
					if (i == j) { continue; }

					Vector2 positionDiff = currentState[j].Position - currentState[i].Position; // Position of J relative to I
					float distanceSquared = positionDiff.LengthSquared();
					const float G = 0.05f;
					float acceleration = G * currentState[j].Mass / distanceSquared;

					Vector2 deltaV = Vector2.Normalize(positionDiff) * acceleration * timeStep;
					GravObject temp = futureState[i];
					temp.Velocity += deltaV;
					futureState[i] = temp;
				}
			}
		}

		void MoveObjectsByVelocity()
		{
			for (int i = 0; i < futureState.Count; i++)
			{
				GravObject temp = futureState[i];
				temp.Position += futureState[i].Velocity * timeStep;
				futureState[i] = temp;
			}
		}

		void CombineCollisions()
		{
			List<(int, int)> collisions = new();

			// Detect Collisions
			for (int i = 0; i < futureState.Count; i++)
			{
				for (int j = i + 1; j < futureState.Count; j++)
				{
					if (i == j) { continue; }

					Vector2 positionDiff = futureState[j].Position - futureState[i].Position; // Position of J relative to I
					if (positionDiff.Length() < futureState[i].Radius + futureState[j].Radius)
					{
						collisions.Add((i, j));
					}
				}
			}

			// Combine Collisions Into Clusters TODO
			List<List<int>> collisionClusters = new();
			while (collisions.Count > 0)
			{
				collisionClusters.Add(new List<int> { collisions[0].Item1, collisions[0].Item1 });
				for (int j = 0; j < collisions.Count; j++)
				{

				}
			}

			// Create And Destroy Bodies According to Collisions TODO
		}
	}
}

