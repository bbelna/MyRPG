using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyRPG.ISupports;
using System.Collections.Generic;
using System.Linq;

namespace MyRPG.Input {
  public class InputManager : ISupportUpdate {
    private IList<GameInput> _activeInputs;

    public InputManager() {
      _activeInputs = new List<GameInput>();
    }

    public void Update(GameTime gameTime) {
      _activeInputs.Clear();
      if (Keyboard.GetState().IsKeyDown(Keys.Down))
        _activeInputs.Add(GameInput.Down);
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        RpgGame.Instance.Exit();
    }

    public bool IsActive(GameInput gameInput) {
      return _activeInputs.Where(o => o == gameInput).Count() > 0;
    }

    public IEnumerable<GameInput> GetAllActivated() => _activeInputs.ToList();
  }
}
