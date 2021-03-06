using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;
using Engine.Graphics;

namespace BaconBear.Entities.Components
{
	public class BaconGraphicComponent : EntityComponent
	{
		private Texture2D texture;

		public override void Load()
		{
			texture = Engine.Engine.Content.Load<Texture2D>("Textures/Bacon");

			base.Load();
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			spriteBatch.Draw(texture,
							new Rectangle((int)Parent.Position.X, (int)Parent.Position.Y, 10, 25),
							null,
							Color.White,
							Parent.Rotation,
							Vector2.Zero,
							SpriteEffects.None,
							0);

			spriteBatch.End();
		}
	}
}
