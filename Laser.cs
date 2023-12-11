using Godot;
using System;

public partial class Laser : Area2D
{

    private int _speed = 10;

    [Export]
    public Vector2 Direction;

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _speed * Direction;
    }

    private void _OnVisibleOnScreenNotifier2dScreenExited()
    {
        QueueFree();
    }

    private void OnBodyEntered(Node2D body)
    {
        body.QueueFree();
    }
}
