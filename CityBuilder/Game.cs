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
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;

namespace CityBuilder
{
    public class Game
    {
        private const int _tileSize = 8;

        #region Private Fields
        private Stack<IGameState> _states;
        private RenderWindow _window;
        private Sprite _background;
        private TextureManager _textureManager;
        private Dictionary<string, Tile> _tileAtlas;
        #endregion

        #region Public Properties
        public Stack<IGameState> States
        {
            get
            {
                return this._states;
            }

            private set
            {
                this._states = value;
            }
        }
        public RenderWindow Window
        {
            get
            {
                return this._window;
            }

            set
            {
                this._window = value;
            }
        }
        public Sprite Background
        {
            get
            {
                return this._background;
            }

            set
            {
                this._background = value;
            }
        }
        public TextureManager TextureManager
        {
            get
            {
                return _textureManager;
            }

            set
            {
                this._textureManager = value;
            }
        }
        public Dictionary<string, Tile> TileAtlas
        {
            get
            {
                return _tileAtlas;
            }

            set
            {
                this._tileAtlas = value;
            }
        }
        #endregion

        public Game()
        {
            this.States = new Stack<IGameState>();
            this.TextureManager = new TextureManager();
            this.Background = new Sprite();
            this.TileAtlas = new Dictionary<string, Tile>();

            this.LoadTextures();
            this.LoadTiles();

            this.Window = new RenderWindow(new VideoMode(800, 600), "City Builder");
            this.Window.SetFramerateLimit(60);

            this.Background.Texture = this.TextureManager.Textures["background"];
        }

        private void LoadTextures()
        {
            this.TextureManager.LoadTexture("background", "media/background.png");

            this.TextureManager.LoadTexture("commercial", "media/commercial.png");
            this.TextureManager.LoadTexture("industrial", "media/industrial.png");
            this.TextureManager.LoadTexture("residential", "media/residential.png");

            this.TextureManager.LoadTexture("forest", "media/forest.png");
            this.TextureManager.LoadTexture("grass", "media/grass.png");
            this.TextureManager.LoadTexture("water", "media/water.png");
            this.TextureManager.LoadTexture("road", "media/road.png");
        }

        public void GameLoop()
        {
            Stopwatch clock = new Stopwatch();

            while (this.Window.IsOpen)
            {
                var dt = clock.Elapsed.Seconds;
                clock.Restart();

                IGameState state = null;

                if (this.States.Count > 0)
                    state = this.States.Peek();
                else
                    continue;

                this.Window.DispatchEvents();
                state.Update(dt);


                this.Window.Clear();
                state.Draw(dt);
                this.Window.Display();
            }
        }
        public void ChangeState(IGameState state)
        {
            if (this.States.Count > 0)
                this.States.Pop();

            this.States.Push(state);
        }
        public void LoadTiles()
        {
            Animation staticAnim = new Animation(0, 0, 1);

            this.TileAtlas.Add("grass", new Tile(_tileSize, 1, this.TextureManager.Textures["grass"],
                                new List<Animation>() { staticAnim }, TileType.Grass, 50, 0, 1));

            this.TileAtlas.Add("forest", new Tile(_tileSize, 1, this.TextureManager.Textures["forest"],
                                new List<Animation>() { staticAnim }, TileType.Forest, 100, 0, 1));

            this.TileAtlas.Add("water", new Tile(_tileSize, 1, this.TextureManager.Textures["water"],
                                new List<Animation>() { new Animation(0, 3, 0.5f),
                                                        new Animation(0, 3, 0.5f),
                                                        new Animation(0, 3, 0.5f) },
                                TileType.Water, 0, 0, 1));

            this.TileAtlas.Add("residential", new Tile(_tileSize, 2, this.TextureManager.Textures["residential"],
                                new List<Animation>() { staticAnim, staticAnim, staticAnim,
                                                        staticAnim, staticAnim, staticAnim },
                                TileType.Residential, 300, 50, 6));

            this.TileAtlas.Add("commercial", new Tile(_tileSize, 2, this.TextureManager.Textures["commercial"],
                                new List<Animation>() { staticAnim, staticAnim,
                                                        staticAnim, staticAnim },
                                TileType.Commercial, 300, 50, 4));

            this.TileAtlas.Add("industrial", new Tile(_tileSize, 2, this.TextureManager.Textures["industrial"],
                                new List<Animation>() { staticAnim, staticAnim,
                                                        staticAnim, staticAnim },
                                TileType.Industrial, 300, 50, 4));

            this.TileAtlas.Add("road", new Tile(_tileSize, 1, this.TextureManager.Textures["road"],
                                new List<Animation>() { staticAnim, staticAnim, staticAnim,
                                                        staticAnim, staticAnim, staticAnim,
                                                        staticAnim, staticAnim, staticAnim,
                                                        staticAnim, staticAnim },
                                TileType.Road, 100, 0, 1));
        }
    }
}