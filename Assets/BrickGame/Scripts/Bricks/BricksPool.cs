// <copyright file="BricksPool.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/21/2017 20:01</date>

using System;
using System.Reflection;
using JetBrains.Annotations;
using Lean;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    public interface IBricksSpriteChanger
    {
        bool Image { get; }
        void ChangeSprite(Sprite sprite);
    }
    /// <summary>
    /// BricksPool
    /// </summary>
    public abstract class AbstractBricksPool<T> : LeanPool, IBricksSpriteChanger where T : Component
    {
        private const string PropertyName = "sprite";
        //================================       Public Setup       =================================
        /// <inheritdoc />
        public abstract bool Image { get; }

        /// <inheritdoc />
        //================================    Systems properties    =================================
        private int _index;
        //================================      Public methods      =================================

        /// <inheritdoc />
        public void ChangeSprite(Sprite sprite)
        {
            if (Prefab.GetComponent<T>() == null)
            {
                Debug.LogError("Prefab doesn't contains valid component for sprite updating!");
                return;
            }
            UpdateInPool(sprite);
        }
        private void UpdateInPool(Sprite sprite)
        {
            Type type = Prefab.GetComponent<T>().GetType();
            PropertyInfo prop = type.GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);
            MethodInfo setMethod = prop.GetSetMethod(false);
            object[] parameters = {sprite};

            UpdateSprite(transform, setMethod, parameters);
            GameObject[] holders = GameObject.FindGameObjectsWithTag(SRTags.BrickHolder);
            foreach (GameObject holder in holders)
                UpdateSprite(holder.transform, setMethod, parameters);
        }

        //================================ Private|Protected methods ================================
        private void UpdateSprite([NotNull] Transform holder, [NotNull] MethodInfo method,
            [NotNull] object[] parameters)
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                var component = holder.GetChild(i).GetComponent<T>();
                if (component == null) continue;
                method.Invoke(component, parameters);
            }
        }
    }
}