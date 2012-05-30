namespace Engine.Entities
{
	public interface IMoveable
	{
		event MoveEventHandler Moved;

		void Move(MoveDirection direction, float speed);
	}

	public delegate void MoveEventHandler(MoveDirection direction, float speed);

	public enum MoveDirection
	{
		Left,
		Right,
		Up,
		Down
	}
}
