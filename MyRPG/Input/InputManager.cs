using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MyRPG.Input {
  public class InputManager {
    private IList<GameInput> _downInputs;
    private IList<GameInput> _upInputs;
    private GameInput[] _allInputs = new GameInput[4] {
      GameInput.Down,
      GameInput.Up,
      GameInput.Left,
      GameInput.Right
    };

    public InputManager() {
      _downInputs = new List<GameInput>();
      _upInputs = new List<GameInput>();
    }

    public void Update(GameTime gameTime) {
      _downInputs.Clear();
      _upInputs.Clear();
      CheckAllBindings();
    }

    protected void CheckAllBindings() {
      foreach (GameInput input in  _allInputs) {
        CheckBinding(GetBindingForGameInput(input));
      }
    }

    public InputBinding GetBindingForGameInput(GameInput gameInput) {
      var gameSettings = RpgGame.Instance.GameSettings;
      if (gameSettings != null) {
        var inputBindings = RpgGame.Instance.GameSettings.InputBindings;
        var binding = inputBindings.FirstOrDefault(b => b.GameInput == gameInput);
        return binding;
      }
      return null;
    }

    public bool CheckBinding(InputBinding binding) {
      if (binding == null) return false;
      if (binding.Type == InputDeviceType.Keyboard) {
        if (Keyboard.GetState().IsKeyDown((Keys)binding.Mapping)) {
          _downInputs.Add(binding.GameInput);
        }
        if (Keyboard.GetState().IsKeyUp((Keys)binding.Mapping)) {
          _upInputs.Add(binding.GameInput);
        }
      }
      // TODO gamepad support
      return false;
    }

    public bool IsDown(GameInput gameInput) {
      return _downInputs.Where(o => o == gameInput).Count() > 0;
    }

    public bool IsUp(GameInput gameInput) {
      return _upInputs.Where(o => o == gameInput).Count() > 0;
    }

    public IEnumerable<GameInput> GetAllDown() => _downInputs.ToList();

    public IEnumerable<GameInput> GetAllUp() => _upInputs.ToList();
  }
}
