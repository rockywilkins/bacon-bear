using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Engine.Entities;
using BaconBear.Entities.Components.Graphics;
using BaconBear.Entities.Components.Input;

namespace BaconBear.Entities
{
	public class Bear : Entity
	{
		public Bear() : base()
		{
			AddComponent(new BearGraphicComponent());
			AddComponent(new BearInputComponent());
		}
	}
}
