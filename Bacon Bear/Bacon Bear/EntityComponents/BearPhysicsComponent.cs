using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Physics;

namespace BaconBear.Entities.Components
{
	public class BearPhysicsComponent : EntityComponent
	{
		World world;
		Body body;

		public override void Load()
		{
			base.Load();

			world = Parent.Parent.PhysicsWorld;

			body = BodyFactory.CreateBody(world);
			body.FixedRotation = true;

			float width = ConvertUnits.ToSimUnits(70);
			float height = ConvertUnits.ToSimUnits(50);

			Vertices bounds = new Vertices(4);
			bounds.Add(new Vector2(0, 0));
			bounds.Add(new Vector2(width, 0));
			bounds.Add(new Vector2(width, height));
			bounds.Add(new Vector2(0, height));

			PolygonShape shape = new PolygonShape(bounds, 5f);

			Fixture fixture = body.CreateFixture(shape);

			body.BodyType = BodyType.Dynamic;
			body.Position = ConvertUnits.ToSimUnits(Parent.Position);
			body.Restitution = 0.3f;
			body.UserData = Parent;

			body.OnCollision += body_OnCollision;

			((IMoveable)Parent).Moved += Moved;
		}

		public override void Unload()
		{
			body.Dispose();

			base.Unload();
		}

		public override void Update(GameTime gameTime)
		{
			Parent.Position = ConvertUnits.ToDisplayUnits(body.Position);
		}

		private void Moved(MoveDirection direction, float speed)
		{
			float velocityX;

			switch (direction)
			{
				case MoveDirection.Left:
					velocityX = -6.0f;
					break;
				case MoveDirection.Right:
					velocityX = 6.0f;
					break;
				default:
					velocityX = 0;
					break;
			}

			body.LinearVelocity = new Vector2(velocityX, body.LinearVelocity.Y);
		}

		bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			if (fixtureB.Body.UserData is IEnemy)
			{
				IPhysics enemy = fixtureB.Body.UserData as IPhysics;
				if (enemy != null)
				{
					((IPhysics) Parent).Collide(enemy);
					((IAlive) enemy).TakeDamage(10, Parent);
					return false;
				}
			}

			return true;
		}
	}
}
