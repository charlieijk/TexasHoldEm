using Godot;
using System;

public partial class PokerTable : Node2D
{
	public override void _Ready()
	{
		QueueRedraw();
	}
	
	public override void _Draw()
	{
		Vector2 screenSize = new Vector2(1920, 1080);
		Vector2 center = screenSize / 2;

		// Background (darker)
		DrawRect(new Rect2(0, 0, screenSize.X, screenSize.Y), new Color(0.05f, 0.1f, 0.05f));

		// Outer border (wood frame) - scaled up
		DrawEllipse(center, new Vector2(800, 466), new Color(0.35f, 0.2f, 0.1f), true, -1, true);

		// Table felt (green) - scaled up
		DrawEllipse(center, new Vector2(767, 433), new Color(0.1f, 0.5f, 0.1f), true, -1, true);

		// Inner rail (lighter wood) - scaled up
		DrawEllipse(center, new Vector2(733, 400), new Color(0.4f, 0.25f, 0.15f), false, 12, true);

		// Subtle pattern lines on felt - scaled up
		DrawLine(new Vector2(center.X - 667, center.Y), new Vector2(center.X + 667, center.Y),
				 new Color(0.08f, 0.45f, 0.08f), 3);
	}
	
	private void DrawEllipse(Vector2 center, Vector2 radius, Color color, bool filled, float width = -1, bool antialiased = true)
	{
		int segments = 64;
		Vector2[] points = new Vector2[segments];
		
		for (int i = 0; i < segments; i++)
		{
			float angle = (float)i / segments * Mathf.Pi * 2;
			points[i] = center + new Vector2(
				Mathf.Cos(angle) * radius.X,
				Mathf.Sin(angle) * radius.Y
			);
		}
		
		if (filled)
		{
			DrawColoredPolygon(points, color);
		}
		else
		{
			for (int i = 0; i < segments; i++)
			{
				DrawLine(points[i], points[(i + 1) % segments], color, width, antialiased);
			}
		}
	}
}
