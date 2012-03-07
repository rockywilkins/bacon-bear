using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Graphics
{
	public abstract class Sprite
	{
		#region Fields

		private Texture2D texture;
		private Rectangle rectangle;
		private Color color;
		private SpriteEffects effect = SpriteEffects.None;
		private bool visible;

		#endregion


		#region Properties

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}

		public Rectangle Rectangle
		{
			get { return rectangle; }
			set { rectangle = value; }
		}

		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		public SpriteEffects Effect
		{
			get { return effect; }
			set { effect = value; }
		}

		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}

		#endregion


		#region Methods

		public Sprite(Texture2D texture)
		{
			this.texture = texture;
		}

		public Sprite(Texture2D texture, Rectangle rectangle)
		{
			this.texture = texture;
			this.rectangle = rectangle;
		}

		public Sprite(Texture2D texture, Rectangle rectangle, Color color)
		{
			this.texture = texture;
			this.rectangle = rectangle;
			this.color = color;
		}

		public Sprite(Texture2D texture, Rectangle rectangle, Color color, SpriteEffects effect)
		{
			this.texture = texture;
			this.rectangle = rectangle;
			this.color = color;
			this.effect = effect;
		}

		public Sprite(Texture2D texture, Rectangle rectangle, Color color, SpriteEffects effect, bool visible)
		{
			this.texture = texture;
			this.rectangle = rectangle;
			this.color = color;
			this.effect = effect;
			this.visible = visible;
		}

		#endregion
	}
}
