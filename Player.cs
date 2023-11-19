using Godot;
using System;

public partial class Player : CharacterBody2D
{

    private int speed = 400;

    public override void _PhysicsProcess(double delta)
    {
        var target = GetGlobalMousePosition();
        var direction = (target - GlobalPosition).Normalized();
        var velocity = direction * speed;
        Velocity = velocity;
        MoveAndSlide();
        LookAt(target);
    }
}
