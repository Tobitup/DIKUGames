using DIKUArcade.Entities;
using Breakout.Effect;
using Breakout.Levels;
using Breakout.PlayerScore;

namespace Breakout.Blocks;

public static class BlockController {

    /// <summary> Updates the blocks. </summary>
    /// <returns> Void. </returns>
    public static void UpdateBlocks(Level currentLevel) {
        foreach (IBlock block in currentLevel.BlockContainer) {
                block.Update();
        }
    }

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
