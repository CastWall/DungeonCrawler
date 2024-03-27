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
        _splashImage.Decompress();
        // _splashImage.Convert(Image.Format.Rgba8);

        _mat = _wallPlane.GetActiveMaterial(0) as BaseMaterial3D;
        GD.Print("mat : ", _mat);

        // _img = _mat.AlbedoTexture.GetImage().Duplicate() as Image;
        // _img.Decompress();

        _img = Image.Create(1024, 1024, false, Image.Format.Rgba8);
        Random rnd = new Random();
        _img.Fill(new Color(
            1,
            1,
            1
        ));

        _mat.AlbedoTexture = ImageTexture.CreateFromImage(_img);
        UpdateTexture();

        for (int i = 0; i < 100; i++)
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
        // RandomSplash();
    }

    public void RandomSplash()
    {
        Random rnd = new Random();

        Vector2I splashPoint = new Vector2I(
            rnd.Next(_img.GetWidth()),
            rnd.Next(_img.GetHeight())
        );

        Vector2I splashDim = _splashImage.GetSize();
        // GD.Print("splash point : ", splashPoint, ", splash dim : ", splashDim);
        // Vector2I splashDim = new Vector2I(128,128);

        // GD.Print("tex format :", (_mat.AlbedoTexture as ImageTexture).GetFormat(), ", splash format : ", _splashImage.GetFormat());
        _img.BlendRect(_splashImage, new Rect2I(0, 0, splashDim), splashPoint);
        UpdateTexture();
    }
}