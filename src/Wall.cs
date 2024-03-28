using System;
using Godot;

namespace DungeonCrawler;

public partial class Wall : PaintablePlane, IPaintable
{
    protected const float Bottom = 0;
    protected const float Top = 2;
    protected const float Left = -1;
    protected const float Right = 1;
    
    public override void _Process(double delta)
    {
        base._Process(delta);
        // RandomSplash();
    }

    public void RandomSplash()
    {
        Random rnd = new Random();

        Vector2I splashPoint = new Vector2I(
            rnd.Next(Img.GetWidth()),
            rnd.Next(Img.GetHeight())
        );

        // GD.Print("splash point : ", splashPoint, ", splash dim : ", splashDim);
        // Vector2I splashDim = new Vector2I(128,128);

        // GD.Print("tex format :", (_mat.AlbedoTexture as ImageTexture).GetFormat(), ", splash format : ", _splashImage.GetFormat());
        Paint(splashPoint);
    }
    public override Vector2I ToTextureCoords(Vector3 localCoords)
    {
        var wallSizeX = Right - Left;
        var wallSizeY = Top - Bottom;


        return new Vector2I(
            (int)((localCoords.X - Left) * Img.GetWidth() / wallSizeX),
            (int)((Top - localCoords.Y) * Img.GetHeight() / wallSizeY)
        );
    }
}