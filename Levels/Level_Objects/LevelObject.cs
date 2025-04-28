using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

public abstract class LevelObject
{
    // Properties
    public int ID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; } // Width of the object
    public float Height { get; set; } // Height of the object
    public bool isSolid; // Determines if the object is solid or not

    // Constructor
    public LevelObject(int id, float x, float y, bool isSolid)
    {
        ID = id;
        X = x;
        Y = y;
        Width = 0; // Default value, can be set later
        Height = 0; // Default value, can be set later
        this.isSolid = isSolid;
    }
}
