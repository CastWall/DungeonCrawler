#if TOOLS
using Godot;
using System;

[Tool]
public partial class WallPlugin : EditorPlugin
{
    private Control _dock;
    public override void _EnterTree()
    {
        var script = GD.Load<Script>("res://addons/WallPlugin/WallComputeButton.cs");
        var texture = GD.Load<Texture2D>("res://icon.svg");
        AddCustomType("WallButton", "Button", script, texture);

        _dock = GD.Load<PackedScene>("res://addons/WallPlugin/wall_compute_dock.tscn").Instantiate<Control>();
        AddControlToDock(DockSlot.RightBl, _dock);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        RemoveCustomType("WallButton");
        RemoveControlFromDocks(_dock);
        _dock.Free();
    }
}
#endif