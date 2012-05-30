namespace Engine.Entities
{
	public interface IPhysics
	{
		event CollisionEventHandler Collided;

		void Collide(IPhysics entity);
	}

	public delegate bool CollisionEventHandler(IPhysics entity);
}
