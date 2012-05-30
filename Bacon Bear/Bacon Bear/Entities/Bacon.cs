using Microsoft.Xna.Framework;
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
		}

		public override void Load()
		{
			AddComponent(new BaconPhysicsComponent());
			AddComponent(new BaconGraphicComponent());

			base.Load();
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
