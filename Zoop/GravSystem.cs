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
					const float G = 0.01f;
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

			// Combine Collisions Into Clusters
			List<List<int>> collisionClusters = new();
			while (collisions.Count > 0)
			{
				bool hit = false;
				for (int j = 0; j < collisionClusters.Count; j++)
				{
					for (int i = 0; i < collisionClusters[j].Count; i++)
					{
						if (!hit)
						{
							if (collisions[0].Item1 == collisionClusters[j][i])
							{
								collisionClusters[j].Add(collisions[0].Item2);
								hit = true;
								collisions.RemoveAt(0);
								break;
							}
							else if (collisions[0].Item2 == collisionClusters[j][i])
							{
								collisionClusters[j].Add(collisions[0].Item1);
								hit = true;
								collisions.RemoveAt(0);
								continue;
							}
						}
					}
				}
				if (!hit)
				{
					collisionClusters.Add(new List<int> { collisions[0].Item1, collisions[0].Item1 });
				}
			}

			// Create New Bodies According to Collision Clusters
			List<GravObject> newBodies = new();
			for (int i = 0; i < collisionClusters.Count; i++)
			{
				List<int> collision = collisionClusters[i].Distinct().ToList();

				List<GravObject> bodiesToCombine = new();
				for (int j = 0; j < collision.Count; j++)
				{
					bodiesToCombine.Add(futureState[collision[j]]);
				}
				newBodies.Add(new GravObject(bodiesToCombine));
			}

			// Destroy All Bodies Which Have Collided
			List<int> allCollidingBodyIndices = new();
			for (int i = 0; i < collisionClusters.Count; i++)
			{
				allCollidingBodyIndices = allCollidingBodyIndices.Concat(collisionClusters[i]).ToList();
			}
			allCollidingBodyIndices = allCollidingBodyIndices.Distinct().ToList();
			allCollidingBodyIndices.Sort();
			allCollidingBodyIndices.Reverse();

			for (int i = 0; i < allCollidingBodyIndices.Count; i++)
			{
				futureState.RemoveAt(allCollidingBodyIndices[i]);
			}

			// Add New Bodies To The Future State
			futureState = futureState.Concat(newBodies).ToList();
		 }
	}
}

