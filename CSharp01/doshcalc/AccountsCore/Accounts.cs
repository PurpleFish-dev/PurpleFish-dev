using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;



namespace AccountsCore
{
    public class Accounts
    {
        public enum eProtectionState
        {
            Open,
            Reconciled,
            Locked,
            LockedAndHidden
        }

        IAccountsData _data = null;//new MemAccountsData();
        [NonSerialized] public ActionMaster ActionMaster;
        [NonSerialized] EntrySorter _sorter;
        [NonSerialized] Dictionary<AccountId, Filter> _filters;

        public Filter GetFilter(AccountId accId)
        {
            if (!_filters.ContainsKey(accId))
            {
                _filters[accId] = new Filter();
                _filters[accId].Accounts.Add(accId);
            }
            return _filters[accId];
        }

        public void SetFilter(AccountId accId, Filter filter)
        {
            _filters[accId] = filter;
            var list = new List<AccountId>();
            list.Add(accId);
            EntriesChanged?.Invoke(this, new EntryEventArgs(list));
        }

       
        
        //CATAGORIES////////////////////////////////////////////////////////
        //		public Catagory CreateCatagory() { return this._data.CreateCatagory(); }
        public Catagory Catagory(CatagoryId id) { return _data.Catagory(id); }
        public IdClassListReadOnly<CatagoryId, Catagory> CatagoryList { get { return _data == null ? null : _data.CatagoryList; } }

        public bool CatagoryCanAdd(Catagory catagory) { return this._data.Catagory_CanAdd(catagory); }
        public bool CatagoryCanRemove(CatagoryId id) { return this._data.Catagory_CanRemove(id); }
        public bool Catagory_CanReplace(CatagoryId id, Catagory catagory) { return this._data.Catagory_CanReplace(id, catagory); }

        public delegate void AddCatagoryHandler(object sender, CatagoryEventArgs e);
        public event AddCatagoryHandler CatagoryAdded;

        public delegate void RemoveCatagoryHandler(object sender, CatagoryEventArgs e);
        public event RemoveCatagoryHandler CatagoryRemoved;

        public delegate void ModifyCatagoryHandler(object sender, CatagoryEventArgs e);
        public event ModifyCatagoryHandler CatagoryModified;

        public delegate void CatagoriesChangedHandler(object sender);
        public event CatagoriesChangedHandler CatagoriesChanged;



        private class ActionAddCatagory : UserAction
        {
            private Accounts _accounts;
            private Catagory _catagory;
            private CatagoryId _id;

            public CatagoryId Id
            {
                get { return _id; }
            }

            public ActionAddCatagory(Accounts accounts, Catagory catagory)
            {
                _id = new CatagoryId();
                _catagory = catagory;
                _accounts = accounts;
            }

            public override void Do()
            {
               _accounts._data.Catagory_Add(_catagory, _id);
                
                    //events
                    _accounts.CatagoryAdded?.Invoke(this, new CatagoryEventArgs(_id));

                    _accounts.CatagoriesChanged?.Invoke(this);
                
            }

            public override void Undo()
            {
                bool oK = _accounts._data.Catagory_Remove(_id);
                Debug.Assert(oK);
                if (oK)
                {
                    //events
                    _accounts.CatagoryRemoved?.Invoke(this, new CatagoryEventArgs(_id));

                    _accounts.CatagoriesChanged?.Invoke(this);
                }
            }

            public override string Description()
            {
                return "add catagory " + _catagory.Name;
            }
        }
        public CatagoryId AddCatagory(Catagory catagory)
        {
            ActionAddCatagory action = new Accounts.ActionAddCatagory(this, catagory);
            ActionMaster.Do(action);
            return action.Id;
        }

        class ActionDeleteCatagory : UserAction
        {
            private Accounts _accounts;
            private Catagory _catagory;
            private CatagoryId _id;

            public ActionDeleteCatagory(Accounts accounts, CatagoryId id)
            {
                _id = id;
                _accounts = accounts;
            }

            public override void Do()
            {
                _catagory = _accounts.Catagory(_id);
                _accounts._data.Catagory_Remove(_id);
               
                    //events
                    _accounts.CatagoryRemoved?.Invoke(_catagory, new CatagoryEventArgs(_id));

                    _accounts.CatagoriesChanged?.Invoke(this);
               
            }

            public override void Undo()
            {
                _accounts._data.Catagory_Add(_catagory, _id);

                //events
                _accounts.CatagoryAdded?.Invoke(_catagory, new CatagoryEventArgs(_id));

                _accounts.CatagoriesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "delete catagory " + _catagory.Name;
            }
        }
        public bool Category_Remove(CatagoryId id)
        {
            if (!CatagoryCanRemove(id))
                return false;
            
            ActionDeleteCatagory action = new ActionDeleteCatagory(this, id);
            this.ActionMaster.Do(action);
            return true;
        }

        class ActionReplaceCatagory : UserAction
        {
            private Accounts _accounts;
            private Catagory _unusedCatagory;
            private CatagoryId _id;

            public ActionReplaceCatagory(Accounts accounts, CatagoryId id, Catagory catagory)
            {
                _accounts = accounts;
                _id = id;
                _unusedCatagory = catagory;
            }

