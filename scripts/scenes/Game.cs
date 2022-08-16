using Godot;
using System;
using System.Threading;
using Thread = System.Threading.Thread;

public class Game : Node2D
{
	private DialogSystem _ds;
	private bool _started = false;

	private bool _menorah_can_interact = false;
	private AudioStreamPlayer _soundtrack;
	
	public override void _Ready()
	{
		base._Ready();

		_ds = GetNode<DialogSystem>("/root/Game/DialogSystem");
		_soundtrack = GetNode<AudioStreamPlayer>("/root/Game/Soundtrack");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionJustPressed("ui_accept") && !_started && _menorah_can_interact)
		{
			_started = true;

			if (!_soundtrack.Playing)
			{
				_soundtrack.Play();
			}

			new Thread(() =>
			{
				_ds.Show();

				_ds.Say(@"сьлржалсч пыж сься...
ньжпмжчмщмьсзсжмььмпьмзчььщыьмньььыьмоь
сьлржалсч пыж тьс, нно нйрйхлрьы лйщй тфщс йщрс йшй фжйе хь", format: false);
				_ds.Say(@"чй цкйпщ рйфйчюлй лйдй.
сьлржалсч пыж, а он, в свою чердй очередь, - пчтьсясшчсьлсь.
В то время, как он (пыж) сьйвлсь л чпчшлй сьчяш.
", format: false);
				_ds.Say(@"Затем пыж сьйвлялсся к чшм и пщсл на место.
На рис. 2 сйрвлься чшмыщ.
сьлржалсч-1 ", format: false);
				_ds.Say(
					@"еощссьещшж1а1ея1сяццшршв1о1с1ьь1кжччхн1шп1оцхыов1еб1т1оы11и1ч1н1э1ц1ф2э1е1
юс1хщмрщщръъуыуьуъыъ1ъжчшщюяфзфуьтй1фгфхфн1х1хщо1у1");
				_ds.Say(@"сьлржалсч - ссьлржась,
нпрс - нпрсжавшись, нрс - нрсказившись,
всьрж - всьржавшись.", format: false);
				_ds.Say(@"сьлржалсч - сьпщрялсь , ( сьдрьжалься, сьдрижжать) - сдьржа (сдьрижжать) сьгдьрагълъйсьт
 - сьглълъйсьт , (съгдьржать, съгдьгржать) , (сьглъжять, сьглажжать).");
				
				_ds.Hide();

				_started = false;
			}).Start();
		}
		
	}
	
	public void MenorahTriggerEnter(Area2D area)
	{
		if (area.Name == "Guy")
		{
			_menorah_can_interact = true;
		}
	}
	
	public void MenorahTriggerExit(Area2D area)
	{
		if (area.Name == "Guy")
		{
			_menorah_can_interact = false;
		}
	}
}

