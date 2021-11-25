﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BlockChainConsoleApp;
using System.Security.Policy;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

        [TestMethod]
        public void ShouldCreateHashOfABC_ABD()
        {
            int nonce = 0;

            SHA256 sHA256 = SHA256.Create();
            byte[] inputBytes_ABC = Encoding.ASCII.GetBytes("ABC");
            byte[] outputBytes_ABC = sHA256.ComputeHash(inputBytes_ABC);

            //byte[] inputBytes_ABD = Encoding.ASCII.GetBytes("ABD");
            //byte[] outputBytes_ABD = sHA256.ComputeHash(inputBytes_ABD);

            //string ABC_Hash = Convert.ToBase64String(outputBytes_ABC);

            //string ABD_Hash = Convert.ToBase64String(outputBytes_ABD);

            for (int i = 0; i < 200000; i++)
            {
                byte[] inputBytes_ABC_PlusNonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
                byte[] outputBytes_ABC_PlusNonce = sHA256.ComputeHash(inputBytes_ABC_PlusNonce);

                string ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_PlusNonce);

                //bool originalHash = ABC_Hash.StartsWith("t");

                bool checkABC_For_0 = ABC_PlusNonce.StartsWith("000");

                if (checkABC_For_0)
                {
                    string message = "nonce is " + nonce;
                    // winner winner chicken dinner
                    Assert.IsFalse(checkABC_For_0, message);
                }
                nonce++;
            }

        }
    }
}
