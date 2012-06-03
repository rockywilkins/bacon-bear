using Microsoft.Xna.Framework;
using Engine.Entities;

namespace BaconBear.Entities.Components
{
	public class BearAIComponent : EntityComponent
	{
		private Entity target;

		public override void Load()
		{
			base.Load();

			((ITargeter)Parent).Targeted += BearAIComponent_Targeted;
		}

		void BearAIComponent_Targeted(Entity target)
		{
			this.target = target;
		}

		public override void Update(GameTime gameTime)
		{
			if (target != null)
			{
				MoveDirection direction = target.Position.X > Parent.Position.X ? MoveDirection.Right : MoveDirection.Left;

				((IMoveable)Parent).Move(direction, 1);
			}
		}
	}
}
