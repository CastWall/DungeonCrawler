using Godot;

namespace DungeonCrawler;

public partial class GraffitiController : Node3D
{
    // [Export] private Texture2D _decalTexture;
    [Export] private PackedScene _decalPrefab;
    [Export] public Texture2D SplashTexture { get; protected set; }


    private Camera3D _camera;
    private RayCast3D _raycast;

    public long NumberOfGraffitis { get; protected set; }

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("../Camera3D");
        _raycast = _camera.GetNode<RayCast3D>("RayCast3D");
        NumberOfGraffitis = 0;
    }

    public void Paint()
    {
        if (!_raycast.IsColliding()) return;
        Node3D collidedObject = _raycast.GetCollider() as Node3D;
        IPaintable paintable = GetPaintableRecursively(collidedObject);
        // GD.Print($"collided object : {collidedObject} ({paintable}) at {_raycast.GetCollisionPoint()}");
        if (paintable != null)
        {
            paintable.Paint(_raycast.GetCollisionPoint());
            NumberOfGraffitis++;
        }
    }

    public void Erase()
    {
        if (!_raycast.IsColliding()) return;
        Node3D collidedObject = _raycast.GetCollider() as Node3D;
        IPaintable paintable = GetPaintableRecursively(collidedObject);
        if (paintable != null)
        {
            paintable.Erase(_raycast.GetCollisionPoint());
        }
    }

    private void SpawnDecal()
    {
        var decal = _decalPrefab.Instantiate<Decal>();
        AddChild(decal);
        decal.GlobalPosition = _raycast.GetCollisionPoint();
        // decal.Rotation = _camera.Rotation;
        // decal.RotationDegrees = new Vector3(90, 90, 90);

        // Node3D collidedObject = _raycast.GetCollider() as Node3D;
        // decal.GlobalRotation = collidedObject.GlobalRotation; 
        decal.Rotation = _camera.Rotation;
        decal.Rotate(_camera.Basis.X, Mathf.Pi / 2);
        // decal.Rotation = new Vector3(-Mathf.Pi/2, 0, 0);
        // decal.RotateX(Mathf.Pi);
        NumberOfGraffitis++;
    }

    /// <summary>
    /// Returns n if it is a paintable, or the closest paintable parent
    /// </summary>
    /// <param name="n"></param>
    /// <returns>the closest paintable parent, null if not found</returns>
    private IPaintable GetPaintableRecursively(Node3D n)
    {
        if (n is IPaintable paintable)
            return paintable;
        var parent = n.GetParent<Node3D>();
        if (parent != null)
        {
            return GetPaintableRecursively(parent);
        }

        return null;
    }
}