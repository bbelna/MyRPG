using Microsoft.Xna.Framework;

namespace MyRPG.GameObjects {
  public abstract class GameObjectWithVelocity : GameObject {
    protected Vector2 _velocity = new Vector2(0, 0);

    public Vector2 GetVelocity() => new Vector2(_velocity.X, _velocity.Y);

    public void SetVelocity(Vector2 velocity) => _velocity = velocity;
  }
}
