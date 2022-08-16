using Godot;
using System;

public class Guy : KinematicBody2D
{
	private AnimatedSprite _s;
	const int Speed = 150;

	public override void _Ready()
	{
		base._Ready();
		
		_s = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 ratio = new Vector2();
		float rspeed = Speed * delta;
		
		if (Input.IsActionPressed("ui_left"))
		{
			if (!_s.Playing)
			{
				_s.Play("left_go");

				_s.Playing = true;
			}

			ratio.x -= rspeed;
		}

		if (Input.IsActionPressed("ui_right"))
		{
			if (!_s.Playing)
			{
				_s.Play("right_go");

				_s.Playing = true;
			}
			
			ratio.x += rspeed;
		}

		if (Input.IsActionPressed("ui_up"))
		{
			if (!_s.Playing)
			{
				_s.Play("up_go");

				_s.Playing = true;
			}
			
			ratio.y -= rspeed;
		}

		if (Input.IsActionPressed("ui_down"))
		{
			if (!_s.Playing)
			{
				_s.Play("down_go");

				_s.Playing = true;
			}
			
			ratio.y += rspeed;
		}

		if (Input.IsActionJustReleased("ui_left"))
		{
			_s.Play("left");
			
			_s.Playing = false;
		}
		
		if (Input.IsActionJustReleased("ui_right"))
		{
			_s.Play("right");
			
			_s.Playing = false;
		}
		
		if (Input.IsActionJustReleased("ui_up"))
		{
			_s.Play("up");
			
			_s.Playing = false;
		}
		
		if (Input.IsActionJustReleased("ui_down"))
		{
			_s.Play("down");
			
			_s.Playing = false;
		}

		MoveAndCollide(ratio);
	}
}
