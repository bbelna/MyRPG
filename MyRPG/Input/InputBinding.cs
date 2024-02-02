using Microsoft.Xna.Framework.Input;

namespace MyRPG.Input {
  public class InputBinding {
    public InputDeviceType Type { get; set; }
    public Keys Key { get; set; }
    public Buttons Button { get; set; }
    public GameInput GameInput { get; set; }
  }
}
