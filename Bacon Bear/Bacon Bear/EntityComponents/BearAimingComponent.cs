using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Engine.Entities;
using Engine.Graphics;
using Engine.Input;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class BearAimingComponent : EntityComponent
	{
		private Texture2D baconTexture;
		private TouchInputHandler touchInput;
		private bool aiming = false;
		private Vector2 aimOrigin;
		private Vector2 currentAim;
		private Vector2 aimDifference;

		public override void Load()
		{
			baconTexture = Engine.Engine.Content.Load<Texture2D>("Textures/Bacon");

			base.Load();
		}

		public override void ReceiveMessage(string name, object value)
		{
			if (name == "touch_input")
			{
				touchInput = value as TouchInputHandler;
			}
		}

		public override void Update(GameTime gameTime)
		{
			TouchCollection locations = touchInput.GetTouchLocations();

			// Check if the screen was touched
			if (locations.Count == 0)
				return;

			TouchLocation location = locations[0];

			switch (location.State)
			{
				case TouchLocationState.Pressed:
					StartAim(location);
					MoveAim(location);
					break;
				case TouchLocationState.Moved:
					if (aiming)
					{
						MoveAim(location);
					}
					break;
				case TouchLocationState.Released:
					StopAim();
					break;
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			if (aiming)
			{
				// Get positions of aiming line from touch positions
				Vector2 bearPosition = camera.ConvertToScreenPos(Parent.Position);
				Vector2 aimPosition = camera.ConvertToScreenPos(Vector2.Add(Parent.Position, aimDifference));

				// Draw the aiming line
				Parent.Parent.PrimitiveBatch.Begin(PrimitiveType.LineStrip);
				Parent.Parent.PrimitiveBatch.AddVertex(bearPosition, Color.Black);
				Parent.Parent.PrimitiveBatch.AddVertex(aimPosition, Color.Black);
				Parent.Parent.PrimitiveBatch.End();
			}
		}

		public void StartAim(TouchLocation location)
		{
			aiming = true;
			aimOrigin = location.Position;
		}

		public void StopAim()
		{
			aiming = false;

			// Create bacon and fire it
			Bacon bacon = new Bacon(Parent.Parent);
			bacon.Position = Vector2.Add(Parent.Position, new Vector2(0, -50));
			bacon.SendMessage("physics_world", Parent.Parent.PhysicsWorld);
			bacon.SendMessage("physics_impulse", Vector2.Negate(aimDifference) / 4);
			Parent.Parent.Items.Add(bacon);
		}

		public void MoveAim(TouchLocation location)
		{
			currentAim = location.Position;

			Vector2 point1 = aimOrigin;
			Vector2 point2 = currentAim;

			aimDifference = Vector2.Subtract(point2, point1);
		}
	}
}
