using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.EntityClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Controls;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections;

namespace AccountsCore
{


    [Serializable]
    public abstract class Id
	 {
		internal Id(Guid guid) { _id = guid; }
		internal Id() { _id = Guid.NewGuid(); }
		//internal void NewId(){_id = Guid.NewGuid(); }
		//public static Id Empty() { return new Id(); }
		public bool IsEmpty() { return (_id == Guid.Empty); }
		public Guid GetGuid() { return _id; }
		static public bool operator==(Id lhs, Id rhs) 
		{ 
			if(((System.Object)lhs) == null && ((System.Object)rhs) == null)
			{
				return true;
			}

			if(((System.Object)lhs) == null || ((System.Object)rhs) == null)
			{
				return false;
			}

			return (lhs._id == rhs._id);
		}
		static public bool operator!=(Id lhs, Id rhs) { return !(lhs == rhs); }
		public override int GetHashCode() { return _id.GetHashCode(); }
		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			Id rhsId = obj as Id;
			if ((System.Object)rhsId == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (this == rhsId);
		}

		public bool Equals(Id rhsId)
		{
			// If parameter is null return false:
			if ((object)rhsId == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (this == rhsId);
		}
		private Guid _id;
	}


    [Serializable]
    public class AccountId : Id
	{
		
	}

    [Serializable]
    public class TaxCodeId : Id
	{
		
	}

    [Serializable]
    public class EntryId : Id { }


    [Serializable]
    public class PayeeId : Id
	{
		
	}

    [Serializable]
    public class CatagoryId : Id
	{
       
    }

    [Serializable]
    public class PropertyId : Id
	{
		
	}

	/*
	 * {
		internal AccountId(Guid guid) { _id = guid; }
		internal AccountId() {_id = Guid.Empty; }
		internal void NewId(){_id = Guid.NewGuid(); }
		public static AccountId Empty() { return new AccountId(); }
		public bool IsEmpty() { return (_id == Guid.Empty); }
		public Guid GetGuid() { return _id; }
		static public bool operator==(AccountId lhs, AccountId rhs) { return(lhs._id == rhs._id); }
		static public bool operator!=(AccountId lhs, AccountId rhs) { return !(lhs == rhs); }
		private Guid _id;
	}
	 * */
	
	interface IAccountsData
	{
		bool Open(string filename);
		void Create();
        bool SaveAs(string filename);

		bool ExportXML(string filename);


        //Catagories
		bool Catagory_CanAdd(Catagory catagory);
		bool Catagory_Add(Catagory catagory, CatagoryId id);

		bool Catagory_CanRemove(CatagoryId id);
		bool Catagory_Remove(CatagoryId id);

		bool Catagory_CanReplace(CatagoryId id, Catagory catagory);
		bool Catagory_Replace(CatagoryId id, Catagory catagory, out Catagory oldCatagory);

		//Catagory CreateCatagory();
		Catagory Catagory(CatagoryId id);
        IdClassListReadOnly<CatagoryId, Catagory> CatagoryList { get; }



		//TaxCodes
		bool TaxCode_CanAdd(TaxCode taxCode);
		bool TaxCode_Add(TaxCode taxCode, TaxCodeId id);

		bool TaxCode_CanRemove(TaxCodeId id);
		bool TaxCode_Remove(TaxCodeId id);

		bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode);
		bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode, out TaxCode oldTaxCode);

