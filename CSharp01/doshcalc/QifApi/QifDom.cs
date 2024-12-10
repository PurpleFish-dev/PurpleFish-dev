using System.Collections.Generic;
using QifApi.Transactions;
using System.IO;
using QifApi.Logic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System;

namespace QifApi
{
    /// <summary>
    /// Represents a Document Object Model for a QIF file.
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    public class QifDom
    {
		public enum yearFormat
		{
			 yyyy,
			 //_19yy,
			 //_20yy,
             yy,
			 Undetermined
		}

        public enum dayMonthFormat
        {
            ddmm,
            mmdd,
            //xxxx,
            Undetermined
        }

        private yearFormat _yearFormat = yearFormat.Undetermined;
        public yearFormat YearFormat { get; set; }

        private dayMonthFormat _dayMonthFormat = dayMonthFormat.Undetermined;
        public dayMonthFormat DayMonthFormat { get; set; }


        //private fileDayMonthFormat _dateFormat = fileDateFormat.Undetermined;
        //public fileDayMonthFormat DateFormat
        //{
        //    get { return _dateFormat; }
        //    set 
        //    { 
        //        fileDateFormat old = _dateFormat;
        //        if(old != value)
        //        {
        //            _dateFormat = value;
        //            if( (_dateFormat == QifDom.fileDateFormat.ddmmyyyy) 
        //                || (_dateFormat == QifDom.fileDateFormat.mmddyyyy)
        //                || (_dateFormat == QifDom.fileDateFormat.ddmm19yy)
        //                || (_dateFormat == QifDom.fileDateFormat.mmdd19yy)
        //                || (_dateFormat == QifDom.fileDateFormat.ddmm20yy)
        //                || (_dateFormat == QifDom.fileDateFormat.mmdd20yy) )
        //            {
        //                foreach(InvestmentTransaction transaction in InvestmentTransactions)
        //                {
        //                    transaction.ParseDate(_dateFormat);
        //                }

        //                foreach(Account account in Accounts)
        //                {
        //                    account.ParseDate(_dateFormat);
        //                }
						
        //                foreach(MemorizedTransactionListTransaction transaction in MemorizedTransactionListTransactions)
        //                {
        //                    transaction.ParseAmortizationFirstPaymentDate(_dateFormat);
        //                }
        //            }
        //        }
        //    }
        //}
		
		/// <summary>
        /// Represents a collection of investment transactions.
        /// </summary>
        public List<InvestmentTransaction> InvestmentTransactions
        {
            get;
            set;
        }
        
        /// <summary>
        /// Represents a collection of category list transactions.
        /// </summary>
        public List<CategoryListTransaction> CategoryListTransactions
        {
            get;
            set;
        }

        /// <summary>
        /// Represents a collection of class list transactions.
        /// </summary>
        public List<ClassListTransaction> ClassListTransactions
        {
            get;
            set;
        }

        /// <summary>
        /// Represents a collection of memorized transaction list transactions.
        /// </summary>
        public List<MemorizedTransactionListTransaction> MemorizedTransactionListTransactions
        {
            get;
            set;
        }

		/// <summary>
        /// Represents a collection of memorized transaction list transactions.
        /// </summary>
        public List<Account> Accounts
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new QIF DOM.
        /// </summary>
        public QifDom()
        {
            Accounts = new List<Account>();            
            CategoryListTransactions = new List<CategoryListTransaction>();
            ClassListTransactions = new List<ClassListTransaction>();
            MemorizedTransactionListTransactions = new List<MemorizedTransactionListTransaction>();
			InvestmentTransactions = new List<InvestmentTransaction>();
        }

        /// <summary>
        /// Imports the specified file and replaces the current instance properties with details found in the import file.
        /// </summary>
        /// <param name="fileName">Name of the file to import.</param>
        public void Import(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                Import(reader);
            }
        }

        /// <summary>
        /// Imports a stream in a QIF format and replaces the current instance properties with details found in the import stream.
        /// </summary>
        /// <param name="reader">The import reader stream.</param>
        public void Import(StreamReader reader)
        {
            QifDom import = ImportFile(reader);

            this.CategoryListTransactions = import.CategoryListTransactions;
            this.ClassListTransactions = import.ClassListTransactions;
            this.Accounts = import.Accounts;
            this.InvestmentTransactions = import.InvestmentTransactions;
            this.MemorizedTransactionListTransactions = import.MemorizedTransactionListTransactions;
        }

        /// <summary>
        /// Exports the current instance properties to the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>This will overwrite an existing file.</remarks>
        public void Export(string fileName)
        {
            ExportFile(this, fileName);
        }

