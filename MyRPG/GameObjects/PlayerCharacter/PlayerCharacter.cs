using Microsoft.Xna.Framework;
using MyRPG.Input;

namespace MyRPG.GameObjects.PlayerCharacter {
  public class PlayerCharacter : GameObject {
    private Vector2 _position;
    private PlayerAnimation _animation = new PlayerAnimation();

    public override void Draw(GameTime gameTime) {
      _animation.Draw(gameTime);
      base.Draw(gameTime);
    }

    public override void Update(GameTime gameTime) {
      _animation.Update(gameTime);

      var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
      var walkSpeed = deltaSeconds * 64;

      if (_input.IsDown(GameInput.Down)) {
        _animation.SetAnimation(PlayerAnimations.WalkDown, true);
        _position.Y += walkSpeed;
      } else if (_input.IsDown(GameInput.Up)) {
        _animation.SetAnimation(PlayerAnimations.WalkUp, true);
        _position.Y -= walkSpeed;
      } else if (_input.IsDown(GameInput.Left)) {
        _animation.SetAnimation(PlayerAnimations.WalkLeft, true);
        _position.X -= walkSpeed;
      } else if (_input.IsDown(GameInput.Right)) {
        _animation.SetAnimation(PlayerAnimations.WalkRight, true);
        _position.X += walkSpeed;
      } else {
        _animation.Pause();
      }

      _animation.SetPosition(_position);

      base.Update(gameTime);
    }

    public override void LoadContent() {
      _animation.LoadContent();
      _animation.SetPosition(new Vector2(100, 100));

      base.LoadContent();
    }
  }
}
