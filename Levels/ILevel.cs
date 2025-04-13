using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickManWallJump;

internal interface ILevel
{
    /// <summary>
    /// 
    /// Levels will have their own variables, such as ground/platform friction, so some levels are slippery and some aren't.
    /// Levels will also have their own textures, such as the background, platforms, and walls.
    /// Backgrounds will be audio visualizers that react to that level's music. Levels will have different music.
    /// 
    /// Below are example functions, some might be fleshed out, some might be removed.
    /// </summary>
    void LoadLevel();
    void UnloadLevel();
    void UpdateLevel(float deltaTime);
    void DrawLevel();
    void ResetLevel();
    bool IsLevelComplete();
    bool IsLevelFailed();
    void RestartLevel();
}
