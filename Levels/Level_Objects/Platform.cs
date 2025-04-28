using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

public class Platform : LevelObject
{
    // Constructor
    public Platform(int id, float x, float y, bool isSolid) : base(id, x, y, isSolid)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
        this.Width = 81; // Image is 81 pixels wide
        this.Height = 9; // Image is 9 pixels tall
        this.isSolid = isSolid;
    }
}