using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Graphics;
using Engine.Input;
using Engine.Physics;
using Engine.Scene;
using Engine.Screens;
using BaconBear.Entities;

namespace BaconBear.Screens
{
	public class Level : Screen
	{
		private Scene scene;
		private TouchInputHandler touchHandler;
		private World physicsWorld;

		public Level() : base()
		{
			Camera camera = new Camera(800, 480);
			camera.Position = new Vector2(400f, 240f);

			scene = new Scene();
			scene.Cameras.Add(camera);
			Bear baconBear = new Bear(scene);
			scene.Items.Add(baconBear);

			touchHandler = new TouchInputHandler(scene.Cameras[0]);
			physicsWorld = new World(Vector2.Zero);

			// Get the dimensions of the world boundary
			float width = ConvertUnits.ToSimUnits(800);
			float height = ConvertUnits.ToSimUnits(480);

			// Create a vertice for each corner of the boundary
			Vertices bounds = new Vertices(4);
			bounds.Add(new Vector2(0, 0));
			bounds.Add(new Vector2(width, 0));
			bounds.Add(new Vector2(width, height));
			bounds.Add(new Vector2(0, height));

			// Create the boundary shape and make it collide with everything
			Body boundary = BodyFactory.CreateLoopShape(physicsWorld, bounds);
			boundary.CollisionCategories = Category.All;
			boundary.CollidesWith = Category.All;
			
			baconBear.SendMessage("touch_input", touchHandler);
			baconBear.SendMessage("physics_world", physicsWorld);
		}

		public override void Update(GameTime gameTime)
		{
			foreach (SceneItem item in scene.Items)
			{
				item.Update(gameTime);
			}

			physicsWorld.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));

			scene.Cameras[0].Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach (SceneItem item in scene.Items)
			{
				item.Draw(gameTime, spriteBatch, scene.Cameras[0]);
			}

			scene.Cameras[0].Draw(scene.Items, gameTime, spriteBatch);
		}
	}
}