            public override void Do()
            {
                _accounts._data.Catagory_Replace(_id, new Catagory(_unusedCatagory), out _unusedCatagory);
                _accounts.CatagoryModified?.Invoke(this, new CatagoryEventArgs(_id));
                _accounts.CatagoriesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                _accounts._data.Catagory_Replace(_id, new Catagory(_unusedCatagory), out _unusedCatagory);
                _accounts.CatagoryModified?.Invoke(this, new CatagoryEventArgs(_id));
                _accounts.CatagoriesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "Replace catagory " + _unusedCatagory.Name;
            }
        }
        public bool Catagory_Replace(CatagoryId id, Catagory catagory)
        {
            if (!this.Catagory_CanReplace(id, catagory))
                return false;
            ActionReplaceCatagory action = new ActionReplaceCatagory(this, id, catagory);
            this.ActionMaster.Do(action);
            return true;
        }

        //END CATAGORIES/////////////////////////////////////////////////////////////////////////////////////











        //TAX CODES////////////////////////////////////////////////////////
        public TaxCode TaxCode(TaxCodeId id) { return _data.TaxCode(id); }
        public IdClassListReadOnly<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems)
        {
            if (_data != null)
            {
                return _data.TaxCodeList(includeObsoleteItems);
            }
            return null;
        }

