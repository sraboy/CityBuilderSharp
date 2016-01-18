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
using SFML.Graphics;

namespace CityBuilder
{
    public class TextureManager
    {
        private Dictionary<string, Texture> _textures;

        public Dictionary<string, Texture> Textures
        {
            get
            {
                return _textures;
            }

            private set
            {
                this._textures = value;
            }
        }

        public TextureManager()
        {
            this._textures = new Dictionary<string, Texture>();
        }

        public void LoadTexture(string name, string fileName)
        {
            Texture tex = new Texture(fileName);
            this.Textures.Add(name, tex);
        }
    }
}
