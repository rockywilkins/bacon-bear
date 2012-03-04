using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;
using Engine.Graphics;

namespace Engine.Screens
{
	public abstract class Screen
	{
		#region Fields

		private Engine parent;
		private List<Entity> entities;
		private Camera camera;

		#endregion


		#region Properties

		public Engine Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		public List<Entity> Entities
		{
			get { return entities; }
		}

		public Camera Camera
		{
			get { return camera; }
			set { camera = value; }
		}

		#endregion


		#region Methods

		public Screen()
		{
			entities = new List<Entity>();

			// TODO: Get the screen dimensions
			camera = new Camera(800, 480);
		}

		public virtual void Update(GameTime gameTime)
		{
			foreach (Entity entity in entities)
			{
				entity.Update(gameTime);
			}
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			camera.Draw(entities, gameTime, spriteBatch);
		}

		#endregion
	}
}
