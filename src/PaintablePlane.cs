using System;
using Godot;

namespace DungeonCrawler;

public abstract partial class PaintablePlane : Node3D, IPaintable
{
    [Export] protected Texture2D SplashTexture;
    [Export] protected MeshInstance3D Plane;
    protected Image SplashImage;

    protected BaseMaterial3D Mat;
    protected Image Img;

    public override void _Ready()
    {
        SplashImage = SplashTexture.GetImage();
        SplashImage.Decompress();

        Mat = Plane.GetActiveMaterial(0) as BaseMaterial3D;
        var matBaseImg = Mat.AlbedoTexture.GetImage();
        matBaseImg.Decompress();
        Img = Image.Create(matBaseImg.GetWidth(), matBaseImg.GetHeight(), false, Image.Format.Rgba8);
        Img.BlitRect(matBaseImg, new Rect2I(0, 0, Img.GetSize()), new Vector2I(0, 0));

        Mat.AlbedoTexture = ImageTexture.CreateFromImage(Img);
        UpdateTexture();
    }

    protected void UpdateTexture()
    {
        if (Mat == null)
            return;

        (Mat.AlbedoTexture as ImageTexture).Update(Img);
    }

    protected void Paint(Vector2I texCoords)
    {
        var size = SplashImage.GetSize();
        Img.BlendRect(SplashImage, new Rect2I(0, 0, size), texCoords - (size / 2));
        UpdateTexture();
    }

    public abstract Vector2I ToTextureCoords(Vector3 localCoords);

    public void Paint(Vector3 point)
    {
        var localCoords = ToLocal(point);
        GD.Print("painting wall at coords ", localCoords, " -> tex coord = ", ToTextureCoords(localCoords));
        Paint(ToTextureCoords(localCoords));
    }
}