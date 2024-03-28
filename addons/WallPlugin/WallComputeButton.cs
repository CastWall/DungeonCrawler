using Godot;

namespace DungeonCrawler.addons.WallPlugin;

[Tool]
public partial class WallComputeButton : Button
{
    // [Export] private String debug;
    public override void _EnterTree()
    {
        Pressed += Clicked;
    }

    public void Clicked()
    {
        GD.Print("You clicked me in C#!");
        var editor = EditorInterface.Singleton;

        var root = editor.GetEditedSceneRoot();
        if (root.Name != "Level")
        {
            GD.Print("Select Level scene first");
            return;
        }

        foreach (var node in root.GetChildren())
        {
            GD.Print("node : ", node.Name);
            root.SetEditableInstance(node, true);
            HandleWalls(node);
        }
    }

    private void HandleWalls(Node block)
    {
        
    }
}