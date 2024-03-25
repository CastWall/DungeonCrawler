using Godot;

namespace DungeonCrawler;

public partial class PlayerController : Node3D
{
    public Direction CurrentDirection { get; protected set; }

    protected Camera3D Camera;
    protected Node3D DebugObject;

    public override void _Ready()
    {
        Camera = GetNode<Camera3D>("Camera3D");
        DebugObject = GetNode<Node3D>("Debug");

        GD.Print("Player ready");
        SetCurrentDirectionFromCamera();
    }

    public override void _Process(double delta)
    {
        SetCurrentDirectionFromCamera();
        if (Input.IsActionJustPressed("snap"))
        {
            SnapCamera();
        }
    }


    protected void SnapCamera() => SnapCamera(CurrentDirection);

    protected void SnapCamera(Direction direction)
    {
        Camera.RotateY(0); // Reset rotation between -180 and 180
        
        Tween tween = GetTree().CreateTween();
        tween.SetProcessMode(Tween.TweenProcessMode.Idle); //on frame update
        tween.TweenProperty(Camera,
            "rotation",
            new Vector3(0, DirectionHelper.GetAimedYAngle(Camera), 0),
            0.5f
        );
    }


    protected void SetCurrentDirectionFromCamera()
    {
        CurrentDirection = DirectionHelper.FromCamera(Camera);
    }
}