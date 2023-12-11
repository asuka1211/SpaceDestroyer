using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene MobScene { get; set; }

    private Player _player;

    private int _enemyCount = 0;

    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        GetNode<Timer>("MobTimer").Start();
    }

    private void OnMobTimerTimeout()
    {
        if (_enemyCount < 5)
        {
            Enemy mob = MobScene.Instantiate<Enemy>();
            mob.Player = _player;

            // Choose a random location on Path2D.
            var mobSpawnLocation = GetNode<PathFollow2D>("MobSpawnPath/MobSpawner");
            mobSpawnLocation.ProgressRatio = GD.Randf();

            // Set the mob's position to a random location.
            mob.Position = mobSpawnLocation.Position;

            // Spawn the mob by adding it to the Main scene.
            AddChild(mob);
            _enemyCount++;
        }
    }
}
