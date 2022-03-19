using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Zoop;

internal class Renderer
{
	const int windowWidth = 1360, windowHeight = 720;

	static Camera2D camera = new(new Vector2(windowWidth / 2, windowHeight / 2), Vector2.Zero, 0f, 1f);
	static float camSpeed = 2f;
	static float zoomSpeed = 0.3f;
	static float minZoom = 0.1f;
	static Vector2 prevMousePos = GetMousePosition();

	static bool isPaused = false;

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

		float mouseWheelDelta = GetMouseWheelMove();
		float newZoom = Math.Max(camera.zoom + mouseWheelDelta * zoomSpeed * camera.zoom, minZoom);
		camera.zoom = newZoom;

		Vector2 mousePos = GetMousePosition();
		Vector2 mousePosDelta = prevMousePos - mousePos;
		prevMousePos = mousePos;

		if (IsMouseButtonDown(MouseButton.MOUSE_MIDDLE_BUTTON))
			camera.target = GetScreenToWorld2D(mousePosDelta * camSpeed + camera.offset, camera);

		float totalMassInSystem = 0f;
		Vector2 totalMomentumInSystem = new();

		BeginMode2D(camera);
		for (int i = 0; i < Bodies.Count; i++)
		{
			GravObject body = Bodies[i];

			totalMassInSystem += body.Mass;
			totalMomentumInSystem += body.Velocity * body.Mass;

			DrawCircle((int)body.Position.X, (int)body.Position.Y, body.Radius, body.Color);
		}
		EndMode2D();


		DrawText(
			$"Camera Target: ({camera.target.X.ToString("n2")}, {camera.target.Y.ToString("n2")})\n" +
			$"Mouse Position: ({GetMousePosition().X}, {GetMousePosition().Y})\n" +
			$"Zoom Level: {camera.zoom.ToString("n2")}\n" +
			$"FPS: {(isPaused ? "PAUSED" : (1f / GetFrameTime()).ToString("n2"))}\n" +
			$"Total Mass: {totalMassInSystem}\n" +
			$"Total Momentum: {totalMomentumInSystem}",
			14, 14, 20, Color.BLACK);


		EndDrawing();
	}
}
