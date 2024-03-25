using Godot;

namespace DungeonCrawler;

public partial class BlockController : Node3D
{
    //X+ = east

    [Export] protected bool NorthWallEnabled;
    [Export] protected bool EastWallEnabled;
    [Export] protected bool SouthWallEnabled;
    [Export] protected bool WestWallEnabled;

    protected Node3D NorthWall;
    protected Node3D EastWall;
    protected Node3D SouthWall;
    protected Node3D WestWall;

    private void InitWalls()
    {
        NorthWall = GetNode<Node3D>("Walls/WallNorth");
        EastWall = GetNode<Node3D>("Walls/WallEast");
        SouthWall = GetNode<Node3D>("Walls/WallSouth");
        WestWall = GetNode<Node3D>("Walls/WallWest");

        SetWallState(NorthWall, NorthWallEnabled);
        SetWallState(EastWall, EastWallEnabled);
        SetWallState(SouthWall, SouthWallEnabled);
        SetWallState(WestWall, WestWallEnabled);
    }

    private void SetWallState(Node3D wall, bool state)
    {
        wall.Visible = state;
        wall.SetProcess(state);
        wall.GetNode<Area3D>("Area3D").GetNode<CollisionShape3D>("CollisionShape3D").Disabled = !state;
    }

    public override void _Ready()
    {
        InitWalls();
    }
}