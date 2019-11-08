﻿using SGBank.Data;
using System;
using System.Configuration;

namespace SGBank.BLL
{
    public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRepository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "Live":
                    return new AccountManager(new FileAccountRepository());
                default:
                    throw new Exception("Mode value in app configuration is not valid");
            }
        }
    }
}
