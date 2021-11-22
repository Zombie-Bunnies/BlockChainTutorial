using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockChainConsoleApp;
using System.Security.Policy;

namespace BlockChainTutorial
{

    [TestClass]
    public class UnitTest1
    {
        readonly Block block = new Block();
        //private DateTime timeStamp;
        //private readonly string previousHash;

        [TestMethod]
        public void ShouldCreateBlockChainClass()
        {
            Block block = new Block();

            //Assert
            Assert.IsNotNull(block);

        }

        [TestMethod]
        public void ShouldCreateBlockClass()
        {
            block.Index = 0;
            block.TimeStamp = DateTime.Now;
            block.PreviousHash = "previousHash";
            block.Hash = "abc";
            block.Data = "def";

            //Assert


        }
    }
}
