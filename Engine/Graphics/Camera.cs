using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Entities;

namespace Engine.Graphics
{
	public class Camera
	{
		private List<Entity> visableEntities = new List<Entity>();

		private Vector2 position;
		private Matrix transform;
		private float zoom;
		private float rotation;

		private float screenWidth;
		private float screenHeight;

		public Matrix Transform
		{
			get 
			{
				transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
										Matrix.CreateRotationZ(rotation) *
										Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
										Matrix.CreateTranslation(new Vector3(screenWidth * 0.5f, screenHeight * 0.5f, 0));

				return transform; 
			}
			set { transform = value; }
		}

		public Camera()
		{
			position = new Vector2(0f, 0f);
			zoom = 1f;
			rotation = 0f;

			screenWidth = 800f;
			screenHeight = 480f;
		}

		public void GetVisableEntites(IEnumerable<Entity> entities)
		{
			visableEntities.Clear();

			foreach (Entity entity in entities)
			{
				// Add code for checking if entity is in screen bounds
				visableEntities.Add(entity);
			}
		}

		public void Draw(IEnumerable<Entity> entities, GameTime gameTime, SpriteBatch spriteBatch)
		{
			zoom += 0.001f;
			rotation += 0.001f;

			GetVisableEntites(entities);

			foreach (Entity entity in visableEntities)
			{
				entity.Draw(gameTime, spriteBatch, this);

				// Also needs to pass the camera so it can get the transfom
				//entity.Draw(gameTime, spriteBatch, this);
			}
		}
	}
}
