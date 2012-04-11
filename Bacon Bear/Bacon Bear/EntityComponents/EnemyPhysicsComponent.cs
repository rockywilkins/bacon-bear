using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Physics;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class EnemyPhysicsComponent : EntityComponent
	{
		World world;
		Body body;

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "physics_world")
			{
				world = value as World;

				body = BodyFactory.CreateBody(world);
				body.FixedRotation = true;

				float width = ConvertUnits.ToSimUnits(25);
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

				//body.OnCollision += new OnCollisionEventHandler(body_OnCollision);
			}
			else if (name == "flee")
			{
				string direction = value as string;
				float x = 0;
				if (direction == "left")
				{
					x = 50;
				}
				else
				{
					x = -50;
				}
				body.ApplyForce(new Vector2(x, -150));
			}
			else if (name == "death")
			{
				body.Rotation = 90f;
				body.FixedRotation = false;
			}
		}

		bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			return true;
		}

		public override void Update(GameTime gameTime)
		{
			Parent.Position = ConvertUnits.ToDisplayUnits(body.Position);
			Parent.Rotation = body.Rotation;
		}
	}
}
