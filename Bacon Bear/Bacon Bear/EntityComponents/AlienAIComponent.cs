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
			((IAlive)Parent).Died += Died;

			foreach (SceneItem item in Parent.Parent.Items)
			{
				if (item is Bear)
				{
					((ITargeter) Parent).Target = item as Entity;
				}
			}

			updateTimer = new Timer(500);
			idleTimer = new Timer(1000);
			attackTimer = new Timer(2000);
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
				else if (distance < 500)
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

			Engine.Debug.Console.Write("alienAI", "Idle");
		}

		private void Attack(GameTime gameTime)
		{
			attackTimer.Update(gameTime);

			if (attackTimer.Complete)
			{
				if (((ITargeter)Parent).Target != null)
				{
					// Jump and do damage to target
					((IMoveable)Parent).Move(MoveDirection.Up, 1f);
					//target.TakeDamage(1, Parent);

					Engine.Debug.Console.Write("alienAI", "Attacking");
				}

				attackTimer.Reset();
			}
		}

		private void Flee()
		{
			// Run in opposite direction of target
			Entity target = ((ITargeter)Parent).Target;
			MoveDirection direction = target.Position.X > Parent.Position.X ? MoveDirection.Left : MoveDirection.Right;
			((IMoveable)Parent).Move(direction, 1);

			Engine.Debug.Console.Write("alienAI", "Fleeing");
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

		private void Died(Entity killer)
		{
			Engine.Debug.Console.Write("alienAI", "Dead");
		}

		public enum EntityState
		{
			Idle,
			Attack,
			Flee
		}
	}
}
