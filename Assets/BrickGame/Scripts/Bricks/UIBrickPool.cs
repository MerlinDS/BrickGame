using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.Bricks
{
    [AddComponentMenu("BrickGame/Pools/Images Bricks")]
    // ReSharper disable once InconsistentNaming
    public sealed class UIBrickPool : AbstractBricksPool<Image>
    {
        /// <inheritdoc />
        public override bool Image
        {
            get { return true; }
        }
    }
}