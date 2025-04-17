using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

public class Wall : LevelObject
{
    // Constructor
    public Wall(int id, float x, float y, float width, float height) : base(id, x, y, width, height)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }
}