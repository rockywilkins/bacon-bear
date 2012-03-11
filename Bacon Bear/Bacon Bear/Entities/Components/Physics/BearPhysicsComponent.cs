using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using Engine.Entities;
using Engine.Input;
using Engine.Physics;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components.Physics
{
	public class BearPhysicsComponent : EntityComponent
	{
		World world;
		Body body;

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "physics_world")
			{
				world = value as World;

				body = BodyFactory.CreateBody(world);

				CircleShape shape = new CircleShape(ConvertUnits.ToSimUnits(50f), 5f);
				Fixture fixture = body.CreateFixture(shape);

				body.BodyType = BodyType.Dynamic;
				body.Position = ConvertUnits.ToSimUnits(Parent.Position);
				body.Restitution = 0.8f;
			}
			else if (name == "screen_touch")
			{
				body.ApplyForce((Vector2)value);
			}
		}

		public override void Update(GameTime gameTime)
		{
			Parent.Position = ConvertUnits.ToDisplayUnits(body.Position);
		}
	}
}
