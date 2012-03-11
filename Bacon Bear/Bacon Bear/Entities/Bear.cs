using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using Engine.Scene;
using BaconBear.Entities.Components.Graphics;
using BaconBear.Entities.Components.Input;
using BaconBear.Entities.Components.Physics;

namespace BaconBear.Entities
{
	public class Bear : Entity
	{
		public Bear(Scene parent) : base(parent)
		{
			AddComponent(new BearGraphicComponent());
			AddComponent(new BearInputComponent());
			AddComponent(new BearPhysicsComponent());
		}
	}
}
