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

    // Constructor
    public LevelObject(int id, float x, float y)
    {
        ID = id;
        X = x;
        Y = y;
    }
}
