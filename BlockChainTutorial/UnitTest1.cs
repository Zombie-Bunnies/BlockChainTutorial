using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockChainConsoleApp;
using System.Security.Policy;

namespace BlockChainTutorial
{

    [TestClass]
    public class UnitTest1
    {
        readonly Block block = new Block(DateTime.Now, null, "");
        readonly BlockChain blockChain = new BlockChain();

        //public BlockChain BlockChain => blockChain;

        [TestMethod]
        public void ShouldCreateBlockClass()
        {
            block.Index = 1;
            block.TimeStamp = DateTime.Now;
            block.PreviousHash = "previousHash";
            block.Hash = "abc";
            block.Data = "def";

            //Assert
            Assert.IsNotNull(block);

        }

        [TestMethod]
        public void ShouldCreateBlockChainClass()
        {
            //block.Index = 2;
            //blockChain.InitializeChain();
            blockChain.AddBlock(block);
            Assert.IsNotNull(blockChain);
        }

        [TestMethod]
        public void ShouldCreateShortBlockChain()
        {
            BlockChain phillyCoin = new BlockChain();
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

            //Assert
            Assert.IsNotNull(phillyCoin);
        }

        [TestMethod]
        public void LatestHashAndPreviousHashShouldBeEqual()
        {
            BlockChain phillyCoin = new BlockChain();
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

            string genesisHashCode = phillyCoin.Chain[0].Hash;
            string nextHashCode = phillyCoin.Chain[1].PreviousHash;

            //Assert
            Assert.AreEqual(genesisHashCode, nextHashCode);
        }
    }
}
