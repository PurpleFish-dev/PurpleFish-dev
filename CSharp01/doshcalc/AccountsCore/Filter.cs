using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountsCore
{
    public class Filter
    {
        public Filter() { }
        // public Filter(DateTime) { }

        public DateTime StartDate;
        public DateTime EndDate;
        //public bool DateFilter;

        public List<CatagoryId> Catagories;
        public bool CatagoryFilter;

        public List<TaxCodeId> TaxCodes;
        public bool TaxCodeFilter;

        public List<PropertyId> Properties;
        public bool PropertyFilter;

        public List<AccountId> Accounts = new List<AccountId>();
        //public bool AccountFilter;

        public void AmountFilter(bool on, decimal amount)
        {
            _AmountFilter = on;
            _Amount = amount;
        }
        private bool _AmountFilter;
        private decimal _Amount;

        public void LinkFilter(bool on, bool linked)
        {
            _LinkFilter = on;
            _Linked = linked;
        }
        private bool _LinkFilter;
        private bool _Linked;

        public void PayeeFilter(bool on, PayeeId payeeId)
        {
            _PayeeFilter = on;
            _PayeeId = payeeId;
        }
        private bool _PayeeFilter;
        private PayeeId _PayeeId;

        internal bool Test(Entry entry)
        {
            if (_AmountFilter && Accounts.Count == 1 && Math.Abs(entry.GetAmount(Accounts[0])) != _Amount)
                return false;       
                
            if (_LinkFilter && ((_Linked && string.IsNullOrWhiteSpace(entry.RecieptNo)) || (!_Linked && !string.IsNullOrWhiteSpace(entry.RecieptNo))))
                return false;

            if (_PayeeFilter && entry.PayeeId != _PayeeId)
                return false;

            if (CatagoryFilter && !Catagories.Exists(x => x == entry.CatagoryId))
                return false;

            if(StartDate != DateTime.MinValue && entry.Date < StartDate)
                return false;

            if (EndDate != DateTime.MinValue && entry.Date > EndDate)
                return false;

            if ((Accounts.Count() > 0)
                && (!Accounts.Exists(x => x == entry.AccountId))
                && (!Accounts.Exists(x => x == entry.TransferAccountId)))
                return false;

            return true;
        }       
    }
}