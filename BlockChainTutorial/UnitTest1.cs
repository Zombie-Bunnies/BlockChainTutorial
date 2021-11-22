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
        readonly BlockChain blockChain = new BlockChain();

        [TestMethod]
        public void ShouldCreateBlockClass()
        {
            block.Index = 0;
            block.TimeStamp = DateTime.Now;
            block.PreviousHash = "previousHash";
            block.Hash = "abc";
            block.Data = "def";

            //Assert
            Assert.IsNotNull(block);

        }

        //[TestMethod]
        //public void ShouldCreateBlockChainClass()
        //{

        //}
    }
}
