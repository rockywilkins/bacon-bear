using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Bacon : Entity
	{
		private double maxLifetime = 5000f;
		private double currentLifetime = 0;

		public Bacon(Scene parent) : base(parent)
		{
			AddComponent(new BaconPhysicsComponent());
			AddComponent(new BaconGraphicComponent());
		}

		public override void Update(GameTime gameTime)
		{
			if (currentLifetime > maxLifetime)
			{
				Unload();
				Parent.Items.Remove(this);
			}

			currentLifetime += gameTime.ElapsedGameTime.TotalMilliseconds;

			base.Update(gameTime);
		}
	}
}
