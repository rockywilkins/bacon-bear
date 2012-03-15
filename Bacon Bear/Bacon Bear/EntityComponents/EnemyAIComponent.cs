using System;
using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class EnemyAIComponent : EntityComponent
	{
		private double updateDuration = 0;

		public override void ReceiveMessage(string name, object value)
		{
		}

		public override void Update(GameTime gameTime)
		{
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
	}
}
