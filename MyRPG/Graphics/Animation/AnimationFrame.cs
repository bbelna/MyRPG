using Microsoft.Xna.Framework;

namespace MyRPG.Graphics.Animation {
  public class AnimationFrame {
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle ToRectangle() {
      return new Rectangle(X, Y, Width, Height);
    }
  }
}
