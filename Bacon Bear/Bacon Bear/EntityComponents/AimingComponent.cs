using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Engine.Entities;
using Engine.Graphics;
using Engine.Input;

namespace BaconBear.Entities.Components
{
	public class AimingComponent : EntityComponent
	{
		private TouchInputHandler touchInput;
		private bool aiming;
		private Vector2 aimOrigin;
		private Vector2 currentAim;
		private Vector2 aimDifference;

		public event AimingEventHandler Started;
		public event AimingEventHandler Stopped;
		public event AimingEventHandler Moved;

		public override void Load()
		{
			touchInput = Parent.Parent.TouchInputHandler;

			base.Load();
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
					StopAim(location);
					break;
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			if (aiming)
			{
				// Get positions of aiming line from touch positions
				Vector2 parentPosition = camera.ConvertToScreenPos(Parent.Position);
				Vector2 aimPosition = camera.ConvertToScreenPos(Vector2.Add(Parent.Position, aimDifference));

				// Draw the aiming line
				Parent.Parent.PrimitiveBatch.Begin(PrimitiveType.LineStrip);
				Parent.Parent.PrimitiveBatch.AddVertex(parentPosition, Color.Black);
				Parent.Parent.PrimitiveBatch.AddVertex(aimPosition, Color.Black);
				Parent.Parent.PrimitiveBatch.End();
			}
		}

		public void StartAim(TouchLocation location)
		{
			aiming = true;
			aimOrigin = location.Position;

			if (Started != null)
			{
				Started(location.Position, Vector2.Zero);
			}
		}

		public void StopAim(TouchLocation location)
		{
			aiming = false;

			if (Stopped != null)
			{
				Stopped(location.Position, aimDifference);
			}
		}

		protected void MoveAim(TouchLocation location)
		{
			currentAim = location.Position;

			aimDifference = Vector2.Subtract(currentAim, aimOrigin);

			if (Moved != null)
			{
				Moved(location.Position, aimDifference);
			}
		}
	}

	public delegate void AimingEventHandler(Vector2 position, Vector2 difference);
}
