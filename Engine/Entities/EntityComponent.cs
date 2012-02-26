using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Entities
{
	public abstract class EntityComponent
	{
		#region Fields

		private Entity parent;

		#endregion


		#region Properties

		public Entity Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		#endregion


		#region Methods

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		public virtual void ReceiveMessage(object message)
		{
		}

		#endregion
	}
}
