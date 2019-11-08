using SGBank.UI.Workflows;
using System;

namespace SGBank.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Look up an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");

                Console.WriteLine("\nQ to quit");
                Console.WriteLine("\nEnter selection: ");

                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        AccountLookUpWorkflow lookupWorkFlow = new AccountLookUpWorkflow();
                        lookupWorkFlow.Execute();
                        break;
                    case "2":
                        DepositWorkFlow depositWorkFlow = new DepositWorkFlow();
                        depositWorkFlow.Execute();
                        break;
                    case "3":
                        WithrawWorkFlow withrawWorkFlow = new WithrawWorkFlow();
                        withrawWorkFlow.Execute();
                        break;
                    case "Q":
                        return;

                }
            }
        }
    }
}
