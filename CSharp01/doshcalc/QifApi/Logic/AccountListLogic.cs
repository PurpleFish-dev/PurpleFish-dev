using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using QifApi.Transactions;
using QifApi.Transactions.Fields;

namespace QifApi.Logic
{
    internal static class AccountListLogic
    {
        /// <summary>
        /// Creates a collection of account list transactions
        /// </summary>
        /// <param name="transactionItems">The transaction delimited string</param>
        /// <returns>A collection of bank transactions</returns>
        public static List<AccountHeader> Import(string transactionItems, ref QifDom.yearFormat yearFormat, ref QifDom.dayMonthFormat dayMonthFormat)
        {
            List<AccountHeader> result = new List<AccountHeader>();

            // Create a new transaction
            AccountHeader alt = new AccountHeader();

            // Split the string by new lines
            string[] sEntries = Regex.Split(transactionItems, "$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            // Iterate over the array
            for (int i = 0; i < sEntries.Length; i++)
            {
                // Extract a line entry
                string sEntry = sEntries[i].Replace("\r", "").Replace("\n", "");

                // If the string has a value
                if (sEntry.Length > 0)
                {
                    // Test the first value of the string
                    switch (sEntry[0].ToString())
                    {
                        case AccountInformationFields.AccountType:
                            // Set the date value
                            alt.Type = ((AccountHeader.eType)sEntry.Substring(1));

                            // Stop processing
                            break;
                        case AccountInformationFields.CreditLimit:
                            // Set the amount value
                            alt.CreditLimit = Common.GetDecimal(sEntry.Substring(1));

                            // Stop processing
                            break;
                        case AccountInformationFields.Description:
                            // Set the cleared status value
                            alt.Description = sEntry.Substring(1);

                            // Stop processing
                            break;
                        case AccountInformationFields.Name:
                            // Set the number value
                            alt.Name = sEntry.Substring(1);

                            // Stop processing
                            break;
                        case AccountInformationFields.StatementBalance:
                            // Set the payee value
                            alt.StatementBalance = Common.GetDecimal(sEntry.Substring(1));

                            // Stop processing
                            break;
                        case AccountInformationFields.StatementBalanceDate:
                            // Set the memo value
                            alt.RawDate(sEntry.Substring(1));                            
                            Common.DetermineDateFormat(sEntry.Substring(1), ref yearFormat, ref dayMonthFormat);

                            // Stop processing
                            break;
                        case AccountInformationFields.EndOfEntry:
                            // Add the transaction instance to the collection
                            result.Add(alt);

                            // Call the destructor
                            alt = null;

                            // Create a new bank transaction
                            alt = new AccountHeader();

                            // Stop processing
                            break;
                    }
                }
            }

            // Return the populated collection
            return result;
        }

		public static void Export(StreamWriter writer, List<AccountHeader> list)
        {
            if ((list != null) && (list.Count > 0))
            {
                writer.WriteLine(Headers.AccountList);
                foreach (AccountHeader item in list)
				{
					AccountListLogic.Export(writer, item);
				}
			}
		}

        public static void Export(StreamWriter writer, AccountHeader item)
        {
            if (item.Type != AccountHeader.eType.None)
            {
                writer.WriteLine(AccountInformationFields.AccountType + item.Type);
            }

            writer.WriteLine(AccountInformationFields.CreditLimit + item.CreditLimit.ToString(CultureInfo.CurrentCulture));

            if (!string.IsNullOrEmpty(item.Description))
            {
                writer.WriteLine(AccountInformationFields.Description + item.Description);
            }

            if (!string.IsNullOrEmpty(item.Name))
            {
                writer.WriteLine(AccountInformationFields.Name + item.Name);
            }

            writer.WriteLine(AccountInformationFields.StatementBalance + item.StatementBalance.ToString(CultureInfo.CurrentCulture));

            writer.WriteLine(AccountInformationFields.StatementBalanceDate + item.StatementBalanceDate.ToShortDateString());

            writer.WriteLine(AccountInformationFields.EndOfEntry);
        }

		internal static void Export(StreamWriter writer, List<Account> list)
		{
			throw new System.NotImplementedException();
		}
	}
}