using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace enstam_anna_labIII.UnitTests
{
    [TestClass]
    public class GameTests
    {
        string inputDepth2 = "NW.CC , NC.CC , NW.NW , NE.CC , NW.SE , CE.CC , CW.CC , SE.CC , CW.NW , CC.CC , CW.SE , CC.NW , CC.SE , CE.NW , SW.CC , CE.SE , SW.NW , SE.SE , SW.SE";
        string inputDepth3 = "NW.NW.NW, NE.NE.NC,NW.SW.CW ,NE.NE.CC,NW.NW. SW,NE.NE.SC,NW.CW.NW, NE.CE.NC, NW.CW.CW ,NE.CE.CC,NW.CW. SW,NE.CE.SC,NW.SW.NW, NE.SE.NC, NW.NW.CW ,NE.SE.CC,NW.SW. SW, NE.SE.SC,CC.NW.NW,CE.NC.NC,CC.NW.CW, CE.NC.CC,CC.NW.SW,CE.NC.SC,CC.CW.NW,CE.CC.NC,CC.CW.CW,CE.CC.CC, CC.CW.SW,CE.CC.SC,CC.SW.NW,CE.SC.NC,CC.SW.CW,CE.SC.CC,CC.SW.SW, CE.SC.SC,SE.NC.NC,SW.NW.NC,SE.NC.CC,SW.NW.CC,SE.NC.SC,SW.NW.SC, SE.CC.NC,SW.CW.NC,SE.CC.CC,SW.CW.CC,SE.CC.SC,SW.CW.SC,SE.SC.NC, SW.SW.NC,SE.SC.CC,SW.SW.CC,SE.SC.SC,SW.SW.SC,NW.CC.CC";

        [TestMethod]
        public void GetWinningOrderLarge_GetCorrectOutput1Depth2_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth2));

            //Act
            string result = string.Join(", ", game.GameTreeRoot.GetWinningOrderLarge());

            //Assert
            string expectedOutput = "NW, CW, SW";
            Assert.AreEqual<string>(expectedOutput, result);
        }

        [TestMethod]
        public void GetWinningOrderLarge_GetCorrectOutput1Depth3_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth3));

            //Act
            string result = string.Join(", ", game.GameTreeRoot.GetWinningOrderLarge());

            //Assert
            string expectedOutput = "NW, CC, SE";
            Assert.AreEqual<string>(expectedOutput, result);
        }

        [TestMethod]
        public void GetWinningOrderSmall_GetCorrectOutput2Depth2_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth2));

            //Act
            string result = game.GetWinningOrderSmall();

            //Assert
            string expectedOutput = "NW.CC, NW.NW, NW.SE, CW.CC, CW.NW, CW.SE, SW.CC, SW.NW, SW.SE";
            Assert.AreEqual<string>(expectedOutput, result);
        }

        [TestMethod]
        public void GetWinningOrderSmall_GetCorrectOutput2Depth3_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth3));

            //Act
            string result = game.GetWinningOrderSmall();

            //Assert
            string expectedOutput = "NW.NW.NW, NW.SW.CW, NW.NW.SW, NW.CW.NW, NW.CW.CW, NW.CW.SW, NW.SW.NW, NW.NW.CW, NW.SW.SW, CC.NW.NW, CC.NW.CW, CC.NW.SW, CC.CW.NW, CC.CW.CW, CC.CW.SW, CC.SW.NW, CC.SW.CW, CC.SW.SW, SE.NC.NC, SE.NC.CC, SE.NC.SC, SE.CC.NC, SE.CC.CC, SE.CC.SC, SE.SC.NC, SE.SC.CC, SE.SC.SC";
            Assert.AreEqual<string>(expectedOutput, result);
        }

        [TestMethod]
        public void GetWinningCount_GetCorrectOutput3Depth2_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth2));

            //Act
            string result = game.GetWinningCount();

            //Assert
            string expectedOutput = "Winning count O, X: 1.3, 0.1";
            Assert.AreEqual<string>(expectedOutput, result);
        }

        [TestMethod]
        public void GetWinningCount_GetCorrectOutput3Depth3_ReturnsTrue()
        {
            //Arrange
            var parser = new ParseInput();
            Game game;
            GameBuilder gameBuilder = new GameBuilder();
            GameDirector gameDirector = new GameDirector(gameBuilder);
            game = gameDirector.CreateGame(parser.ParseGameInput(inputDepth3));

            //Act
            string result = game.GetWinningCount();

            //Assert
            string expectedOutput = "Winning count O, X: 1.3.9, 0.3.9";
            Assert.AreEqual<string>(expectedOutput, result);
        }
    }
}
