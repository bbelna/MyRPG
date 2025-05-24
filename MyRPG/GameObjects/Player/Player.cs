using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyRPG.Input;

namespace MyRPG.GameObjects.Player {
  public class Player : GameObjectWithVelocity {
    private PlayerAnimation _animation { get; set; } = new PlayerAnimation();
    private bool _debugMode = false;
    private static Texture2D _rectangleTexture;

    public Player(Vector2 position = default) : base(position) {
      _animation.SetPosition(_position);
    }

    public override void LoadContent() {
      _animation.LoadContent();

      if (_debugMode && _rectangleTexture == null) {
        _rectangleTexture = new Texture2D(_graphicsDevice, 1, 1);
        _rectangleTexture.SetData(new[] { Color.White });
      }

      base.LoadContent();
    }

    public override void Update(GameTime gameTime) {
      var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      var walkSpeed = deltaSeconds * 64;

      _velocity = new Vector2(0, 0);
      if (_input.IsDown(GameInputType.Down)) {
        _animation.SetAnimation(PlayerAnimationType.WalkDown, true);
        _velocity.Y = walkSpeed;
      } else if (_input.IsDown(GameInputType.Up)) {
        _animation.SetAnimation(PlayerAnimationType.WalkUp, true);
        _velocity.Y = -1 * walkSpeed;
      } else if (_input.IsDown(GameInputType.Left)) {
        _animation.SetAnimation(PlayerAnimationType.WalkLeft, true);
        _velocity.X = -1 * walkSpeed;
      } else if (_input.IsDown(GameInputType.Right)) {
        _animation.SetAnimation(PlayerAnimationType.WalkRight, true);
        _velocity.X = walkSpeed;
      } else {
        _animation.Pause();
      }
      if (CanMove()) _position += _velocity;
      else _velocity = new Vector2(0, 0);

      _animation.SetPosition(_position);

      _animation.Update(gameTime);

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime) {
      _animation.Draw(gameTime);

      if (_debugMode && _rectangleTexture != null) {
        var bounds = GetCollisionRectangle();
        _spriteBatch.Draw(_rectangleTexture, bounds, Color.Red * 0.5f);
      }

      base.Draw(gameTime);
    }

    public PlayerAnimation GetAnimation() => _animation;

    public Rectangle GetCollisionRectangle() {
      var animationFrame = _animation.GetCurrentFrame();
      return new Rectangle(
        (int)_position.X,
        (int)_position.Y,
        animationFrame.Width,
        animationFrame.Height
      );
    }

    protected bool CanMove() {
      var gameMap = _gameObjectManager.GetFirstObjectWithType<GameMap.GameMap>();
      if (gameMap == null) return true; // fail-safe

      var animationFrame = _animation.GetCurrentFrame();
      var offsetGameMapWidth = gameMap.Width - animationFrame.Width;
      var offsetGameMapHeight = gameMap.Height - animationFrame.Height;

      // check if player would still be in bounds after moving
      var nextPosition = _position + _velocity;
      bool inBounds =
        nextPosition.X >= 0 &&
        nextPosition.X <= offsetGameMapWidth &&
        nextPosition.Y >= 0 &&
        nextPosition.Y <= offsetGameMapHeight;

      if (!inBounds) return false;

      // check projected bounding box for collision
      var nextBounds = new Rectangle(
        (int)nextPosition.X,
        (int)nextPosition.Y,
        animationFrame.Width,
        animationFrame.Height
      );

      var collisionRects = gameMap.GetCollisionRectangles();
      foreach (var collider in collisionRects) {
        if (nextBounds.Intersects(collider)) {
          return false;
        }
      }

      return true;
    }
  }
}
