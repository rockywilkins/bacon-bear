﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaconBear.Entities;
using BaconBear.Entities.Components;
using Engine.Entities;
using Engine.Graphics;

namespace BaconBear.Entities.Components
{
	public class EnemyGraphicComponent : EntityComponent
	{
		private Texture2D texture;

		public override void Load()
		{
			texture = Engine.Engine.Content.Load<Texture2D>("Textures/Enemy");

			base.Load();
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.Matrix);

			spriteBatch.Draw(texture, new Rectangle((int)Parent.Position.X, (int)Parent.Position.Y, 28, 51), Color.White);

			spriteBatch.End();
		}

		public override void ReceiveMessage(string name, object value)
		{

			base.ReceiveMessage(name, value);
		}
	}
}
