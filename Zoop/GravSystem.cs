using System.Numerics;

namespace Zoop;

internal class GravSystem
{
	private List<GravObject> currentState = new();
	public List<GravObject> CurrentState => currentState;
	private List<GravObject> futureState = new();
	public List<GravObject> FutureState => futureState;

	private float timeStep = 0.1f;
	public float TimeStep { set => timeStep = value; get => timeStep; }

	public void Insert(GravObject body)
	{
		futureState.Add(body);
	}

	public void Update()
	{
		currentState = new(futureState);

		AlterVelocityOfObjects();
		MoveObjectsByVelocity();
		Collisions();

		void AlterVelocityOfObjects()
		{
			for (int i = 0; i < currentState.Count; i++)
			{
				for (int j = 0; j < currentState.Count; j++)
				{
					if (i == j) { continue; }

					Vector2 positionDiff = currentState[j].Position - currentState[i].Position; // Position of J relative to I
					float distanceSquared = positionDiff.LengthSquared();
					const float G = 0.001f;
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

		void Collisions()
		{
			// Detect Collisions
			List<(int, int)> collisions = DetectCollisions();

			// Combine Collisions Into Clusters
			List<List<int>> collisionClusters = CreateCollisionClusters(collisions);

			// Create New Bodies According to Collision Clusters
			List<GravObject> newBodies = CreateNewBodies(collisionClusters);

			// Destroy All Bodies Which Have Collided
			List<int> allCollidingBodyIndices = GetUniqueBodyIndices(collisionClusters);
			for (int i = 0; i < allCollidingBodyIndices.Count; i++)
			{
				futureState.RemoveAt(allCollidingBodyIndices[i]);
			}

			// Add New Bodies To The Future State
			futureState = futureState.Concat(newBodies).ToList();
		}
	}

	private List<(int, int)> DetectCollisions()
	{
		List<(int, int)> collisions = new();
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
		return collisions;
	}

	private List<List<int>> CreateCollisionClusters(List<(int, int)> Collisions)
	{
		List<List<int>> collisionClusters = new();
		while (Collisions.Count > 0)
		{
			bool hit = false;
			for (int j = 0; j < collisionClusters.Count; j++)
			{
				for (int i = 0; i < collisionClusters[j].Count; i++)
				{
					if (!hit)
					{
						if (Collisions[0].Item1 == collisionClusters[j][i])
						{
							collisionClusters[j].Add(Collisions[0].Item2);
							hit = true;
							Collisions.RemoveAt(0);
							break;
						}
						else if (Collisions[0].Item2 == collisionClusters[j][i])
						{
							collisionClusters[j].Add(Collisions[0].Item1);
							hit = true;
							Collisions.RemoveAt(0);
							break;
						}
					}
				}
			}
			if (!hit)
			{
				collisionClusters.Add(new List<int> { Collisions[0].Item1, Collisions[0].Item1 });
			}
		}
		return collisionClusters;
	}

	private List<GravObject> CreateNewBodies(List<List<int>> CollisionClusters)
	{
		List<GravObject> newBodies = new();
		for (int i = 0; i < CollisionClusters.Count; i++)
		{
			List<int> collision = CollisionClusters[i].Distinct().ToList();

			List<GravObject> bodiesToCombine = new();
			for (int j = 0; j < collision.Count; j++)
			{
				bodiesToCombine.Add(futureState[collision[j]]);
			}
			newBodies.Add(new GravObject(bodiesToCombine));
		}
		return newBodies;
	}


	private List<int> GetUniqueBodyIndices(List<List<int>> CollisionClusters)
	{
		List<int> allCollidingBodyIndices = new();
		for (int i = 0; i < CollisionClusters.Count; i++)
		{
			allCollidingBodyIndices = allCollidingBodyIndices.Concat(CollisionClusters[i]).ToList();
		}
		allCollidingBodyIndices = allCollidingBodyIndices.Distinct().ToList();
		allCollidingBodyIndices.Sort();
		allCollidingBodyIndices.Reverse();

		return allCollidingBodyIndices;
	}
}

