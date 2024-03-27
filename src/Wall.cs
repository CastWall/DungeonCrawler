using Godot;
using System;

public partial class Wall : Node3D
{
    [Export] private Texture2D _splashTexture;
    private Image _splashImage;

    private MeshInstance3D _wallPlane;
    private BaseMaterial3D _mat;
    private Image _img;

    public override void _Ready()
    {
        _wallPlane = GetNode<MeshInstance3D>("WallModel/Plane");
        GD.Print("wall plane :", _wallPlane);

        _splashImage = _splashTexture.GetImage();

        _mat = _wallPlane.GetActiveMaterial(0) as BaseMaterial3D;
        GD.Print("mat : ", _mat);
        // mat.AlbedoTexture.
        _img = Image.Create(1024, 1024, false, Image.Format.Rgba8);
        Random rnd = new Random();
        _img.Fill(new Color(
            rnd.NextSingle(),
            rnd.NextSingle(),
            rnd.NextSingle()
        ));
        _mat.AlbedoTexture = ImageTexture.CreateFromImage(_img);
        UpdateTexture();

        RandomSplash();
    }

    private void UpdateTexture()
    {
        if (_mat == null)
            return;

        (_mat.AlbedoTexture as ImageTexture).Update(_img);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        RandomSplash();
    }

    public void RandomSplash()
    {
        Random rnd = new Random();

        Vector2I splashPoint = new Vector2I(
            rnd.Next(_img.GetWidth()),
            rnd.Next(_img.GetHeight())
        );

        // Vector2I splashDim = _splashImage.GetSize();
        Vector2I splashDim = new Vector2I(128,128);
        // GD.Print("splashing at ", splashPoint, " with dim ", splashDim);

        _img.FillRect(new Rect2I(splashPoint, splashDim), new Color(rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle()));
        // _img.Rect
        // _img.BlitRect(_splashImage, new Rect2I(splashPoint, splashDim), splashDim);
        UpdateTexture();
    }
}