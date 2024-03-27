using Godot;

namespace DungeonCrawler;

public partial class PlayerController : Node3D
{
    public Direction CurrentDirection { get; protected set; }

    private Camera3D _camera;
    private Node3D _debugObject;
    private GraffitiController _graffitiController;

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("Camera3D");
        _debugObject = GetNode<Node3D>("Debug");
        _graffitiController = GetNode<GraffitiController>("GraffitiController");

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

        if (Input.IsActionJustPressed("graff"))
        // if (Input.IsActionPressed("graff"))
        {
            _graffitiController.Graff();
        }
    }


    protected void SnapCamera() => SnapCamera(CurrentDirection);

    protected void SnapCamera(Direction direction)
    {
        _camera.RotateY(0); // Reset rotation between -180 and 180
        
        Tween tween = GetTree().CreateTween();
        tween.SetProcessMode(Tween.TweenProcessMode.Idle); //on frame update
        tween.TweenProperty(_camera,
            "rotation",
            new Vector3(0, DirectionHelper.GetAimedYAngle(_camera), 0),
            0.5f
        );
    }


    protected void SetCurrentDirectionFromCamera()
    {
        CurrentDirection = DirectionHelper.FromCamera(_camera);
    }
}