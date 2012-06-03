using System;
using Engine.Helpers;
using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;

namespace BaconBear.Entities.Components
{
	public class AlienAIComponent : EntityComponent
	{
		private double updateDuration = 0;
		private EntityState state = EntityState.Idle;
		private Timer updateTimer;
		private Timer idleTimer;
		private Timer attackTimer;
		private MoveDirection direction = MoveDirection.Right;

		public override void Load()
		{
			((IAlive)Parent).Damaged += TakeDamage;

			foreach (SceneItem item in Parent.Parent.Items)
			{
				if (item is Bear)
				{
					((ITargeter) Parent).Target = item as Entity;
				}
			}

			updateTimer = new Timer(500);
			idleTimer = new Timer(1000);
		}

		public override void Update(GameTime gameTime)
		{
			// No need to do anything if dead
			if (((Alien)Parent).Alive == false)
				return;

			// Add time elapsed since last update
			updateTimer.Update(gameTime);

			// Work out what state parent should be in
			if (updateTimer.Complete)
			{
				Entity target = ((ITargeter)Parent).Target;

				float distance = Math.Abs(target.Position.X - Parent.Position.X);
				if (((ITargeter)target).Target == Parent)
				{
					state = EntityState.Flee;
				}
				else if (distance < 200)
				{
					state = EntityState.Attack;
				}
				else
				{
					state = EntityState.Idle;
				}

				updateTimer.Reset();
			}

			// Do the appropriate action for the state
			switch (state)
			{
				case EntityState.Idle: Idle(gameTime); break;
				case EntityState.Attack: Attack(gameTime); break;
				case EntityState.Flee: Flee(); break;
			}
		}

		private void Idle(GameTime gameTime)
		{
			idleTimer.Update(gameTime);

			if (idleTimer.Complete)
			{
				direction = direction == MoveDirection.Left ? MoveDirection.Right : MoveDirection.Left;
				idleTimer.Reset();
			}

			((IMoveable)Parent).Move(direction, 0.7f);
			Engine.Debug.Console.Write("Idle");
		}

		private void Attack(GameTime gameTime)
		{
			attackTimer.Update(gameTime);

			if (attackTimer.Complete)
			{
				// Jump and do damage to target
				((IMoveable)Parent).Move(MoveDirection.Up, 1f);
				ITargeter parent = (ITargeter)Parent;
				((IAlive)parent.Target).TakeDamage(1, Parent);

				Engine.Debug.Console.Write("Attacking");

				attackTimer.Reset();
			}
		}

		private void Flee()
		{
			// Run in opposite direction of target
			Entity target = ((ITargeter)Parent).Target;
			MoveDirection direction = target.Position.X > Parent.Position.X ? MoveDirection.Left : MoveDirection.Right;
			((IMoveable)Parent).Move(direction, 1);

			Engine.Debug.Console.Write("Fleeing");
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
				}
			}
		}

		public enum EntityState
		{
			Idle,
			Attack,
			Flee
		}
	}
}
