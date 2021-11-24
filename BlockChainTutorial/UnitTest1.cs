using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockChainConsoleApp;
using System.Security.Policy;
using System.Collections.Generic;

namespace BlockChainTutorial
{

    [TestClass]
    public class UnitTest1
    {
        IList<Transaction> PendingTransactions = new List<Transaction>();
        public int Difficulty { get; set; } = 2;

        [TestMethod]
        public void ShouldCreateBlockClass()
        {
            Block block = new Block(DateTime.Now, null, transactions: PendingTransactions);

            block.Mine(Difficulty);
            PendingTransactions = new List<Transaction>();

            //Assert
            Assert.IsNotNull(block);

        }

        [TestMethod]
        public void ShouldCreateBlockChainClass()
        {
            BlockChain blockChain = new BlockChain();
            //Assert
            Assert.IsNotNull(blockChain);
        }

        [TestMethod]
        public void ShouldCreateShortBlockChain()
        {
            BlockChain phillyCoin = new BlockChain();
            phillyCoin.CreateTransaction(new Transaction("Mahesh", "Henry", 10));

            //Assert
            Assert.IsNotNull(phillyCoin);
        }

        [TestMethod]
        public void LatestHashAndPreviousHashShouldBeEqual()
        {
            BlockChain phillyCoin = new BlockChain();
            phillyCoin.CreateTransaction(new Transaction("Mahesh", "Henry", 10));
            phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            phillyCoin.ProcessPendingTransactions("Bill");

            string genesisHashCode = phillyCoin.Chain[0].Hash;
            string nextHashCode = phillyCoin.Chain[1].PreviousHash;

            //Assert
            Assert.AreEqual(genesisHashCode, nextHashCode);
        }
    }
}
