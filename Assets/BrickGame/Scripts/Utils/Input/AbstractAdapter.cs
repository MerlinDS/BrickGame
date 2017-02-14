// <copyright file="AbstractAdapter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 18:00</date>

using UnityEngine;

namespace BrickGame.Scripts.Utils.Input
{
    /// <summary>
    /// AbstractAdapter - abstract class of inpunt adapter.
    /// </summary>
    public abstract class AbstractAdapter : IInputAdapter
    {
        private static readonly float Slop = Mathf.Round(20F / 252F * Screen.dpi);
        private static readonly float SlopPow = Slop * Slop;
        private static readonly float SwipeTime = 0.3F;

        private static readonly float TapTime = 2F;
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private float _startTime;

        private Vector2 _start;
        private Vector2 _previous;
        private Vector2 _position;

        private Touch _touch;

        //================================      Public methods      =================================
        /// <inheritdoc />
        protected AbstractAdapter()
        {
            _touch = new Touch {phase = TouchPhase.Ended};
            _previous = _position = new Vector2();
        }

        /// <inheritdoc />
        public bool Detect(out Touch touch)
        {
            InputGesture gesture;
            return Detect(out touch, out gesture);
        }

        /// <inheritdoc />
        public bool Detect(out Touch touch, out InputGesture gesture)
        {
            gesture = InputGesture.None;
            bool result = HasTouch(ref _position);
            //Update touch
            if (result && _touch.phase == TouchPhase.Ended)
            {
                _touch.phase = TouchPhase.Began;
                //clean previous
                _start.x = _previous.x = _position.x;
                _start.y = _previous.y = _position.y;
                _startTime = Time.realtimeSinceStartup;

            }else if (result && _touch.phase == TouchPhase.Began)
            {
                _touch.phase = TouchPhase.Moved;
            }
            else if (!result && (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Began) )
            {
                _touch.phase = TouchPhase.Ended;
                Vector2 delta = _position - _start;
                if (delta.sqrMagnitude <= SlopPow)
                {
                    if (Time.realtimeSinceStartup - _startTime < TapTime)
                        gesture = InputGesture.Tap;
                }
                else
                {
                    if (Time.realtimeSinceStartup - _startTime < SwipeTime)
                    {
                        delta.Normalize();
                        //swipe down
                        if (delta.y < 0 && delta.x > -0.5f && delta.x < 0.5f)
                            gesture = InputGesture.Swipe;
                    }
                }
                result = true;
            }

            _touch.position = _position;
            _touch.deltaPosition = _previous - _position;
            _previous = _position;
            touch = _touch;
            return result;
        }

        /// <inheritdoc />
        public void Reset()
        {
            _touch.phase = TouchPhase.Ended;
        }

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Check if touch was occure
        /// </summary>
        /// <param name="position">Position of the touch if it was occure</param>
        /// <returns>True if touch was occure, false in other case</returns>
        protected abstract bool HasTouch(ref Vector2 position);
    }
}