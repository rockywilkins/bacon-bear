using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Alien : Entity, IAlive, IPhysics, IEnemy, IMoveable, ITargeter
	{
		private Entity target;

		public float Health { get; set; }
		public bool Alive { get; set; }
		public Entity Target
		{
			get { return target; }
			set
			{
				target = value;
				if (Targeted != null)
				{
					Targeted(value);
				}
			}
		}

		public event CollisionEventHandler Collided;
		public event DamageEventHandler Damaged;
		public event DeathEventHandler Died;
		public event MoveEventHandler Moved;
		public event TargetEventHandler Targeted;

		public Alien(Scene parent) : base(parent)
		{
			Alive = true;
		}

		public override void Load()
		{
			AddComponent(new AlienPhysicsComponent());
			AddComponent(new AlienAIComponent());
			AddComponent(new AlienGraphicComponent());

			base.Load();
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
				Alive = false;
				Health = 0;
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
