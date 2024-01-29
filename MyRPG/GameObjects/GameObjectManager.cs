﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRPG.GameObjects {
  public class GameObjectManager {
    protected IEnumerable<GameObject> _activeGameObjects;

    public GameObjectManager() {
      _activeGameObjects = new List<GameObject>();
    }

    public void Update(GameTime gameTime) {
      foreach (var gameObject in _activeGameObjects) {
        gameObject.Update(gameTime);
      }
    }

    public void Draw(GameTime gameTime) {
      foreach (var gameObject in _activeGameObjects) {
        gameObject.Draw(gameTime);
      }
    }

    public void Add(GameObject gameObject) {
      if (_activeGameObjects.Where(o => o.Id == gameObject.Id).Count() > 0) {
        throw new Exception("New GameObject Id collides with existing GameObject");
      }
      if (!gameObject.Initialized) {
        gameObject.LoadContent();
      }
      _activeGameObjects = _activeGameObjects.Append(gameObject);
    }

    public IEnumerable<T> GetObjectsWithType<T>() where T : GameObject {
      return _activeGameObjects.OfType<T>().ToList();
    }

    public T GetObjectWithId<T>(string id) where T : GameObject {
      return _activeGameObjects.OfType<T>().Where(o => o.Id == id).FirstOrDefault();
    }
  }
}
