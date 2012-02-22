using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;

namespace Engine.Screens
{
	public abstract class Screen
	{
		#region Fields

		private Engine parent;
		private List<Entity> entities;

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

		#endregion


		#region Methods

		public Screen()
		{
			entities = new List<Entity>();
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
			foreach (Entity entity in entities)
			{
				entity.Draw(gameTime, spriteBatch);
			}
		}

		#endregion
	}
}
