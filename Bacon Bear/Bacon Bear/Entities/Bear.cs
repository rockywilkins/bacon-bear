using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Bear : Entity, IPhysics, ITargeter, IMoveable
	{
		public event CollisionEventHandler Collided;
		public event TargetEventHandler Targeted;
		public event MoveEventHandler Moved;

		public Entity Target
		{
			get { throw new System.NotImplementedException(); }
		}

		public Bear(Scene parent) : base(parent)
		{
			AimingComponent aiming = new AimingComponent();
			AddComponent(new BearPhysicsComponent());
			AddComponent(aiming);
			AddComponent(new BearAIComponent());
			AddComponent(new BearGraphicComponent());

			aiming.Stopped += aiming_Stopped;
		}

		public void Collide(IPhysics entity)
		{
			if (Collided != null)
			{
				Collided(entity);
			}
		}

		public void SetTarget(Entity target)
		{
			if (Targeted != null)
			{
				Targeted(target);
			}
		}

		public void Move(MoveDirection direction, float speed)
		{
			if (Moved != null)
			{
				Moved(direction, speed);
			}
		}

		protected void aiming_Stopped(Vector2 position)
		{
			// Create bacon and fire it
			Bacon bacon = new Bacon(Parent)
							  {
								Position = Vector2.Add(Position, new Vector2(0, -50))
							  };
			
			//bacon.SendMessage("physics_impulse", Vector2.Negate(aimDifference) / 4);
			Parent.Items.Add(bacon);
		}
	}
}
