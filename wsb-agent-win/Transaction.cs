//using System.Collections.Generic;

using System;

namespace wsb_agent_win
{
    public class Transaction
    {
        public DateTime transactionDate;
        public int productCode;

        public Transaction(int productCode, DateTime transactionDate)
        {
            this.transactionDate = transactionDate;
            this.productCode = productCode;
        }
    }
}