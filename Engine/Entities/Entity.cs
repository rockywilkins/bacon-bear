using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Graphics;

namespace Engine.Entities
{
	public abstract class Entity
	{
		#region Fields

		private Vector2 position;
		private Vector2 velocity;

		private List<EntityComponent> components;

		#endregion


		#region Properties

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		public List<EntityComponent> Components
		{
			get { return components; }
		}

		#endregion


		#region Methods

		public Entity()
		{
			components = new List<EntityComponent>();
		}

		public virtual void Update(GameTime gameTime)
		{
			foreach (EntityComponent component in components)
			{
				component.Update(gameTime);
			}
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			foreach (EntityComponent component in components)
			{
				component.Draw(gameTime, spriteBatch, camera);
			}
		}

		public void AddComponent(EntityComponent component)
		{
			if (!components.Contains(component))
			{
				component.Parent = this;
				components.Add(component);
			}
		}

		public void RemoveComponent(EntityComponent component)
		{
			component.Parent = null;
			components.Remove(component);
		}

		public void SendMessage(string message)
		{
			foreach (EntityComponent component in components)
			{
				component.ReceiveMessage(message);
			}
		}

		#endregion
	}
}
