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
namespace CityBuilder
{
    public class Animation
    {
        private int _startFrame;
        private int _endFrame;
        private float _duration;

        public int StartFrame
        {
            get
            {
                return this._startFrame;
            }

            set
            {
                this._startFrame = value;
            }
        }
        public int EndFrame
        {
            get
            {
                return this._endFrame;
            }

            set
            {
                this._endFrame = value;
            }
        }
        public float Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                this._duration = value;
            }
        }
        public int Length
        {
            get
            {
                return this.EndFrame - this.StartFrame + 1;
            }
        }

        public Animation(int startFrame, int endFrame, float duration)
        {
            this.StartFrame = startFrame;
            this.EndFrame = endFrame;
            this.Duration = duration;
        }
    }
}
