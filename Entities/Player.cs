using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickManWallJump;

public class Player : Entity
{
    // Texture
    public override Texture2D Texture { get; set; }

    // Player constants
    public override float MaxSpeed { get; set; } = 400f;
    public float WallJumpForce { get; set; } = 200f; // Force applied during wall jumps

    // Variables
    private float _timeSinceWallContact = 0f; // Time since the player last touched a wall

    // Flags
    private bool _isOnWall;

    // Getters and Setters
    public bool IsOnWall
    {
        get => _isOnWall;
        set
        {
            _isOnWall = value;
            if (_isOnWall)
            {
                _timeSinceWallContact = 0f; // Reset the time since wall contact
            }
        }
    }

    // Constructor
    public Player(Vector2 Position, float SpeedX, float SpeedY, float JumpForce, float NextPositionX, float NextPositionY, bool facingRight)
    {
        this.Position = Position;
        this.SpeedX = SpeedX;
        this.SpeedY = SpeedY;
        this.JumpForce = JumpForce;
        this.NextPositionX = NextPositionX;
        this.NextPositionY = NextPositionY;
        this.FacingRight = facingRight;
        this.CollisionBounds = new Rectangle((int)Position.X, (int)Position.Y, 24, 49); // Player is 24x49 pixels
        this.CollisionBoundsNext = new Rectangle((int)NextPositionX, (int)NextPositionY, 24, 49); // Player is 24x49 pixels
    }

    // Methods
    public bool CanWallJump()
    {
        // Check if the player can wall jump
        return _timeSinceWallContact <= 0.40f; // Player has touched the wall within last 0.40 seconds
    }

    public void Update(GameTime gameTime)
    {
        if (!_isOnWall)
        {
            _timeSinceWallContact += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
