using System.Collections.Generic;
using Engine.Graphics;

namespace Engine.Scene
{
	public class Scene
	{
		#region Fields

		private List<SceneItem> items = new List<SceneItem>();
		private List<Camera> cameras = new List<Camera>();

		#endregion


		#region Properties

		public List<SceneItem> Items
		{
			get { return items; }
			set { items = value; }
		}

		public List<Camera> Cameras
		{
			get { return cameras; }
			set { cameras = value; }
		}

		#endregion
	}
}
