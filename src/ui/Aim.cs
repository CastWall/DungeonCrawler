using Godot;
using System;
using DungeonCrawler;

public partial class Aim : Label
{
    private PlayerController _player;
    
    public override void _Ready()
    {
        _player = GetNode<PlayerController>("/root/Game/Player");
    }

    public override void _Process(double delta)
    {
        Text = $"{_player.CurrentDirection} -> {_player.AimedCoords}";
    }
}
