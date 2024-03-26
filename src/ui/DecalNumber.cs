using Godot;

namespace DungeonCrawler.ui;

public partial class DecalNumber : Label
{
    private GraffitiController _graffitiController;
    public override void _Ready()
    {
        _graffitiController = GetNode<GraffitiController>("/root/Game/Player/GraffitiController");
    }

    public override void _Process(double delta)
    {
        Text = $"{_graffitiController.NumberOfGraffitis}";
    }
}