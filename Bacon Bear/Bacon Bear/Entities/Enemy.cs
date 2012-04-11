using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Enemy : Entity
	{
		public float Health { get; set; }
		public bool Alive { get; set; }

		public Enemy(Scene parent) : base(parent)
		{
			Alive = true;

			AddComponent(new EnemyPhysicsComponent());
			AddComponent(new EnemyAIComponent());
			AddComponent(new EnemyGraphicComponent());
		}
	}
}
