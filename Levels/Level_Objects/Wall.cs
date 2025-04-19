using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

public class Wall : LevelObject
{
    // Constructor
    public Wall(int id, float x, float y) : base(id, x, y)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
    }
}