using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Screens;

namespace BaconBear.Screens
{
	public class Level : Screen
	{
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			Texture2D texture = Parent.Game.Content.Load<Texture2D>("Textures/Bear");

			spriteBatch.Draw(texture, new Rectangle(50, 50, 68, 46), Color.White);

			spriteBatch.End();

			base.Draw(gameTime, spriteBatch);
		}
	}
}
