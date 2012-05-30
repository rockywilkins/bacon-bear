using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Engine;
using BaconBear.Screens;

namespace BaconBear
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class BBGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;

		Engine.Engine engine;

		public BBGame()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
			graphics.SynchronizeWithVerticalRetrace = false;
			graphics.IsFullScreen = true;
			IsFixedTimeStep = false;

			Content.RootDirectory = "Content";

			// Set frame rate to 60fps
			TargetElapsedTime = TimeSpan.FromTicks(166666);

			// Extend battery life under lock.
			InactiveSleepTime = TimeSpan.FromSeconds(1);
		}

		void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
			// Use device native refresh rate
			e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.One;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			engine = new Engine.Engine(this);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			engine.PushScreen(new Level());
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			engine.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			engine.Draw(gameTime);

			base.Draw(gameTime);
		}
	}
}
