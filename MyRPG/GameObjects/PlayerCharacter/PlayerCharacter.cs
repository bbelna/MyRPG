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

      if (_input.IsActive(GameInput.Down)) {
      } else if (_input.IsActive(GameInput.Up)) {
      } else if (_input.IsActive(GameInput.Left)) {
      } else if (_input.IsActive(GameInput.Right)) {
      }

      base.Update(gameTime);
    }

    public override void LoadContent() {
      _animation.LoadContent();
      _animation.SetPosition(new Vector2(100, 100));

      base.LoadContent();
    }
  }
}
