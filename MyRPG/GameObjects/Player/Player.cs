using Microsoft.Xna.Framework;
using MyRPG.Input;

namespace MyRPG.GameObjects.Player {
  public class Player : GameObject {
    private PlayerAnimation _animation = new PlayerAnimation();

    public override void Draw(GameTime gameTime) {
      _animation.Draw(gameTime);
      base.Draw(gameTime);
    }

    public override void Update(GameTime gameTime) {
      var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      var walkSpeed = deltaSeconds * 64;

      if (_input.IsDown(GameInput.Down)) {
        _animation.SetAnimation(PlayerAnimations.WalkDown, true);
        Position.Y += walkSpeed;
      } else if (_input.IsDown(GameInput.Up)) {
        _animation.SetAnimation(PlayerAnimations.WalkUp, true);
        Position.Y -= walkSpeed;
      } else if (_input.IsDown(GameInput.Left)) {
        _animation.SetAnimation(PlayerAnimations.WalkLeft, true);
        Position.X -= walkSpeed;
      } else if (_input.IsDown(GameInput.Right)) {
        _animation.SetAnimation(PlayerAnimations.WalkRight, true);
        Position.X += walkSpeed;
      } else {
        _animation.Pause();
      }

      _animation.SetPosition(Position);
      _animation.Update(gameTime);

      base.Update(gameTime);
    }

    public override void LoadContent() {
      _animation.LoadContent();
      _animation.SetPosition(new Vector2(100, 100));

      base.LoadContent();
    }
  }
}
