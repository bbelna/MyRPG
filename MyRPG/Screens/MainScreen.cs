using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MyRPG.GameObjects;
using MyRPG.GameObjects.Player;
using MyRPG.GameObjects.GameMap;
using MonoGame.Extended;

namespace MyRPG.Screens {
  public class MainScreen : GameScreen {
    private new RpgGame Game => (RpgGame)base.Game;
    private GameObjectManager _gameObjectManager { get; set; }
    private Player _player { get; set; }
    private GameMap _gameMap { get; set; }
    private OrthographicCamera _camera { get; set; }

    public MainScreen(
      GameObjectManager gameObjectManager
    ) : base(RpgGame.Instance) {
      _gameObjectManager = gameObjectManager;
    }

    public override void LoadContent() {
      _camera = Game.Camera;
      _player = new Player();
      _gameMap = new GameMap(Content.RootDirectory + "\\Maps\\SampleLand2.tmx");

      LoadGameObjects();

      base.LoadContent();
    }

    public override void Update(GameTime gameTime) {
      _gameObjectManager.Update(gameTime);

      UpdateCamera();
    }

    public override void Draw(GameTime gameTime) {
      _gameObjectManager.Draw(gameTime);
    }

    protected void LoadGameObjects() {
      _gameObjectManager.Add(_gameMap);
      _gameObjectManager.Add(_player);
    }

    protected void UpdateCamera() {
      var boundingRectangle = _camera.BoundingRectangle;
      var moveCameraDirection = new Vector2(0, 0);
      var velocity = _player.GetVelocity();
      var viewportAdapter = Game.ViewportAdapter;

      var moveRight = (boundingRectangle.TopRight.X < _gameMap.Width && velocity.X > 0)
        && _player.GetPosition().X >= boundingRectangle.TopRight.X - viewportAdapter.VirtualWidth / 2 - _player.GetAnimation().GetCurrentFrame().Width / 2;
      var moveLeft = (boundingRectangle.TopLeft.X > 0 && velocity.X < 0)
        && _player.GetPosition().X <= boundingRectangle.TopRight.X - viewportAdapter.VirtualWidth / 2 - _player.GetAnimation().GetCurrentFrame().Width / 2;
      var moveUp = (boundingRectangle.TopRight.Y > 0 && velocity.Y < 0)
        && _player.GetPosition().Y <= boundingRectangle.TopRight.Y + viewportAdapter.VirtualHeight / 2 - _player.GetAnimation().GetCurrentFrame().Height;
      var moveDown = (boundingRectangle.BottomRight.Y < _gameMap.Height && velocity.Y > 0)
        && _player.GetPosition().Y >= boundingRectangle.TopRight.Y + viewportAdapter.VirtualHeight / 2 - _player.GetAnimation().GetCurrentFrame().Height;

      if (moveRight || moveLeft || moveUp || moveDown) moveCameraDirection = velocity;

      _camera.Move(moveCameraDirection);
    }
  }
}
