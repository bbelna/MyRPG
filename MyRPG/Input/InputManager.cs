using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MyRPG.Input {
  public class InputManager {
    private IList<GameInputType> _downInputs;
    private IList<GameInputType> _upInputs;
    private GameInputType[] _allInputs = new GameInputType[4] {
      GameInputType.Down,
      GameInputType.Up,
      GameInputType.Left,
      GameInputType.Right
    };

    public InputManager() {
      _downInputs = new List<GameInputType>();
      _upInputs = new List<GameInputType>();
    }

    public void Update(GameTime gameTime) {
      _downInputs.Clear();
      _upInputs.Clear();
      CheckAllBindings();
    }

    protected void CheckAllBindings() {
      foreach (GameInputType input in  _allInputs) {
        CheckBinding(GetBindingForGameInput(input));
      }
    }

    public InputBinding GetBindingForGameInput(GameInputType gameInput) {
      var gameSettings = RpgGame.Instance.InputBindings;
      if (gameSettings != null) {
        var inputBindings = RpgGame.Instance.InputBindings.Bindings;
        var binding = inputBindings.FirstOrDefault(b => b.GameInput == gameInput);
        return binding;
      }
      return null;
    }

    public bool CheckBinding(InputBinding binding) {
      if (binding == null) return false;
      if (binding.Type == InputDeviceType.Keyboard) {
        if (Keyboard.GetState().IsKeyDown(binding.Key)) {
          _downInputs.Add(binding.GameInput);
        }
        if (Keyboard.GetState().IsKeyUp(binding.Key)) {
          _upInputs.Add(binding.GameInput);
        }
      }
      // TODO gamepad support
      return false;
    }

    public bool IsDown(GameInputType gameInput) {
      return _downInputs.Where(o => o == gameInput).Count() > 0;
    }

    public bool IsUp(GameInputType gameInput) {
      return _upInputs.Where(o => o == gameInput).Count() > 0;
    }

    public IEnumerable<GameInputType> GetAllDown() => _downInputs.ToList();

    public IEnumerable<GameInputType> GetAllUp() => _upInputs.ToList();
  }
}
