using Godot;
using System;

public partial class Main : Node2D
{
    [Export]
    public PackedScene MobScene { get; set; }

     [Export]
    public PackedScene PlayerScene { get; set; }

    private Player _player;

    private int _enemyCount = 0;

    private void OnMobTimerTimeout()
    {
        if (_enemyCount < 5)
        {
            Enemy mob = MobScene.Instantiate<Enemy>();
            mob.Player = _player;

            var mobSpawnLocation = GetNode<PathFollow2D>("MobSpawnPath/MobSpawner");
            mobSpawnLocation.ProgressRatio = GD.Randf();

            mob.Position = mobSpawnLocation.Position;
            mob.TreeExited += EnemyIsDead;
            mob.AddToGroup("enemy");

            AddChild(mob);
            _enemyCount++;
        }
    }

    private void StartGamePressed()
    {
        _player = PlayerScene.Instantiate<Player>();
        _player.Position = GetViewportRect().Size / 2;
        _player.TreeExited += PlayerIsDead;
        AddChild(_player);
        GetNode<Timer>("MobTimer").Start();
    }

    private void PlayerIsDead()
    {
        _player = null;
        GetTree().CallGroup("enemy", "queue_free");
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Hud>("HUD").ShowStartButton();
    }

    private void EnemyIsDead()
    {
        _enemyCount--;
    }
}
