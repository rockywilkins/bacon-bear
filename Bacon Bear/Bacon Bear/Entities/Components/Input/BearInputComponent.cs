using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using BaconBear.Entities;
using BaconBear.Entities.Components;
using Engine.Entities;

namespace BaconBear.Entities.Components.Input
{
	public class BearInputComponent : EntityComponent
	{
		public override void Update(GameTime gameTime)
		{
			TouchCollection locations = TouchPanel.GetState();

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
