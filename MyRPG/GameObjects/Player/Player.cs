using Microsoft.Xna.Framework;
using MyRPG.Input;

namespace MyRPG.GameObjects.Player {
  public class Player : GameObjectWithVelocity {
    private PlayerAnimation _animation { get; set; } = new PlayerAnimation();

    public override void Draw(GameTime gameTime) {
      _animation.Draw(gameTime);
      base.Draw(gameTime);
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

      _animation.SetPosition(_position);

      _animation.Update(gameTime);

      base.Update(gameTime);
    }

    public override void LoadContent() {
      _animation.LoadContent();
      base.LoadContent();
    }

    public PlayerAnimation GetAnimation() => _animation;

    protected bool CanMove() {
      var gameMap = _gameObjectManager.GetFirstObjectWithType<GameMap.GameMap>();

      var animationFrame = _animation.GetCurrentFrame();
      var offsetGameMapWidth = gameMap.Width - animationFrame.Width;
      var offsetGameMapHeight = gameMap.Height - animationFrame.Height;

      var moveRight = _position.X < offsetGameMapWidth && _velocity.X > 0;
      var moveLeft = _position.X > 0 && _velocity.X < 0;
      var moveUp = _position.Y > 0 && _velocity.Y < 0;
      var moveDown = _position.Y < offsetGameMapHeight && _velocity.Y > 0;

      return moveRight || moveLeft || moveUp || moveDown;
    }
  }
}
