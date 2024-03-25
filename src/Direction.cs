using System;
using Godot;

namespace DungeonCrawler;

public enum Direction
{
    NORTH, // -z
    EAST, // +x
    SOUTH, // +z
    WEST, // -x
}

public static class DirectionHelper
{
    public static Vector3 GetOffset(Direction direction)
    {
        return direction switch
        {
            Direction.NORTH => new Vector3(0, 0, -1),
            Direction.EAST => new Vector3(1, 0, 0),
            Direction.SOUTH => new Vector3(0, 0, 1),
            Direction.WEST => new Vector3(-1, 0, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static float GetAimedYAngle(Camera3D camera)
    {
        var direction = FromCamera(camera);
        var angle = (-(int)direction * Mathf.Tau / 4 + Mathf.Tau) % Mathf.Tau;
        
        // if diff > 180,  reduce diff to < 180
        while (Mathf.Abs(angle - camera.Rotation.Y) > Mathf.Pi)
        {
            angle -= Mathf.Tau * Mathf.Sign(angle - camera.Rotation.Y);
        }

        return angle;
    }

    public static Direction FromCamera(Camera3D camera)
    {
        var angle = -camera.GlobalRotation.Y + Mathf.Tau;
        var rounded = Mathf.RoundToInt(4 * angle / Mathf.Tau) % 4;

        return (Direction)rounded;
    }

    public static Vector3 GetHorizontalVector(Camera3D camera)
    {
        return -new Vector3(0, 0, 1).Rotated(Vector3.Up, camera.Rotation.Y);
    }
}