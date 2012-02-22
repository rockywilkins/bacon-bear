using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Engine.UI;

namespace Engine.Debug
{
	public static class Console
	{
		private static Game game;
		private static TextRendering textRendering;
		private static List<TextBlock> listTextBlocks = new List<TextBlock>();
		private static int lastUpdate = 0;
		private static int lines = 3;

		public static void Start(Game game)
		{
			Console.game = game;

			textRendering = (TextRendering)game.Services.GetService(typeof(TextRendering)); 
		}
		
		public static void Write(string text)
		{
			if (listTextBlocks.Count > lines -1)
			{
				listTextBlocks[lastUpdate].Text = text;
				if (lastUpdate < lines - 1)
					lastUpdate++;
				else
					lastUpdate = 0;
			}
			else
			{
				TextBlock textBlock = new TextBlock(text, new Vector2(10, listTextBlocks.Count * 20), Color.White, 0);

				listTextBlocks.Add(textBlock);
			}

			foreach (TextBlock tb in listTextBlocks)
			{
				//if (!textRendering.TextBlocks.Contains(tb))
				//    textRendering.TextBlocks.Add(tb);
			}
		}

		public static void Write(string text, int line)
		{
			if (line > lines)
				line = lines;

			if (listTextBlocks.Count >= line)
				listTextBlocks[line - 1].Text = text;
			else
				Write(text);
		}
	}
}
