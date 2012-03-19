using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Input;
using Engine.Physics;
using Engine.Scene;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class BaconPhysicsComponent : EntityComponent
	{
		World world;
		Body body;

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "physics_world")
			{
				world = value as World;

				body = BodyFactory.CreateBody(world);

				float width = ConvertUnits.ToSimUnits(10);
				float height = ConvertUnits.ToSimUnits(25);

				Vertices bounds = new Vertices(4);
				bounds.Add(new Vector2(0, 0));
				bounds.Add(new Vector2(width, 0));
				bounds.Add(new Vector2(width, height));
				bounds.Add(new Vector2(0, height));

				PolygonShape shape = new PolygonShape(bounds, 5f);

				Fixture fixture = body.CreateFixture(shape);

				body.BodyType = BodyType.Dynamic;
				body.Position = ConvertUnits.ToSimUnits(Parent.Position);
				body.Restitution = 0f;
				body.UserData = Parent;

				body.OnCollision += new OnCollisionEventHandler(body_OnCollision);
			}
			else if (name == "physics_impulse")
			{
				Vector2 direction = (Vector2)value;
				body.ApplyForce(direction);
			}
		}

		public override void Unload()
		{
			body.Dispose();

			base.Unload();
		}

		bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			if (fixtureB.Body.UserData != null && fixtureB.Body.UserData.GetType().Name == "Enemy")
			{
				foreach (SceneItem item in Parent.Parent.Items)
				{
					if (item.GetType().Name == "Bear")
					{
						Bear bear = item as Bear;
						bear.SendMessage("target", fixtureB.Body.UserData);
					}
				}

				Parent.Unload();
				Parent.Parent.Items.Remove(Parent);
			}

			return true;
		}

		public override void Update(GameTime gameTime)
		{
			Parent.Position = ConvertUnits.ToDisplayUnits(body.Position);
			Parent.Rotation = body.Rotation;
		}
	}
}
