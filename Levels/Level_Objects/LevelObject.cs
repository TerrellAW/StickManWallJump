using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

internal abstract class LevelObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public bool IsSolid { get; set; }
    public bool IsVisible { get; set; }
    public bool IsActive { get; set; }

    public LevelObject(int id, string name, bool isSolid, bool isVisible, bool isActive)
    {
        ID = id;
        Name = name;
        IsSolid = isSolid;
        IsVisible = isVisible;
        IsActive = isActive;
    }
}
