using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Linq;

namespace AccountsCore
{
    [Serializable]
    public class Entry /*: IComparable<Entry>*/
    {

        public enum eType
        {
            Predicted,
            Imported,
            Confirmed,
        }

        public eType Type { get; set; }
        public string Description { get; set; }
        public string ImportDescription { get; set; }
        public DateTime Date { get; set; }
        public string RecieptNo { get; set; }
        public PayeeId PayeeId { get; set; }
        public CatagoryId CatagoryId { get; private set; }

        override public string ToString()
        {
            //return Date.ToShortDateString() + " " + Description + GetAmount(null).ToString();
            return string.Format("{0} {1, -80} {2}", Date.ToShortDateString(), Description, GetAmount(null).ToString());
        }

        public void SetEntry(AccountId pivot, CatagoryId catagoryId)
        {
            Debug.Assert(pivot != null);
            this.CatagoryId = catagoryId;
            if (this.AccountId == null)
            {
                Debug.Assert(this.transferAccountId == null); //this would be an invalid state for and entry/transfer
                this.accountId = pivot;
            }
            else
            {
                if (pivot == transferAccountId)
                    this.Amount = decimal.Negate(this.Amount);

                this.accountId = pivot;
                transferAccountId = null;
            }
        }

        public void SetTransfer(AccountId pivot, AccountId change)
        {
            Debug.Assert(pivot != null);
            Debug.Assert(change != null);
            Debug.Assert((pivot == accountId) || (pivot == transferAccountId));
            Debug.Assert(pivot != change);
            if ((pivot == null)
                || (change == null)
                || ((pivot != accountId) && (pivot != transferAccountId))
                || (pivot == change))
                return;

            if (pivot == transferAccountId) accountId = change;
            else transferAccountId = change;

            this.CatagoryId = null;
        }

        public PropertyId PropertyId { get; set; }
        private decimal Amount { get; set; }

        public decimal GetAmount(AccountId accountId)
        {
            if ((accountId == null) && (TransferAccountId == null))
            {
                return Amount;
            }
            else if (accountId == AccountId) return Amount;
            else if (accountId == TransferAccountId) return decimal.Negate(Amount);
            return 0;
        }

        public void SetAmount(AccountId accountId, decimal amount)
        {
            if (accountId == AccountId) Amount = amount;
            else if (accountId == TransferAccountId) Amount = decimal.Negate(amount);
        }

        public AccountId GetTransferAccountId(AccountId id) { return (id == TransferAccountId) ? accountId : transferAccountId; }
        public bool IsTransfer() { return transferAccountId != null; }


        private AccountId accountId;
        public AccountId AccountId { get { return accountId; } /*set { accountId = value; }*/ }

        private AccountId transferAccountId;
        internal AccountId TransferAccountId { get { return transferAccountId; } /*set { transferAccountId = value; }*/ }

        //public bool SetAccountId(AccountId account)
        //{
        //    if (account == null)
        //    {
        //        Debug.Assert(false);
        //        return false;
        //    }
        //    accountId = account;
        //    if (account == transferAccountId)
        //        this.Amount = decimal.Negate(this.Amount);
        //    transferAccountId = null;
        //    return true;
        //}

        [NonSerialized]
        private decimal balance;
        public decimal Balance { get { return balance; } internal set { balance = value; } }

        [NonSerialized]
        private EntryId id;
        public EntryId Id 
        { 
            get { return id; } 
            internal set 
            { 
                id = value;
                //Debug.Assert(id != null);
                //Debug.Assert(id.IsEmpty() == false);
            } 
        }

        public Entry()
        { }

        public Entry(AccountId accountId)
        {
            initialize(accountId);
        }
        
        private void initialize(AccountId accountId)
        {
            this.accountId = accountId;
            Amount = 0.0M;
            Date = DateTime.Today;
            Description = "";
            ImportDescription = "";
            RecieptNo = "";
            Type = eType.Predicted;
        }

        public Entry(Entry arg)
        {
            Type = arg.Type;
            Description = arg.Description;
            ImportDescription = arg.ImportDescription;
            Date = arg.Date;
            RecieptNo = arg.RecieptNo;
            PayeeId = (arg.PayeeId != null) && arg.PayeeId.IsEmpty() ? null : arg.PayeeId;
            CatagoryId = (arg.CatagoryId != null) && arg.CatagoryId.IsEmpty() ? null : arg.CatagoryId;
            PropertyId = (arg.PropertyId != null) && arg.PropertyId.IsEmpty() ? null : arg.PropertyId;
            accountId = (arg.AccountId != null) && arg.AccountId.IsEmpty() ? null : arg.AccountId;
            transferAccountId = (arg.TransferAccountId != null) && arg.TransferAccountId.IsEmpty() ? null : arg.TransferAccountId;
            Amount = arg.Amount;

            Balance = arg.Balance;
            Id = arg.Id;
        }

