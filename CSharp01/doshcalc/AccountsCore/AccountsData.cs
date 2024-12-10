using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.EntityClient;


namespace AccountsCore
{
	
	
	public abstract class Id
	 {
		internal Id(Guid guid) { _id = guid; }
		internal Id() {_id = Guid.Empty; }
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
	

	public class AccountId : Id
	{
		public static AccountId Empty() { return new AccountId(); }			
		internal AccountId(Guid id) : base(id) {}
		private AccountId() {}		
	}

	public class TaxCodeId : Id
	{
		public static TaxCodeId Empty() { return new TaxCodeId(); }		
		internal static readonly TaxCodeId SystemTaxCode_NotTaxable;
		static TaxCodeId() {SystemTaxCode_NotTaxable = new TaxCodeId(new Guid("53c8d0ce-f595-4140-ac02-26d00f2998d9"));}		
		internal TaxCodeId(Guid id) : base(id) {}
		private TaxCodeId() {}		
	}

	public class EntryId : Id
	{
		public static EntryId Empty() { return new EntryId(); }			
		public EntryId(Guid id) : base(id) {}
		internal EntryId() {} //internal to allow the creation of entries by accounts for importing		
	}

	public class PayeeId : Id
	{
		public static PayeeId Empty() { return new PayeeId(); }	
		internal static readonly PayeeId SystemPayee_Unknown;
		static PayeeId() {SystemPayee_Unknown = new PayeeId(new Guid("1118d0ce-f595-4140-ac02-26d00f2998d9"));}
		internal PayeeId(Guid id) : base(id) {}
		private PayeeId() {}		
	}

	public class CatagoryId : Id
	{
		public static CatagoryId Empty() { return new CatagoryId(); }	
		internal static readonly CatagoryId SystemCatagory_Unknown;
		static CatagoryId() {SystemCatagory_Unknown = new CatagoryId(new Guid("2228d0ce-f595-4140-ac02-26d00f2998d9"));}
		internal CatagoryId(Guid id) : base(id) {}
		private CatagoryId() {}		
	}

	public class PropertyId : Id
	{
		public static PropertyId Empty() { return new PropertyId(); }	
		internal static readonly PropertyId SystemProperty_Unknown;
		static PropertyId() {SystemProperty_Unknown = new PropertyId(new Guid("3338d0ce-f595-4140-ac02-26d00f2998d9"));}
		internal PropertyId(Guid id) : base(id) {}
		private PropertyId() {}		
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
		void Open(string filename);
		void Create(string filename);
		void Close();
		
		//Catagories
		bool Catagory_CanAdd(Catagory catagory);
		bool Catagory_Add(Catagory catagory, CatagoryId id);

		bool Catagory_CanRemove(CatagoryId id);
		bool Catagory_Remove(CatagoryId id);

		bool Catagory_CanReplace(CatagoryId id, Catagory catagory);
		bool Catagory_Replace(CatagoryId id, Catagory catagory, out Catagory oldCatagory);

		Catagory CreateCatagory();
		Catagory Catagory(CatagoryId id);
		Dictionary<CatagoryId, Catagory> CatagoryList {get;}



		//TaxCodes
		bool TaxCode_CanAdd(TaxCode taxCode);
		bool TaxCode_Add(TaxCode taxCode, TaxCodeId id);

		bool TaxCode_CanRemove(TaxCodeId id);
		bool TaxCode_Remove(TaxCodeId id);

		bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode);
		bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode, out TaxCode oldTaxCode);

		TaxCode TaxCode(TaxCodeId id);
		Dictionary<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems);



		//Property
		bool Property_CanAdd(Property property);
		bool Property_Add(Property property, PropertyId id);

		bool Property_CanRemove(PropertyId id);
		bool Property_Remove(PropertyId id);

		bool Property_CanReplace(PropertyId id, Property property);
		bool Property_Replace(PropertyId id, Property property, out Property oldProperty);

		Property Property(PropertyId id);
		Dictionary<PropertyId, Property> PropertyList {get;}



		//Payees
		bool Payee_CanAdd(Payee payee);
		bool Payee_Add(Payee payee, PayeeId id);

		bool Payee_CanRemove(PayeeId id);
		bool Payee_Remove(PayeeId id);

		bool Payee_CanReplace(PayeeId id, Payee payee);
		bool Payee_Replace(PayeeId id, Payee payee, out Payee oldPayee);

		Payee Payee(PayeeId id);
		Dictionary<PayeeId, Payee> PayeeList {get;}

		//Accounts
		bool Account_CanAdd(Account account);
		bool Account_Add(Account accountHeader, AccountId id);

		bool Account_CanRemove(AccountId id);
		bool Account_Remove(AccountId id);

		bool Account_CanReplace(AccountId id, Account account);
		bool Account_Replace(AccountId id, Account account, out Account oldAccount);

		Account Account(AccountId id);
		Dictionary<AccountId, Account> AccountList(bool includeHiddenItems);

		bool Entry_CanAdd(Entry entry);
		bool Entry_Add(Entry entry, EntryId idEntry);
		bool Entry_Add(Dictionary<EntryId, Entry> entries);
		

		bool Entry_CanRemove(EntryId idEntry);
		bool Entry_Remove(EntryId idEntry);
		bool Entry_Remove(IEnumerable<EntryId> ids);

		bool Entry_CanModify(EntryId idEntry, Entry entry);
		bool Entry_Modify(EntryId idEntry, Entry entry, out Entry oldEntry);

		Entry Entry(EntryId id);
		Dictionary<EntryId, Entry>EntryList(AccountId AccountId);



		//bool Commit();
		//bool Commit(string connection);
		//bool Close();

		
	}
