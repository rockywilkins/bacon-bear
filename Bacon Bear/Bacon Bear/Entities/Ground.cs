using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;
using Engine.Graphics;
using Engine.Scene;

namespace BaconBear.Entities
{
	public class Ground : Entity
	{
		private Texture2D texture;

		public Ground(Scene parent) : base(parent)
		{
		}

		public override void Load()
		{
			texture = Engine.Engine.Content.Load<Texture2D>("Textures/Ground");
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			int total = 16;

			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			for (int i = 0; i < total; i++)
			{
				spriteBatch.Draw(texture, new Rectangle(100 * i, 475, 100, 200), Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime, spriteBatch, camera);
		}

	}
}
