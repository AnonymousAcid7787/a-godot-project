using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] public int walkSpeed = 200;

    public override void _PhysicsProcess(double delta)
    {
        Velocity = GetInput();
        MoveAndSlide();
    }

    public Vector2 GetInput()
    {
        Vector2 tempVelocity = new Vector2();
        if (Input.IsActionPressed("right"))
            tempVelocity.X += 1;
        
        if (Input.IsActionPressed("left"))
            tempVelocity.X -= 1;
        
        if (Input.IsActionPressed("down"))
            tempVelocity.Y += 1;
        
        if (Input.IsActionPressed("up"))
            tempVelocity.Y -= 1;

        tempVelocity = tempVelocity.Normalized() * walkSpeed;
        
        return tempVelocity;
    }
}
