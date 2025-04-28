using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

public class Wall : LevelObject
{
    // Properties
    public bool FacingRight { get; set; } // Direction the wall is facing

    // Constructor
    public Wall(int id, float x, float y, bool isSolid, bool facingRight) : base(id, x, y, isSolid)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
        this.Width = 97; // Image is 97 pixels wide
        this.Height = 750; // Image is 750 pixels tall
        this.isSolid = isSolid;
        this.FacingRight = facingRight;
    }
}