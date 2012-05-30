namespace Engine.Entities
{
	public interface IAlive
	{
		float Health { get; set; }

		event DamageEventHandler Damaged;
		event DeathEventHandler Died;

		void TakeDamage(int amount, Entity attacker);
		void Kill(Entity killer);
	}

	public delegate void DamageEventHandler(float damage, Entity attacker);
	public delegate void DeathEventHandler(Entity killer);
}
