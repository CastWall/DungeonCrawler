using Godot;

namespace DungeonCrawler;

public partial class Trap : Node3D
{
    public bool Activated { get; protected set; }
    public float ActivationTime { get; protected set; } = 0.2f;

    public void Activate()
    {
        if (Activated)
            return;
        Activated = true;
        ActivateTween().Finished += () => { GD.Print("activate finished"); };
    }

    private Tween ActivateTween()
    {
        Tween tween = GetTree().CreateTween();

        tween.TweenProperty(this,
            "position",
            new Vector3(0, 1, 0),
            ActivationTime
        );
        
        return tween;
    }
}