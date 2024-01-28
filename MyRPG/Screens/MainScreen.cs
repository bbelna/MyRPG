using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MyRPG.GameObjects;

namespace MyRPG.Screens
{
    public class MainScreen : GameScreen {
    private new RpgGame Game => (RpgGame)base.Game;

    private GameObjectManager _gameObjectManager { get; set; }

    public MainScreen(): base(RpgGame.Instance) {
      _gameObjectManager = new GameObjectManager();
    }

    public override void LoadContent() {
      LoadGameObjects();

      base.LoadContent();
    }

    public override void Update(GameTime gameTime) {
      _gameObjectManager.Update(gameTime);
    }

    public override void Draw(GameTime gameTime) {
      _gameObjectManager.Draw(gameTime);
    }

    protected void LoadGameObjects() {
      _gameObjectManager.Add(new PlayerCharacter());
    }
  }
}
