using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;

namespace Engine.Graphics
{
	public class Camera
	{
		#region Fields

		private Vector2 position = new Vector2();
		private float zoom = 1f;
		private float rotation = 0f;

		private Matrix transform;

		private int width;
		private int height;

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

		public Matrix Transform
		{
			get { return transform; }
			set { transform = value; }
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

		public List<Entity> GetVisibleEntites(IEnumerable<Entity> entities)
		{
			List<Entity> visibleEntities = new List<Entity>();

			foreach (Entity entity in entities)
			{
				// TODO: Add code for checking if entity is in screen bounds

				visibleEntities.Add(entity);
			}

			return visibleEntities;
		}

		public void Update(GameTime gameTime)
		{
			// Create the matrix for transforming sprites to camera space
			transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
										Matrix.CreateRotationZ(rotation) *
										Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
										Matrix.CreateTranslation(new Vector3(width / 2f, height / 2f, 0));
		}

		public void Draw(IEnumerable<Entity> entities, GameTime gameTime, SpriteBatch spriteBatch)
		{
			List<Entity> visibleEntities = GetVisibleEntites(entities);

			foreach (Entity entity in visibleEntities)
			{
				entity.Draw(gameTime, spriteBatch, this);
			}
		}

		#endregion
	}
}
