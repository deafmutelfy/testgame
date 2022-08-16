using Godot;
using System;
using System.Linq;

public class DialogSystem : Node2D
{
	private Label _l;
	private string _currentMessage = "";
	
	const float Period = (float)0.02; 
	const int CharsPerLine = 58;
	
	private float _timer = 0;
	private Semaphore _semaphore;
	private bool _can_exit = false;

	public override void _Ready()
	{
		base._Ready();

		_l = GetNode<Label>("Label");
		_semaphore = new Semaphore();
	}

	public new void Show()
	{
		Visible = true;
	}

	public new void Hide()
	{
		Visible = false;
	}

	public void Say(string msg, bool format = true)
	{
		if (format)
		{
			msg = msg.Replace("\n", "");
			int chunkSize = CharsPerLine;

			for (int i = 0; i < msg.Length; i += chunkSize)
			{
				if (i + chunkSize > msg.Length) chunkSize = msg.Length - i;
				_currentMessage += msg.Substring(i, chunkSize) + "\n";
			}
		}
		else
		{
			_currentMessage = msg;
		}

		_semaphore.Wait();
	}

	public override void _PhysicsProcess(float delta)
	{
		_timer += delta;

		if (_timer > Period && _currentMessage.Length > 0)
		{
			_l.Text += _currentMessage[0];

			_currentMessage = _currentMessage.Remove(0, 1);

			_timer = 0;

			if (_currentMessage.Length == 0)
			{
				_can_exit = true;
			}
		}

		if (Input.IsActionJustPressed("ui_skip") && _currentMessage.Length > 0)
		{
			_l.Text += _currentMessage;

			_currentMessage = "";
				
			_can_exit = true;
		}

		if (Input.IsActionJustPressed("ui_accept") && _can_exit)
		{
			_l.Text = "";
			_can_exit = false;
			
			_semaphore.Post();
		}
	}
}
