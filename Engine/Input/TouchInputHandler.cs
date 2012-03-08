using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Engine.Graphics;

namespace Engine.Input
{
	public class TouchInputHandler
	{
		private Camera camera;

		public TouchInputHandler(Camera camera)
		{
			this.camera = camera;
		}

		public TouchCollection GetTouchLocations()
		{
			TouchCollection touchLocations = TouchPanel.GetState();
			TouchLocation[] convertedLocations = new TouchLocation[touchLocations.Count];

			for (int count = 0; count < touchLocations.Count; count++)
			{
				TouchLocation location = touchLocations[count];
				Vector2 position = camera.ConvertToWorldPos(location.Position);
				TouchLocation newLocation = new TouchLocation(location.Id, location.State, position);

				convertedLocations[count] = newLocation;
			}

			return new TouchCollection(convertedLocations);
		}
	}
}
