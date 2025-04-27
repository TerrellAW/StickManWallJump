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
    public Wall(int id, float x, float y, bool facingRight) : base(id, x, y)
    {
        this.ID = id;
        this.X = x;
        this.Y = y;
        this.FacingRight = facingRight;
    }
}