        public bool TaxCodeCanAdd(TaxCode taxCode) { return this._data.TaxCode_CanAdd(taxCode); }
        public bool TaxCodeCanRemove(TaxCodeId id) { return this._data.TaxCode_CanRemove(id); }
        public bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode) { return this._data.TaxCode_CanReplace(id, taxCode); }

        public delegate void AddTaxCodeHandler(object sender, TaxCodeEventArgs e);
        public event AddTaxCodeHandler TaxCodeAdded;

        public delegate void RemoveTaxCodeHandler(object sender, TaxCodeEventArgs e);
        public event RemoveTaxCodeHandler TaxCodeRemoved;

        public delegate void ModifyTaxCodeHandler(object sender, TaxCodeEventArgs e);

        public event ModifyTaxCodeHandler TaxCodeModified;

        public delegate void TaxCodesChangedHandler(object sender);
        public event TaxCodesChangedHandler TaxCodesChanged;

        class ActionAddTaxCode : UserAction
        {
            private Accounts _accounts;
            private TaxCode _taxCode;
            private TaxCodeId _id;

            public TaxCodeId Id { get { return _id; } }

            public ActionAddTaxCode(Accounts accounts, TaxCode taxCode)
            {
                _id = new TaxCodeId();
                _taxCode = taxCode;
                _accounts = accounts;
            }

            public override void Do()
            {
                _accounts._data.TaxCode_Add(_taxCode, _id);
              
                //events
                _accounts.TaxCodeAdded?.Invoke(this, new TaxCodeEventArgs(_id));
                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                //remove from database
                bool oK = _accounts._data.TaxCode_Remove(_id);
                Debug.Assert(oK);

                //events
                _accounts.TaxCodeRemoved?.Invoke(this, new TaxCodeEventArgs(_id));

                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "add _taxCode " + _taxCode.Name;
            }
        }
        public TaxCodeId AddTaxCode(TaxCode taxCode)
        {
            ActionAddTaxCode action = new Accounts.ActionAddTaxCode(this, taxCode);
            ActionMaster.Do(action);

            return action.Id;
        }

        class ActionDeleteTaxCode : UserAction
        {
            private Accounts _accounts;
            private TaxCode _taxCode;
            private TaxCodeId _id;

            public ActionDeleteTaxCode(Accounts accounts, TaxCodeId id)
            {
                _id = id;
                _accounts = accounts;
                _taxCode = _accounts._data.TaxCode(_id);
            }

            public override void Do()
            {
                _accounts._data.TaxCode_Remove(_id);
                _accounts.TaxCodeRemoved?.Invoke(this, new TaxCodeEventArgs(_id));
                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                _accounts._data.TaxCode_Add(_taxCode, _id);

                //events
                _accounts.TaxCodeAdded?.Invoke(this, new TaxCodeEventArgs(_id));
                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "delete tax code " + _taxCode.Name;
            }
        }
        public bool RemoveTaxCode(TaxCodeId id)
        {
            if (!this.TaxCodeCanRemove(id))
                return false;
            ActionDeleteTaxCode action = new ActionDeleteTaxCode(this, id);
            this.ActionMaster.Do(action);
            return true;
        }

        class ActionTaxCodeReplace : UserAction
        {
            private Accounts _accounts;
            private TaxCode _unusedTaxCode;
            private TaxCodeId _id;

            public ActionTaxCodeReplace(Accounts accounts, TaxCodeId id, TaxCode taxCode)
            {
                _accounts = accounts;
                _id = id;
                _unusedTaxCode = taxCode;
            }

            public override void Do()
            {
                _accounts._data.TaxCode_Replace(_id, new TaxCode(_unusedTaxCode), out _unusedTaxCode);
                _accounts.TaxCodeModified?.Invoke(this, new TaxCodeEventArgs(_id));
                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                _accounts._data.TaxCode_Replace(_id, new TaxCode(_unusedTaxCode), out _unusedTaxCode);
                _accounts.TaxCodeModified?.Invoke(this, new TaxCodeEventArgs(_id));
                _accounts.TaxCodesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "Replace tax code " + _unusedTaxCode.Name;
            }
        }

        public EntrySorter GetSorter(AccountId idPivot)
        {
            this._sorter.SetPivotAccount(this, idPivot);
            return this._sorter;
        }

        public bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode)
        {
            if (!this.TaxCode_CanReplace(id, taxCode))
                return false;

            ActionTaxCodeReplace action = new ActionTaxCodeReplace(this, id, taxCode);
            this.ActionMaster.Do(action);
            return true;
        }

        //END TAX CODES/////////////////////////////////////////////////////////////////////////////////////

        //PROPERTY////////////////////////////////////////////////////////
        public Property Property(PropertyId id) { return _data.Property(id); }
        public IdClassListReadOnly<PropertyId, Property> PropertyList { get { return _data.PropertyList; } }

        public bool PropertyCanAdd(Property Property) { return this._data.Property_CanAdd(Property); }
        public bool PropertyCanRemove(PropertyId id) { return this._data.Property_CanRemove(id); }
        public bool PropertyCanReplace(PropertyId id, Property Property) { return this._data.Property_CanReplace(id, Property); }

        public delegate void AddPropertyHandler(object sender, PropertyEventArgs e);
        public event AddPropertyHandler PropertyAdded;

        public delegate void RemovePropertyHandler(object sender, PropertyEventArgs e);
        public event RemovePropertyHandler PropertyRemoved;

        public delegate void ModifyPropertyHandler(object sender, PropertyEventArgs e);
        public event ModifyPropertyHandler PropertyModified;

        public delegate void PropertiesChangedHandler(object sender);
        public event PropertiesChangedHandler PropertiesChanged;

        class ActionAddProperty : UserAction
        {
            private Accounts _accounts;
            private Property _Property;
            private PropertyId _id;

            public PropertyId Id { get { return _id; } }

            public ActionAddProperty(Accounts accounts, Property Property)
            {
                _id = new PropertyId();
                _Property = Property;
                _accounts = accounts;
            }

            public override void Do()
            {
                _accounts._data.Property_Add(_Property, _id);
                
                _accounts.PropertyAdded?.Invoke(this, new PropertyEventArgs(_id));
                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                //remove from database
                bool oK = _accounts._data.Property_Remove(_id);
                Debug.Assert(oK);

                //events
                _accounts.PropertyRemoved?.Invoke(this, new PropertyEventArgs(_id));

                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "add _Property " + _Property.Name;
            }
        }
        public PropertyId AddProperty(Property Property)
        {
            ActionAddProperty action = new Accounts.ActionAddProperty(this, Property);
            ActionMaster.Do(action);

            return action.Id;
        }

        class ActionPropertyRemove : UserAction
        {
            private Accounts _accounts;
            private Property _Property;
            private PropertyId _id;

            public ActionPropertyRemove(Accounts accounts, PropertyId id)
            {
                _id = id;
                _accounts = accounts;
                _Property = _accounts._data.Property(id);
            }

            public override void Do()
            {
                _accounts._data.Property_Remove(_id);
                _accounts.PropertyRemoved?.Invoke(this, new PropertyEventArgs(_id));
                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                _accounts._data.Property_Add(_Property, _id);

                //events
                _accounts.PropertyAdded?.Invoke(this, new PropertyEventArgs(_id));
                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "delete tax code " + _Property.Name;
            }
        }
        public bool Property_Remove(PropertyId id)
        {
            if (!this.PropertyCanRemove(id))
                return false;
            ActionPropertyRemove action = new ActionPropertyRemove(this, id);
            this.ActionMaster.Do(action);
            return true;
        }

        class ActionReplaceProperty : UserAction
        {
            private Accounts _accounts;
            private Property _unusedProperty;
            private PropertyId _id;

            public ActionReplaceProperty(Accounts accounts, PropertyId id, Property Property)
            {
                _accounts = accounts;
                _id = id;
                _unusedProperty = Property;
            }

            public override void Do()
            {
                _accounts._data.Property_Replace(_id, new Property(_unusedProperty), out _unusedProperty);
                _accounts.PropertyModified?.Invoke(this, new PropertyEventArgs(_id));
                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                _accounts._data.Property_Replace(_id, new Property(_unusedProperty), out _unusedProperty);
                _accounts.PropertyModified?.Invoke(this, new PropertyEventArgs(_id));
                _accounts.PropertiesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "Replace tax code " + _unusedProperty.Name;
            }
        }
        public bool Property_Replace(PropertyId id, Property property)
        {
            if (!this.PropertyCanReplace(id, property))
                return false;
            ActionReplaceProperty action = new ActionReplaceProperty(this, id, property);
            this.ActionMaster.Do(action);
            return true;
        }

        //END PROPERTY/////////////////////////////////////////////////////////////////////////////////////


        //PAYEE////////////////////////////////////////////////////////
        public Payee Payee(PayeeId id) { return _data.Payee(id); }
        public IdClassListReadOnly<PayeeId, Payee> PayeeList { get { if (_data != null) { return _data.PayeeList; } return null; } }

        public bool PayeeCanAdd(Payee payee) { return this._data.Payee_CanAdd(payee); }
        public bool Payee_CanRemove(PayeeId id) { return this._data.Payee_CanRemove(id); }
        public bool PayeeCanReplace(PayeeId id, Payee payee) { return this._data.Payee_CanReplace(id, payee); }

        public delegate void AddPayeeHandler(object sender, PayeeEventArgs e);
        public event AddPayeeHandler PayeeAdded;

        public delegate void RemovePayeeHandler(object sender, PayeeEventArgs e);
        public event RemovePayeeHandler PayeeRemoved;

        public delegate void ModifyPayeeHandler(object sender, PayeeEventArgs e);
        public event ModifyPayeeHandler PayeeModified;

        public delegate void PayeesChangedHandler(object sender);
        public event PayeesChangedHandler PayeesChanged;

        class ActionAddPayee : UserAction
        {
            private Accounts _accounts;
            private Payee _Payee;
            private PayeeId _id;

            internal PayeeId Id { get { return _id; } }

            public ActionAddPayee(Accounts accounts, Payee payee)
            {
                _id = new PayeeId();
                _Payee = payee;
                _accounts = accounts;
            }

            public override void Do()
            {
                _accounts._data.Payee_Add(_Payee, _id);
                _accounts.PayeeAdded?.Invoke(this, new PayeeEventArgs(_id));
                _accounts.PayeesChanged?.Invoke(this);
            }

            public override void Undo()
            {
                //remove from database
                _accounts._data.Payee_Remove(_id);
                

                //events
                _accounts.PayeeRemoved?.Invoke(this, new PayeeEventArgs(_id));

                _accounts.PayeesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "add _Payee " + _Payee.Name;
            }
        }
        public PayeeId AddPayee(Payee Payee)
        {
            ActionAddPayee action = new Accounts.ActionAddPayee(this, Payee);
            ActionMaster.Do(action);

            return action.Id;
        }

        class ActionPayeeRemove : UserAction
        {
            private Accounts _accounts;
            private Payee _Payee;
            private PayeeId _id;

            public ActionPayeeRemove(Accounts accounts, PayeeId id)
            {
                _id = id;
                _accounts = accounts;
                _Payee = _accounts._data.Payee(_id);
            }

            public override void Do()
            {

                _accounts._data.Payee_Remove(_id);
                _accounts.PayeeRemoved?.Invoke(this, new PayeeEventArgs(_id));

                    _accounts.PayeesChanged?.Invoke(this);
               
            }

            public override void Undo()
            {
                _accounts._data.Payee_Add(_Payee, _id);
               
                _accounts.PayeeAdded?.Invoke(this, new PayeeEventArgs(_id));
                _accounts.PayeesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "delete payee " + _Payee.Name;
            }
        }
        public bool Payee_Remove(PayeeId id)
        {
            if (!this.Payee_CanRemove(id))
                return false;
            ActionPayeeRemove action = new ActionPayeeRemove(this, id);
            this.ActionMaster.Do(action);
            return true;
        }

        class ActionPayeeReplace : UserAction
        {
            private Accounts _accounts;
            private Payee _unusedPayee;
            private PayeeId _id;

            public ActionPayeeReplace(Accounts accounts, PayeeId id, Payee Payee)
            {
                _accounts = accounts;
                _id = id;
                _unusedPayee = Payee;
            }

            public override void Do()
            {
                _accounts._data.Payee_Replace(_id, new Payee(_unusedPayee), out _unusedPayee);
                _accounts.PayeeModified?.Invoke(this, new PayeeEventArgs(_id));
                _accounts.PayeesChanged?.Invoke(this);

            }

            public override void Undo()
            {
                _accounts._data.Payee_Replace(_id, new Payee(_unusedPayee), out _unusedPayee);
                _accounts.PayeeModified?.Invoke(this, new PayeeEventArgs(_id));
                _accounts.PayeesChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "Replace payee " + _unusedPayee.Name;
            }
        }
        public bool Payee_Replace(PayeeId id, Payee payee)
        {
            if (!this.PayeeCanReplace(id, payee))
                return false;
            ActionPayeeReplace action = new ActionPayeeReplace(this, id, payee);
            this.ActionMaster.Do(action);
            return true;
        }

        //END PAYEE/////////////////////////////////////////////////////////////////////////////////////

        //ACCOUNT////////////////////////////////////////////////////////
        public Account Account(AccountId id) { return _data.Account(id); }
        public IdClassListReadOnly<AccountId, Account> AccountList(bool includeHiddenAccounts)
        {
            return _data.AccountList(includeHiddenAccounts);
        }

        public bool AccountCanAdd(Account account) { return this._data.Account_CanAdd(account); }
        public bool Account_CanRemove(AccountId id) { return this._data.Account_CanRemove(id); }
        public bool Account_CanReplace(AccountId id, Account account) { return this._data.Account_CanReplace(id, account); }

        public delegate void AddAccountHandler(object sender, AccountEventArgs e);
        public event AddAccountHandler AccountAdded;

        public delegate void RemoveAccountHandler(object sender, AccountEventArgs e);
        public event RemoveAccountHandler AccountRemoved;

        public delegate void ModifyAccountHandler(object sender, AccountEventArgs e);
        public event ModifyAccountHandler AccountModified;

        public delegate void AccountsChangedHandler(object sender);
        public event AccountsChangedHandler AccountsChanged;

        class ActionAddAccount : UserAction
        {
            private Accounts _accounts;
            private Account _account;
            private AccountId _id;

            public AccountId Id { get { return _id; } }

            public ActionAddAccount(Accounts accounts, Account account)
            {
                _id = new AccountId();
                _account = account;
                _accounts = accounts;
            }

            public override void Do()
            {
                _accounts._data.Account_Add(_account, _id);              
                _accounts.AccountAdded?.Invoke(this, new AccountEventArgs(_id));
                _accounts.AccountsChanged?.Invoke(this);               
            }

            public override void Undo()
            {
                //remove from database
                bool oK = _accounts._data.Account_Remove(_id);
                Debug.Assert(oK);

                //events
                _accounts.AccountRemoved?.Invoke(this, new AccountEventArgs(_id));

                _accounts.AccountsChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "add _account " + _account.Name;
            }
        }
        public AccountId AddAccount(Account account)
        {
            ActionAddAccount action = new Accounts.ActionAddAccount(this, account);
            ActionMaster.Do(action);

            return action.Id;
        }

        class ActionAccountRemove : UserAction
        {
            private Accounts _accounts;
            private Account _account;
            private AccountId _id;

            public ActionAccountRemove(Accounts accounts, AccountId id)
            {
                _id = id;
                _accounts = accounts;
                _account = _accounts._data.Account(_id);
            }

            public override void Do()
            {

                _accounts._data.Account_Remove(_id);
                
               
                    _accounts.AccountRemoved?.Invoke(this, new AccountEventArgs(_id));

                    _accounts.AccountsChanged?.Invoke(this);
               
                
            }

            public override void Undo()
            {
                bool oK = _accounts._data.Account_Add(_account, _id);
                Debug.Assert(oK);

                //events
                _accounts.AccountAdded?.Invoke(this, new AccountEventArgs(_id));
                _accounts.AccountsChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "delete account " + _account.Name;
            }
        }
        public bool Account_Remove(AccountId id)
        {
            if (!this.Account_CanRemove(id))
                return false;
            ActionAccountRemove action = new ActionAccountRemove(this, id);
            this.ActionMaster.Do(action);
            return true;
        }

        class ActionAccountReplace : UserAction
        {
            private Accounts _accounts;
            private Account _unusedAccount;
            private AccountId _id;

            public ActionAccountReplace(Accounts accounts, AccountId id, Account account)
            {
                _accounts = accounts;
                _id = id;
                _unusedAccount = account;
            }

            public override void Do()
            {
                _accounts._data.Account_Replace(_id, new Account(_unusedAccount), out _unusedAccount);
                 _accounts.AccountModified?.Invoke(this, new AccountEventArgs(_id));

                    _accounts.AccountsChanged?.Invoke(this);
               
            }

            public override void Undo()
            {
               _accounts._data.Account_Replace(_id, new Account(_unusedAccount), out _unusedAccount);
               
                _accounts.AccountModified?.Invoke(this, new AccountEventArgs(_id));

                _accounts.AccountsChanged?.Invoke(this);
            }

            public override string Description()
            {
                return "Replace Account " + _unusedAccount.Name;
            }
        }
        public bool Account_Replace(AccountId id, Account account)
        {
            if (!this.Account_CanReplace(id, account))
                return false;
            ActionAccountReplace action = new ActionAccountReplace(this, id, account);
            this.ActionMaster.Do(action);
            return true;
        }

        //END ACCOUNT/////////////////////////////////////////////////////////////////////////////////////



        //ENTRY////////////////////////////////////////////////////////
        public Entry Entry(EntryId id) { return _data.Entry(id); }
       
        public List<Entry> EntryList(Filter filter, EntrySorter sorter)
        {
            //if (sorter == null) sorter = this._sorter;
            if (_data != null) { return _data.EntryList(filter, sorter); }
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="reasons"></param>
        /// <returns></returns>
        public bool EntryCanAdd(Entry entry, ref string reason)
        {
            var entries = new List<Entry>();
            entries.Add(entry);
            var reasons = new List<string>();
            bool result = _data.Entry_CanAdd(entries, ref reasons);
            if (result == false)
            { reason = reasons.First();
                Debug.Assert(reasons.Count() == 1);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="reasons"></param>
        /// <returns></returns>
        public bool EntryCanAdd(IEnumerable<Entry> entries, ref List<string> reasons)
        {
            return _data.Entry_CanAdd(entries, ref reasons);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEntry"></param>
        /// <returns></returns>
        public bool EntryCanRemove(EntryId idEntry, ref string reason)
        {
            var ids = new List<EntryId>();
            ids.Add(idEntry);
            var reasons = new List<string>();
            bool result = _data.Entry_CanRemove(ids, ref reasons);
            if (result == false)
            { reason = reasons.First();
                Debug.Assert(reasons.Count() == 1);
            }
            return result;
        }

        public bool Entry_CanRemove(IEnumerable<EntryId> ids, ref List<string> reasons)
        {
            return _data.Entry_CanRemove(ids, ref reasons);
        }

        public bool EntryCanReplace(IEnumerable<Entry> entries)
        {
            return _data.Entry_CanModify(entries);
        } 
        public bool EntryCanReplace(Entry entry) 
        {
            var entries = new List<Entry>();
            entries.Add(entry);
            return _data.Entry_CanModify(entries); 
        }

        public delegate void EntriesChangedHandler(object sender, EntryEventArgs e);
        public event EntriesChangedHandler EntriesChanged;

        class ActionAddEntries : UserAction
        {
            private Accounts _accounts;
            private Dictionary<EntryId, Entry> _entries;

            public ActionAddEntries(Accounts accounts, IEnumerable<Entry> entries)
            {
                _accounts = accounts;
                _entries = new Dictionary<EntryId, Entry>();
                foreach (Entry entry in entries)
                    _entries.Add(new EntryId(), entry);
            }

            public override void Do()
            {
                _accounts._data.Entry_Add(_entries);
                _accounts.EntriesChanged?.Invoke(this, new EntryEventArgs(_entries.Values.AsEnumerable<Entry>().ToList()));                
            }

            internal IEnumerable<EntryId> GetIds() { return new List<EntryId>(_entries.Keys); }

            public override void Undo()
            {
                _accounts._data.Entry_Remove(_entries.Keys);
               
                _accounts.EntriesChanged?.Invoke(this, new EntryEventArgs(_entries.Values.AsEnumerable<Entry>().ToList()));
            }

            public override string Description()
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Entries - Add");
                foreach (Entry entry in _entries.Values)
                {
                    result.AppendLine(entry.ToString());
                }
                return result.ToString();
            }
        }

        public bool AddEntries(IEnumerable<Entry> entries, ref IEnumerable<EntryId> ids)
        {
            List<string> reason = new List<string>();
            if (!this.EntryCanAdd(entries, ref reason))
                return false;
            
            ActionAddEntries action = new Accounts.ActionAddEntries(this, entries);
            ActionMaster.Do(action);
            ids = action.GetIds();
            return true;
        }

        public EntryId AddEntry(Entry entry)
        {
            var entries = new List<Entry>();
            entries.Add(entry);
            IEnumerable<EntryId> ids = new List<EntryId>();
            return AddEntries(entries, ref ids) ? ids.First() : null;
        }

        class ActionDeleteEntries : UserAction
        {
            private Accounts _accounts;
            private Dictionary<EntryId, Entry> _entries;

            public ActionDeleteEntries(Accounts accounts, IEnumerable<EntryId> ids)
            {
                _accounts = accounts;
                _entries = new Dictionary<EntryId, Entry>();
                foreach (EntryId id in ids)
                    _entries.Add(id, _accounts.Entry(id));

            }

            public override void Do()
            {
                _accounts._data.Entry_Remove(_entries.Keys);
                _accounts.EntriesChanged?.Invoke(this, new EntryEventArgs(_entries.Values.AsEnumerable<Entry>().ToList()));
            }

            public override void Undo()
            {
                bool oK = _accounts._data.Entry_Add(_entries);
                Debug.Assert(oK);
                _accounts.EntriesChanged?.Invoke(this, new EntryEventArgs(_entries.Values.AsEnumerable<Entry>().ToList()));
            }

            public override string Description()
            {
                return "delete entries";
            }
        }

        public bool Entry_Remove(IEnumerable<EntryId> ids)
        {
            List<string> reasons = new List<string>();
            if (!this.Entry_CanRemove(ids, ref reasons))
                return false;
            var action = new ActionDeleteEntries(this, ids);
            ActionMaster.Do(action);
            return true;
        }

        public bool Entry_Remove(EntryId id)
        {
            var ids = new List<EntryId>();
            ids.Add(id);
            return Entry_Remove(ids);
        }
       
        class ActionReplaceEntries : UserAction
        {
            private Accounts _accounts;
            private List<Entry> _unusedEntries;

            public ActionReplaceEntries(Accounts accounts, IEnumerable<Entry> entries)
            {
                _accounts = accounts;
                _unusedEntries = new List<Entry>(entries);
            }
            public override void Do()
            {
                List<Entry> swap = new List<Entry>(_unusedEntries);               
                _accounts._data.Entry_Modify(swap, out _unusedEntries);
                _accounts.EntriesChanged?.Invoke(this, new EntryEventArgs(swap, _unusedEntries));
            }
            
            public override void Undo()
            {
                Do();
            }

            public override string Description()
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Entries - Replace");
                foreach (Entry entry in _unusedEntries)
                {
                    result.AppendLine(entry.ToString());
                }
                return result.ToString();
            }
        }

        public bool EntryReplace(IEnumerable<Entry> entries)
        {
            if (!this.EntryCanReplace(entries))
                return false;
            ActionReplaceEntries action = new ActionReplaceEntries(this, entries);
            this.ActionMaster.Do(action);
            return true;
        }
        public bool EntryReplace(Entry entry)
        {
            var entries = new List<Entry>();
            entries.Add(entry);
            return EntryReplace(entries);
        }
        //END ENTRY/////////////////////////////////////////////////////////////////////////////////////

        //decimal Sum(Filter filter)
        //{
        //    decimal total = 0;
        //    foreach (Entry entry in this.EntryList(filter))
        //    {
        //        total += entry.Amount;
        //    }
        //    return total;
        //}
        public bool IsModified()
        {
            return ActionMaster.CanUndo();
        }

        public void Create()
        {
            _sorter = new EntrySorter();
            _filters = new Dictionary<AccountId, Filter>();
            _data = new MemAccountsData();
            ActionMaster.Clear();



            _data.Create();
            //			Guid id;
            //			_data.TaxCodeAdd(new Item("Exempt", true), out id);
            TaxCodesChanged?.Invoke(this);
            CatagoriesChanged?.Invoke(this);
            PropertiesChanged?.Invoke(this);
            PayeesChanged?.Invoke(this);
            AccountsChanged?.Invoke(this);
        }

        public void Open(string filename)
        {
            _sorter = new EntrySorter();
            _filters = new Dictionary<AccountId, Filter>();
            _data = new MemAccountsData();
            if (_data.Open(filename))
            {
                TaxCodesChanged(this);
                CatagoriesChanged(this);
                PropertiesChanged(this);
                PayeesChanged(this);
                AccountsChanged(this);
            }
        }

        public bool SaveAs(string filename)
        {
            bool saved = _data.SaveAs(filename);
            if (saved) ActionMaster.Clear();
            return saved;
        }

		public bool ExportXML(string filename)
        {
            bool saved = _data.ExportXML(filename);
            //if (saved) ActionMaster.Clear();
            return saved;
        }


        public Accounts()
        {
            ActionMaster = new ActionMaster();

        }

        ~Accounts()
        {
            //
        }
        //Reconciliation////////////////////////////////////////////////////////////////////////////////
        private List<ReconcileEntry> reconciliationEntries = new List<ReconcileEntry>();
        public DateTime endDate;
        private AccountId reconciliationAccount;

        public void Reconcile_Start(AccountId accountId)
        {
            reconciliationAccount = accountId;
            reconciliationEntries = new List<ReconcileEntry>();
            
            //get a list is unreconciled entries for the given account.
            Filter flt = new Filter();
            flt.StartDate = Account(accountId).ReconciledOn.AddDays(1);
            flt.Accounts.Add(accountId);
            List<Entry> entries = EntryList(flt, this._sorter);

            //add all imported            
            foreach (Entry ent in entries)
            {
                if (ent.Type == AccountsCore.Entry.eType.Imported)
                {
                    ReconcileEntry re = new ReconcileEntry();
                    re.Imported = ent;
                    reconciliationEntries.Add(re);
                    endDate = endDate < ent.Date ? ent.Date : endDate;
                }
            }
            entries.RemoveAll(x => x.Type == AccountsCore.Entry.eType.Imported);

            //look for a perfect match in the r
            foreach (var re in reconciliationEntries)
            {
                DateTime date = re.Imported.Date;
                decimal amount = re.Imported.GetAmount(accountId);
                int matches = entries.Count(x => x.Date == date && x.GetAmount(accountId) == amount);
                if (matches == 1)
                {
                    Entry match = entries.Find(x => x.Date == date && x.GetAmount(accountId) == amount);
                    re.Predicted = match;
                    entries.Remove(match);
                }
            }

            //look for a fuzzy match in the r
            foreach (var re in reconciliationEntries)
            {
                DateTime date = re.Imported.Date;
                decimal amount = re.Imported.GetAmount(accountId);
                double daysDeviation = 5.0;
                decimal poundDeviation = 1;
                int fuzzymatches = entries.Count(x => (Math.Abs((x.Date - date).TotalDays) <= daysDeviation) && (Math.Abs(x.GetAmount(accountId) - amount) <= poundDeviation));
                if (fuzzymatches == 1)
                {
                    Entry match = entries.Find(x => (Math.Abs((x.Date - date).TotalDays) <= daysDeviation) && (Math.Abs(x.GetAmount(accountId) - amount) <= poundDeviation));
                    double td = (match.Date - date).TotalDays;
                    re.Predicted = match;
                    entries.Remove(match);
                }
            }

            //add the remaining
            foreach (Entry ent in entries)
            {
                ReconcileEntry re = new ReconcileEntry();
                re.Predicted = ent;
                reconciliationEntries.Add(re);
            }
           


            //    int matches = entries.Count(x => x.Date == date && x.GetAmount(accountId) == amount);
            //    if (matches == 1)
            //    {
            //        Entry match = entries.Find(x => x.Date == date && x.GetAmount(accountId) == amount);
            //        re.entryB = match.Id;
            //        entries.Remove(match);
            //    }





            //
            // foreach(Entry ent in entries)
            //     ent.Amount = Math.Round(ent.Amount * 1.00m, 2, MidpointRounding.AwayFromZero);

            //while (entries.Count != 0)
            //{
            //    //remove the last entry in the list and add to a new reconcile entry
            //    ReconcileEntry re = new ReconcileEntry();
            //    re.entryA = entries.Last().Id;
            //    DateTime date = entries.Last().Date;
            //    decimal amount = entries.Last().GetAmount(accountId);
            //    entries.Remove(entries.Last());

            //    //look for a match in the remaining entries and add if found
            //    int matches = entries.Count(x => x.Date == date && x.GetAmount(accountId) == amount);
            //    if (matches == 1)
            //    {
            //        Entry match = entries.Find(x => x.Date == date && x.GetAmount(accountId) == amount);
            //        re.entryB = match.Id;
            //        entries.Remove(match);
            //    }
            //    else
            //    {
            //        double daysDeviation = 3.0;
            //        decimal poundDeviation = 1;
            //        int fuzzymatches = entries.Count(x => (Math.Abs((x.Date - date).TotalDays) <= daysDeviation) && (Math.Abs(x.GetAmount(accountId) - amount) <= poundDeviation));
            //        if (fuzzymatches == 1)
            //        {                        
            //            Entry match = entries.Find(x => (Math.Abs((x.Date - date).TotalDays) <= daysDeviation) && (Math.Abs(x.GetAmount(accountId) - amount) <= poundDeviation));
            //            double td = (match.Date - date).TotalDays;
            //            re.entryB = match.Id;
            //            entries.Remove(match);
            //        }
            //    }

            //    //add the new re to the list.
            //    reconciliationEntries.Add(re);
            // }
        }

        public void Reconcile_Match(ReconcileEntryId A, ReconcileEntryId B)
        {
            if (Reconcile_CanMatch(A, B))
            {
                ReconcileEntry reA = reconciliationEntries.Find(x => x.Id == A);
                ReconcileEntry reB = reconciliationEntries.Find(x => x.Id == B);
                if (reA.Predicted != null)
                {
                    reB.Predicted = reA.Predicted;
                    reconciliationEntries.Remove(reA);
                }
                else
                {
                    reA.Predicted = reB.Predicted;
                    reconciliationEntries.Remove(reB);
                }
            }
        }

        public bool Reconcile_CanMatch(ReconcileEntryId A, ReconcileEntryId B)
        {
            ReconcileEntry reA = reconciliationEntries.Find(x => x.Id == A);
            ReconcileEntry reB = reconciliationEntries.Find(x => x.Id == B);

            bool OKWithAImported = reA != null && reA.Imported != null && reA.Predicted == null
                && reB != null && reB.Predicted != null && reB.Imported == null
                ? true : false;

            bool OKWithBImported = reB != null && reB.Imported != null && reB.Predicted == null
                && reA != null && reA.Predicted != null && reA.Imported == null
                ? true : false;

            return OKWithAImported || OKWithBImported;
        }

        public void Reconcile_UnMatch(List<ReconcileEntryId> ids)
        {
            foreach (ReconcileEntryId id in ids)
            {
                ReconcileEntry re1 = reconciliationEntries.Find(x => x.Id == id);

                if (re1.Predicted == null || re1.Imported == null || re1.reconciled == true) continue;

                ReconcileEntry re2 = new ReconcileEntry();
                re2.Predicted = re1.Predicted;
                reconciliationEntries.Add(re2);

                re1.Predicted = null;
                re1.reconciled = false;
            }
        }

        public void Reconcile_Reconcile(List<ReconcileEntryId> ids)
        {
            foreach (ReconcileEntryId entry in ids)
            {
                ReconcileEntry re = reconciliationEntries.Find(x => x.Id == entry);
                if (re.Imported == null)
                    return;
            }

            foreach (ReconcileEntryId entry in ids)
            {
                ReconcileEntry re = reconciliationEntries.Find(x => x.Id == entry);
                re.reconciled = true;
            }            
        }

        public void Reconcile_UnReconcile(List<ReconcileEntryId> entries)
        {
            foreach (ReconcileEntryId entry in entries)
                reconciliationEntries.Find(x => x.Id == entry).reconciled = false;
        }

        public void Reconcile_Remove(List<ReconcileEntryId> ids)
        {
            var entriesToRemove = new List<EntryId>();
            foreach (ReconcileEntryId entry in ids)
            {
                ReconcileEntry re = reconciliationEntries.Find(x => x.Id == entry);
                string reason = "";
                if (re.Imported != null || re.Predicted.Date > endDate || !EntryCanRemove(re.Predicted.Id, ref reason))
                    return;
                reconciliationEntries.Remove(re);
            }
        }

        public void Reconcile_End()
        {
            reconciliationEntries.RemoveAll(x => x.reconciled == false);

            foreach (ReconcileEntry re in reconciliationEntries)
            {
                if (re.Predicted != null)
                {
                    re.Predicted.SetAmount(re.Imported.AccountId, re.Imported.GetAmount(re.Imported.AccountId));
                    re.Predicted.Date = re.Imported.Date;
                    re.Predicted.Description = re.Predicted.Description + " (" + re.Imported.Description + ")";
                    re.Predicted.Type = AccountsCore.Entry.eType.Confirmed;
                    re.Imported = null;
                }
                else
                {
                    re.Imported.Type = AccountsCore.Entry.eType.Confirmed;
                }
            }            

            TimeSpan ts = new TimeSpan(1,0,0,0);
            Filter flt = new Filter();
            flt.Accounts.Add(reconciliationAccount);
            flt.StartDate = Account(reconciliationAccount).ReconciledOn + ts;
            flt.EndDate = this.endDate;
            List<Entry> ents = EntryList(flt, this._sorter);
            List<EntryId> ids = new List<EntryId>();

            foreach (Entry ent in ents)
            {
                if ((reconciliationEntries.Find(x => x.Imported != null && x.Imported.Id == ent.Id) == null) 
                    && (reconciliationEntries.Find(x => x.Predicted != null && x.Predicted.Id == ent.Id) == null))
                {
                    ids.Add(ent.Id);
                }
            }
            Entry_Remove(ids);

            foreach (ReconcileEntry re in reconciliationEntries)
            {
                if (re.Imported != null)
                    EntryReplace(re.Imported);
                if (re.Predicted != null)
                    EntryReplace(re.Predicted);

                //   if (Entry(re.Imported.Id) != null)
                //        EntryReplace(re.Imported.Id, re.Imported);
                //    else
                //        AddEntry(re.Imported);
            }            

            Account(this.reconciliationAccount).ReconciledOn = endDate;

            reconciliationEntries = new List<ReconcileEntry>();
            endDate = new DateTime();
            reconciliationAccount = null;
        }

        public bool Reconcile_CanEnd()
        {
            foreach (ReconcileEntry re in reconciliationEntries)
            {
                DateTime date = re.Imported != null ? re.Imported.Date : re.Predicted.Date;
                if (re.reconciled == false && date <= endDate)
                    return false;
            }
            return true;
        }

        public class ReconcileEntryId : Id { }

        public class ReconcileEntry
        {
            public readonly ReconcileEntryId Id = new ReconcileEntryId();
            public Entry Imported;
            public Entry Predicted;
            public bool reconciled;
            public ReconcileEntry() { }
        }

        public List<ReconcileEntry> Reconcile_ListEntries()
        {
            var result = new List<ReconcileEntry>(reconciliationEntries);
            result.Sort(new ReconcileEntryComparer());
            return result;
        }

        public class ReconcileEntryComparer : IComparer<ReconcileEntry>
        {
            public int Compare(ReconcileEntry x, ReconcileEntry y)
            {
                DateTime xDate = x.Imported != null ? x.Imported.Date : x.Predicted.Date;
                DateTime yDate = y.Imported != null ? y.Imported.Date : y.Predicted.Date;
                return DateTime.Compare(xDate, yDate);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //
    }
}
