using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Linq;

namespace AccountsCore
{
	[Serializable]
	public class Catagory : NamedItem
	{
		public Catagory()
		{

		}

		internal Catagory(string name, TaxCodeId taxCodeId, bool obsolete, bool system)
			: base(name, obsolete, system)
		{
			_taxCodeId = taxCodeId;
		}

		public Catagory(Catagory arg) : base(arg)
		{
			_taxCodeId = arg._taxCodeId;
			_income = arg._income;
			_propertySpecific = arg._propertySpecific;
		}

		public Catagory(CatagoryWR arg)
			: base(arg)
		{
			_taxCodeId = arg.TaxCodeId;
			_income = arg.Income;
			_propertySpecific = arg.PropertySpecific;
		}

		public new string Name { get { return _name; } set { _name = value; } }
		public new bool Obsolete { set { _obsolete = value; } get { return _obsolete; } }
		//public new bool System { set { _system = value; } get { return _system; } }

		private TaxCodeId _taxCodeId;
		public TaxCodeId TaxCodeId { get { return _taxCodeId; } }

		private bool _income;
		public bool Income
		{
			get { return _income; }
			//set { _income = value; }
		}
		public bool Expense
		{
			get { return !_income; }
			//set { _income = !value; }
		}

		private bool _propertySpecific;
		public bool PropertySpecific
		{
			get { return _propertySpecific; }
			//set { _propertySpecific = value; }
		}

		public static bool operator ==(Catagory A, Catagory B)
		{
			return ((NamedItem)A == (NamedItem)B &&
					A.Obsolete == B.Obsolete &&
					A.PropertySpecific == B.PropertySpecific &&
					A.TaxCodeId == B.TaxCodeId &&
					A.Income == B.Income);
		}
		public static bool operator !=(Catagory A, Catagory B)
		{
			return !(A == B);
		}

		public XElement toXElement(CatagoryId id)
		{
			var res = new XElement("Catagory"
								, new XElement("Id", id.GetGuid())
								, new XElement("Name", Name)
								, new XElement("Obsolete", Obsolete)
								, new XElement("PropertySpecific", PropertySpecific)
								, new XElement("TaxCodeId", TaxCodeId == null ? Guid.Empty : TaxCodeId.GetGuid())
								, new XElement("Income", Income));
			return res;
		}
	}

	[Serializable]
    public class CatagoryWR : NamedItem
    {
        public CatagoryWR()
        {
            
        }

        ////internal CatagoryWR(string name, TaxCodeId taxCodeId, bool obsolete, bool system)
        ////    : base(name, obsolete, system)
        ////{
        ////    _taxCodeId = taxCodeId;
        ////}

        public CatagoryWR(Catagory arg)
            : base(arg)
        {
            _taxCodeId = arg.TaxCodeId;
            _income = arg.Income;
            _propertySpecific = arg.PropertySpecific;
        }

        public new string Name { get { return _name; } set { _name = value; } }
        public new bool Obsolete { set { _obsolete = value; } get { return _obsolete; } }
        public new bool System { set { _system = value; } get { return _system; } }

        private TaxCodeId _taxCodeId;
        public TaxCodeId TaxCodeId { get { return _taxCodeId; } set { _taxCodeId = value; } }

        private bool _income;
        public bool Income
        {
            get { return _income; }
            set { _income = value; }
        }
        public bool Expense
        {
            get { return !_income; }
            set { _income = !value; }
        }

        private bool _propertySpecific;
        public bool PropertySpecific
        {
            get { return _propertySpecific; }
            set { _propertySpecific = value; }
        }

        public static bool operator ==(CatagoryWR A, CatagoryWR B)
        {
            return ((NamedItem)A == (NamedItem)B &&
                    A.Obsolete == B.Obsolete &&
                    A.PropertySpecific == B.PropertySpecific &&
                    A.TaxCodeId == B.TaxCodeId &&
                    A.Income == B.Income);
        }
        public static bool operator !=(CatagoryWR A, CatagoryWR B)
        {
            return !(A == B);
        }
        public static implicit operator Catagory(CatagoryWR b)
        {
            // Code to convert the book into an XML structure
            return new Catagory(b);
        }
    }

	

    //public class EntryEventArgs : EventArgs
    //{
    //    public readonly EntryId EntryId;
    //    public readonly Entry AccountId;
    //    public readonly AccountId TransferAccountId;
    //    public EntryEventArgs(AccountId accountId, AccountId transferAccountId, EntryId entryId)
    //    { 
    //        AccountId = accountId;
    //        TransferAccountId = transferAccountId;
    //        EntryId = entryId;
    //    }
    //}
    public class EntryEventArgs : EventArgs
    {
        public readonly List<AccountId> AccountIds = new List<AccountId>();
        internal EntryEventArgs(List<AccountId> accountIds)
        {
            AccountIds = accountIds;
        }
        internal EntryEventArgs(Entry entry)
        {
           AccountIds.Add(entry.AccountId);
            if (entry.TransferAccountId != null) AccountIds.Add(entry.TransferAccountId);
        }

        internal EntryEventArgs(List<Entry> entries)
        {
            foreach (Entry entry in entries)
            {
                if(AccountIds.Count(x => x == entry.AccountId) == 0) AccountIds.Add(entry.AccountId);
                if ((entry.TransferAccountId != null) && (AccountIds.Count(x => x == entry.TransferAccountId) == 0))
                    AccountIds.Add(entry.TransferAccountId);
            }
        }

        internal EntryEventArgs(List<Entry> entries, List<Entry> entries2)
        {
            foreach (Entry entry in entries)
            {
                if (AccountIds.Count(x => x == entry.AccountId) == 0) AccountIds.Add(entry.AccountId);
                if ((entry.TransferAccountId != null) && (AccountIds.Count(x => x == entry.TransferAccountId) == 0))
                    AccountIds.Add(entry.TransferAccountId);
            }

            foreach (Entry entry in entries2)
            {
                if (AccountIds.Count(x => x == entry.AccountId) == 0) AccountIds.Add(entry.AccountId);
                if ((entry.TransferAccountId != null) && (AccountIds.Count(x => x == entry.TransferAccountId) == 0))
                    AccountIds.Add(entry.TransferAccountId);
            }
        }

        
    }

	public class TaxCodeEventArgs : EventArgs
	{
		public readonly TaxCodeId TaxCodeId;
		public TaxCodeEventArgs(TaxCodeId id) { TaxCodeId = id; }
	}

	public class PayeeEventArgs : EventArgs
	{
		public readonly PayeeId PayeeId;
		public PayeeEventArgs(PayeeId id) { PayeeId = id; }
	}

	public class CatagoryEventArgs : EventArgs
	{
		public readonly CatagoryId CatagoryId;
		public CatagoryEventArgs(CatagoryId id) { CatagoryId = id; }
	}

	public class PropertyEventArgs : EventArgs
	{
		public readonly PropertyId PropertyId;
		public PropertyEventArgs(PropertyId id) { PropertyId = id; }
	}

	

	public class AccountEventArgs : EventArgs
	{
		public readonly AccountId Id;
		public AccountEventArgs(AccountId id)
		{
			Id = id;
		}
	}

	
}
