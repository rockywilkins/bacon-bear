using Microsoft.Xna.Framework;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Bear : Entity, IPhysics, ITargeter, IMoveable
	{
		private Entity target;

		public event CollisionEventHandler Collided;
		public event TargetEventHandler Targeted;
		public event MoveEventHandler Moved;

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

		public Bear(Scene parent) : base(parent)
		{
		}

		public override void Load()
		{
			AimingComponent aiming = new AimingComponent();
			AddComponent(new BearPhysicsComponent());
			AddComponent(aiming);
			AddComponent(new BearAIComponent());
			AddComponent(new BearGraphicComponent());

			aiming.Stopped += aiming_Stopped;

			base.Load();
		}

		public void Collide(IPhysics entity)
		{
			if (Collided != null)
			{
				Collided(entity);
			}
		}

		public void Move(MoveDirection direction, float speed)
		{
			if (Moved != null)
			{
				Moved(direction, speed);
			}
		}

		protected void aiming_Stopped(Vector2 position, Vector2 difference)
		{
			// Create bacon and fire it
			Bacon bacon = new Bacon(Parent);
			bacon.Position = Vector2.Add(Position, new Vector2(0, -50));
			bacon.Velocity = Vector2.Negate(difference) / 10;
			bacon.Load();

			//bacon.SendMessage("physics_impulse", Vector2.Negate(aimDifference) / 4);
			Parent.Items.Add(bacon);
		}
	}
}
