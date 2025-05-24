using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiledCS;

namespace MyRPG.GameObjects.GameMap {
  public class GameMap : GameObject {
    public int Width { get => _map.Width * _map.TileWidth; }
    public int Height { get => _map.Height * _map.TileHeight; }

    protected TiledMap _map { get; set; }
    protected Texture2D _tileset { get; set; }
    protected Dictionary<int, TiledTileset> _tilesets { get; set; }
    protected IList<TiledLayer> _tileLayers { get; set; }
    protected string _mapPath { get; set; }
    protected Dictionary<string, Texture2D> _tilesetTextures { get; set; } = new Dictionary<string, Texture2D>();
    protected List<Rectangle> _collisionRects { get; private set; } = new();

    public GameMap(
      string mapPath
    ) {
      _mapPath = mapPath;
    }

    public override void LoadContent() {
      try {
        _map = new TiledMap(_mapPath);
        _tilesets = _map.GetTiledTilesets("Content\\Maps\\");
        _tileLayers = _map.Layers.Where(x => x.type == TiledLayerType.TileLayer).ToList();

        LoadTextures();
        LoadCollisions();
      } catch (Exception e) {
        _game.HaltWithException(e);
      }
    }

    public List<Rectangle> GetCollisionRectangles() {
      return _collisionRects;
    }

    private void LoadTextures() {
      var textureDirectory = "Content\\Maps\\Tilesets\\";
      var textureFiles = Directory.EnumerateFiles(textureDirectory, "*.png", SearchOption.AllDirectories);
      foreach (var textureFile in textureFiles) {
        Texture2D texture;
        using (FileStream fileStream = new FileStream(textureFile, FileMode.Open)) {
          texture = Texture2D.FromStream(_graphicsDevice, fileStream);
        }
        if (texture != null) _tilesetTextures.Add(textureFile, texture);
      }
    }

    private void LoadCollisions() {
      _collisionRects.Clear();
      foreach (var kv in _tilesets) {
        var firstGid = kv.Key;
        var tileset = kv.Value;

        if (tileset.Tiles == null) continue;

        foreach (var tile in tileset.Tiles) {
          int globalId = firstGid + tile.id;
          if (tile.objects == null) continue;

          // Find all instances of this tile in the map
          foreach (var layer in _tileLayers) {
            for (int y = 0; y < layer.height; y++) {
              for (int x = 0; x < layer.width; x++) {
                int index = y * layer.width + x;
                if (layer.data[index] != globalId) continue;

                int tileX = x * _map.TileWidth;
                int tileY = y * _map.TileHeight;

                foreach (var obj in tile.objects) {
                  var worldRect = new Rectangle(
                    tileX + (int)obj.x,
                    tileY + (int)obj.y,
                    (int)obj.width,
                    (int)obj.height
                  );
                  _collisionRects.Add(worldRect);
                }
              }
            }
          }
        }
      }
    }

    public override void Draw(GameTime gameTime) {
      foreach (var layer in _tileLayers) {
        for (var y = 0; y < layer.height; y++) {
          for (var x = 0; x < layer.width; x++) {
            var index = (y * layer.width) + x;
            var gid = layer.data[index];
            var tileX = x * _map.TileWidth;
            var tileY = y * _map.TileHeight;

            if (gid == 0) continue;

            var mapTileset = _map.GetTiledMapTileset(gid);
            var tileset = _tilesets[mapTileset.firstgid];
            var rect = _map.GetSourceRect(mapTileset, tileset, gid);
            var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
            var destination = new Rectangle(tileX, tileY, _map.TileWidth, _map.TileHeight);

            TileTransformation tileTrans = TileTransformation.None;
            if (_map.IsTileFlippedHorizontal(layer, x, y)) tileTrans |= TileTransformation.Flip_H;
            if (_map.IsTileFlippedVertical(layer, x, y)) tileTrans |= TileTransformation.Flip_V;
            if (_map.IsTileFlippedDiagonal(layer, x, y)) tileTrans |= TileTransformation.Flip_D;

            SpriteEffects effects = SpriteEffects.None;
            double rotation = 0f;
            switch (tileTrans) {
              case TileTransformation.Flip_H: effects = SpriteEffects.FlipHorizontally; break;
              case TileTransformation.Flip_V: effects = SpriteEffects.FlipVertically; break;

              case TileTransformation.Rotate_90:
                rotation = Math.PI * .5f;
                destination.X += _map.TileWidth;
                break;

              case TileTransformation.Rotate_180:
                rotation = Math.PI;
                destination.X += _map.TileWidth;
                destination.Y += _map.TileHeight;
                break;

              case TileTransformation.Rotate_270:
                rotation = Math.PI * 3 / 2;
                destination.Y += _map.TileHeight;
                break;

              case TileTransformation.Rotate_90AndFlip_H:
                effects = SpriteEffects.FlipHorizontally;
                rotation = Math.PI * .5f;
                destination.X += _map.TileWidth;
                break;

              default:
                break;
            }

            var tilesetTexture = _tilesetTextures.FirstOrDefault(t => t.Key == "Content\\Maps\\Tilesets\\" + tileset.Image.source).Value;
            if (tilesetTexture != null) {
              _spriteBatch.Draw(tilesetTexture, destination, source, Color.White, (float)rotation, Vector2.Zero, effects, 0);
            }
          }
        }
      }
    }

    public override void Update(GameTime gameTime) {

    }
  }
}