using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Enemy : Entity, IAlive, IPhysics, IEnemy, IMoveable
	{
		public float Health { get; set; }
		public bool Alive { get; set; }

		public event CollisionEventHandler Collided;
		public event DamageEventHandler Damaged;
		public event DeathEventHandler Died;
		public event MoveEventHandler Moved;

		public Enemy(Scene parent) : base(parent)
		{
			Alive = true;

			AddComponent(new EnemyPhysicsComponent());
			AddComponent(new EnemyAIComponent());
			AddComponent(new EnemyGraphicComponent());
		}

		public void Collide(IPhysics entity)
		{
			if (Collided != null)
			{
				Collided(entity);
			}
		}

		public void TakeDamage(int amount, Entity attacker)
		{
			if (Damaged != null)
			{
				Damaged(amount, attacker);
			}
		}

		public void Kill(Entity killer)
		{
			if (Died != null)
			{
				Died(killer);
			}
		}

		public void Move(MoveDirection direction, float speed)
		{
			if (Moved != null)
			{
				Moved(direction, speed);
			}
		}
	}
}
