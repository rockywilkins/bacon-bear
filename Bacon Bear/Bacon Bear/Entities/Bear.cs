using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components;

namespace BaconBear.Entities
{
	public class Bear : Entity
	{
		public Bear(Scene parent) : base(parent)
		{
			AddComponent(new BearPhysicsComponent());
			AddComponent(new BearAimingComponent());
			AddComponent(new BearGraphicComponent());
		}
	}
}
