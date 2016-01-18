using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.System;

namespace CityBuilder
{
    public class Map
    {
        #region Private Fields
        private int _width;
        private int _height;
        private List<Tile> _tiles;
        private List<int> _resources;
        private int _tileSize;
        private int _numSelected;
        private int[] _numRegions = new int[1];
        #endregion

        #region Public Properties
        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                this._width = value;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                this._height = value;
            }
        }
        public List<Tile> Tiles
        {
            get
            {
                return _tiles;
            }

            set
            {
                this._tiles = value;
            }
        }
        public List<int> Resources
        {
            get
            {
                return _resources;
            }

            set
            {
                this._resources = value;
            }
        }
        public int TileSize
        {
            get
            {
                return _tileSize;
            }

            set
            {
                this._tileSize = value;
            }
        }
        public int[] NumRegions
        {
            get
            {
                return _numRegions;
            }

            set
            {
                this._numRegions = value;
            }
        }
        #endregion

        #region Constructors
        public Map()
        {
            this.TileSize = 8;
            this.Width = 0;
            this.Height = 0;
            this.NumRegions[0] = 1;
        }
        public Map(string fileName, int width, int height, Dictionary<string, Tile> tileAtlas)
        {
            this.TileSize = 8;
            Load(fileName, width, height, tileAtlas);
        }
        #endregion

        /// <summary>
        /// Loads a map file from disk
        /// </summary>
        /// <param name="fileName">The map file to load</param>
        /// <param name="width">The width, in tiles, of the map</param>
        /// <param name="height">The height, in tiles, of the map</param>
        /// <param name="tileAtlas">The actual map data</param>
        public void Load(string fileName, int width, int height, Dictionary<string, Tile> tileAtlas)
        {
            this.Width = width;
            this.Height = height;

            using (FileStream fs = File.Open(fileName, FileMode.Open))
            {
                for (int pos = 0; pos < this.Width * this.Height; pos++)
                {
                    this.Resources.Add(255);

                    TileType tileType;
                    byte[] tileBytes = new byte[sizeof(int)];
                    fs.Read(tileBytes, 0, sizeof(int));
                    tileType = (TileType)BitConverter.ToInt32(tileBytes, 0);

                    Tile tile = tileAtlas[tileType.ToString().ToLower()];

                    fs.Read(tileBytes, 0, sizeof(int));
                    tile.TileVariant = BitConverter.ToInt32(tileBytes, 0);

                    fs.Read(tileBytes, 0, sizeof(int));
                    tile.Regions[0] = BitConverter.ToInt32(tileBytes, 0);

                    tileBytes = new byte[sizeof(double)];
                    fs.Read(tileBytes, 0, sizeof(double));
                    tile.Population = BitConverter.ToDouble(tileBytes, 0);

                    tileBytes = new byte[sizeof(float)];
                    fs.Read(tileBytes, 0, sizeof(float));
                    tile.StoredGoods = BitConverter.ToSingle(tileBytes, 0);

                    this.Tiles.Add(tile);
                }
            }
        }
        public void Save(string fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.Open))
            {
                foreach (var tile in this.Tiles)
                {
                    fs.Write(BitConverter.GetBytes((int)tile.TileType), 0, sizeof(int));
                    fs.Write(BitConverter.GetBytes(tile.TileVariant), 0, sizeof(int));
                    fs.Write(BitConverter.GetBytes(tile.Regions[0]), 0, sizeof(int));
                    fs.Write(BitConverter.GetBytes(tile.Regions[1]), 0, sizeof(int));
                    fs.Write(BitConverter.GetBytes(tile.Population), 0, sizeof(double));
                    fs.Write(BitConverter.GetBytes(tile.StoredGoods), 0, sizeof(float));
                }
            }
        }
        public void Draw(RenderWindow window, float dt)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Height; x++)
                {
                    //set the tile's position in the 2D world
                    Vector2f pos = new Vector2f();
                    pos.X = ((x - y) * this.TileSize) + (this.Width * this.TileSize);
                    pos.Y = (x + y) * this.TileSize * 0.5f;
                    this.Tiles[y * this.Width + x].Sprite.Position = pos;

                    //draw it
                    this.Tiles[y * this.Width + x].Draw(window, dt);
                }
            }
        }
        public void FindConnectedRegions(List<TileType> whitelist, int type)
        {

        }
        public void UpdateDirection(TileType tileType)
        {

        }

        private void DepthFirstSearch(List<TileType> whitelist, Vector2i pos, int label, int type)
        {

        }
    }
}
