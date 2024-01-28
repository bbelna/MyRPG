using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyRPG.Graphics {
  public abstract class Drawable {
    protected RpgGame _game = RpgGame.Instance;
    protected SpriteBatch _spriteBatch = RpgGame.Instance.SpriteBatch;
    protected ContentManager _content = RpgGame.Instance.Content;
  }
}
