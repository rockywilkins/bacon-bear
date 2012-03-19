using System;
using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class BearAIComponent : EntityComponent
	{
		private Enemy target;

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "target")
			{
				target = value as Enemy;
			}
			else if (name == "remove_target")
			{
				target = null;
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (target != null)
			{
				Vector2 direction = Vector2.Subtract(Parent.Position, target.Position);
				direction = Vector2.Negate(Vector2.Normalize(direction));

				Parent.SendMessage("move", direction * 10);
			}
		}
	}
}
