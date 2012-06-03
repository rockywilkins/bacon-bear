using Microsoft.Xna.Framework;

namespace Engine.Helpers
{
	public class Timer
	{
		private double elapsedTime;
		private bool complete;

		public double Duration { get; set; }
		public double ElapsedTime
		{
			get { return elapsedTime; }
		}
		public bool Complete
		{
			get { return complete; }
		}

		public event TimerEventHandler Completed;

		public Timer(double duration)
		{
			Duration = duration;
		}

		public void Update(GameTime gameTime)
		{
			elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (!complete && elapsedTime > Duration)
			{
				complete = true;

				if (Completed != null)
				{
					Completed(this);
				}
			}
		}

		public void Reset()
		{
			elapsedTime = 0;
			complete = false;
		}
	}

	public delegate void TimerEventHandler(Timer timer);
}
