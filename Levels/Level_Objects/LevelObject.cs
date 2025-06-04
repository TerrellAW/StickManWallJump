using Microsoft.Xna.Framework;

namespace StickManWallJump;

internal abstract class LevelObject
{
    // Properties
    internal int ID { get; set; }
    internal float X { get; set; }
    internal float Y { get; set; }
    internal float Width { get; set; } // Width of the object
    internal float Height { get; set; } // Height of the object
    internal bool isSolid; // Determines if the object is solid or
    // TODO: Add isKill property for spikes and map boundaries

    // Collision bounds
    internal Rectangle CollisionBounds { get; set; }

    // Constructor
    public LevelObject(int id, float x, float y, bool isSolid)
    {
        ID = id;
        X = x;
        Y = y;
        Width = 0; // Default value, can be set later
        Height = 0; // Default value, can be set later
        this.isSolid = isSolid;
        CollisionBounds = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
    }
}
