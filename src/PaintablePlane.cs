using System;
using Godot;

namespace DungeonCrawler;

public abstract partial class PaintablePlane : Node3D, IPaintable
{
    [Export] protected MeshInstance3D Plane;
    protected Image SplashImage;

    protected BaseMaterial3D Mat;
    protected Image BaseImg;
    protected Image Img;

    public override void _Ready()
    {
        if (Engine.IsEditorHint())
            return;
        SetupTexturePainting();
    }

    private void SetupTexturePainting()
    {
        GraffitiController graffitiController = GetNode<GraffitiController>("/root/Game/Player/GraffitiController");
        SplashImage = graffitiController.SplashTexture.GetImage();
        SplashImage.Decompress();

        Mat = Plane.GetActiveMaterial(0) as BaseMaterial3D;
        BaseImg = Mat.AlbedoTexture.GetImage();
        BaseImg.Decompress();
        Img = Image.Create(BaseImg.GetWidth(), BaseImg.GetHeight(), false, Image.Format.Rgba8);
        Img.BlitRect(BaseImg, new Rect2I(0, 0, Img.GetSize()), new Vector2I(0, 0));

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

    protected void Erase(Vector2I texCoords)
    {
        // GD.Print("erasing");
        var size = SplashImage.GetSize();
        var topLeftCorner = texCoords - (size / 2);
        // Img.BlendRectMask(BaseImg, SplashImage,new Rect2I(topLeftCorner, size), topLeftCorner);
        Img.BlendRect(BaseImg, new Rect2I(topLeftCorner, size), topLeftCorner);
        UpdateTexture();
    }

    public abstract Vector2I ToTextureCoords(Vector3 localCoords);

    public void Paint(Vector3 point)
    {
        var localCoords = ToLocal(point);
        // GD.Print("painting wall at coords ", localCoords, " -> tex coord = ", ToTextureCoords(localCoords));
        Paint(ToTextureCoords(localCoords));
    }

    public void Erase(Vector3 point)
    {
        var texCoords = ToTextureCoords(ToLocal(point));
        Erase(texCoords);
    }
}