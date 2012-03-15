using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Graphics;
using Engine.Scene;

namespace Engine.Entities
{
	public abstract class Entity : SceneItem
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

		public Entity(Scene.Scene parent) : base(parent)
		{
			components = new List<EntityComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (EntityComponent component in components)
			{
				component.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
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
				component.Load();
				components.Add(component);
			}
		}

		public void RemoveComponent(EntityComponent component)
		{
			component.Parent = null;
			components.Remove(component);
		}

		public virtual void Load()
		{
			foreach (EntityComponent component in components)
			{
				component.Load();
			}
		}

		public virtual void Unload()
		{
			foreach (EntityComponent component in components)
			{
				component.Unload();
			}
		}

		public virtual void SendMessage(string name, object value)
		{
			foreach (EntityComponent component in components)
			{
				component.ReceiveMessage(name, value);
			}
		}

		#endregion
	}
}
