using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;
using Engine.Scene;

namespace Engine.Graphics
{
	public class Camera
	{
		#region Fields

		private Vector2 position = new Vector2();
		private float zoom = 1f;
		private float rotation = 0f;

		private Matrix matrix;

		private int width;
		private int height;

		private List<SceneItem> visibleItems;

		#endregion


		#region Properties

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public float Zoom
		{
			get { return zoom; }
			set { zoom = value; }
		}

		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		public Matrix Matrix
		{
			get { return matrix; }
			set { matrix = value; }
		}

		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		#endregion


		#region Methods

		public Camera(int width, int height)
		{
			// Set the camera dimensions
			this.width = width;
			this.height = height;
		}

		public List<SceneItem> GetVisibleItems(IEnumerable<SceneItem> items)
		{
			List<SceneItem> visibleItems = new List<SceneItem>();

			foreach (SceneItem item in items)
			{
				// TODO: Add code for checking if entity is in screen bounds

				visibleItems.Add(item);
			}

			return visibleItems;
		}

		public Vector2 ConvertToScreenPos(Vector2 position)
		{
			return Vector2.Transform(position, matrix);
		}

		public Vector2 ConvertToWorldPos(Vector2 position)
		{
			return Vector2.Transform(position, Matrix.Invert(matrix));
		}

		public void Update(GameTime gameTime)
		{
			// Create the matrix for transforming sprites to camera space
			matrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
										Matrix.CreateRotationZ(rotation) *
										Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
										Matrix.CreateTranslation(new Vector3(width / 2f, height / 2f, 0));
		}

		public void Draw(IEnumerable<SceneItem> items, GameTime gameTime, SpriteBatch spriteBatch)
		{
			visibleItems = GetVisibleItems(items);

			foreach (SceneItem item in visibleItems)
			{
				item.Draw(gameTime, spriteBatch, this);
			}
		}

		#endregion
	}
}
