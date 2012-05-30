namespace Engine.Entities
{
	public interface ITargeter
	{
		Entity Target { get; }

		event TargetEventHandler Targeted;

		void SetTarget(Entity target);
	}

	public delegate void TargetEventHandler(Entity target);
}
