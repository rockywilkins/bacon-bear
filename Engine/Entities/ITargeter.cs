namespace Engine.Entities
{
	public interface ITargeter
	{
		Entity Target { get; set; }

		event TargetEventHandler Targeted;
	}

	public delegate void TargetEventHandler(Entity target);
}
