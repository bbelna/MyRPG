using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyRPG.Xml;

namespace MyRPG.Graphics {
  public abstract class Drawable {
    protected RpgGame _game = RpgGame.Instance;
    protected SpriteBatch _spriteBatch = RpgGame.Instance.SpriteBatch;
    protected ContentManager _content = RpgGame.Instance.Content;
    protected XmlManager _xmlManager = RpgGame.Instance.XmlManager;
    protected GraphicsDevice _graphicsDevice = RpgGame.Instance.GraphicsDevice;
  }
}
