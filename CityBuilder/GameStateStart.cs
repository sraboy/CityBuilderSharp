#region Copyright Notice
/***************************************************************************
* The MIT License (MIT)
*
* Copyright © 2014 Daniel Mansfield
* Copyright © 2015-2016 Steven Lavoie
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of
* this software and associated documentation files (the "Software"), to deal in
* the Software without restriction, including without limitation the rights to
* use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
* the Software, and to permit persons to whom the Software is furnished to do so,
* subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
* COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
* IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
***************************************************************************/
#endregion
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CityBuilder
{
    public class GameStateStart : IGameState
    {
        #region Private Fields
        private Game _game;
        private View _view;
        #endregion

        public Game Game
        {
            get
            {
                return this._game;
            }

            set
            {
                if (this._game != value)
                    this._game = value;
            }
        }

        public GameStateStart(Game game)
        {
            this._view = new View();

            this.Game = game;
            var pos = (Vector2f)this.Game.Window.Size;//new Vector2f(this.Game.Window.Size.X, this.Game.Window.Size.Y);
            this._view.Size = pos;
            this._view.Center = pos * 0.5f;

            this.Game.Window.Resized += Window_Resized;
            this.Game.Window.KeyPressed += Window_KeyPressed;
        }

        #region Event Handlers
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Space:
                    LoadGame();
                    break;
                case Keyboard.Key.Escape:
                    this.Game.Window.Close();
                    break;
            }
        }
        private void Window_Resized(object sender, SizeEventArgs e)
        {
            //resizes the view but would stretch/pixelate without
            //the adjustments below to rescale the background
            this._view.Size = new Vector2f(e.Width, e.Height);

            this.Game.Background.Position = this.Game.Window.MapPixelToCoords(new Vector2i(0, 0));
            this.Game.Background.Scale =
                new Vector2f((float)e.Width / (float)this.Game.Background.Texture.Size.X,
                             (float)e.Height / (float)this.Game.Background.Texture.Size.Y);
        }
        #endregion

        private void LoadGame()
        {
            this.Game.States.Push(new GameStateEditor(this.Game));
        }

        public void Draw(float dt)
        {
            this.Game.Window.SetView(this._view);
            this.Game.Window.Clear();
            this.Game.Window.Draw(this.Game.Background);
        }
        public void Update(float dt)
        {
        }
    }
}
