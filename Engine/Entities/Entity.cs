using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Entities
{
	public abstract class Entity
	{
		#region Fields

		private Vector2 position;
		private Vector2 velocity;

		#endregion


		#region Properties

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		#endregion


		#region Methods

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		#endregion
	}
}
