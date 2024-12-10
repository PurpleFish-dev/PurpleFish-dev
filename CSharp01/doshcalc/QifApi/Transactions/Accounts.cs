using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QifApi.Transactions
{
	public class Account
	{
		public Account(QifApi.Transactions.AccountHeader header)
		{
			Transactions = new List<BasicTransaction>();
			AccountListTransaction = header;
		}
		public List<BasicTransaction> Transactions
        {
            get;
            set;
        }
		
		public AccountHeader AccountListTransaction
        {
            get;
            set;
        }
		//Transactions = new List<BasicTransaction>();
		//	AccountListTransactions = new List<AccountListTransaction>();
			
			
		public enum Type
		{
			Bank,
			Cash,
			CreditCard,
			Asset,
			Liability
		}

		internal void parseDate(QifDom.yearFormat yearFormat, QifDom.dayMonthFormat dayMonthFormat)
		{
			foreach(BasicTransaction transaction in Transactions)
			{
                transaction.parseDate(yearFormat, dayMonthFormat);
			}
		}
	}
}
