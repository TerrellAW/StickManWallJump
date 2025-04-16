using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

internal class Platform : LevelObject
{
    public Platform(int id, float x, float y, float width, float height) : base(id, x, y, width, height)
    {
    }
}