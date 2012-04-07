using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Debug
{
	public static class Console
	{
		private static Engine engine;
		private static SpriteFont font;
		private static List<Text> messages = new List<Text>();
		private static List<Text> staticMessages = new List<Text>();

		public static void Start(Engine engine)
		{
		   Console.engine = engine;
		   font = Engine.Content.Load<SpriteFont>("Fonts/Segoe");
		}

		public static void Write(string text)
		{
			messages.Add(new Text { Message = text});

			if (messages.Count >= 10)
				messages.RemoveAt(0);

			UpdateMessageLocation();
		}

		public static void Write(string id, string text)
		{
			bool newmsg = false;

			foreach (Text txt in staticMessages)
			{
				if (txt.ID == id)
				{
					txt.Message = text;
					newmsg = true;
					return;
				}
			}

			if (newmsg == false)
			{
				staticMessages.Add(new Text { ID = id, Message = text });
				UpdateMessageLocation();
			}

			UpdateStaticMessageLocation();
		}


		private static void UpdateMessageLocation()
		{
			int count = 0;

			if (staticMessages.Count > 0)
				count = staticMessages.Count + 1;

			foreach (Text txt in messages)
			{
				txt.Location = new Vector2(5f, count * 18);
				count++;
			}
		}

		private static void UpdateStaticMessageLocation()
		{
			int count = 0;

			foreach (Text txt in staticMessages)
			{
				if (txt.Location == Vector2.Zero)
					txt.Location = new Vector2(5f, count * 18);

				count++;
			}
		}

		public static void Draw(GameTime gameTime)
		{
			engine.SpriteBatch.Begin();

			foreach (Text txt in staticMessages)
				engine.SpriteBatch.DrawString(font, txt.Message, txt.Location, Color.Red);

			foreach (Text txt in messages)
				engine.SpriteBatch.DrawString(font, txt.Message, txt.Location, Color.Red);

			engine.SpriteBatch.End();
		}
	}

	public class Text
	{
		public string ID;
		public string Message;
		public Vector2 Location;
	}
}
