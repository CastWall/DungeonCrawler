using System;
using Godot;

namespace DungeonCrawler;

public partial class PlayerController : Node3D
{
    public Direction CurrentDirection { get; protected set; }

    public Vector2I AimedCoords
    {
        get
        {
            int deltaX = 0, deltaY = 0;
            switch (CurrentDirection)
            {
                case Direction.NORTH:
                    deltaY--;
                    break;
                case Direction.EAST:
                    deltaX++;
                    break;
                case Direction.SOUTH:
                    deltaY++;
                    break;
                case Direction.WEST:
                    deltaX--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return CurrentCoords + new Vector2I(deltaX, deltaY);
        }
    }

    private Camera3D _camera;
    private Node3D _debugObject;
    private GraffitiController _graffitiController;
    public Vector2I CurrentCoords { get; protected set; }

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
        else if (Input.IsActionJustPressed("advance"))
        {
            SnapCamera().Finished += () => { Advance(); };
        }

        // if (Input.IsActionJustPressed("paint"))
        if (Input.IsActionPressed("paint"))
        {
            _graffitiController.Paint();
        }

        if (Input.IsActionPressed("erase"))
        {
            _graffitiController.Erase();
        }
    }


    protected Tween SnapCamera() => SnapCamera(CurrentDirection);

    protected Tween SnapCamera(Direction direction)
    {
        _camera.RotateY(0); // Reset rotation between -180 and 180

        Tween tween = GetTree().CreateTween();
        tween.SetProcessMode(Tween.TweenProcessMode.Idle); //on frame update
        tween.TweenProperty(_camera,
            "rotation",
            new Vector3(0, DirectionHelper.GetAimedYAngle(_camera), 0),
            0.5f
        );
        return tween;
    }

    private Tween Advance()
    {
        Tween tween = GetTree().CreateTween();

        tween.SetProcessMode(Tween.TweenProcessMode.Idle);
        tween.TweenProperty(this,
            "position",
            Position + DirectionHelper.GetOffset(CurrentDirection) * 2,
            1f
        );

        return tween;
    }


    protected void SetCurrentDirectionFromCamera()
    {
        CurrentDirection = DirectionHelper.FromCamera(_camera);
    }
}