        /// <summary>
        /// Exports the specified instance properties to the specified file.
        /// </summary>
        /// <param name="qif">The <seealso cref="T:QifDom"/> to export.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>This will overwrite an existing file.</remarks>
        public static void ExportFile(QifDom qif, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
            }

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.AutoFlush = true;

                AccountListLogic.Export(writer, qif.Accounts);
                //AssetLogic.Export(writer, qif.AssetTransactions);
                //BankLogic.Export(writer, qif.BankTransactions);
                //CashLogic.Export(writer, qif.CashTransactions);
                CategoryListLogic.Export(writer, qif.CategoryListTransactions);
                ClassListLogic.Export(writer, qif.ClassListTransactions);
                //CreditCardLogic.Export(writer, qif.CreditCardTransactions);
                InvestmentLogic.Export(writer, qif.InvestmentTransactions);
                //LiabilityLogic.Export(writer, qif.LiabilityTransactions);
                MemorizedTransactionListLogic.Export(writer, qif.MemorizedTransactionListTransactions);
				AssetLogic.Export(writer, qif.Accounts);

            }
        }

        /// <summary>
        /// Imports a QIF file and returns a QifDom object.
        /// </summary>
        /// <param name="fileName">The QIF file to import.</param>
        /// <returns>A QifDom object of transactions imported.</returns>
        public static QifDom ImportFile(string fileName)
        {
            QifDom result = null;

            // If the file doesn't exist
            if (File.Exists(fileName) == false)
            {
                // Identify the file doesn't exist
                throw new FileNotFoundException();
            }

            // Open the file
            using (StreamReader sr = new StreamReader(fileName))
            {
                result = ImportFile(sr);
            }

            return result;
        }

        /// <summary>
        /// Imports a QIF file stream reader and returns a QifDom object.
        /// </summary>
        /// <param name="reader">The stream reader pointing to an underlying QIF file to import.</param>
        /// <returns>A QifDom object of transactions imported.</returns>
        public static QifDom ImportFile(StreamReader reader)
        {
            QifDom result = new QifDom();
			QifDom.yearFormat yearFormat = QifDom.yearFormat.Undetermined;// = result.DateFormat;
            QifDom.dayMonthFormat dayMonthFormat = QifDom.dayMonthFormat.Undetermined;// = result.DateFormat;
			string activeAccountName =null;


			//xxx result.Accounts.Add(new Account(header));

			Account account_Bank = new Account(new AccountHeader(AccountHeader.eType.Bank));
			Account account_Asset = new Account(new AccountHeader(AccountHeader.eType.Asset));
			Account account_Cash = new Account(new AccountHeader(AccountHeader.eType.Cash));
			Account account_CreditCard = new Account(new AccountHeader(AccountHeader.eType.CreditCard));
			Account account_Liability = new Account(new AccountHeader(AccountHeader.eType.Liability));
			Account activeAccount;

			
			// Read the entire file
            string input = reader.ReadToEnd();

            // Split the file by header types
            string[] transactionTypes = Regex.Split(input, @"^(!.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            // Loop through the transaction types
            for (int i = 0; i < transactionTypes.Length; i++)
            {
                // Get the exact transaction type
                string transactionType = transactionTypes[i].Replace("\r", "").Replace("\n", "").Trim();

                // If the string has a value
                if (transactionType.Length > 0)
                {
                    // Check the transaction type
                    switch (transactionType)
                    {
                        case Headers.Bank:
						{   
                            i++;
                            activeAccount = result.Accounts.Find(p => p.AccountListTransaction.Name == activeAccountName);
							if(activeAccount != null)
							{
								activeAccount.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
							else
							{
                                account_Bank.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
                            break;
						}
						case Headers.Asset:
						{   
                            i++;
                            activeAccount = result.Accounts.Find(p => p.AccountListTransaction.Name == activeAccountName);
							if(activeAccount != null)
							{
                                activeAccount.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
							else
							{
                                account_Asset.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
                            break;
						}
						case Headers.Cash:
						{   
                            i++;
                            activeAccount = result.Accounts.Find(p => p.AccountListTransaction.Name == activeAccountName);
							if(activeAccount != null)
							{
                                activeAccount.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
							else
							{
                                account_Cash.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
                            break;
						}
						case Headers.CreditCard:
						{   
                            i++;
                            activeAccount = result.Accounts.Find(p => p.AccountListTransaction.Name == activeAccountName);
							if(activeAccount != null)
							{
                                activeAccount.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
							else
							{
                                account_CreditCard.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
                            break;
						}
						case Headers.Liability:
                        {   
                            i++;
                            activeAccount = result.Accounts.Find(p => p.AccountListTransaction.Name == activeAccountName);
							if(activeAccount != null)
							{
                                activeAccount.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
							else
							{
                                account_Liability.Transactions.AddRange(AssetLogic.Import(transactionTypes[i], ref yearFormat, ref dayMonthFormat));
							}
                             break;
						}
                        case Headers.AccountList:
                            // Increment the array counter
                            i++;

                            // Extract the transaction items
                            string accountListItems = transactionTypes[i];

                            // Import all transaction types
                            List<AccountHeader> accountHeaders = AccountListLogic.Import(accountListItems, ref yearFormat, ref dayMonthFormat);
							foreach(AccountHeader header in accountHeaders)
							{
								if(result.Accounts.Find(p => p.AccountListTransaction.Name == header.Name) == null)
								{
									result.Accounts.Add(new Account(header));
								}
								activeAccountName = header.Name;
							}
							// All done
                            break;   
						case Headers.CategoryList:
                            // Increment the array counter
                            i++;

                            // Extract the transaction items
                            string catItems = transactionTypes[i];

                            // Import all transaction types
                            result.CategoryListTransactions.AddRange(CategoryListLogic.Import(catItems));

                            // All done
                            break;
                        case Headers.ClassList:
                            // Increment the array counter
                            i++;

                            // Extract the transaction items
                            string classItems = transactionTypes[i];

                            // Import all transaction types
                            result.ClassListTransactions.AddRange(ClassListLogic.Import(classItems));

                            // All done
                            break;
                        case Headers.Investment:
                            // Increment the array counter
                            i++;

                            // Extract the transaction items
                            string investItems = transactionTypes[i];

                            // Import all transaction types
                            result.InvestmentTransactions.AddRange(InvestmentLogic.Import(investItems, ref yearFormat, ref dayMonthFormat));

                            // All done
                            break;
                        case Headers.MemorizedTransactionList:
                            // Increment the array counter
                            i++;

                            // Extract the transaction items
                            string memItems = transactionTypes[i];

                            // Import all transaction types
                            result.MemorizedTransactionListTransactions.AddRange(MemorizedTransactionListLogic.Import(memItems, ref yearFormat, ref dayMonthFormat));

                            // All done
                            break;
                        default:
                            // Don't do any processing
                            break;
                    }
                }
            }

			result.YearFormat = yearFormat;
            result.DayMonthFormat = dayMonthFormat;

			//Account account_Bank = new Account(new AccountHeader(AccountHeader.eType.Bank));
			//Account account_Asset = new Account(new AccountHeader(AccountHeader.eType.Asset));
			//Account account_Cash = new Account(new AccountHeader(AccountHeader.eType.Cash));
			//Account account_CreditCard = new Account(new AccountHeader(AccountHeader.eType.CreditCard));
			//Account account_Liability = new Account(new AccountHeader(AccountHeader.eType.Liability));
			if(account_Bank.Transactions.Count > 0) { result.Accounts.Add(account_Bank); }
			if(account_Asset.Transactions.Count > 0) { result.Accounts.Add(account_Asset); }
			if(account_Cash.Transactions.Count > 0) { result.Accounts.Add(account_Cash); }
			if(account_CreditCard.Transactions.Count > 0) { result.Accounts.Add(account_CreditCard); }
			if(account_Liability.Transactions.Count > 0) { result.Accounts.Add(account_Liability); }
			
            return result;
        }

		private void parseDates()
		{
            foreach (Account account in Accounts)
            {
                account.parseDate(YearFormat, DayMonthFormat);
            }
		}

		public List<AccountsCore.Entry> GetEntriesFromSelectedAccounts(AccountsCore.AccountId accountId, bool p, bool p_2)
		{
			var entries = new List<AccountsCore.Entry>();
			foreach(Account account in this.Accounts)
			{
				if(account.AccountListTransaction.Selected == true)
				{
					foreach(BasicTransaction transaction in account.Transactions)
					{
						var entry = new AccountsCore.Entry(accountId);
						if(p)
						{
							entry.Description = transaction.Payee;
						}
						entry.Date = transaction.Date;
						entry.SetAmount(accountId, transaction.Amount);
						entries.Add(entry);
					}
				}
			}
			return entries;
		}

        public bool Finalize()
        {
            if ((this.YearFormat == QifDom.yearFormat.Undetermined) || (this.DayMonthFormat == QifDom.dayMonthFormat.Undetermined)) return false;
            parseDates();
            return true;
        }
    }
}
