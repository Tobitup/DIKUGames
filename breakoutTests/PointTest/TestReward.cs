
using Breakout.Score;

namespace breakoutTests.TestStateMachine;

[TestFixture]
public class TestReward {

    [SetUp]
    public void TestReward() {

    }

    [Test]
    public void TestStartingScore() {
    /// ARANGE
    Score score = new Score();

    /// ACT
    /// ASSERT
    Assert.That(score.GetCurrentScore, (uint) 1);
    }

    [TestCase(1, 1)]
    [TestCase(4, 4)]
    [TestCase(100, 100)]
    public void TestIncrementingScore(uint value, uint expectedScore) {
    /// ARANGE
    Score score = new Score();

    /// ACT
    score.IncrementScore(value);
    /// ASSERT
    Assert.That(score.GetCurrentScore, expectedScore);
    }

    [Test]
    public void TestNegativeScore() {
    /// ARANGE
    Score score = new Score();

    /// ACT
    score.IncrementScore(-1);
    /// ASSERT // IDK FEJLER CHECK AT DEN FEJLER MED IKKE UINT
    Assert.That(score.GetCurrentScore, (uint) 0);
    }

    [Test]
    public void TestScoreReset() {
    /// ARANGE
    Score score = new Score();

    /// ACT
    score.IncrementScore((uint) 5);
    score.ResetScore();
    /// ASSERT // IDK FEJLER CHECK AT DEN FEJLER MED IKKE UINT
    Assert.That(score.GetCurrentScore, (uint) 5);
    }

    
}
