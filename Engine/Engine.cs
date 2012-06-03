using System.Collections.Generic;
using Engine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Screens;

namespace Engine
{
	public class Engine
	{
		#region Fields

		private Game game;
		private SpriteBatch spriteBatch;
		private Stack<Screen> screens;

		#endregion


		#region Properties

		public Game Game
		{
			get { return game; }
		}

		public SpriteBatch SpriteBatch
		{
			get { return spriteBatch; }
		}

		public Stack<Screen> Screens
		{
			get { return screens; }
		}

		#endregion


		#region Static Properties

		public static ContentManager Content { get; set; }

		#endregion


		#region Methods

		public Engine(Game game)
		{
			this.game = game;
			spriteBatch = new SpriteBatch(Game.GraphicsDevice);
			screens = new Stack<Screen>();
			Content = game.Content;
			Debug.Console.Start(this);
		}

		public void Update(GameTime gameTime)
		{
			TimerManager.Update(gameTime);

			foreach (Screen screen in screens)
			{
				screen.Update(gameTime);
			}
		}

		public void Draw(GameTime gameTime)
		{
			foreach (Screen screen in screens)
			{
				screen.Draw(gameTime, spriteBatch);
			}

			Debug.Console.Draw(gameTime);
		}

		public void PushScreen(Screen screen)
		{
			if (!screens.Contains(screen))
			{
				screen.Parent = this;
				screen.Load();
				screens.Push(screen);
			}
		}

		public Screen PopScreen()
		{
			if (screens.Count == 0)
			{
				return null;
			}

			Screen screen = screens.Pop();
			screen.Unload();
			screen.Parent = null;
			return screen;
		}

		#endregion
	}
}
