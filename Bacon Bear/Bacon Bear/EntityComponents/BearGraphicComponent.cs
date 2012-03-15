using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaconBear.Entities;
using BaconBear.Entities.Components;
using Engine.Entities;
using Engine.Graphics;

namespace BaconBear.Entities.Components
{
	public class BearGraphicComponent : EntityComponent
	{
		private Texture2D texture;
		private bool flipped = false;

		public override void Load()
		{
			texture = Engine.Engine.Content.Load<Texture2D>("Textures/Bear");

			base.Load();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			Parent.Parent.Cameras[0].Position = Parent.Position;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			SpriteEffects effect = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			spriteBatch.Draw(texture, new Rectangle((int)Parent.Position.X, (int)Parent.Position.Y, 68, 46), null, Color.White, 0, Vector2.Zero, effect, 0);

			spriteBatch.End();
		}

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "direction")
			{
				string direction = value as string;
				if (direction == "right")
				{
					flipped = false;
				}
				else
				{
					flipped = true;
				}
			}

			base.ReceiveMessage(name, value);
		}
	}
}
