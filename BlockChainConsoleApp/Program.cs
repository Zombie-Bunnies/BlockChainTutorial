using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;

            BlockChain phillyCoin = new BlockChain();
            //phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            //phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}}"));
            //phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));
            phillyCoin.CreateTransaction(new Transaction("Henry", "Mahesh", 10));
            phillyCoin.ProcessPendingTransactions("Bill");

            phillyCoin.CreateTransaction(new Transaction("Mahesh", "Henry", 5));
            phillyCoin.CreateTransaction(new Transaction("Mahesh", "Henry", 5));
            phillyCoin.ProcessPendingTransactions("Bill");

            var endTime = DateTime.Now;

            Console.WriteLine($"Duration: {endTime - startTime}");

            Console.WriteLine("============================");
            Console.WriteLine($"Henry'  balance:  {phillyCoin.GetBalance("Henry")}");
            Console.WriteLine($"Mahesh' balance:  {phillyCoin.GetBalance("Mahesh")}");
            Console.WriteLine($"Bill'   balance:  {phillyCoin.GetBalance("Bill")}");
            Console.WriteLine("============================");
            Console.WriteLine($"phillycoin");
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            
            Console.Read();
        }
    }
}
