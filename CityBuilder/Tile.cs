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
using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace CityBuilder
{
    public class Tile
    {
        #region Private Fields
        private AnimationHandler _animationHandler;
        private Sprite _sprite;
        private TileType _tileType;
        private int _tileVariant;
        private int[] _regions = new int[1];
        private int _cost;
        private double _population;
        private int _maxPopulationPerLevel;
        private int _maxLevels;
        private float _production;
        private float _storedGoods;
        private Random _rand = new Random(DateTime.Now.Second);
        #endregion

        #region Public Properties
        public AnimationHandler AnimationHandler
        {
            get
            {
                return _animationHandler;
            }

            set
            {
                this._animationHandler = value;
            }
        }
        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }

            set
            {
                this._sprite = value;
            }
        }
        public TileType TileType
        {
            get
            {
                return _tileType;
            }

            set
            {
                this._tileType = value;
            }
        }
        public int TileVariant
        {
            get
            {
                return _tileVariant;
            }

            set
            {
                this._tileVariant = value;
            }
        }
        public int[] Regions
        {
            get
            {
                return _regions;
            }

            set
            {
                this._regions = value;
            }
        }
        public int Cost
        {
            get
            {
                return _cost;
            }

            set
            {
                this._cost = value;
            }
        }
        public double Population
        {
            get
            {
                return _population;
            }

            set
            {
                this._population = value;
            }
        }
        public int MaxPopulationPerLevel
        {
            get
            {
                return _maxPopulationPerLevel;
            }

            set
            {
                this._maxPopulationPerLevel = value;
            }
        }
        public int MaxLevels
        {
            get
            {
                return _maxLevels;
            }

            set
            {
                this._maxLevels = value;
            }
        }
        public float Production
        {
            get
            {
                return _production;
            }

            set
            {
                this._production = value;
            }
        }
        public float StoredGoods
        {
            get
            {
                return _storedGoods;
            }

            set
            {
                this._storedGoods = value;
            }
        }
        #endregion

        public Tile()
        {
        }
        public Tile(int tileSize, int height, Texture texture, List<Animation> animations,
                    TileType tileType, int cost, int maxPopulationPerLevel, int maxLevels)
        {
            this.TileType = tileType;
            this.TileVariant = 0;
            this.Regions[0] = 0;

            this.Cost = cost;
            this.Population = 0;
            this.MaxPopulationPerLevel = maxPopulationPerLevel;
            this.MaxLevels = maxLevels;
            this.Production = 0;
            this.StoredGoods = 0;

            this.Sprite.Origin = new Vector2f(0, tileSize * (height - 1));
            this.Sprite.Texture = texture;
            this.AnimationHandler.FrameSize = new IntRect(0, 0, tileSize * 2, tileSize * height);

            this.AnimationHandler.Animations.AddRange(animations);
            this.AnimationHandler.Update(0);
        }

        public void Draw(RenderWindow window, float dt)
        {
            this.AnimationHandler.ChangeAnimation(this.TileVariant);
            this.AnimationHandler.Update(dt);
            this.Sprite.TextureRect = this.AnimationHandler.Bounds;
            window.Draw(this.Sprite);
        }
        public void Update()
        {
            //if the population is at the max value for the tile,
            //there's a small chance the tile will increase it's building stage

            switch (this.TileType)
            {
                case TileType.Residential:
                case TileType.Commercial:
                case TileType.Industrial:
                    if (this.Population == this.MaxPopulationPerLevel * (this.TileVariant + 1) &&
                        this.TileVariant < this.MaxLevels)
                    {

                        if (this._rand.Next() % 1e4 < 1e2 / (this.TileVariant + 1))
                            this.TileVariant++;
                    }
                    break;
            }
        }
    }
}
