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
    public class GameStateEditor : IGameState
    {
        #region Private Fields
        private Game _game;
        private View _gameView;
        private View _guiView;
        #endregion

        public Game Game
        {
            get
            {
                return this._game;
            }

            set
            {
                this._game = value;
            }
        }

        public GameStateEditor(Game game)
        {
            this.Game = game;

            var pos = (Vector2f)this.Game.Window.Size;
            this._guiView.Size = pos;
            this._guiView.Center = pos * 0.5f;
            this._gameView.Size = pos;
            this._gameView.Center = pos * 0.5f;

            this.Game.Window.Resized += Window_Resized;
            this.Game.Window.KeyPressed += Window_KeyPressed;
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                this.Game.Window.Close();
        }
        private void Window_Resized(object sender, SizeEventArgs e)
        {
            //resizes the view but would stretch/pixelate without
            //the adjustments below to rescale the background
            this._gameView.Size = new Vector2f(e.Width, e.Height);
            this._guiView.Size = new Vector2f(e.Width, e.Height);

            this.Game.Background.Position = this.Game.Window.MapPixelToCoords(new Vector2i(0, 0));
            this.Game.Background.Scale =
                new Vector2f((float)e.Width / (float)this.Game.Background.Texture.Size.X,
                             (float)e.Height / (float)this.Game.Background.Texture.Size.Y);
        }

        public void Draw(float dt)
        {
            this.Game.Window.Clear();
            this.Game.Window.Draw(this.Game.Background);
        }

        public void Update(float dt)
        {
        }
    }
}
