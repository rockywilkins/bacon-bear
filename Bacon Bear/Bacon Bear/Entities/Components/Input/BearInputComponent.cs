using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Engine.Entities;
using Engine.Input;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components.Input
{
	public class BearInputComponent : EntityComponent
	{
		private TouchInputHandler touchInput;

		public override void ReceiveMessage(object message)
		{
			if (message.GetType().Name == "TouchInputHandler")
			{
				touchInput = message as TouchInputHandler;
			}

			base.ReceiveMessage(message);
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
					Parent.Position = location.Position;
					break;
				case TouchLocationState.Moved:
					break;
				case TouchLocationState.Released:
					break;
			}
		}
	}
}
