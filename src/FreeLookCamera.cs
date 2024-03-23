using System;
using Godot;

namespace DungeonCrawler;

public partial class FreeLookCamera : Camera3D
{
    [Export(PropertyHint.Range, "0f,10f,0.01f")]
    public float Sensitivity = 3f;

    public override void _Input(InputEvent @event)
    {
        if (!Current)
            return;
        base._Input(@event); //On est sûr de ça ?

        if (Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            if (@event is InputEventMouseMotion mouseMotionEvent)
            {
                Vector3 tempRot = Rotation;
                tempRot.Y -= mouseMotionEvent.Relative.X / 1000 * Sensitivity;
                tempRot.X -= mouseMotionEvent.Relative.Y / 1000 * Sensitivity;
                tempRot.X = Mathf.Clamp(tempRot.X, Mathf.Pi / -2, Mathf.Pi / 2);
                Rotation = tempRot;
            }
        }

        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            switch (mouseButtonEvent.ButtonIndex)
            {
                case MouseButton.Right:
                    Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
                    break;
            }
        }
    }

    public override void _Process(double delta)
    {
        if (!Current)
            return;
        base._Process(delta);
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
}