// <copyright file="BricksDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 16:23</date>

using System.Collections.Generic;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// BricksDrawer - a component that responsible for drawing group of game bricks.
    /// </summary>
    public class BricksDrawer : GameBehaviour
    {
        //================================       Public Setup       =================================
        [SerializeField] [Tooltip("Prefab of the brick")]
        // ReSharper disable once InconsistentNaming
        protected Brick _brickPrefab;

        [SerializeField] [Tooltip("Content holder")]
        // ReSharper disable once InconsistentNaming
        protected Transform _content;

        /// <inheritdoc />
        public Brick BrickPrefab
        {
            get { return _brickPrefab; }
            set { _brickPrefab = value; }
        }

        //================================    Systems properties    =================================
        /// <summary>
        /// Flag of the component validity
        /// </summary>
        private bool _valid;

        //================================      Public methods      =================================
        public bool Validate()
        {
            //Validate component fields
            if (_brickPrefab == null)
            {
                Debug.LogError("BrickPrefab is null");
                enabled = false;
                return _valid;
            }

            if (_content == null)
            {
                Debug.LogError("Content is null");
                enabled = false;
                return _valid;
            }
            _valid = true;
            return _valid;
        }

        /// <summary>
        /// Destroy all briks instances in content holder container
        /// </summary>
        public void DestroyBricks()
        {
            if (!_valid) return;
            //Save desctruction of game objects
            int i = _content.childCount;
            Queue<GameObject> children = new Queue<GameObject>();
            while (--i >= 0) children.Enqueue(_content.GetChild(i).gameObject);
            while (children.Count > 0) Destroy(children.Dequeue());
        }
        //================================ Private|Protected methods ================================

        /// <summary>
        /// Draw bricks
        /// </summary>
        /// <param name="width">Width of the bricks maxtrix</param>
        /// <param name="height">Height of the bricks matrix</param>
        /// <returns>new bricks matrix</returns>
        protected Brick[] DrawBricks(int width, int height)
        {
            return DrawBricks(width, height, Vector3.one);
        }

        /// <summary>
        /// Draw bricks
        /// </summary>
        /// <param name="width">Width of the bricks maxtrix</param>
        /// <param name="height">Height of the bricks matrix</param>
        /// <param name="scale">Local scale</param>
        /// <returns>new bricks matrix</returns>
        protected Brick[] DrawBricks(int width, int height, Vector3 scale)
        {
            if (!_valid) return new Brick[0];
            Vector3 offset = new Vector3();
            float bw = _brickPrefab.Size.x * scale.x;
            float bh = _brickPrefab.Size.y * scale.y;


            Brick[] bricks = new Brick[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var instance = Instantiate(_brickPrefab.gameObject);
                    instance.name = "Brick(" + x + ":" + y + ")";
                    var brick = instance.GetComponent<Brick>();
                    instance.transform.SetParent(_content, true);
                    instance.transform.localScale = scale;
                    brick.Active = false;
                    brick.X = x;
                    brick.Y = y;


                    offset.x = x * bw;
                    offset.y = y * -bh;
                    SetBrickPosition(instance.transform, offset);
                    instance.isStatic = true;
                    bricks[x + y * width] = brick;
                }
            }
            return bricks;
        }

        /// <summary>
        /// Set position of a brick
        /// </summary>
        /// <param name="brick">Instance of a brick</param>
        /// <param name="offset">Offset from top left corner of the component</param>
        protected virtual void SetBrickPosition(Transform brick, Vector3 offset)
        {
            brick.position = transform.position + offset;
        }

        protected Brick[] RestoreBricks(int width, int height)
        {
            return RestoreBricks(width, height, Vector3.one);
        }

        protected Brick[] RestoreBricks(int width, int height, Vector3 scale)
        {
            int n = width * height;
            if (_content.childCount != n)
            {
                DestroyBricks();
                return DrawBricks(width, height, scale);
            }
            Brick[] bricks = new Brick[n];
            for (int i = 0; i < n; i++)
            {
                Brick brick = _content.GetChild(i).GetComponent<Brick>();
                bricks[brick.X + brick.Y * width] = brick;
            }
            return bricks;
        }


    }
}