        public void FixUpEmptyIds()
        {
            PayeeId = (PayeeId != null) && PayeeId.IsEmpty() ? null : PayeeId;
            CatagoryId = (CatagoryId != null) && CatagoryId.IsEmpty() ? null : CatagoryId;
            PropertyId = (PropertyId != null) && PropertyId.IsEmpty() ? null : PropertyId;
            accountId = (AccountId != null) && AccountId.IsEmpty() ? null : AccountId;
            transferAccountId = (TransferAccountId != null) && TransferAccountId.IsEmpty() ? null : TransferAccountId;
        }

		public XElement toXElement(EntryId id)
		{
			var res = new XElement("Entry"
									, new XElement("Id", id.GetGuid())
									, new XElement("Type", Type)
									, new XElement("Description", Description)
									, new XElement("ImportDescription", ImportDescription)
									, new XElement("Date", Date)
									, new XElement("RecieptNo", RecieptNo)
									, new XElement("PayeeId", PayeeId == null ? Guid.Empty : PayeeId.GetGuid())
									, new XElement("CatagoryId", CatagoryId == null ? Guid.Empty : CatagoryId.GetGuid())
									, new XElement("PropertyId", PropertyId == null ? Guid.Empty : PropertyId.GetGuid())
									, new XElement("AccountId", accountId == null ? Guid.Empty : accountId.GetGuid())
									, new XElement("transferAccountId", transferAccountId == null ? Guid.Empty : transferAccountId.GetGuid())
									, new XElement("Amount", Amount));
			return res;
		}

		//public void ToQIF(string qif)
		//{
		//    //qif += QIFParser.DateToken.Segment();
		//}

		//public bool IsValid()
		//{
		//    return true;
		//}


	}

    public class EntrySorter : IComparer<Entry>
    {           
        private delegate int partCompare(Entry x, Entry y, Accounts accounts, AccountId idPivot);

        public static int partCompareDate(Entry x, Entry y, Accounts accounts, AccountId idPivot)
        { return x.Date.CompareTo(y.Date); }

        public static int partCompareDescription(Entry x, Entry y, Accounts accounts, AccountId idPivot)
        { return x.Description.CompareTo(y.Description); }

        public static int partCompareAmount(Entry x, Entry y, Accounts accounts, AccountId idPivot)
        {
            if (x.GetAmount(idPivot) == y.GetAmount(idPivot))
                return 0;
            else if (x.GetAmount(idPivot) > y.GetAmount(idPivot))
                return 1;
            return -1;
        }

        public static int partCompareCategoryAndTransfer(Entry x, Entry y, Accounts accounts, AccountId idPivot)
        {
            if ((x.CatagoryId == null) && (y.CatagoryId != null)) return -1;
            else if ((x.CatagoryId != null) && (y.CatagoryId == null)) return 1;
            else if ((x.CatagoryId != null) && (y.CatagoryId != null))
            {
                string xCatName = accounts.Catagory(x.CatagoryId).Name;
                string yCatName = accounts.Catagory(y.CatagoryId).Name;
                return xCatName.CompareTo(yCatName);
            }
            else
            {
                return 0;
                string xTransName = accounts.Account(x.TransferAccountId).Name;
                string yTransName = accounts.Account(y.TransferAccountId).Name;
                return xTransName.CompareTo(yTransName);
            }
        }

        class delEnt
        {
            public partCompare func;
            public string Key;
            public bool Reverse;
        };

        List<delEnt> dels;
        Accounts _accounts;
        AccountId _idPivot;
        //Dictionary<string, System.Delegate>

        public EntrySorter()
        {
            dels = new List<delEnt>();
            
            delEnt ent = new delEnt();
            ent.func = partCompareDate;
            ent.Key = "Date";            
            dels.Add(ent);

            ent = new delEnt();
            ent.func = partCompareDescription;
            ent.Key = "Description";
            dels.Add(ent);

            ent = new delEnt();
            ent.func = partCompareAmount;
            ent.Key = "Amount";
            dels.Add(ent);

            ent = new delEnt();
            ent.func = partCompareCategoryAndTransfer;
            ent.Key = "CategoryAndTransfer";
            dels.Add(ent);
        }

        public void SetPivotAccount(Accounts accounts, AccountId idPivot)
        {
            _accounts = accounts;
            _idPivot = idPivot;
        }

        int IComparer<Entry>.Compare(Entry x, Entry y)
        {
            int nResult =0;
            foreach (delEnt info in dels)
            {
                nResult = info.Reverse ? info.func(y, x, _accounts, _idPivot) : info.func(x, y, _accounts, _idPivot);
                if (nResult != 0) return nResult;
            }
            return nResult;
        }

        public void SetPrimaryCol(string name)
        {
            int i = dels.FindIndex(x => x.Key == name);// ("seat")));
            if (i == 0)
            {
                dels[0].Reverse = !dels[0].Reverse;
            }
            else if (i > 0)
            {
                delEnt item = dels[i];
                item.Reverse = false;
                dels.RemoveAt(i);
                dels.Insert(0, item);
            }
        }        
    }
}


