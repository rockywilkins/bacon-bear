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

			((ITargeter)Parent).Targeted += Targeted;
		}

		public override void Update(GameTime gameTime)
		{
			if (target != null)
			{
				MoveDirection direction = target.Position.X > Parent.Position.X ? MoveDirection.Right : MoveDirection.Left;

				((IMoveable)Parent).Move(direction, 1);
			}
		}

		void Targeted(Entity target)
		{
			this.target = target;

			if (target != null)
			{
				((IAlive) target).Died += TargetDied;
			}
		}

		void TargetDied(Entity killer)
		{
			((IAlive)target).Died -= TargetDied;
			target = null;
			((ITargeter) Parent).Target = null;
		}
	}
}
