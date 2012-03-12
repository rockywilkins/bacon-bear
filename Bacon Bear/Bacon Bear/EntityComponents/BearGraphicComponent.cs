using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaconBear.Entities;
using BaconBear.Entities.Components;
using Engine.Entities;
using Engine.Graphics;

namespace BaconBear.Entities.Components.Graphics
{
	public class BearGraphicComponent : EntityComponent
	{
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			Texture2D texture = Engine.Engine.Content.Load<Texture2D>("Textures/Bear");

			spriteBatch.Draw(texture, new Rectangle((int)Parent.Position.X, (int)Parent.Position.Y, 68, 46), Color.White);

			spriteBatch.End();
		}
	}
}