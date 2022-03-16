using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Zoop;

internal class Renderer
{
	const int windowWidth = 1360, windowHeight = 720;
	static Color backgroundColor = Color.BEIGE;
	static Renderer()
	{
		InitWindow(windowWidth, windowHeight, "Gravity System");
		SetTargetFPS(120);
		BeginDrawing();
		ClearBackground(backgroundColor);
		EndDrawing();
	}

	public void Render(List<GravObject> Bodies)
	{
		BeginDrawing();
		ClearBackground(backgroundColor);

		for (int i = 0; i < Bodies.Count; i++)
		{
			GravObject body = Bodies[i];

			DrawCircle((int)body.Position.X, (int)body.Position.Y, body.Radius, body.Color);
		}

		EndDrawing();
	}
	
}
