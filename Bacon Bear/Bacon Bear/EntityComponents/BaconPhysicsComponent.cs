using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Physics;
using Engine.Scene;

namespace BaconBear.Entities.Components
{
	public class BaconPhysicsComponent : EntityComponent
	{
		World world;
		Body body;

		public override void Load()
		{
			base.Load();

			world = Parent.Parent.PhysicsWorld;

			body = BodyFactory.CreateBody(world);

			float width = ConvertUnits.ToSimUnits(10);
			float height = ConvertUnits.ToSimUnits(25);

			Vertices bounds = new Vertices(4);
			bounds.Add(new Vector2(0, 0));
			bounds.Add(new Vector2(width, 0));
			bounds.Add(new Vector2(width, height));
			bounds.Add(new Vector2(0, height));

			PolygonShape shape = new PolygonShape(bounds, 5f);

			body.CreateFixture(shape);

			body.BodyType = BodyType.Dynamic;
			body.Position = ConvertUnits.ToSimUnits(Parent.Position);
			body.Restitution = 0f;
			body.UserData = Parent;

			body.OnCollision += body_OnCollision;
		}

		public override void Unload()
		{
			body.Dispose();

			base.Unload();
		}

		bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			if (fixtureB.Body.UserData is IEnemy)
			{
				foreach (SceneItem item in Parent.Parent.Items)
				{
					if (item is Bear)
					{
						((ITargeter)item).SetTarget(fixtureB.Body.UserData as Entity);
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
			Parent.Velocity = body.LinearVelocity;
		}
	}
}
