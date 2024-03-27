using Godot;

namespace DungeonCrawler;

public partial class GraffitiController : Node3D
{
    // [Export] private Texture2D _decalTexture;
    [Export] private PackedScene _decalPrefab;

    private Camera3D _camera;
    private RayCast3D _raycast;

    public long NumberOfGraffitis { get; protected set; }

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("../Camera3D");
        _raycast = _camera.GetNode<RayCast3D>("RayCast3D");
        NumberOfGraffitis = 0;
    }

    public void Graff()
    {
        GD.Print("camera : ", _camera, ", raycast : ", _raycast, ", hit = ", _raycast.IsColliding());
        if (_raycast.IsColliding())
        {
            // GD.Print("collinding with ", _raycast.GetCollider());
            // SpawnDecal();
            PaintTexture();
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

    private void PaintTexture()
    {
        Node3D collidedObject = _raycast.GetCollider() as Node3D;
        GD.Print("collided object : ", collidedObject, " at ", _raycast.GetCollisionPoint());
        IPaintable paintable = GetPaintable(collidedObject);
        if (paintable != null)
        {
            paintable.Paint(_raycast.GetCollisionPoint());
        }
        else
        {
            GD.Print("object ", collidedObject, " is not paintable");
        }
    }

    private IPaintable GetPaintable(Node3D n)
    {
        if (n is IPaintable paintable)
            return paintable;
        var parent = n.GetParent<Node3D>();
        if (parent != null)
        {
            return GetPaintable(parent);
        }

        return null;
    }
}