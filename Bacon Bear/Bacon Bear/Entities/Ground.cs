using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Engine.Entities;
using Engine.Graphics;
using Engine.Physics;
using Engine.Scene;

namespace BaconBear.Entities
{
	public class Ground : Entity
	{
		private World physicsWorld;

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			Parent.PrimitiveBatch.Begin(PrimitiveType.LineStrip);
			Parent.PrimitiveBatch.AddVertex(new Vector2(0, 400), Color.Black);
			Parent.PrimitiveBatch.AddVertex(new Vector2(800, 400), Color.Black);
			Parent.PrimitiveBatch.End();

			base.Draw(gameTime, spriteBatch, camera);
		}

		public override void Load()
		{
			float width = ConvertUnits.ToSimUnits(800);
			float height = ConvertUnits.ToSimUnits(400);

			// Add floor shape
			Body floor = BodyFactory.CreateEdge(physicsWorld, new Vector2(0, height), new Vector2(width, height));
			floor.CollisionCategories = Category.All;
			floor.CollidesWith = Category.All;
		}

		public override void SendMessage(string name, object value)
		{
			if (name == "physics_world")
			{
				physicsWorld = value as World;
			}

			base.SendMessage(name, value);
		}
	}
}
