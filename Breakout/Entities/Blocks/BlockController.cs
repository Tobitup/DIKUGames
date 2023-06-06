using DIKUArcade.Entities;
using Breakout.Effect;
using Breakout.Levels;
using Breakout.PlayerScore;

namespace Breakout.Blocks;

public static class BlockController {

    /// <summary> Updates the blocks. </summary>
    public static void UpdateBlocks(Level currentLevel) {
        foreach (IBlock block in currentLevel.BlockContainer) {
                block.Update();
        }
    }

    /// <summary>
    /// Finds and removes dead blocks from the specified block container, while updating the score.
    /// </summary>
    /// <param name="blockContainer"> The container that holds the blocks. </param>
    /// <param name="effectsContainer"> The container that holds the effects. </param>
    /// <param name="levelScore"> The score object representing the level score. </param>
    public static void FindAndRemoveDeadBlocks(EntityContainer<Entity> blockContainer,
                                                EntityContainer<Entity> effectsContainer, 
                                                Score levelScore) {
        blockContainer.Iterate(block => {
            var currentBlock = block as IBlock;
            if (currentBlock.IsDead()) {
                levelScore.IncrementScore(currentBlock.Value);
                EffectController.SpawnEffect(blockContainer,effectsContainer);
                block.DeleteEntity();
            }
        });
    }
}
