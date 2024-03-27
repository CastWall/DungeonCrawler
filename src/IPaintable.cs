using Godot;

namespace DungeonCrawler;

public interface IPaintable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="point">in global coordinates</param>
    public void Paint(Vector3 point);
}