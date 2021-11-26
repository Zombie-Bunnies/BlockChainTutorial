using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            int difficulty = 3;
            var leadingZeros = new string('0', difficulty);

            SHA256 sHA256 = SHA256.Create();
            byte[] inputBytes_ABC = Encoding.ASCII.GetBytes("ABC");
            byte[] outputBytes_ABC = sHA256.ComputeHash(inputBytes_ABC);

            //byte[] inputBytes_ABD = Encoding.ASCII.GetBytes("ABD");
            //byte[] outputBytes_ABD = sHA256.ComputeHash(inputBytes_ABD);

            //string ABC_Hash = Convert.ToBase64String(outputBytes_ABC);

            //string ABD_Hash = Convert.ToBase64String(outputBytes_ABD);
            var startTime = DateTime.Now;

            for (int i = 0; i < 900000000; i++)
            {
                byte[] inputBytes_ABC_PlusNonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
                byte[] outputBytes_ABC_PlusNonce = sHA256.ComputeHash(inputBytes_ABC_PlusNonce);

                string ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_PlusNonce);

                //bool originalHash = ABC_Hash.StartsWith("t");

                bool checkABC_For_leadingZeros = ABC_PlusNonce.StartsWith(leadingZeros);

                if (checkABC_For_leadingZeros)
                {
                    var endTime = DateTime.Now;
                    TimeSpan elapsedTimeInSeconds = endTime - startTime;
                    double mSeconds = elapsedTimeInSeconds.TotalSeconds;
                    string nonceValue = nonce.ToString();
                    
                    // winner winner chicken dinner nonce = 135819

                    Assert.IsFalse(checkABC_For_leadingZeros, mSeconds + " " + nonceValue);
                }
                nonce++;
            }

        }

        [TestMethod]
        public void ShouldTestTheRotor()
        {
            //int nonce = 0;
            int difficulty = 3;
            var leadingZeros = new string('0', difficulty);
            var startTime = DateTime.Now;
            Random random = new Random();
            int nonce = random.Next(0, 900000000);
            SHA256 sHA256 = SHA256.Create();
            byte[] inputBytes_ABC = Encoding.ASCII.GetBytes("ABC");


            byte[] inputBytes_ABC_PlusNonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
            byte[] outputBytes_ABC_PlusNonce = sHA256.ComputeHash(inputBytes_ABC_PlusNonce);

            string ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_PlusNonce);

            // nonce should be 135819
            bool check_ABC_PlusNonce_For_leadingZeros = ABC_PlusNonce.StartsWith(leadingZeros);

            while (!check_ABC_PlusNonce_For_leadingZeros)
            {
                nonce = random.Next(0, 900000000);
                inputBytes_ABC_PlusNonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
                outputBytes_ABC_PlusNonce = sHA256.ComputeHash(inputBytes_ABC_PlusNonce);

                ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_PlusNonce);

                check_ABC_PlusNonce_For_leadingZeros = ABC_PlusNonce.StartsWith(leadingZeros);
            }

            if (check_ABC_PlusNonce_For_leadingZeros)
            {
                var endTime = DateTime.Now;
                TimeSpan elapsedTimeInSeconds = endTime - startTime;
                double mSeconds = elapsedTimeInSeconds.TotalSeconds;
                string nonceValue = nonce.ToString();
                Assert.IsFalse(check_ABC_PlusNonce_For_leadingZeros, mSeconds + " " + nonceValue);
            }
        }

        [TestMethod]
        public void TestForSpecificNonce()
        {
            //int nonce = 277555444;
            //int nonce = 865140105;
            int nonce = 135819;
            int difficulty = 3;
            var leadingZeros = new string('0', difficulty);
            SHA256 sHA256 = SHA256.Create();

            byte[] inputBytes_ABC = Encoding.ASCII.GetBytes("ABC");
            byte[] outputBytes_ABC = sHA256.ComputeHash(inputBytes_ABC);

            string outputBytes_ABC_Hash = Convert.ToBase64String(outputBytes_ABC);

            byte[] inputBytes_ABCPlusNonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
            byte[] outputBytes_ABCPlusNonce = sHA256.ComputeHash(inputBytes_ABCPlusNonce);

            string ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABCPlusNonce);

            bool check_ABC_PlusNonce_For_leadingZeros = ABC_PlusNonce.StartsWith(leadingZeros);

            Assert.IsFalse(check_ABC_PlusNonce_For_leadingZeros);
        }

        [TestMethod]
        public void ShouldCountNumberOfNonceForHash()
        {
            int nonce = 0;
            int difficulty = 3;
            var leadingZeros = new string('0', difficulty);
            int numberOfNonceToCheck = 10000000;
            bool checkABC_For_LeadingZeros = false;
            int numberOfFoundNonce = 0;
            IList<int> listOfNonce = new List<int>();

            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes_ABC = Encoding.ASCII.GetBytes("ABC");
            byte[] inputBytes_ABC_Plus_Nonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
            byte[] outputBytes_ABC_Plus_Nonce = sha256.ComputeHash(inputBytes_ABC_Plus_Nonce);

            string ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_Plus_Nonce);

            var startTime = DateTime.Now;

            for (int i = 0; i < numberOfNonceToCheck; i++)
            {
                inputBytes_ABC_Plus_Nonce = Encoding.ASCII.GetBytes($"{inputBytes_ABC}-{nonce}");
                outputBytes_ABC_Plus_Nonce = sha256.ComputeHash(inputBytes_ABC_Plus_Nonce);
                ABC_PlusNonce = Convert.ToBase64String(outputBytes_ABC_Plus_Nonce);

                checkABC_For_LeadingZeros = ABC_PlusNonce.StartsWith(leadingZeros);

                if (checkABC_For_LeadingZeros)
                {
                    listOfNonce.Add(nonce);
                }
                nonce++;
            }

            Assert.AreEqual(numberOfFoundNonce, listOfNonce.Count);

        }
    }
}
