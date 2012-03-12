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

		#endregion


		#region Properties

		public Engine Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		#endregion


		#region Methods

		public Screen()
		{
		}

		public virtual void Load()
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		#endregion
	}
}
