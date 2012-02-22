using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Engine.UI
{
    public class TextRendering : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        SpriteFont fontsegoe;

        public List<TextBlock> TextBlocks = new List<TextBlock>();

        public TextRendering(Game game) : base(game)
        {
            game.Components.Add(this);

            fontsegoe = game.Content.Load<SpriteFont>("Fonts/Segoe");
            
            spriteBatch = (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));

        	DrawOrder = 500;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < TextBlocks.Count; i++)
            {
                if (DateTime.Now > TextBlocks[i].RemoveTime && TextBlocks[i].Time != 0)
                    TextBlocks.Remove(TextBlocks[i]);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (TextBlock tb in TextBlocks)
            {
                //spriteBatch.DrawString(fontsegoe, tb.Text, tb.Location, tb.Colour);
            }

            base.Draw(gameTime);
        }

        public void AddText(string text, Vector2 location, Color colour, int time)
        {
            TextBlocks.Add(new TextBlock(text, location, colour, time));
        }

    }

    public class TextBlock
    {
        DateTime removeTime;

        public DateTime RemoveTime
        {
            get { return removeTime; }
            set { removeTime = value; }
        }

        string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        Vector2 location;

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        Color colour;

        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        int time;

        public int Time
        {
            get { return time; }
            set
            {
                removeTime = DateTime.Now.AddMilliseconds(value);
                time = value; 
            }
        }

        public TextBlock()
        {

        }

        public TextBlock(string text, Vector2 location, Color colour, int time)
        {
            this.Text = text;
            this.Location = location;
            this.Colour = colour;
            this.Time = time;
        }

    }
}