/*
	public class SQLAccountsData : IAccountsData
	{
		private  IdClassListWritable<Catagory> _catagoryList = new IdClassListWritable<Catagory>();	
		private System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
		private System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
		
		public void Connect()
		{
			
			csb.InitialCatalog = "Property02";
			csb.DataSource = "foggy-24ca58c98";
			csb.ConnectTimeout = 30;
			csb.IntegratedSecurity = true;
			
			sqlConnection.ConnectionString = csb.ConnectionString;
			sqlConnection.Open();
		}

		public bool CatagoryCanAdd(Catagory catagory)
		{
			if(!catagory.IsValid())
			{
				return false;
			}

			foreach(Catagory c in _catagoryList.Values)
			{
				if(!c.IsCompatible(catagory))
				{
					return false;
				}
			}	
			
			return true;
		}

		public bool CatagoryAdd(Catagory catagory, out Guid id)
		{
			string sql = string.Format("Insert Into Category" + 
			"(Name, TaxCodeId, Income, PropertySpecific) Values" + 
			"('{0}', '{1}', '{2}', '{3}')", catagory.Name, catagory.TaxCodeId, catagory.Income ? 1 : 0, catagory.PropertySpecific ? 1 : 0);

			string sqlres = "Select @@Identity";

			try
			{
				using(System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, this.sqlConnection))
				{
				
					cmd.ExecuteNonQuery();
				
					cmd.CommandText = sqlres;
					id = (Guid)((decimal)cmd.ExecuteScalar());
					return true;
				}
			}	
			catch(Exception)
			{
				
			}
			id = 0;
					
			return false;
		}

		public bool CatagoryCanRemove(Guid id)
		{
			bool bOK = _catagoryList.Keys.Contains(id);			
			return bOK;
		}
		
		public bool CatagoryRemove(Guid id)
		{
			string sql = string.Format("Delete From Category where Id = '{0}'", id);
			try
			{
				using(System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, this.sqlConnection))
				{				
					cmd.ExecuteNonQuery();					
					return true;
				}
			}	
			catch(Exception)
			{
				
			}
			return false;
		}

		public bool CatagoryCanReplace(Guid id, Catagory catagory)
		{
			if(!_catagoryList.Keys.Contains(id))
			{
				return false;
			}
			if(!catagory.IsValid())
			{
				return false;
			}

			foreach(Guid key in _catagoryList.Keys)
			{
				if( (!_catagoryList[key].IsCompatible(catagory)) && (key != id) )
				{
					return false;
				}
			}	
			return true;
		}

		public bool CatagoryReplace(Guid id, Catagory catagory, out Catagory oldCatagory)
		{
			if(!CatagoryCanReplace(id, catagory))
			{
				Debug.Assert(false);
				id =0;
				oldCatagory = new Catagory();
				return false;
			}

			bool bOK = _catagoryList.Replace(id, catagory, out oldCatagory);
			Debug.Assert(bOK);
			return bOK;
		}

		public IdClassListReadOnly<Catagory> CatagoryList
		{			
			get
			{
				var list = new IdClassListWritable<Catagory>();
				string sql = "Select * From Category";
				try
				{
					using(System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, this.sqlConnection))
					{				
						System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader();
						while(dr.Read())
						{
							list.Add(new Catagory { Name = (string)dr["Name"]//, 
													TaxCodeId = (Guid)((decimal)dr["TaxCodeId"]),
													Income = (bool)dr["Income"]//});
						}
						return list;
					}
				}	
				catch(Exception)
				{
				
				}
				return null;				
			}
		}

		public void Close()
		{
			sqlConnection.Close();
		}

		public void Open()
		{

		}


	}

*/
	internal class TaxCodeDB
	{
		internal static bool IsEqual(tblTaxCode item, TaxCode taxCode)
		{
			return( (item.Name == taxCode.Name) && (item.Obsolete == taxCode.Obsolete) && (item.System == taxCode.System) );
		}

		internal static bool IsEqual(TaxCode taxCode, tblTaxCode item)
		{
			return IsEqual(item, taxCode);
		}

		internal static TaxCode CreateFromEntityFrameworkObject(tblTaxCode item)
		{
			TaxCode taxCode = new TaxCode(item.Name);
			PopulateAccountsCoreObject(item, ref taxCode);
			return taxCode;
		}

		internal static void PopulateEntityFrameworkObject(TaxCode taxCode, TaxCodeId id, ref tblTaxCode item)
		{
			item.TaxCodeId = id.GetGuid();
			item.Name = taxCode.Name;
			item.Obsolete = taxCode.Obsolete;
			item.System = taxCode.System;			
		}

		internal static void PopulateAccountsCoreObject(tblTaxCode item, ref TaxCode taxCode)
		{
			taxCode.Name = item.Name;
			taxCode.Obsolete = item.Obsolete;
			taxCode.System = item.System;
		}
	}

	internal class PropertyDB
	{
		internal static bool IsEqual(tblProperte item, Property obj)
		{
			return( (item.Name == obj.Name) && (item.Obsolete == obj.Obsolete) && (item.System == obj.System) );
		}

		internal static bool IsEqual(Property obj, tblProperte item)
		{
			return IsEqual(item, obj);
		}

		internal static Property CreateFromEntityFrameworkObject(tblProperte item)
		{
			Property obj = new Property(item.Name);
			PopulateAccountsCoreObject(item, ref obj);
			return obj;
		}

		internal static void PopulateEntityFrameworkObject(Property obj, PropertyId id, ref tblProperte item)
		{
			item.PropertyId = id.GetGuid();
			item.Name = obj.Name;
			item.Obsolete = obj.Obsolete;
			item.System = obj.System;			
		}

		internal static void PopulateAccountsCoreObject(tblProperte item, ref Property obj)
		{
			obj.Name = item.Name;
			obj.Obsolete = item.Obsolete;
			obj.System = item.System;
		}
	}

	internal class PayeeDB
	{
		internal static bool IsEqual(efPayee item, Payee obj)
		{
			return( (item.Name == obj.Name) && (item.Obsolete == obj.Obsolete) && (item.System == obj.System) );
		}

		internal static bool IsEqual(Payee obj, efPayee item)
		{
			return IsEqual(item, obj);
		}

		internal static Payee CreateFromEntityFrameworkObject(efPayee item)
		{
			Payee obj = new Payee(item.Name);
			PopulateAccountsCoreObject(item, ref obj);
			return obj;
		}

		internal static void PopulateEntityFrameworkObject(Payee obj, PayeeId id, ref efPayee item)
		{
			item.PayeeId = id.GetGuid();
			item.Name = obj.Name;
			item.Obsolete = obj.Obsolete;
			item.System = obj.System;			
		}

		internal static void PopulateAccountsCoreObject(efPayee item, ref Payee obj)
		{
			obj.Name = item.Name;
			obj.Obsolete = item.Obsolete;
			obj.System = item.System;
		}
	}

    internal class CatagoryDB
	{
		internal static bool IsEqual(efCatagory item, Catagory taxCode)
		{
			return( (item.Name == taxCode.Name) 
			&& (item.Obsolete == taxCode.Obsolete) 
			&& (item.System == taxCode.System)
			&& (item.income == taxCode.Income)
			&& (item.PropertySpecific == taxCode.PropertySpecific)
			&& (item.TaxCodeId == taxCode.TaxCodeId.GetGuid()) );
		}

		internal static bool IsEqual(Catagory catagory, efCatagory item)
		{
			return IsEqual(item, catagory);
		}

		internal static Catagory CreateFromEntityFrameworkObject(efCatagory item)
		{
			Catagory taxCode = new Catagory();
			PopulateAccountsCoreObject(item, ref taxCode);
			return taxCode;
		}

		internal static void PopulateEntityFrameworkObject(Catagory catagory, CatagoryId id, ref efCatagory item)
		{
			item.CatagoryId = id.GetGuid();
			item.Name = catagory.Name;
			item.Obsolete = catagory.Obsolete;
			item.System = catagory.System;	
			item.income = catagory.Income;
			item.PropertySpecific = catagory.PropertySpecific;
			item.TaxCodeId = catagory.TaxCodeId.GetGuid();
			
					
		}

		internal static void PopulateAccountsCoreObject(efCatagory item, ref Catagory catagory)
		{
			catagory.Name = item.Name;
			catagory.Obsolete = item.Obsolete;
			catagory.System = item.System;
			catagory.Income = item.income;
			catagory.PropertySpecific = item.PropertySpecific;
			catagory.TaxCodeId = new TaxCodeId(item.TaxCodeId);			
		}
	}

	internal class EntryDB
	{
		internal static bool IsEqual(tblEntre item, Entry taxCode)
		{
			return(item.Description == taxCode.Description);
		}

		internal static bool IsEqual(Entry taxCode, tblEntre item)
		{
			return IsEqual(item, taxCode);
		}

		internal static Entry CreateFromEntityFrameworkObject(tblEntre item)
		{
			Entry entry = new Entry(new AccountId(item.AccountId));
			PopulateAccountsCoreObject(item, ref entry);
			return entry;
		}

		internal static void PopulateEntityFrameworkObject(Entry obj, EntryId id, ref tblEntre item)
		{
			item.EntryId = id.GetGuid();
			item.Date = obj.Date;
			item.Description = obj.Description;
			item.PayeeId = obj.PayeeId.GetGuid();
			item.CatagoryId = obj.CatagoryId.GetGuid();
			item.PropertyId = obj.PropertyId.GetGuid();
			item.AccountId = obj.AccountId.GetGuid();
			//item.Reconciled = obj.Reconciled;
			item.TransferAccountId = obj.TransferAccountId.GetGuid();
			item.RecieptNo = obj.RecieptNo;
			item.Amount = obj.Amount;		
		}

		internal static void PopulateAccountsCoreObject(tblEntre item, ref Entry obj)
		{
			obj.Date = item.Date;
			obj.Description = item.Description;
			obj.PayeeId = new PayeeId(item.PayeeId);
			obj.CatagoryId = new CatagoryId(item.CatagoryId);
			obj.PropertyId = new PropertyId(item.PropertyId);
			obj.AccountId = new AccountId(item.AccountId);
			obj.TransferAccountId = new AccountId(item.TransferAccountId);
			obj.RecieptNo = item.RecieptNo;
			obj.Amount = item.Amount;
		}
	}
	
	internal class AccountDB
	{
		internal static bool IsEqual(tblAccount item, Account obj)
		{
			return (   (item.Name == obj.Name) 
					&& (((Account.eOwnership)item.Ownership) == obj.Ownership) 
					&& (((Account.eType)item.Type) == obj.Type) 
					&& (item.Hidden == obj.Hidden)
					&& (((Account.eLock)item.Lock) == obj.Lock)
					&& (item.LockedUntil == obj.LockedUntil)
					&& (item.ReconciledOn == obj.ReconciledOn) );
		}

		internal static bool IsEqual(Account obj, tblAccount item)
		{
			return IsEqual(item, obj);
		}

		internal static Account CreateFromEntityFrameworkObject(tblAccount item)
		{
			Account obj = new Account();
			PopulateAccountsCoreObject(item, ref obj);
			return obj;
		}

		internal static void PopulateEntityFrameworkObject(Account obj, AccountId id, ref tblAccount item)
		{
			item.AccountId = id.GetGuid();
			item.Name = obj.Name;
			item.Hidden = obj.Hidden;
			item.Lock = obj.Lock.ToString();	
			item.LockedUntil = obj.LockedUntil;
			item.ReconciledOn = obj.ReconciledOn;
			item.Type = obj.Type.ToString();
			item.Ownership = obj.Ownership.ToString();
			//item		
		}

		internal static void PopulateAccountsCoreObject(tblAccount item, ref Account obj)
		{
			obj.Name = item.Name;
			obj.Hidden = item.Hidden;
			obj.Lock = (Account.eLock)item.Lock;
			obj.LockedUntil = item.LockedUntil;
			obj.ReconciledOn = item.ReconciledOn;
			obj.Type = (Account.eType)item.Type;
			obj.Ownership = (Account.eOwnership)item.Ownership;
		}
	}




	public class MemAccountsData : IAccountsData
	{
		//private  IdClassListWritable<Catagory> _catagoryList = new IdClassListWritable<Catagory>();		
		//private  IdClassListWritable<Item> _taxCodeList = new IdClassListWritable<Item>();	
		//private  IdClassListWritable<Item> _propertyList = new IdClassListWritable<Item>();	
		//private  IdClassListWritable<Item> _payeeList = new IdClassListWritable<Item>();	
		//private  IdClassListWritable<Account> _accountList = new IdClassListWritable<Account>();	
		private string entityConnection;
				
		public MemAccountsData()
		{
			
		}

		private string buildConnectionString(string filename) 
		{
			// Specify the provider name, server and database.
			string providerName = "System.Data.SqlServerCe.3.5";
		///	string dataSource = @"C:\Documents and Settings\Dan\My Documents\AccountsDB02.sdf";
			//string password = "mypassword";
			// Initialize the connection string builder for the underlying provider.
			System.Data.SqlClient.SqlConnectionStringBuilder sqlBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
			// Set the properties for the data source.
			sqlBuilder.DataSource = filename; //dataSource
			//sqlBuilder.Password = password;
			// Build the SqlConnection connection string.
			string providerString = sqlBuilder.ToString();

			// Initialize the EntityConnectionStringBuilder.
			EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
			//Set the provider name.
			entityBuilder.Provider = providerName;
			// Set the provider-specific connection string.
			entityBuilder.ProviderConnectionString = providerString;
			// Set the Metadata location.
			//entityBuilder.Metadata = @"res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl";
			entityBuilder.Metadata = @"res://*/EntityModel.csdl|res://*/EntityModel.ssdl|res://*/EntityModel.msl";
			return entityBuilder.ToString();
		}

		public void Close()
		{
		
		}

		public void Create(string filename)
		{
			entityConnection = buildConnectionString(filename);	
			if (File.Exists(filename))
			File.Delete(filename);

			string connStr = "Data Source = " + filename;

			System.Data.SqlServerCe.SqlCeEngine engine = new System.Data.SqlServerCe.SqlCeEngine(connStr);
			engine.CreateDatabase();
			engine.Dispose();

			System.Data.SqlServerCe.SqlCeConnection conn = null;

			try {
				conn = new System.Data.SqlServerCe.SqlCeConnection(connStr);
				conn.Open();

				System.Data.SqlServerCe.SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblPayees (PayeeId uniqueIdentifier NOT NULL, Name nvarchar(80) NOT NULL, Obsolete bit NOT NULL, System bit NOT NULL, PRIMARY KEY (PayeeId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblTaxCodes (TaxCodeId uniqueIdentifier NOT NULL, Name nvarchar(80) NOT NULL, Obsolete bit NOT NULL, System bit NOT NULL, PRIMARY KEY (TaxCodeId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblTtest01 (TaxCodeId uniqueIdentifier NOT NULL, Name nvarchar(80) NOT NULL, Obsolete bit NOT NULL, System bit NOT NULL, PRIMARY KEY (TaxCodeId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblPropertes (PropertyId uniqueIdentifier NOT NULL, Name nvarchar(80) NOT NULL, Obsolete bit NOT NULL, System bit NOT NULL, PRIMARY KEY (PropertyId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblEntres (EntryId uniqueIdentifier NOT NULL, AccountId uniqueIdentifier NOT NULL, Description nvarchar(80) NOT NULL, Date datetime NOT NULL, RecieptNo nvarchar(80) NOT NULL,  Amount numeric NOT NULL, PayeeId uniqueIdentifier, CatagoryId uniqueIdentifier, PropertyId uniqueIdentifier NOT NULL, TransferAccountId uniqueIdentifier NOT NULL, PRIMARY KEY (EntryId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblGroups (GroupId uniqueIdentifier NOT NULL, Name nvarchar(80), Obsolete bit NOT NULL, System bit NOT NULL, PRIMARY KEY (GroupId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblCatagorys (CatagoryId uniqueIdentifier NOT NULL, Name nvarchar(80) NOT NULL, Obsolete bit NOT NULL, System bit NOT NULL, income bit NOT NULL, PropertySpecific bit NOT NULL, GroupId uniqueIdentifier NOT NULL, TaxCodeId uniqueIdentifier NOT NULL, PRIMARY KEY (CatagoryId))";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "ALTER TABLE [tblEntres] ADD FOREIGN KEY (PayeeId) REFERENCES tblPayees(PayeeId)";
				cmd.ExecuteNonQuery();

				cmd = conn.CreateCommand();
				cmd.CommandText = "CREATE TABLE tblAccounts (AccountId uniqueIdentifier NOT NULL PRIMARY KEY, Name nvarchar(80) NOT NULL, Ownership nvarchar(12) NOT NULL, Type nvarchar(12) NOT NULL, Hidden bit NOT NULL, LockedUntil datetime NOT NULL, ReconciledOn datetime NOT NULL, Lock nvarchar(12) NOT NULL)";
				cmd.ExecuteNonQuery();
				}
			catch 
			{
			}
			finally 
			{
				conn.Close();
			}
			bool bSystem = true;
			this.TaxCode_Add(new TaxCode("Not Taxable", bSystem), TaxCodeId.SystemTaxCode_NotTaxable);
			this.Payee_Add(new Payee("Unknown", bSystem), PayeeId.SystemPayee_Unknown);
			this.Property_Add(new Property("Unknown", bSystem), PropertyId.SystemProperty_Unknown);
			Catagory sysCat = new Catagory("UnKnown", TaxCodeId.SystemTaxCode_NotTaxable, bSystem);
			this.Catagory_Add(sysCat, CatagoryId.SystemCatagory_Unknown);

			//this.Catagory_Add(new Catagory(
		}

		public void Open(string filename)
		{
			entityConnection = buildConnectionString(filename);
		}
		
		//catagories//////////////////////////////////////////////////////////////////////
		public Catagory CreateCatagory()
		{
			Catagory catagory = new Catagory();
			catagory.TaxCodeId = TaxCodeId.SystemTaxCode_NotTaxable;
			catagory.System = false;
			return catagory;
		}
		
		public Catagory Catagory(CatagoryId id)
		{
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					efCatagory item = adm.efCatagories.FirstOrDefault(i => guid == i.CatagoryId);
					Catagory catagory = CatagoryDB.CreateFromEntityFrameworkObject(item);
					return catagory;					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}
		
		public Dictionary<CatagoryId, Catagory> CatagoryList 
		{ 
			get 
			{ 
				try
				{
					Dictionary<CatagoryId, Catagory> list = new Dictionary<CatagoryId, Catagory>();
					using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
					{
						var items = from row in adm.efCatagories select row;
						foreach(var item in items)
						{	
							Catagory catagory = CatagoryDB.CreateFromEntityFrameworkObject(item);
							list.Add(new CatagoryId(item.CatagoryId), catagory);
						}	
						return list; 				
					}	
				}
				catch(Exception)
				{
				}
				return null;			
			}
		}

		public bool Catagory_CanAdd(Catagory catagory)
		{
			if(string.IsNullOrWhiteSpace(catagory.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				efCatagory item = adm.efCatagories.FirstOrDefault(i => i.Name == catagory.Name);
				if(item != null)
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
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				efCatagory item = adm.efCatagories.CreateObject();	
				CatagoryDB.PopulateEntityFrameworkObject(catagory, id, ref item);				
				adm.efCatagories.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}
		
		public bool Catagory_CanRemove(CatagoryId id)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				//the catagory must exist and not be a system catagory
				Guid guid = id.GetGuid();
				//efCatagory item = (from row in adm.efCatagories where (row.CatagoryId == guid) select row).FirstOrDefault();
				efCatagory item = adm.efCatagories.FirstOrDefault(i => i.CatagoryId == guid);
				if( (item == null) || (item.System == true) )
				{
					return false;
				}

				//the catagory must not be used by an entry
				tblEntre entry = adm.tblEntres.FirstOrDefault(i => i.CatagoryId == guid);
				//efCatagory c = (from row in adm.efCatagories where (row.TaxCodeId == id) select row).FirstOrDefault();
				if(entry != null)
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
			
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					efCatagory item = adm.efCatagories.First(i => i.CatagoryId == guid);
					adm.efCatagories.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Catagory_CanReplace(CatagoryId id, Catagory catagory)
		{
			if(string.IsNullOrWhiteSpace(catagory.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old catagory from the database
					Guid guid = id.GetGuid();
					efCatagory item = adm.efCatagories.First(i => i.CatagoryId == guid);

					//the old tax code must exist
					if( (item == null) || (item.System == true) )
					{
						return false;
					}
					
					//the new catagory must not be the same as the old tax code
					//(No change)
					if(CatagoryDB.IsEqual(catagory, item))
					{
						return false;
					}

					//the new catagory must not have the same name as an existing catagory.
					efCatagory itemWithSameName = adm.efCatagories.FirstOrDefault(i => ((i.Name == catagory.Name)&&(i.CatagoryId != guid)));		
					if(itemWithSameName != null)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
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

			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old catagory from the database
					Guid guid = id.GetGuid();
					efCatagory item = adm.efCatagories.First(i => i.CatagoryId == guid);
					oldCatagory = CatagoryDB.CreateFromEntityFrameworkObject(item);
					CatagoryDB.PopulateEntityFrameworkObject(catagory, id, ref item);					
					adm.SaveChanges();
				}
				catch
				{
					return false;
				}
			}	
			return true;		
		}

		
		//end catagories/////////////////////////////////////////////////////////////////////


		//TAX CODES//////////////////////////////////////////////////////////////////////
		public TaxCode TaxCode(TaxCodeId id)
		{
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblTaxCode item = adm.tblTaxCodes.FirstOrDefault(i => i.TaxCodeId == guid);
					TaxCode taxCode = TaxCodeDB.CreateFromEntityFrameworkObject(item);
					return taxCode;					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}
		
		public Dictionary<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems) 
		{ 
			try
			{
				Dictionary<TaxCodeId, TaxCode> list = new Dictionary<TaxCodeId, TaxCode>();
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					IQueryable<tblTaxCode> items;
					if(includeObsoleteItems)
					{
						items = from row in adm.tblTaxCodes select row;
					}
					else
					{
						items = from row in adm.tblTaxCodes where (row.Obsolete == false) select row;
					}
					foreach(tblTaxCode item in items)
					{	
						TaxCode taxCode = TaxCodeDB.CreateFromEntityFrameworkObject(item);
						list.Add(new TaxCodeId(item.TaxCodeId), taxCode);
					}	
					return list; 				
				}	
			}
			catch(Exception)
			{
			}
			return null;	
		}

		public bool TaxCode_CanAdd(TaxCode taxCode)
		{
			if(string.IsNullOrWhiteSpace(taxCode.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblTaxCode item = adm.tblTaxCodes.FirstOrDefault(i => i.Name == taxCode.Name);
				if(item != null)
				{
					return false;
				}
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
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblTaxCode item = adm.tblTaxCodes.CreateObject();	
				TaxCodeDB.PopulateEntityFrameworkObject(taxCode, id, ref item);
				adm.tblTaxCodes.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}
		
		public bool TaxCode_CanRemove(TaxCodeId id)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				//the tax code must exist and not be a system tax code
				Guid test = id.GetGuid();
				tblTaxCode item = adm.tblTaxCodes.First(i => i.TaxCodeId == test);
				if((item == null) || (item.System == true))
				{
					return false;
				}

				//the tax code must not be used by a catagory
				Guid guid = id.GetGuid();
				efCatagory c = adm.efCatagories.FirstOrDefault(i => i.TaxCodeId == guid);
				if(c != null)
				{
					return false;
				}				
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
			
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblTaxCode item = adm.tblTaxCodes.First(i => i.TaxCodeId == guid);
					adm.tblTaxCodes.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode)
		{
			if(string.IsNullOrWhiteSpace(taxCode.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblTaxCode item = adm.tblTaxCodes.First(i => i.TaxCodeId == guid);

					//the old tax code must exist and not be a system tax code
					if( (item == null) || (item.System == true) )
					{
						return false;
					}
					
					//the new tax code must not be the same as the old tax code
					//(No change)
					if(TaxCodeDB.IsEqual(taxCode, item))
					{
						return false;
					}

					//the new tax code must not have the same name as an existing taxcode.
					tblTaxCode itemWithSameName = adm.tblTaxCodes.FirstOrDefault(i => ((i.Name == taxCode.Name)&&(i.TaxCodeId != guid)));		
					if(itemWithSameName != null)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
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
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblTaxCode item = adm.tblTaxCodes.First(i => i.TaxCodeId == guid);
					oldTaxCode = TaxCodeDB.CreateFromEntityFrameworkObject(item);
					TaxCodeDB.PopulateEntityFrameworkObject(taxCode, id, ref item);					
					adm.SaveChanges();
				}
				catch
				{
					return false;
				}
			}	
			return true;		
		}
		//END TAX CODES/////////////////////////////////////////////////////////////////////

		//PROPERTY//////////////////////////////////////////////////////////////////////
		public Property Property(PropertyId id){
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblProperte item = adm.tblPropertes.FirstOrDefault(i => i.PropertyId == guid);
					Property property = PropertyDB.CreateFromEntityFrameworkObject(item);
					return property;					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}

		public Dictionary<PropertyId, Property> PropertyList 
		{ 
			get 
			{ 
				try
				{
					Dictionary<PropertyId, Property> list = new Dictionary<PropertyId, Property>();
					using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
					{
						var items = from row in adm.tblPropertes select row;
						foreach(var item in items)
						{	
							Property property = PropertyDB.CreateFromEntityFrameworkObject(item);
							list.Add(new PropertyId(item.PropertyId), property);
						}	
						return list; 				
					}	
				}
				catch(Exception)
				{
				}
				return null;			
			}
		}

		public bool Property_CanAdd(Property property)
		{
			if(string.IsNullOrWhiteSpace(property.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblProperte item = adm.tblPropertes.FirstOrDefault(i => i.Name == property.Name);
				if(item != null)
				{
					return false;
				}
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
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblProperte item = adm.tblPropertes.CreateObject();	
				PropertyDB.PopulateEntityFrameworkObject(property, id, ref item);
				adm.tblPropertes.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}
		
		public bool Property_CanRemove(PropertyId id)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				//the property must exist and not be a system property
				Guid guid = id.GetGuid();
				tblProperte item = adm.tblPropertes.First(i => i.PropertyId == guid);
				if((item == null) || (item.System == true))
				{
					return false;
				}							
			}
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
			
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblProperte item = adm.tblPropertes.First(i => i.PropertyId == guid);
					adm.tblPropertes.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Property_CanReplace(PropertyId id, Property property)
		{
			if(string.IsNullOrWhiteSpace(property.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblProperte item = adm.tblPropertes.First(i => i.PropertyId == guid);

					//the old tax code must exist
					if( (item == null) || (item.System == true) )
					{
						return false;
					}
					
					//the new tax code must not be the same as the old tax code
					//(No change)
					if(PropertyDB.IsEqual(property, item))
					{
						return false;
					}

					//the new tax code must not have the same name as an existing taxcode.
					tblProperte itemWithSameName = adm.tblPropertes.FirstOrDefault(i => ((i.Name == property.Name)&&(i.PropertyId != guid)));		
					if(itemWithSameName != null)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
		}

		public bool Property_Replace(PropertyId id, Property property, out Property oldProperty)
		{
			oldProperty = null;
			
			if(!Property_CanReplace(id, property))
			{
				Debug.Assert(false);  //UI should not allow you to get this far
				Property_CanReplace(id, property);
				return false;
			}		
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblProperte item = adm.tblPropertes.First(i => i.PropertyId == guid);
					oldProperty = PropertyDB.CreateFromEntityFrameworkObject(item);
					PropertyDB.PopulateEntityFrameworkObject(property, id, ref item);					
					adm.SaveChanges();
				}
				catch
				{
					return false;
				}
			}	
			return true;		
		}
		//END PROPERTY/////////////////////////////////////////////////////////////////////

		//PAYEE//////////////////////////////////////////////////////////////////////
		//public IdClassListReadOnly<Item> PayeeList { get { return _payeeList; } }
		public Payee Payee(PayeeId id)
		{
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					efPayee item = adm.efPayees.FirstOrDefault(i => i.PayeeId == guid);
					Payee payee = PayeeDB.CreateFromEntityFrameworkObject(item);
					return payee;					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}

		public Dictionary<PayeeId, Payee> PayeeList 
		{ 
			get 
			{ 
				try
				{
					Dictionary<PayeeId, Payee> list = new Dictionary<PayeeId, Payee>();
					using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
					{
						var items = from row in adm.efPayees select row;
						foreach(var item in items)
						{	
							Payee payee = PayeeDB.CreateFromEntityFrameworkObject(item);
							list.Add(new PayeeId(item.PayeeId), payee);
						}	
						return list; 				
					}	
				}
				catch(Exception)
				{
				}
				return null;			
			}
		}

		public bool Payee_CanAdd(Payee payee)
		{
			if(string.IsNullOrWhiteSpace(payee.Name))
			{
				return false;
			}
			
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					efPayee item = adm.efPayees.FirstOrDefault(i => i.Name == payee.Name);
					if(item != null)
					{
						return false;
					}
				}
			}
			catch(Exception)
			{
				return false;
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
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				efPayee item = adm.efPayees.CreateObject();
				PayeeDB.PopulateEntityFrameworkObject(payee, id, ref item);
				adm.efPayees.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}
		
		public bool Payee_CanRemove(PayeeId id)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				Guid guid = id.GetGuid();
				//the payee must exist and not be a system tax code
				efPayee tc = (from row in adm.efPayees where (row.PayeeId == guid) select row).FirstOrDefault();
				if((tc == null) || (tc.System == true))
				{
					return false;
				}

				//the payee must not be in use
				tblEntre entryWithThisPayee = (from row in adm.tblEntres where (row.PayeeId == guid) select row).FirstOrDefault();
				if(entryWithThisPayee != null)
				{
					return false;
				}				
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

			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					efPayee item = adm.efPayees.First(i => i.PayeeId == guid);
					adm.efPayees.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Payee_CanReplace(PayeeId id, Payee payee)
		{
			if(string.IsNullOrWhiteSpace(payee.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve payee code from the database
					Guid guid = id.GetGuid();
					efPayee item = adm.efPayees.First(i => i.PayeeId == guid);

					//the old tax code must exist
					if( (item == null) || (item.System == true) )
					{
						return false;
					}
					
					//the new tax code must not be the same as the old tax code
					//(No change)
					if(PayeeDB.IsEqual(payee, item))
					{
						return false;
					}

					//the new tax code must not have the same name as an existing taxcode.
					efPayee itemWithSameName = adm.efPayees.FirstOrDefault(i => ((i.Name == payee.Name)&&(i.PayeeId != guid)));			
					if(itemWithSameName != null)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
		}
			

		public bool Payee_Replace(PayeeId id, Payee payee, out Payee oldPayee)
		{
			if(!Payee_CanReplace(id, payee))
			{
				Debug.Assert(false);
				oldPayee = null;
				return false;
			}		
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				Guid guid = id.GetGuid();
				efPayee item = adm.efPayees.First(i => i.PayeeId == guid);
				oldPayee = new Payee(item.Name);
				item.Name = payee.Name;
				item.Obsolete = payee.Obsolete;
				adm.SaveChanges();
			}	
			return true;
		}
		//END PAYEE/////////////////////////////////////////+-////////////////////////////




		//ACCOUNT//////////////////////////////////////////////////////////////////////
		public Account Account(AccountId id) 
		{
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblAccount item = adm.tblAccounts.FirstOrDefault(i => i.AccountId == guid);
					Account account = AccountDB.CreateFromEntityFrameworkObject(item);
					return account;					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}

		public Dictionary<AccountId, Account> AccountList(bool includeHiddenItems)
		{ 
			try
			{
				Dictionary<AccountId, Account> list = new Dictionary<AccountId, Account>();
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					IQueryable<tblAccount> items;
					if(includeHiddenItems == true)
					{
						items = from row in adm.tblAccounts select row;
					}
					else
					{
						items = from row in adm.tblAccounts where (row.Hidden == false) select row;
					}

					foreach(var item in items)
					{	
						Account taxCode = AccountDB.CreateFromEntityFrameworkObject(item);
						list.Add(new AccountId(item.AccountId), taxCode);
					}	
					return list; 				
				}	
			}
			catch(Exception)
			{
			}
			return null;	
		}

		public bool Account_CanAdd(Account account)
		{
			if(string.IsNullOrWhiteSpace(account.Name))
			{
				return false;
			}
			
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					tblAccount item = adm.tblAccounts.FirstOrDefault(i => i.Name == account.Name);
					if(item != null)
					{
						return false;
					}
				}
			}
			catch(Exception)
			{
				return false;
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
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblAccount item = adm.tblAccounts.CreateObject();	
				AccountDB.PopulateEntityFrameworkObject(account, id, ref item);
				adm.tblAccounts.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}
		
		public bool Account_CanRemove(AccountId id)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				//the account must exist and not be a system account
				Guid guid = id.GetGuid();
				tblAccount item = (from row in adm.tblAccounts where (row.AccountId == guid) select row).FirstOrDefault();
				if( (item == null) /*&& (item.System == false)*/ )
				{
					return false;
				}

				//there must be no entries in the account
				tblEntre c = (from row in adm.tblEntres where (row.AccountId == guid) select row).FirstOrDefault();
				if(c != null)
				{
					return false;
				}				
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
			
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblAccount item = adm.tblAccounts.First(i => i.AccountId == guid);
					adm.tblAccounts.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Account_CanReplace(AccountId id, Account account)
		{
			if(string.IsNullOrWhiteSpace(account.Name))
			{
				return false;
			}
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old account from the database
					Guid guid = id.GetGuid();
					tblAccount item = adm.tblAccounts.First(i => i.AccountId == guid);

					//the old tax code must exist
					if(item == null)
					{
						return false;
					}
					
					//the new tax code must not be the same as the old tax code
					//(No change)
					if(AccountDB.IsEqual(account, item))
					{
						return false;
					}

					//the new tax code must not have the same name as an existing taxcode.
					tblAccount itemWithSameName = adm.tblAccounts.FirstOrDefault(i => ((i.Name == account.Name)&&(i.AccountId != guid)));		
					if(itemWithSameName != null)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
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
			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblAccount item = adm.tblAccounts.First(i => i.AccountId == guid);
					oldHeader = AccountDB.CreateFromEntityFrameworkObject(item);
					AccountDB.PopulateEntityFrameworkObject(header, id, ref item);					
					adm.SaveChanges();
				}
				catch
				{
					return false;
				}
			}	
			return true;	
		}
		//END ACCOUNT/////////////////////////////////////////////////////////////////////

		//ENTRY//////////////////////////////////////////////////////////////////////
		public Entry Entry(EntryId id)
		{
			try
			{
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();
					tblEntre item = adm.tblEntres.FirstOrDefault(i => i.EntryId == guid);
					if(item != null)
					{
						Entry entry = EntryDB.CreateFromEntityFrameworkObject(item);
						return entry;
					}					
				}
			}
			catch(Exception)
			{
			}
			return null;
		}

		public Dictionary<EntryId, Entry>EntryList(AccountId id)
		{ 
			try
			{
				Dictionary<EntryId, Entry> list = new Dictionary<EntryId, Entry>();
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = id.GetGuid();						
					var items = from row in adm.tblEntres where (row.AccountId == guid) select row;
					foreach(var item in items)
					{	
						Entry entry = EntryDB.CreateFromEntityFrameworkObject(item);
						list.Add(new EntryId(item.EntryId), entry);
					}	
					return list; 				
				}	
			}
			catch(Exception)
			{
			}
			return null;		
		}

		public bool Entry_CanAdd(Entry entry)
		{
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				Guid guid = entry.AccountId.GetGuid();
				tblAccount item = adm.tblAccounts.FirstOrDefault(i => i.AccountId == guid);
				if(item == null)
				{
					return false;
				}

				Account account = AccountDB.CreateFromEntityFrameworkObject(item);
				if(account.Hidden == true)
				{
					return false;
				}

				if(account.Lock == AccountsCore.Account.eLock.Locked)
				{
					return false;
				}

				if( (account.Lock == AccountsCore.Account.eLock.ByDate) && (entry.Date < account.LockedUntil) )
				{
					return false;
				}

				if(entry.Date <= account.ReconciledOn)
				{
					return false;
				}				
			}	
			return true;	
		}

		public bool Entry_Add(Entry entry, EntryId id)
		{
			if(!Entry_CanAdd(entry))
			{
				Debug.Assert(false);
				Entry_CanAdd(entry);
				return false;
			}			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				tblEntre item = adm.tblEntres.CreateObject();	
				EntryDB.PopulateEntityFrameworkObject(entry, id, ref item);
				adm.tblEntres.AddObject(item);
				adm.SaveChanges();	
			}
			return true;
		}

		public bool Entry_Add(Dictionary<EntryId, Entry> entries)
		{
			foreach(KeyValuePair<EntryId, Entry> kvp in entries)
			{
				if(!Entry_CanAdd(kvp.Value))
				{
					Debug.Assert(false);
					Entry_CanAdd(kvp.Value);
					return false;
				}
			}			
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{
				foreach(KeyValuePair<EntryId, Entry> kvp in entries)
				{
					tblEntre item = adm.tblEntres.CreateObject();	
					EntryDB.PopulateEntityFrameworkObject(kvp.Value, kvp.Key, ref item);
					adm.tblEntres.AddObject(item);
					adm.SaveChanges();	
				}
			}
			return true;
		}
		
		
		public bool Entry_CanRemove(EntryId idEntry)
		{
			return true;
		}
		
		public bool Entry_Remove(EntryId idEntry)
		{
			if(!Entry_CanRemove(idEntry))
			{
				Debug.Assert(false);
				Entry_CanRemove(idEntry);
				return false;
			}
						
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					Guid guid = idEntry.GetGuid();
					tblEntre item = adm.tblEntres.First(i => i.EntryId == guid);
					adm.tblEntres.DeleteObject(item);
					adm.SaveChanges();
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Entry_Remove(IEnumerable<EntryId> ids)
		{
			foreach(EntryId idEntry in ids)
			{
				if(!Entry_CanRemove(idEntry))
				{
					Debug.Assert(false);
					Entry_CanRemove(idEntry);
					return false;
				}
			}
						
			try
			{			
				using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
				{
					foreach(EntryId idEntry in ids)
					{
						Guid guid = idEntry.GetGuid();
						tblEntre item = adm.tblEntres.First(i => i.EntryId == guid);
						adm.tblEntres.DeleteObject(item);
						adm.SaveChanges();
					}
				}
			}
			catch(Exception)
			{
				return false;
			}			
			return true;
		}

		public bool Entry_CanModify(EntryId idEntry, Entry entry)
		{
			//the old entry must exist
			
			//the account must not be locked at the new or old dates
			
			//if there are balance changes to the entry the account must not be reconciled at the new date or old dates
			return true;
		}

		public bool Entry_Modify(EntryId id, Entry entry, out Entry oldEntry)
		{
			oldEntry = null;
			if(!Entry_CanModify(id, entry))
			{
				Debug.Assert(false);
				Entry_CanModify(id, entry);
				
				return false;
			}	
					
			using(AccountsDBEntities adm = new AccountsDBEntities(entityConnection))
			{				
				try
				{
					//retrieve old tax code from the database
					Guid guid = id.GetGuid();
					tblEntre item = adm.tblEntres.First(i => i.EntryId == guid);
					oldEntry = EntryDB.CreateFromEntityFrameworkObject(item);
					EntryDB.PopulateEntityFrameworkObject(entry, id, ref item);					
					adm.SaveChanges();
				}
				catch
				{
					return false;
				}
			}	
			return true;		
		}
		//END ENTRY/////////////////////////////////////////////////////////////////////
	}
}