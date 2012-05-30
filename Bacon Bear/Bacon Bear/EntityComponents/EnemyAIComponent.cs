using System;
using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;

namespace BaconBear.Entities.Components
{
	public class EnemyAIComponent : EntityComponent
	{
		private double updateDuration = 0;

		public override void Load()
		{
			((IAlive)Parent).Damaged += TakeDamage;
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
					if (item is Bear)
					{
						Bear bear = item as Bear;
						if (Math.Abs(bear.Position.X - Parent.Position.X) < 250)
						{
							MoveDirection direction = bear.Position.X < Parent.Position.X ? MoveDirection.Left : MoveDirection.Right;
							((IMoveable)Parent).Move(direction, 1);
						}
					}
				}

				updateDuration = 0;
			}
		}

		private void TakeDamage(float damageAmount, Entity attacker)
		{
			IAlive parent = Parent as IAlive;
			if (parent.Health > 0)
			{
				parent.Health -= damageAmount;

				if (parent.Health <= 0)
				{
					parent.Kill(attacker);
					Engine.Debug.Console.Write("dead");
				}
			}
		}
	}
}
