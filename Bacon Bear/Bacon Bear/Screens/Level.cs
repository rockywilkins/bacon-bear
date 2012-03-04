using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Screens;
using BaconBear.Entities;

namespace BaconBear.Screens
{
	public class Level : Screen
	{
		public Level() : base()
		{
			Entities.Add(new Bear());
		}

		public override void Update(GameTime gameTime)
		{
			Camera.Rotation += 0.01f;

			base.Update(gameTime);
		}
	}
}
