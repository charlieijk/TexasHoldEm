using Godot;
using System;

public partial class AnimatedSprite2d : AnimatedSprite2D
{
	public override void _Ready()
	{
		Play("new_animation_1");
	}
}
