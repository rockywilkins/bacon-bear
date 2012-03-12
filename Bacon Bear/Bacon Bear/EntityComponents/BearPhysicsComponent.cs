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
				body.FixedRotation = true;

				float width = ConvertUnits.ToSimUnits(68);
				float height = ConvertUnits.ToSimUnits(46);

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
			}
			else if (name == "screen_touch")
			{
				Vector2 touchPosition = (Vector2)value;
				float x = 0;
				if (touchPosition.X < Parent.Position.X)
				{
					x = -300f;
					Parent.SendMessage("direction", "left");
				}
				else
				{
					x = 300f;
					Parent.SendMessage("direction", "right");
				}

				body.ApplyForce(new Vector2(x, -500f));
			}
		}

		public override void Update(GameTime gameTime)
		{
			Parent.Position = ConvertUnits.ToDisplayUnits(body.Position);
		}
	}
}
