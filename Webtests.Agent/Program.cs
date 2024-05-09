using System;
using Webtests;
namespace Webtests.Agent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HelloWorldTest test = new HelloWorldTest();
            test.SayHelloToWorld();
        }
    }
}
