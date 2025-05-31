using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StickManWallJump.Input;

namespace StickManWallJump;

public class Engine : Game
{
    // Level object
    private Level level;
    private string _levelName;

    // Level textures
    private Texture2D platformTexture;
    private Texture2D wallTexture;
    private Texture2D _debugTexture;

    // Fonts
    private SpriteFont debugFont;

    // Colors
    private Color levelFilterColor;
    private Color levelBackgroundColor;

    // Physics constants
    private float gravity;
    private float airFriction;

    // Game components
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private LevelManager _levelManager;
    private Camera _camera;

    // Input management
    private IInputManager _inputManager;

    // Debug mode
    private bool _debugMode;

    internal Engine(LevelManager levelManager, string levelName, bool debugMode = false) // TODO: Take index from LevelManager and use it to find the correct LevelLayout
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;

        // Set the screen resolution
        _graphics.PreferredBackBufferWidth = 360; // Set your desired width
        _graphics.PreferredBackBufferHeight = 800; // Set your desired height

        // Store for use in initializer
        this._levelManager = levelManager;
        this._levelName = levelName;
        this._debugMode = debugMode;
    }

    protected override void Initialize()
    {
        // Screen dimensions
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        // Get level
        level = _levelManager.GetLevel(_levelName);

        // Get level colors
        levelFilterColor = level.FilterColor;
        levelBackgroundColor = level.BackgroundColor;

        // Set physics constants
        gravity = level.Gravity;
        airFriction = level.AirFriction;

        // Initialize input manager with platform specific input type
#if ANDROID
            _inputManager = InputManagerFactory.CreateInputManager(InputType.Touch, GraphicsDevice);
#else
        _inputManager = InputManagerFactory.CreateInputManager(InputType.Keyboard, level.Player, GraphicsDevice);
#endif

        base.Initialize();

        _camera = new Camera(level.Player.Position); // Initialize camera at player position
    }

    // TODO: Load textures in level class and pass them to the engine here
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Level textures
        platformTexture = Content.Load<Texture2D>(level.SetPlatformTexture());
        wallTexture = Content.Load<Texture2D>(level.SetWallTexture());

        // Player texture
        level.Player.Texture = Content.Load<Texture2D>("Textures/stickman");

        // Font textures
        debugFont = Content.Load<SpriteFont>("Fonts/debugtext");

        // Debug 1x1 white texture
        _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
        _debugTexture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        // Update input state
        _inputManager.Update(gameTime);

        // Update player state (including wall contact timer)
        level.Player.Update(gameTime);

        // Update wall collision state
        level.Player.IsOnWall = false;

        // Exit condition
        if (_inputManager.IsPause())
            Exit(); // TODO: Replace with pause menu

        // Handle player input
        HandlePlayerInput();

        // Air friction
        if (Math.Abs(level.Player.SpeedY) > 0.01f)
            level.Player.SpeedX *= airFriction;
        else // No air friction if grounded
            level.Player.SpeedX = level.Player.FacingRight ? level.PlayerSpeed : -level.PlayerSpeed;

        // Gravity
        if (level.Player.SpeedY < level.Player.MaxSpeed)
            level.Player.SpeedY += gravity;

        // Framerate based speed
        float updatedPlayerSpeedX = level.Player.SpeedX * (float)gameTime.ElapsedGameTime.TotalSeconds;
        float updatedPlayerSpeedY = level.Player.SpeedY * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Position Prediction
        level.Player.NextPositionY = level.Player.Position.Y + updatedPlayerSpeedY;
        level.Player.NextPositionX = level.Player.Position.X + updatedPlayerSpeedX;

        //level.Player collision bounds update
        level.Player.CollisionBounds = new Rectangle(
            (int)level.Player.Position.X - (level.Player.Texture.Width / 2),
            (int)level.Player.Position.Y - (level.Player.Texture.Height / 2),
            level.Player.Texture.Width,
            level.Player.Texture.Height
        );

        //level.Player collision bounds next update
        level.Player.CollisionBoundsNext = new Rectangle(
            (int)level.Player.NextPositionX - (level.Player.Texture.Width / 2),
            (int)level.Player.NextPositionY - (level.Player.Texture.Height / 2),
            level.Player.Texture.Width,
            level.Player.Texture.Height
        );

        // Check for collisions
        foreach (var platform in level.Platforms) // Platform collisions
        {
            if (platform.isSolid && level.Player.CollisionBoundsNext.Intersects(platform.CollisionBounds))
            {
                // Calculate movement direction
                bool movingDown = level.Player.SpeedY > 0;
                bool movingUp = level.Player.SpeedY < 0;

                // For platforms, we're most concerned with top/bottom collisions

                // Top collision (level.Player landing on platform) - this is most common
                if (movingDown && level.Player.CollisionBounds.Bottom <= platform.CollisionBounds.Top + 5)
                {
                    //level.Player landed on top of platform
                    level.Player.NextPositionY = platform.CollisionBounds.Top - level.Player.Texture.Height / 2;
                    level.Player.SpeedY = 0;
                }
                // Bottom collision (level.Player hitting platform from below)
                else if (movingUp && level.Player.CollisionBounds.Top >= platform.CollisionBounds.Bottom - 5)
                {
                    level.Player.NextPositionY = platform.CollisionBounds.Bottom + level.Player.Texture.Height / 2;
                    level.Player.SpeedY = 0;
                }
                // Handle horizontal collisions with platforms as well
                // else
                // {
                //     // Left collision with platform
                //     if (level.Player.SpeedX > 0 &&
                //         level.Player.CollisionBounds.Right <= platform.CollisionBounds.Left &&
                //         level.Player.CollisionBoundsNext.Right >= platform.CollisionBounds.Left)
                //     {
                //         level.Player.NextPositionX = platform.CollisionBounds.Left - level.Player.Texture.Width / 2;
                //         level.Player.SpeedX = 0;
                //     }
                //     // Right collision with platform
                //     else if (level.Player.SpeedX < 0 &&
                //              level.Player.CollisionBounds.Left >= platform.CollisionBounds.Right &&
                //              level.Player.CollisionBoundsNext.Left <= platform.CollisionBounds.Right)
                //     {
                //         level.Player.NextPositionX = platform.CollisionBounds.Right + level.Player.Texture.Width / 2;
                //         level.Player.SpeedX = 0;
                //     }
                // }

                // Update next position collision bounds after adjustment
                level.Player.CollisionBoundsNext = new Rectangle(
                    (int)level.Player.NextPositionX - (level.Player.Texture.Width / 2),
                    (int)level.Player.NextPositionY - (level.Player.Texture.Height / 2),
                    level.Player.Texture.Width,
                    level.Player.Texture.Height
                );
            }
        }
        foreach (var wall in level.Walls) // Wall collisions
        {
            if (wall.isSolid && level.Player.CollisionBoundsNext.Intersects(wall.CollisionBounds))
            {
                // Calculate movement direction
                bool movingRight = level.Player.SpeedX > 0;
                bool movingLeft = level.Player.SpeedX < 0;
                bool movingDown = level.Player.SpeedY > 0;
                bool movingUp = level.Player.SpeedY < 0;

                // For walls, prioritize horizontal collisions

                // Left collision (level.Player hitting wall from the left)
                if (movingRight && level.Player.CollisionBounds.Right <= wall.CollisionBounds.Left &&
                    level.Player.CollisionBoundsNext.Right >= wall.CollisionBounds.Left)
                {
                    level.Player.NextPositionX = wall.CollisionBounds.Left - level.Player.Texture.Width / 2;
                    level.Player.SpeedX = 0;
                }
                // Right collision (level.Player hitting wall from the right)
                else if (movingLeft && level.Player.CollisionBounds.Left >= wall.CollisionBounds.Right &&
                        level.Player.CollisionBoundsNext.Left <= wall.CollisionBounds.Right)
                {
                    level.Player.NextPositionX = wall.CollisionBounds.Right + level.Player.Texture.Width / 2;
                    level.Player.SpeedX = 0;
                }
                // Top collision (level.Player landing on top of wall)
                else if (movingDown && level.Player.CollisionBounds.Bottom <= wall.CollisionBounds.Top &&
                        level.Player.CollisionBoundsNext.Bottom >= wall.CollisionBounds.Top)
                {
                    level.Player.NextPositionY = wall.CollisionBounds.Top - level.Player.Texture.Height / 2;
                    level.Player.SpeedY = 0;
                }
                // Bottom collision (level.Player hitting wall from below)
                else if (movingUp && level.Player.CollisionBounds.Top >= wall.CollisionBounds.Bottom &&
                        level.Player.CollisionBoundsNext.Top <= wall.CollisionBounds.Bottom)
                {
                    level.Player.NextPositionY = wall.CollisionBounds.Bottom + level.Player.Texture.Height / 2;
                    level.Player.SpeedY = 0;
                }

                // Update next position collision bounds after adjustment
                level.Player.CollisionBoundsNext = new Rectangle(
                    (int)level.Player.NextPositionX - (level.Player.Texture.Width / 2),
                    (int)level.Player.NextPositionY - (level.Player.Texture.Height / 2),
                    level.Player.Texture.Width,
                    level.Player.Texture.Height
                );
            }
        }
        foreach (var wall in level.Walls) // For wall jumping
        {
            // Check if player is touching a wall but not necessarily colliding with it
            bool touchingLeftWall = Math.Abs(level.Player.CollisionBounds.Right - wall.CollisionBounds.Left) <= 2;
            bool touchingRightWall = Math.Abs(level.Player.CollisionBounds.Left - wall.CollisionBounds.Right) <= 2;

            // If player is airborne (not on ground) and touching a wall
            if ((touchingLeftWall || touchingRightWall) && Math.Abs(level.Player.SpeedY) > 0.01f)
            {
                level.Player.IsOnWall = true;
                break; // Found a wall contact, no need to check further
            }
        }

        // Motion
        level.Player.Position = new Vector2(level.Player.NextPositionX, level.Player.NextPositionY);

        // Update Camera (TODO: Add more logic to PositionY so it only follows after landing on a platform)
        _camera.Position = new Vector2(level.Player.NextPositionX, level.Player.NextPositionY); // Follow player

        base.Update(gameTime);
    }

    private void HandlePlayerInput()
    {
        // Handle jumping
        if (_inputManager.IsJump() && Math.Abs(level.Player.SpeedY) < 0.01f)
        {
            level.Player.SpeedY = -level.Player.JumpForce;
        }
        else if (_inputManager.IsWallJump())
        {
            // Apply wall jump force
            level.Player.SpeedY = -level.Player.WallJumpForce;
            if (level.Player.FacingRight) // Check direction and flip it
            {
                level.Player.SpeedX = -level.Player.WallJumpForce; // Push left with wall jump force
                level.Player.FacingRight = false;
            }
            else
            {
                level.Player.SpeedX = level.Player.WallJumpForce; // Push right with wall jump force
                level.Player.FacingRight = true;
            }
        }
    }

    /// <summary>
    /// Draws the game.
    ///
    /// This recursive method is called once per frame and is responsible for rendering the game objects to the screen.
    ///
    /// Vector2 objects position drawn elements with 2 coordinates, x and y.
    /// The x coordinate is horizontal and starts at 0 on the left side of the screen.
    /// The y coordinate is vertical and starts at 0 at the top of the screen.
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(levelBackgroundColor);

        // Get the view matrix from camera
        Matrix viewMatrix = _camera.GetViewMatrix(GraphicsDevice);

        // Begin sprite batch
        _spriteBatch.Begin(transformMatrix: viewMatrix);

        // Draw platforms
        foreach (var platform in level.Platforms)
        {
            _spriteBatch.Draw(
                platformTexture,
                new Vector2(platform.X, platform.Y),
                levelFilterColor
            );
        }

        // Draw walls
        foreach (var wall in level.Walls)
        {
            _spriteBatch.Draw(
                wallTexture,
                new Vector2(wall.X, wall.Y),
                null,
                levelFilterColor,
                0f,
                Vector2.Zero,
                1f,
                wall.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
                0f
            );
        }

        // Drawlevel.Player
        _spriteBatch.Draw(
            level.Player.Texture,
            level.Player.Position,
            null,
            levelFilterColor,
            0f,
            new Vector2(level.Player.Texture.Width / 2, level.Player.Texture.Height / 2),
            Vector2.One,
           level.Player.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, // Flip sprite if not facing right
            0f
        );

        // Draw debug info
        if (_debugMode)
        {
            // Draw player velocity information
            _spriteBatch.DrawString(
                debugFont,
                $"X: {level.Player.SpeedX}, Y: {level.Player.SpeedY} \n" +
                $"Wall Contact: {level.Player.IsOnWall}",
                new Vector2(level.Player.Position.X - 50, level.Player.Position.Y - 60),
                Color.Yellow
            );

            // Drawlevel.Player collision bounds
            DrawRectangle(level.Player.CollisionBounds, Color.Red, 4);
            DrawRectangle(level.Player.CollisionBoundsNext, Color.Yellow, 2);

            // Draw platform collision bounds
            foreach (var platform in level.Platforms)
            {
                DrawRectangle(platform.CollisionBounds, Color.Blue, 2);
            }

            // Draw wall collision bounds
            foreach (var wall in level.Walls)
            {
                DrawRectangle(wall.CollisionBounds, Color.Green, 2);
            }
        }

        // End sprite batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    // Draw collision boundaries for debugging
    private void DrawRectangle(Rectangle rectangle, Color color, int lineWidth = 1)
    {
        // Draw top line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
        // Draw bottom line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        // Draw left line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
        // Draw right line
        _spriteBatch.Draw(_debugTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
    }
}
