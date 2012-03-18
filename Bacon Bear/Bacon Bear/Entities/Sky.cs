using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Graphics;
using Engine.Physics;
using Engine.Scene;

namespace BaconBear.Entities
{
	public class Sky : Entity
	{
		private Texture2D texture;

		public Sky(Scene parent) : base(parent)
		{
		}

		public override void Load()
		{
			texture = Engine.Engine.Content.Load<Texture2D>("Textures/Sky");
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			int total = 16;

			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			for (int i = 0; i < total; i++)
			{
				spriteBatch.Draw(texture, new Rectangle(100 * i, 0, 100, 500), Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime, spriteBatch, camera);
		}
	}
}
