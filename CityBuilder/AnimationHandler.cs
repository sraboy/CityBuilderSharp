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
    public class AnimationHandler
    {
        #region Private Fields
        private List<Animation> _animations;
        private float _elapsed;
        private int _currentAnim;
        private IntRect _bounds;
        private IntRect _frameSize;
        #endregion

        #region Public Properties
        public List<Animation> Animations
        {
            get
            {
                return _animations;
            }

            set
            {
                this._animations = value;
            }
        }
        public IntRect FrameSize
        {
            get
            {
                return _frameSize;
            }

            set
            {
                this._frameSize = value;
            }
        }
        public IntRect Bounds
        {
            get
            {
                return _bounds;
            }

            set
            {
                this._bounds = value;
            }
        }
        #endregion

        #region Constructors & Initialization
        public AnimationHandler()
        {
            Initialize();
        }
        public AnimationHandler(IntRect frameSize)
        {
            Initialize();
            this.FrameSize = frameSize;
        }
        private void Initialize()
        {
            this.Animations = new List<Animation>();
            this._elapsed = 0;
            this._currentAnim = -1;
        }
        #endregion

        /// <summary>
        /// Update the current frame of animation.
        /// </summary>
        /// <param name="dt">The time since Update was last called (i.e., the time for one frame to be executed)</param>
        public void Update(float dt)
        {
            if (this._currentAnim >= this.Animations.Count || this._currentAnim < 0)
                return;

            float duration = this.Animations[this._currentAnim].Duration;

            //If animation has moved to a new frame, change to the next frame
            if ((this._elapsed + dt) / duration > (this._elapsed / duration))
            {
                //calculate the frame number
                int frame = (int)((this._elapsed + dt) / duration);
                frame %= this.Animations[this._currentAnim].Length;

                //set the sprite to the new frame
                var rect = this.FrameSize;
                rect.Left = rect.Width * frame;
                rect.Top = rect.Height * this._currentAnim;
                this.Bounds = rect;
            }

            //increment time elapsed
            this._elapsed += dt;

            if (this._elapsed > duration * this.Animations[this._currentAnim].Length)
                this._elapsed = 0;
        }
        public void ChangeAnimation(int animNum)
        {
            //don't chage if the animation is active or the new one doesn't exist
            if (this._currentAnim == animNum || animNum >= this.Animations.Count || animNum < 0)
                return;

            this._currentAnim = animNum;

            //update the bounds
            var rect = this.FrameSize;
            rect.Top = animNum * rect.Height;
            this.Bounds = rect;

            this._elapsed = 0;
        }
    }
}
