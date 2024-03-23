using Godot;

namespace DungeonCrawler;

public partial class PlayerController : Node3D
{
    public Direction CurrentDirection { get; protected set; }
    public override void _Ready()
    {
        GD.Print("Player ready");
        SetCurrentDirectionFromCamera();
    }

    public override void _Process(double delta)
    {
        SetCurrentDirectionFromCamera();
    }

    protected void SetCurrentDirectionFromCamera()
    {
        CurrentDirection = Direction.NORTH;
    }
}