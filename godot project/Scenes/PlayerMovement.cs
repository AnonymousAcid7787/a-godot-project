using Godot;
using System;
using System.Threading;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] public int walkSpeed = 200;

    public override void _PhysicsProcess(double delta)
    {
        Velocity = GetInput();
        MoveAndSlide();
    }

    public void TestThread() {
        while(true) {
            Thread.Sleep(1000);
        }
    }

    public override void _Ready()
    {
        base._Ready();
        ThreadStart threadStart = new ThreadStart(TestThread);
        Thread thread = new Thread(threadStart);
        thread.Start();
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
