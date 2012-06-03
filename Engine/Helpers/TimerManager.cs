using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Engine.Helpers
{
	public class TimerManager
	{
		private static readonly List<Timer> timers = new List<Timer>();
 
		public static Timer Create(double duration)
		{
			Timer timer = new Timer(duration);
			timers.Add(timer);
			return timer;
		}

		public static void Add(Timer timer)
		{
			timers.Add(timer);
		}

		public static void Update(GameTime gameTime)
		{
			foreach (Timer timer in timers)
			{
				timer.Update(gameTime);
			}
		}
	}
}