		TaxCode TaxCode(TaxCodeId id);
        IdClassListReadOnly<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems);



		//Property
		bool Property_CanAdd(Property property);
		bool Property_Add(Property property, PropertyId id);

		bool Property_CanRemove(PropertyId id);
		bool Property_Remove(PropertyId id);

		bool Property_CanReplace(PropertyId id, Property property);
		bool Property_Replace(PropertyId id, Property property, out Property oldProperty);

		Property Property(PropertyId id);
        IdClassListReadOnly<PropertyId, Property> PropertyList { get; }



		//Payees
		bool Payee_CanAdd(Payee payee);
		bool Payee_Add(Payee payee, PayeeId id);

		bool Payee_CanRemove(PayeeId id);
		bool Payee_Remove(PayeeId id);

		bool Payee_CanReplace(PayeeId id, Payee payee);
		bool Payee_Replace(PayeeId id, Payee payee, out Payee oldPayee);

		Payee Payee(PayeeId id);
        IdClassListReadOnly<PayeeId, Payee> PayeeList { get; }

		//Accounts
		bool Account_CanAdd(Account account);
		bool Account_Add(Account accountHeader, AccountId id);

		bool Account_CanRemove(AccountId id);
		bool Account_Remove(AccountId id);

		bool Account_CanReplace(AccountId id, Account account);
		bool Account_Replace(AccountId id, Account account, out Account oldAccount);

		Account Account(AccountId id);
        IdClassListReadOnly<AccountId, Account> AccountList(bool includeHiddenItems);

        bool Entry_CanAdd(IEnumerable<Entry> entries, ref List<string> reasons);
        bool Entry_Add(Dictionary<EntryId, Entry> entries);		

		bool Entry_CanRemove(IEnumerable<EntryId> ids, ref List<string> reasons);
        bool Entry_Remove(IEnumerable<EntryId> ids);

		bool Entry_CanModify(IEnumerable<Entry> entries);
        bool Entry_Modify(IEnumerable<Entry> entries, out List<Entry> oldEntries);

		Entry Entry(EntryId id);
        List<Entry> EntryList(Filter filter, EntrySorter sorter);



        //bool Commit();
        //bool Commit(string connection);
        //bool Close();








    }

	
	



	public class MemAccountsData : IAccountsData
	{
        private IdClassListWritable<CatagoryId, Catagory> _catagoryList = new IdClassListWritable<CatagoryId, Catagory>();		
		private  IdClassListWritable<TaxCodeId, TaxCode> _taxCodeList = new IdClassListWritable<TaxCodeId, TaxCode>();	
		private  IdClassListWritable<PropertyId, Property> _propertyList = new IdClassListWritable<PropertyId, Property>();
        private IdClassListWritable<PayeeId, Payee> _payeeList = new IdClassListWritable<PayeeId, Payee>();	
		private  IdClassListWritable<AccountId, Account> _accountList = new IdClassListWritable<AccountId, Account>();
        private IdClassListWritable<EntryId, Entry> _entryList = new IdClassListWritable<EntryId, Entry>();

        private string fileName_;
				
		public MemAccountsData()
		{
            
        }

		

		//BinaryFormatter formatter = new BinaryFormatter();
			//using(Stream fStream = new FileStream(_filename, FileMode.Create, FileAccess.Write, FileShare.None))
			//{
			//	formatter.Serialize(fStream, _data);
			//}

        public void Create()
		{
            //bool bSystem = true;
            //this.TaxCode_Add(new TaxCodeWR("Not Taxable", false, bSystem), TaxCodeId.SystemTaxCode_NotTaxable);
            //this.Payee_Add(new Payee("Unknown", false, bSystem), PayeeId.SystemPayee_Unknown);
            //this.Property_Add(new Property("Unknown", false, bSystem), PropertyId.SystemProperty_Unknown);
            //Catagory sysCat = new Catagory("UnKnown", TaxCodeId.SystemTaxCode_NotTaxable, false, bSystem);
            //this.Catagory_Add(sysCat, CatagoryId.SystemCatagory_Unknown);
		}
        public bool SaveAs(string filename)
        {
            using(Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //SoapFormatter formatter = new SoapFormatter();
                    formatter.Serialize(fStream, _taxCodeList);
                    formatter.Serialize(fStream, _catagoryList);
                    formatter.Serialize(fStream, _propertyList);
                    formatter.Serialize(fStream, _payeeList);
                    formatter.Serialize(fStream, _accountList);
                    formatter.Serialize(fStream, _entryList);
                }
                catch (System.Runtime.Serialization.SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    return false;
                } 
            }
			return true;
		}

		public bool ExportXML(string filename)
		{
			var root = new XElement("Accounts");

			var xTaxCodes = new XElement("TaxCodes");
			foreach (KeyValuePair<TaxCodeId, TaxCode> pair in _taxCodeList)
				xTaxCodes.Add(pair.Value.toXElement(pair.Key));
			root.Add(xTaxCodes);

			var xCategories = new XElement("Categories");
			foreach (KeyValuePair<CatagoryId, Catagory> pair in _catagoryList)
				xCategories.Add(pair.Value.toXElement(pair.Key));
			root.Add(xCategories);

			var xProperty = new XElement("Properties");
			foreach (KeyValuePair<PropertyId, Property> pair in _propertyList)
				xProperty.Add(pair.Value.toXElement(pair.Key));
			root.Add(xProperty);

			var xPayee = new XElement("Payees");
			foreach (KeyValuePair<PayeeId, Payee> pair in _payeeList)
				xPayee.Add(pair.Value.toXElement(pair.Key));
			root.Add(xPayee);

			var xAccount = new XElement("Accounts");
			foreach (KeyValuePair<AccountId, Account> pair in _accountList)
				xAccount.Add(pair.Value.toXElement(pair.Key));
			root.Add(xAccount);

			var xEntry = new XElement("Entries");
			foreach (KeyValuePair<EntryId, Entry> pair in _entryList)
				xEntry.Add(pair.Value.toXElement(pair.Key));
			root.Add(xEntry);

			XDocument doc = new XDocument();
			doc.Add(root);

			doc.Save(filename);
			return true;
		}



		//using(StreamWriter wr = new StreamWriter(filename, false, Encoding.UTF8))
		//			{
		//				XmlSerializer ser = new XmlSerializer(typeof(IdClassListWritable<TaxCodeId, TaxCode>));
		//				ser.Serialize(wr, _taxCodeList);


		//				return new XElement(collectionName, collection.Select(x => new XElement("Item", new XAttribute("Object", x.Key), x.Value.SerializeToXElement().Elements())));
		//			}
		//			try
		//			{XElement contacts =
		//				new XElement("Contacts",
		//				new XElement("Contact",
		//        new XElement("Name", "Patrick Hines"),
		//        new XElement("Phone", "206-555-0144",
		//            new XAttribute("Type", "Home")),
		//        new XElement("phone", "425-555-0145",
		//            new XAttribute("Type", "Work")),
		//        new XElement("Address",
		//            new XElement("Street1", "123 Main St"),
		//            new XElement("City", "Mercer Island"),
		//            new XElement("State", "WA"),
		//            new XElement("Postal", "68042")
		//        )
		//    )
		//);


		//                }
		//                catch (System.Runtime.Serialization.SerializationException e)
		//                {
		//                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
		//                    return false;
		//                } 
		//            }

		public bool Open(string filename)
        {
            try
            {
                using (Stream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //SoapFormatter formatter = new SoapFormatter();
                    _taxCodeList = (IdClassListWritable<TaxCodeId, TaxCode>)formatter.Deserialize(fStream);
                    _catagoryList = (IdClassListWritable<CatagoryId, Catagory>)formatter.Deserialize(fStream);
                    _propertyList = (IdClassListWritable<PropertyId, Property>)formatter.Deserialize(fStream);
                    _payeeList = (IdClassListWritable<PayeeId, Payee>)formatter.Deserialize(fStream);
                    _accountList = (IdClassListWritable<AccountId, Account>)formatter.Deserialize(fStream);
                    _entryList = (IdClassListWritable<EntryId, Entry>)formatter.Deserialize(fStream);
                }
                fileName_ = filename;
            }
            catch (Exception ex)
            {
                return false;
            }


            //var foundBad = new List<EntryId>();
            //List<Entry> entriessss = EntryList(new Filter(), this._sort);
            //foreach (Entry entry in entriessss)
            //{
            //    if (entry.AccountId == null)
            //    {
            //         _entryList.Remove(entry.Id);
            //    }
            //}

            //foreach (KeyValuePair<EntryId, Entry> pair in _entryList)
            //{ 
            //    pair.Value.FixUpEmptyIds();
            //}

            return true;
        }
        
        
        
       

        //static void Serialize()
        //{
        //    // Create a hashtable of values that will eventually be serialized.
        //    Hashtable addresses = new Hashtable();
        //    addresses.Add("Jeff", "123 Main Street, Redmond, WA 98052");
        //    addresses.Add("Fred", "987 Pine Road, Phila., PA 19116");
        //    addresses.Add("Mary", "PO Box 112233, Palo Alto, CA 94301");

        //    // To serialize the hashtable and its key/value pairs,  
        //    // you must first open a stream for writing. 
        //    // In this case, use a file stream.
        //    FileStream fs = new FileStream("DataFile.dat", FileMode.Create);

        //    // Construct a BinaryFormatter and use it to serialize the data to the stream.
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    try
        //    {
        //        formatter.Serialize(fs, addresses);
        //    }
        //    catch (SerializationException e)
        //    {
        //        Console.WriteLine("Failed to serialize. Reason: " + e.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        fs.Close();
        //    }
        //}


        //static void Deserialize()
        //{
        //    // Declare the hashtable reference.
        //    Hashtable addresses = null;

        //    // Open the file containing the data that you want to deserialize.
        //    FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
        //    try
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();

        //        // Deserialize the hashtable from the file and 
        //        // assign the reference to the local variable.
        //        addresses = (Hashtable)formatter.Deserialize(fs);
        //    }
        //    catch (SerializationException e)
        //    {
        //        Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        fs.Close();
        //    }

        //    // To prove that the table deserialized correctly, 
        //    // display the key/value pairs.
        //    foreach (DictionaryEntry de in addresses)
        //    {
        //        Console.WriteLine("{0} lives at {1}.", de.Key, de.Value);
        //    }
        //}
		
		//catagories//////////////////////////////////////////////////////////////////////
        public Catagory Catagory(CatagoryId id)
		{
			return _catagoryList[id];
		}

        public IdClassListReadOnly<CatagoryId, Catagory> CatagoryList 
		{
            get
            {
                return (IdClassListReadOnly<CatagoryId, Catagory>)_catagoryList;
            }
		}

		public bool Catagory_CanAdd(Catagory catagory)
		{
			if(string.IsNullOrWhiteSpace(catagory.Name))
			{
				return false;
			}

            foreach (Catagory existing in this._catagoryList.Values)
            {
                if (existing.Name == catagory.Name)
                {
                    return false;
                }
            }            
			return true;	
		}

		public bool Catagory_Add(Catagory catagory, CatagoryId id)
		{
			if(!Catagory_CanAdd(catagory))
			{
				Debug.Assert(false);
				Catagory_CanAdd(catagory);
				return false;
			}
            _catagoryList.Add(id, catagory);
			return true;
		}
		
		public bool Catagory_CanRemove(CatagoryId id)
		{
			//the catagory must exist and not be a system catagory
            Catagory catagory = this._catagoryList[id];
            if ((catagory == null) || (catagory.System == true))
			{
				return false;
			}

			//the catagory must not be used by an entry
            foreach (Entry existing in this._entryList.Values)
            {
                if (existing.CatagoryId == id)
                {
                    return false;
                }
            }			
			return true;
		}
		
		public bool Catagory_Remove(CatagoryId id)
		{
			if(!Catagory_CanRemove(id))
			{
				Debug.Assert(false);
				Catagory_CanRemove(id);
				return false;
			}

            this._catagoryList.Remove(id);
			return true;
		}

		public bool Catagory_CanReplace(CatagoryId id, Catagory catagory)
		{
			if(string.IsNullOrWhiteSpace(catagory.Name))
			{
				return false;
			}

            Catagory old = this._catagoryList[id];

            //the catagory must exist and not be a system catagory
            if ((old == null) || (old.System == true))
            {
                return false;
            }

            //the new catagory must not be the same as the catagory
            //(No change)
            if (catagory == old)
            {
                return false;
            }

            //the new catagory must not have the same name as an existing catagory
            foreach(KeyValuePair<CatagoryId, Catagory> pair in this._catagoryList)
            {
                if( (pair.Value.Name == catagory.Name) && (pair.Key != id) )
                {
                    return false;
                }
            }
            return true;
		}

		public bool Catagory_Replace(CatagoryId id, Catagory catagory, out Catagory oldCatagory)
		{
			oldCatagory = null;			
			if(!Catagory_CanReplace(id, catagory))
			{
				Debug.Assert(false);
				Catagory_CanReplace(id, catagory);				
				return false;
			}

            this._catagoryList.Replace(id, catagory, out oldCatagory);
			return true;		
		}

		
		//end catagories/////////////////////////////////////////////////////////////////////


		//TAX CODES//////////////////////////////////////////////////////////////////////
		public TaxCode TaxCode(TaxCodeId id)
		{
            return (id == null || id.IsEmpty()) ? null : this._taxCodeList[id];
		}

        public IdClassListReadOnly<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems) 
		{
            if(includeObsoleteItems) return this._taxCodeList;

            IdClassListWritable<TaxCodeId, TaxCode> nonObsCodes = new IdClassListWritable<TaxCodeId, TaxCode>();
            foreach (KeyValuePair<TaxCodeId, TaxCode> pair in this._taxCodeList)
            {
                if (!pair.Value.Obsolete) nonObsCodes.Add(pair.Key, pair.Value);
            }
            return nonObsCodes;
        }

		public bool TaxCode_CanAdd(TaxCode taxCode)
		{
			if(string.IsNullOrWhiteSpace(taxCode.Name))
			{
				return false;
			}
			
			foreach(TaxCode existing in this._taxCodeList.Values)
            {
                if (existing.Name == taxCode.Name) return true;
            }
			return true;	
		}

		public bool TaxCode_Add(TaxCode taxCode, TaxCodeId id)
		{
			if(!TaxCode_CanAdd(taxCode))
			{
				Debug.Assert(false);
				TaxCode_CanAdd(taxCode);
				return false;
			}
            _taxCodeList.Add(id, taxCode);
			return true;
		}
		
		public bool TaxCode_CanRemove(TaxCodeId id)
		{
            //the tax code must exist and not be a system tax code
            if (this._taxCodeList[id] == null) return false;
            if (this._taxCodeList[id].System == true) return false;

            //the tax code must not be used by a catagory			
            foreach (Catagory catagory in this._catagoryList.Values)
            {
                if (catagory.TaxCodeId == id) return false;
            }
			return true;
		}
        		
		public bool TaxCode_Remove(TaxCodeId id)
		{
			if(!TaxCode_CanRemove(id))
			{
				Debug.Assert(false);
				TaxCode_CanRemove(id);				
				return false;
			}
            this._taxCodeList.Remove(id);
			return true;
		}

		public bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode)
		{
			if(string.IsNullOrWhiteSpace(taxCode.Name))
			{
				return false;
			}

            //the old tax code must exist and not be a system tax code
            if (this._taxCodeList[id] == null) return false;
            if (this._taxCodeList[id].System == true) return false;


            //the new tax code must not be the same as the old tax code
            //(No change)
            if (this._taxCodeList[id] == taxCode) return false;


            //the new tax code must not have the same name as an existing taxcode.
            foreach (KeyValuePair<TaxCodeId, TaxCode> pair in this._taxCodeList)
            {
               if ((pair.Value.Name == taxCode.Name) && (pair.Key != id)) return false;
            }
            return true;
		}

		public bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode, out TaxCode oldTaxCode)
		{
			oldTaxCode = null;
			
			if(!TaxCode_CanReplace(id, taxCode))
			{
				Debug.Assert(false);  //UI should not allow you to get this far
				TaxCode_CanReplace(id, taxCode);
				return false;
			}

            this._taxCodeList.Replace(id, taxCode, out oldTaxCode);
			return true;		
		}
		//END TAX CODES/////////////////////////////////////////////////////////////////////

		//PROPERTY//////////////////////////////////////////////////////////////////////
		public Property Property(PropertyId id)
        {
            return this._propertyList[id];
		}

		public IdClassListReadOnly<PropertyId, Property> PropertyList 
		{ 
			get
            {
                return this._propertyList;
            }			
		}

		public bool Property_CanAdd(Property property)
		{
			if(string.IsNullOrWhiteSpace(property.Name))
			{
				return false;
			}
			
			foreach(Property existing in this._propertyList.Values)
            {
              if(existing.Name == property.Name) return false;
            }
			return true;	
		}

		public bool Property_Add(Property property, PropertyId id)
		{
			if(!Property_CanAdd(property))
			{
				Debug.Assert(false);
				Property_CanAdd(property);
				return false;
			}
            this._propertyList.Add(id, property);
			return true;
		}
		
		public bool Property_CanRemove(PropertyId id)
		{
            if (this._propertyList[id] == null) return false;
            if (this._propertyList[id].System == true) return false;
            return true;
		}
		
		public bool Property_Remove(PropertyId id)
		{
			if(!Property_CanRemove(id))
			{
				Debug.Assert(false);
				Property_CanRemove(id);
				return false;
			}

            this._propertyList.Remove(id);
			return true;
		}

		public bool Property_CanReplace(PropertyId id, Property property)
		{
			if(string.IsNullOrWhiteSpace(property.Name))
			{
				return false;
			}

            //the old property must exist
            if (this._propertyList[id] == null) return false;

            //it must not be a system property
            if (this._propertyList[id].System == true) return false;

            //the new property must not be the same as the old
            //(No change)
            if (this._propertyList[id] == property)
            {
                return false;
            }

            //the new property must not have the same name as an existing property.
            foreach(KeyValuePair<PropertyId, Property> pair in this._propertyList)
            {
                if ((pair.Value.Name == property.Name) && (pair.Key != id)) return false;
            }
            return true;
		}

		public bool Property_Replace(PropertyId id, Property property, out Property oldProperty)
		{
            oldProperty = null;
            if (!Property_CanReplace(id, property))
            {
                Debug.Assert(false);  //UI should not allow you to get this far
                Property_CanReplace(id, property);
                return false;
            }
            this._propertyList.Replace(id, property, out oldProperty);
            return true;
		}
		//END PROPERTY/////////////////////////////////////////////////////////////////////

		//PAYEE//////////////////////////////////////////////////////////////////////
		//public IdClassListReadOnly<Item> PayeeList { get { return _payeeList; } }
		public Payee Payee(PayeeId id)
		{
            return this._payeeList[id];
		}

        public IdClassListReadOnly<PayeeId, Payee> PayeeList 
		{ 
			get 
			{
                return this._payeeList;		
			}
		}

		public bool Payee_CanAdd(Payee payee)
		{
			if(string.IsNullOrWhiteSpace(payee.Name))
			{
				return false;
			}
			
			foreach(Payee existing in this._payeeList.Values)
            {
                if (existing.Name == payee.Name) return false;
            }
            return true;
		}

		public bool Payee_Add(Payee payee, PayeeId id)
		{
			if(!Payee_CanAdd(payee))
			{
				Debug.Assert(false);
				Payee_CanAdd(payee);
				return false;
			}
            this._payeeList.Add(id, payee);
			return true;
		}
		
		public bool Payee_CanRemove(PayeeId id)
		{
            //the payee must exist and not be a system payee
            if (this._payeeList[id] == null) return false;

            if (this._payeeList[id].System) return false;

            //the payee must not be in use
            foreach(Entry entry in this._entryList.Values)
            {
                if (entry.PayeeId == id) return false;
            }

            return true;           
		}
		
		public bool Payee_Remove(PayeeId id)
		{
			if(!Payee_CanRemove(id))
			{
				Debug.Assert(false);
				Payee_CanRemove(id);
				return false;
			}
            this._payeeList.Remove(id);		
			return true;
		}

		public bool Payee_CanReplace(PayeeId id, Payee payee)
		{
			if(string.IsNullOrWhiteSpace(payee.Name))
			{
				return false;
			}

            //the payee must exist
            if (this._payeeList[id] == null) return false;

            //and not be a system payee
            if (this._payeeList[id].System) return false;

            //the new payee must not be the same as the old payee
			//(No change)
            if (this._payeeList[id] == payee) return false;

            //the new payee must not have the same name as an existing payee.
            foreach (Payee existing in this._payeeList.Values)
            {
                if (existing.Name == payee.Name) return false;
            }
            return true;
		}
			

		public bool Payee_Replace(PayeeId id, Payee payee, out Payee oldPayee)
		{
			if(!Payee_CanReplace(id, payee))
			{
				Debug.Assert(false);
				oldPayee = null;
				return false;
			}
            this._payeeList.Replace(id, payee, out oldPayee);
			return true;
		}
		//END PAYEE/////////////////////////////////////////+-////////////////////////////




		//ACCOUNT//////////////////////////////////////////////////////////////////////
		public Account Account(AccountId id) 
		{
            return this._accountList[id];
		}

		public IdClassListReadOnly<AccountId, Account> AccountList(bool includeHiddenItems)
		{
            IdClassListWritable<AccountId, Account> accounts = new IdClassListWritable<AccountId, Account>();
            if(!includeHiddenItems)
            {
                foreach(KeyValuePair<AccountId, Account> pair in this._accountList)
                {
                    if(pair.Value.Hidden == false)
                    {
                       accounts.Add(pair.Key, pair.Value);
                    }
                }
                return accounts;
            }
            return this._accountList;
		}

		public bool Account_CanAdd(Account account)
		{
			if(string.IsNullOrWhiteSpace(account.Name))
			{
				return false;
			}

            foreach (Account existing in this._accountList.Values)
			{
				if(existing.Name == account.Name) return false;
			}
            return true;	
		}

		public bool Account_Add(Account account, AccountId id)
		{
			if(!Account_CanAdd(account))
			{
				Debug.Assert(false);
				Account_CanAdd(account);
				return false;
			}
            this._accountList.Add(id, account);
			return true;
		}
		
		public bool Account_CanRemove(AccountId id)
		{
            //the account must exist and not be a system account
            if (this._accountList[id] == null) return false;
            Account existing = this._accountList[id];            
            if (existing.IsSystem()) return false;

            //the account must be empty
            foreach (Entry entry in this._entryList.Values)
            {
                if ((entry.AccountId == id) || (entry.TransferAccountId == id)) return false;
            }
            return true;          
		}
		
		public bool Account_Remove(AccountId id)
		{
			if(!Account_CanRemove(id))
			{
				Debug.Assert(false);
				Account_CanRemove(id);
				return false;
			}

            this._accountList.Remove(id);	
			return true;
		}

		public bool Account_CanReplace(AccountId id, Account account)
		{
			if(string.IsNullOrWhiteSpace(account.Name))
			{
				return false;
			}

            //the account must exist
            if (this._accountList[id] == null) return false;
            Account old = this._accountList[id];
           
            //the new account musst not equal the old account
            //(No change)
            if (old == account) return false;

            //the modified account must not have the same name as any other existing account
            foreach (KeyValuePair<AccountId, Account> pair in this._accountList)
            {
                if ((pair.Value.Name == account.Name) && (pair.Key != id)) return false;
            }
            return true;
        }

		public bool Account_Replace(AccountId id, Account header, out Account oldHeader)
		{
			oldHeader = null;
			if(!Account_CanReplace(id, header))
			{
				Debug.Assert(false);
				oldHeader = null;
				return false;
			}

            this._accountList.Replace(id, header, out oldHeader);
			return true;	
		}
        //END ACCOUNT/////////////////////////////////////////////////////////////////////

        //ENTRY//////////////////////////////////////////////////////////////////////
        public Entry Entry(EntryId id)
        {
            if ((id == null) || (_entryList[id] == null))
                return null;
                        
            Entry result = new Entry(_entryList[id]);
            result.Id = id;
            return result;
        }

        public List<Entry> EntryList(Filter filter, EntrySorter sorter)
        {
            var result = new List<Entry>();
            foreach (KeyValuePair<EntryId, Entry> pair in this._entryList)
            {
                if (filter.Test(pair.Value))
                {
                    Entry ent = new Entry(pair.Value);
                    ent.Id = pair.Key;
                    result.Add(ent);
                }
            }
            if(sorter != null)
                result.Sort(sorter);

            if (filter.Accounts.Count == 1)
            {
                decimal total = 0m;
                foreach (Entry e in result)
                {
                    total += e.GetAmount(filter.Accounts[0]);
                    e.Balance = total;
                }
            }

            return result;
        }

		public bool Entry_CanAdd(IEnumerable<Entry> entries, ref List<string> reasons)
        {
            if (entries.Count() == 0)
                reasons.Add("No entries to add");

            foreach (Entry entry in entries)
            {
                if (entry.AccountId == null)
                {   reasons.Add("No account selected");
                    return reasons.Count() == 0;
                }

                if (this._accountList[entry.AccountId] == null)
                {
                    reasons.Add("Account does not exist");
                    return reasons.Count() == 0;
                }

                //locked or reconciled account reasons
                string reason = "";
                    Account account = _accountList[entry.AccountId];
                if (account.Lock == AccountsCore.Account.eLock.Locked)
                    reasons.Add("Account is locked, ");
                else if ((account.Lock == AccountsCore.Account.eLock.ByDate) && (entry.Date < account.LockedUntil))
                    reasons.Add("Account is locked on the given date, ");

                if (entry.Date <= account.ReconciledOn)
                    reasons.Add("Account is reconciled on the given date");

                if ((entry.TransferAccountId != null) && (_accountList[entry.TransferAccountId] == null))
                {
                    Account transferAccount = _accountList[entry.TransferAccountId];

                    if (transferAccount.Lock == AccountsCore.Account.eLock.Locked)
                        reasons.Add("Account is locked, ");
                    else if ((transferAccount.Lock == AccountsCore.Account.eLock.ByDate) && (entry.Date < transferAccount.LockedUntil))
                        reasons.Add("Account is locked on the given date, ");

                    if (entry.Date <= transferAccount.ReconciledOn)
                        reasons.Add("Account is reconciled on the given date");
                }
            }
            return reasons.Count() == 0;
        }
		

            
      
		//public bool Entry_Add(Entry entry, EntryId id)
		//{
  //          string reason;
  //          if (!Entry_CanAdd(entry, out reason))
  //          {
		//		Debug.Assert(false);                
  //              Entry_CanAdd(entry, out reason);
		//		return false;
		//	}

  //          this._entryList.Add(id, entry);
		//	return true;
		//}

        public bool Entry_Add(Dictionary<EntryId, Entry> entries)
        {
            var reasons = new List<String>();
            if (!Entry_CanAdd(entries.Values, ref reasons))
            {
                Debug.Assert(false);                
                return false;
            }

            foreach (KeyValuePair<EntryId, Entry> kvp in entries)
                _entryList.Add(kvp.Key, kvp.Value);

            return true;
        }		
		
		public bool Entry_CanRemove(IEnumerable<EntryId> entryIds, ref List<string> reasons)
        {
            if (entryIds.Count() == 0)
                reasons.Add("No entries to remove");

            foreach (EntryId id in entryIds)
            {
                //the existing entry must exist
                Entry existing = _entryList[id];
                if (existing == null)
                {
                    reasons.Add("The entry does not exist");
                }
                else
                {
                    var accountsToCheck = new List<AccountId>();
                    accountsToCheck.Add(existing.AccountId);
                    if (existing.TransferAccountId != null) accountsToCheck.Add(existing.TransferAccountId);

                    foreach (AccountId accountId in accountsToCheck)
                    {
                        //the account must not be locked at the new or old dates
                        if (_accountList[accountId].IsLocked(existing.Date))
                        {
                            string reason = "Thw account " + _accountList[accountId].Name + " is locked";
                            reasons.Add(reason);
                        }

                        //if there are balance or date changes to the entry the account must not be reconciled at the new date or old dates
                        if (_accountList[accountId].IsReconciled(existing.Date) && (existing.GetAmount(accountId) != 0.0m))
                        {
                            string reason = "Thw account " + _accountList[accountId].Name + " is reconciled";
                            reasons.Add(reason);
                        }
                    }
                }
            }
            return reasons.Count() == 0;
        }
		
		public bool Entry_Remove(IEnumerable<EntryId> ids)
        {
            var reasons = new List<string>();
            if (!Entry_CanRemove(ids, ref reasons))
            {
                Debug.Assert(false);
                return false;
            }

            foreach (EntryId idEntry in ids)
                _entryList.Remove(idEntry);

            return true;
        }

        private bool entry_CanModify(Entry entry)
		{
            if (entry.Id == null) return false;
            
            //the existing entry must exist
            Entry existing = this._entryList[entry.Id];
            if (existing == null) return false;

            //The entries account can not be modified.
            var accountsToCheck = new List<AccountId>();
            accountsToCheck.Add(entry.AccountId);
            accountsToCheck.Add(existing.AccountId);
            if (entry.TransferAccountId != null) accountsToCheck.Add(entry.TransferAccountId);
            if (existing.TransferAccountId != null) accountsToCheck.Add(existing.TransferAccountId);

            foreach (AccountId accountId in accountsToCheck)
            {
                Account account = _accountList[accountId];
                //the account must not be locked at the new or old dates
                if (account.IsLocked(entry.Date) || account.IsLocked(existing.Date)) return false;

                //if there are balance or date changes to the entry the account must not be reconciled at the new date or old dates
                if ((account.IsReconciled(entry.Date)) || (account.IsReconciled(existing.Date)))
                {
                    if (entry.GetAmount(accountId) != existing.GetAmount(accountId)) return false;
                    if (entry.Date != existing.Date) return false;
                }
            }
            return true;
		}

		private bool entry_Modify(Entry entry, out Entry oldEntry)
		{
			oldEntry = null;
			if(!entry_CanModify(entry))
			{
				Debug.Assert(false);
                entry_CanModify(entry);				
				return false;
			}

            this._entryList.Replace(entry.Id, entry, out oldEntry);
			return true;		
		}

        public bool Entry_CanModify(IEnumerable<Entry> entries)
        {
            foreach (Entry entry in entries)
            {
                if (!entry_CanModify(entry))
                    return false;
            }
            return true;
        }

        public bool Entry_Modify(IEnumerable<Entry> entries, out List<Entry> oldEntries)
        {

            oldEntries = null;
            if(!Entry_CanModify(entries))
            {
                Debug.Assert(false);
                return false;
            }

            var oldEntList = new List<Entry>();
            foreach (Entry entry in entries)
            {                
                Entry oldEntry;
                this._entryList.Replace(entry.Id, entry, out oldEntry);
                oldEntList.Add(oldEntry);
            }
            oldEntries = oldEntList;
            return true;
        }
        //END ENTRY/////////////////////////////////////////////////////////////////////
    }
}