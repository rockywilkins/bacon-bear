using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Graphics;

namespace Engine.Scene
{
	public abstract class SceneItem
	{
		#region Fields

		private Scene parent;

		#endregion


		#region Properties

		protected Scene Parent
		{
			get { return parent; }
		}

		#endregion


		#region Methods

		public SceneItem(Scene parent)
		{
			this.parent = parent;
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
		}

		#endregion
	}
}
