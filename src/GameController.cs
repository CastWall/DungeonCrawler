using Godot;

namespace DungeonCrawler;

public partial class GameController : Node
{
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("advance"))
        {
            GD.Print("advance");
        }
    }
}