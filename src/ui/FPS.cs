using Godot;

namespace DungeonCrawler.ui;

public partial class FPS : Label
{
    public override void _Process(double delta)
    {
        Text = $"{Engine.GetFramesPerSecond()}";
    }
}