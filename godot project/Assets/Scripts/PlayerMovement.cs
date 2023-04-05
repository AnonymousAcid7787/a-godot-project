using Godot;
using System;

public class Movement : CharacterBody2D
{
    [Export] public int Speed = 200;

    Vector2 velocity = new Vector2();

    public void GetInput()
    {
        velocity = new Vector2();
        if (Input.IsActionPressed("right"))
        {
            velocity.X += 1;
        }
        if (Input.IsActionPressed("left"))
        {
            velocity.X -= 1;
        }
        if (Input.IsActionPressed("down"))
        {
            velocity.Y += 1;
        }
        if (Input.IsActionPressed("up"))
        {
            velocity.Y -= 1;
        }
        velocity = velocity.Normalized() * Speed;
    }
}