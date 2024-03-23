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
        NorthWall = GetNode<Node3D>("Walls/Wall-Z");
        EastWall = GetNode<Node3D>("Walls/Wall+X");
        SouthWall = GetNode<Node3D>("Walls/Wall+Z");
        WestWall = GetNode<Node3D>("Walls/Wall-X");

        NorthWall.Visible = NorthWallEnabled;
        EastWall.Visible = EastWallEnabled;
        SouthWall.Visible = SouthWallEnabled;
        WestWall.Visible = WestWallEnabled;
    }

    public override void _Ready()
    {
        InitWalls();
    }
}