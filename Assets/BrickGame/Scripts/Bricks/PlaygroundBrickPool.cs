using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    [AddComponentMenu("BrickGame/Pools/Playground Bricks")]
    public sealed class PlaygroundBrickPool : AbstractBricksPool<SpriteRenderer>
    {
        /// <inheritdoc />
        public override bool Image
        {
            get { return false; }
        }
    }
}