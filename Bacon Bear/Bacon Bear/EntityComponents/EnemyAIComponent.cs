using System;
using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;
using Engine.Debug;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class EnemyAIComponent : EntityComponent
	{
		private double updateDuration = 0;

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "damage")
			{
				TakeDamage((float)value);
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (((Enemy)Parent).Alive == false)
				return;

			updateDuration += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (updateDuration > 500f)
			{
				foreach (SceneItem item in Parent.Parent.Items)
				{
					if (item.GetType().Name == "Bear")
					{
						Bear bear = item as Bear;
						if (Math.Abs(bear.Position.X - Parent.Position.X) < 250)
						{
							string direction = bear.Position.X < Parent.Position.X ? "left" : "right";
							Parent.SendMessage("flee", direction);
						}
					}
				}

				updateDuration = 0;
			}
		}

		private void TakeDamage(float damageAmount)
		{
			Enemy parent = Parent as Enemy;
			parent.Health -= damageAmount;

			if (parent.Health <= 0)
			{
				parent.Alive = false;
				parent.SendMessage("death", true);
				Engine.Debug.Console.Write("dead");
			}
		}
	}
}
