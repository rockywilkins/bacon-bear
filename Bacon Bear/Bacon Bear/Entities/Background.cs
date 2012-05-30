using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Graphics;
using Engine.Physics;
using Engine.Scene;

using Engine.Debug;

namespace BaconBear.Entities
{
	public class Background : Entity
	{
		private Bear bear;
		private Texture2D texture;
		private Vector2 positiona;
		private Vector2 positionb;
		private Vector2 lastPosition;
		private float speed = 0f;

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}

		public Background(Scene parent) : base(parent)
		{

		}

		public override void Load()
		{
			foreach (SceneItem item in Parent.Items)
			{
				if (item is Bear)
				{
					bear = item as Bear;
				}
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			speed = 0;

			if (lastPosition.X < bear.Position.X)
			{
				//Console.Write("Bear Moved", "Background Info - Moved: " + (lastpos.X - bear.Position.X).ToString());
				speed = (lastPosition.X - bear.Position.X) / 10;
				lastPosition = bear.Position;
			}
			else if (lastPosition.X > bear.Position.X)
			{
				//Console.Write("Bear Moved", "Background Info - Moved: " + (lastpos.X - bear.Position.X).ToString());
				speed = (lastPosition.X - bear.Position.X) / 10;
				lastPosition = bear.Position;
			}

			if (speed > 0)
			{
				positiona += new Vector2(speed, 0f);
				positionb = positiona.X > 0 ? new Vector2(positiona.X - Texture.Width, 0) : new Vector2(positiona.X + Texture.Width, 0);

				if (positiona.X >= Texture.Width)
				{
					positiona = Vector2.Zero;
				}

				Console.Write("background", "Background Info - Moved: <<<<: " + speed.ToString());
			}
			else if (speed < 0)
			{
				positiona += new Vector2(speed, 0f);
				positionb = positiona.X > 0 ? new Vector2(positiona.X - Texture.Width, 0) : new Vector2(positiona.X + Texture.Width, 0);

				if (positiona.X <= -Texture.Width)
				{
					positiona = Vector2.Zero;
				}

				Console.Write("background", "Background Info - Moved: >>>>>: " + speed.ToString());
			}

			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null);

			spriteBatch.Draw(Texture, positiona, Color.White);
			spriteBatch.Draw(Texture, positionb, Color.White);

			spriteBatch.End();

			base.Draw(gameTime, spriteBatch, camera);
		}
	}
}
