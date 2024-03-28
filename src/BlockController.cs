using Godot;

namespace DungeonCrawler;

[Tool]
public partial class BlockController : Node3D
{
    public const float BlockSize = 2;

    //X+ = east

    [Export] protected bool NorthWallEnabled;
    [Export] protected bool EastWallEnabled;
    [Export] protected bool SouthWallEnabled;
    [Export] protected bool WestWallEnabled;

    private Node3D _northWall;
    private Node3D _eastWall;
    private Node3D _southWall;
    private Node3D _westWall;

    private Label3D _debugLabel;
    public Vector2I GridCoords { get; private set; }

    public override void _Ready()
    {
        InitWalls();
        InitGridCoords();
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            InitWalls();
            Name = $"{GridCoords}";
            // GD.Print("Init walls");
        }
    }

    private void InitWalls()
    {
        _northWall = GetNode<Node3D>("Walls/WallNorth");
        _eastWall = GetNode<Node3D>("Walls/WallEast");
        _southWall = GetNode<Node3D>("Walls/WallSouth");
        _westWall = GetNode<Node3D>("Walls/WallWest");

        SetWallState(_northWall, NorthWallEnabled);
        SetWallState(_eastWall, EastWallEnabled);
        SetWallState(_southWall, SouthWallEnabled);
        SetWallState(_westWall, WestWallEnabled);
    }

    private void InitGridCoords()
    {
        _debugLabel = GetNode<Label3D>("DebugText");

        GridCoords = new Vector2I(
            Mathf.RoundToInt(GlobalPosition.X / BlockSize),
            Mathf.RoundToInt(GlobalPosition.Z / BlockSize)
        );

        _debugLabel.Text = $"{GridCoords}";
    }

    private void SetWallState(Node3D wall, bool state)
    {
        wall.Visible = state;
        wall.SetProcess(state);
        wall.GetNode<Area3D>("Area3D").GetNode<CollisionShape3D>("CollisionShape3D").Disabled = !state;
    }
}