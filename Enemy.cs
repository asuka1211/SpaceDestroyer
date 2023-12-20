using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    private int _speed = 100;
    private AnimationPlayer _animationPlayer;
    private PackedScene _laser = ResourceLoader.Load("res://laser.tscn") as PackedScene;
    private Marker2D _gun1;
    private Marker2D _gun2;
    private Marker2D _gun3;
    private Marker2D _gun4;
    private Timer _bulletTimer;
    private bool isEnemyCanShoot = true;

    public Player Player;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _gun1 = GetNode<Marker2D>("LaserSpawn1");
        _gun2 = GetNode<Marker2D>("LaserSpawn2");
        _gun3 = GetNode<Marker2D>("LaserSpawn3");
        _gun4 = GetNode<Marker2D>("LaserSpawn4");
        _bulletTimer = GetNode<Timer>("BulletTimer");
    }

    private void Shoot()
    {
        InstantiateLaser(_gun1);
        InstantiateLaser(_gun2);
        InstantiateLaser(_gun3);
        InstantiateLaser(_gun4);
    }

    public override void _PhysicsProcess(double delta)
    {
        Navigation(Player.Position);
    }

    private void Navigation(Vector2 target)
    {
        var direction = (target - GlobalPosition).Normalized();
        Velocity = direction * _speed;
        if (Position.DistanceTo(target) > 10)
        {
            MoveAndSlide();
            _animationPlayer.Play("fly");
        }
        else
        {
            _animationPlayer.Stop();
        }
        LookAt(target);
    }

    public override void _Process(double delta)
    {
        if (GlobalPosition.DirectionTo(Player.GlobalPosition).Dot(GlobalPosition) > 0 && isEnemyCanShoot)
        {
            if(_bulletTimer.IsStopped()){
                _bulletTimer.Start();
            }
            Shoot();
            isEnemyCanShoot = false;
        }
    }

    private void InstantiateLaser(Marker2D gun)
    {
        var laser = _laser.Instantiate() as Laser;
        var direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
        laser.GlobalPosition = gun.GlobalPosition;
        laser.Direction = direction.Normalized();
        laser.Rotation = direction.Angle();
        GetTree().CurrentScene.AddChild(laser);
    }

    private void OnBulletTimerTimeout()
    {
        isEnemyCanShoot = true;
    }
}