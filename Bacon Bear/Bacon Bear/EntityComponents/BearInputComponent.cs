using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Engine.Entities;
using Engine.Input;
using BaconBear.Entities;
using BaconBear.Entities.Components;

namespace BaconBear.Entities.Components
{
	public class BearInputComponent : EntityComponent
	{
		private TouchInputHandler touchInput;

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
					Parent.SendMessage("screen_touch", location.Position);
					//Parent.Position = location.Position;
					break;
				case TouchLocationState.Moved:
					//Parent.Position = location.Position;
					break;
				case TouchLocationState.Released:
					break;
			}
		}
	}
}
