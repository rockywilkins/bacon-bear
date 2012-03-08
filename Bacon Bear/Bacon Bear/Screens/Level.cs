using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Graphics;
using Engine.Scene;
using Engine.Screens;
using Engine.Input;
using BaconBear.Entities;

namespace BaconBear.Screens
{
	public class Level : Screen
	{
		private Scene scene;
		private TouchInputHandler touchHandler;

		public Level() : base()
		{
			scene = new Scene();
			scene.Cameras.Add(new Camera(800, 480));
			Bear baconBear = new Bear(scene);
			scene.Items.Add(baconBear);
			touchHandler = new TouchInputHandler(scene.Cameras[0]);

			baconBear.SendMessage(touchHandler);
		}

		public override void Update(GameTime gameTime)
		{
			scene.Cameras[0].Rotation += 0.01f;

			foreach (SceneItem item in scene.Items)
			{
				item.Update(gameTime);
			}

			scene.Cameras[0].Update(gameTime);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach (SceneItem item in scene.Items)
			{
				item.Draw(gameTime, spriteBatch, scene.Cameras[0]);
			}

			scene.Cameras[0].Draw(scene.Items, gameTime, spriteBatch);

			base.Draw(gameTime, spriteBatch);
		}
	}
}
