using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Diagnostics;
using System.Xml.Linq;


//<?xml version="1.0" encoding="utf-8"?>
///<configuration>
//  <connectionStrings>
//  <add name="AccountsDBEntities" connectionString="metadata=res://*/EntityModel.csdl|res://*/EntityModel.ssdl|res://*/EntityModel.msl;provider=System.Data.SqlServerCe.3.5;provider connection string=&quot;Data Source=C:\Documents and Settings\Dan\My Documents\AccountsDB.sdf&quot;" providerName="System.Data.EntityClient" />
// </connectionStrings>
//</configuration>

namespace AccountsCore
{
    [Serializable]
    public class Account
	{
        [Serializable]
        public sealed class eType 
		{
			private static readonly Dictionary<string, eType> instance = new Dictionary<string,eType>();
			public static readonly eType Credit = new eType (1, "Credit");
			public static readonly eType Liability = new eType (2, "Liability");			
			public static bool operator==(eType lhs, eType rhs) { return (lhs.value == rhs.value); }
			public static bool operator!=(eType lhs, eType rhs) { return !(lhs == rhs); }

			public override String ToString() { return name; }
			public static explicit operator eType(string str)
			{
				eType result;
				if (instance.TryGetValue(str, out result))
					return result;
				else
					throw new InvalidCastException();
			}
	      
			private readonly String name;
			private readonly int value;
			
			private eType(int value, String name)
			{
				this.name = name;
				this.value = value;
				instance[name] = this;
			}			
		}

        [Serializable]
        public sealed class eOwnership 
		{
			private static readonly Dictionary<string, eOwnership> instance = new Dictionary<string,eOwnership>();
			public static readonly eOwnership Internal = new eOwnership (1, "Internal");
			public static readonly eOwnership External = new eOwnership (2, "External");
			public static bool operator==(eOwnership lhs, eOwnership rhs) { return (lhs.value == rhs.value); }
			public static bool operator!=(eOwnership lhs, eOwnership rhs) { return !(lhs == rhs); }

			public override String ToString() { return name; }
			public static explicit operator eOwnership(string str)
			{
				eOwnership result;
				if (instance.TryGetValue(str, out result))
					return result;
				else
					throw new InvalidCastException();
			}
	      
			private readonly String name;
			private readonly int value;
			
			private eOwnership(int value, String name)
			{
				this.name = name;
				this.value = value;
				instance[name] = this;
			}			
		}

        [Serializable]
        public sealed class eLock 
		{
			private static readonly Dictionary<string, eLock> instance = new Dictionary<string,eLock>();
			public static readonly eLock Locked = new eLock (1, "Locked");
			public static readonly eLock Open = new eLock (2, "Open");
			public static readonly eLock ByDate = new eLock (3, "ByDate");  
			public static bool operator==(eLock lhs, eLock rhs) { return (lhs.value == rhs.value); }
			public static bool operator!=(eLock lhs, eLock rhs) { return !(lhs == rhs); }

			public override String ToString() { return name; }
			public static explicit operator eLock(string str)
			{
				eLock result;
				if (instance.TryGetValue(str, out result))
					return result;
				else
					throw new InvalidCastException();
			}
	      
			private readonly String name;
			private readonly int value;
			
			private eLock(int value, String name)
			{
				this.name = name;
				this.value = value;
				instance[name] = this;
			}			
		}




		public Account()
		{
			Name = "";
			Ownership = eOwnership.Internal;
			Type = eType.Credit;
			Hidden = false;
			Lock = eLock.Open;
			LockedUntil = DateTime.Today;
            ReconciledOn = DateTime.Today.AddYears(-10);
		}
			
		/*
		 * public Account(string name, Account.eOwnership ownership, Account.eType type)
		{
			Name = name;
			Ownership = ownership;
			Type = type;
		}
		 * */

		public Account(Account arg)
		{
			Name = arg.Name;
			Ownership = arg.Ownership;
			Type = arg.Type;
			Hidden = arg.Hidden;
			Lock = arg.Lock;
			LockedUntil = arg.LockedUntil;
			ReconciledOn = arg.ReconciledOn;
		}

		public static bool operator ==(Account arg1, Account arg2)
		{
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(arg1, arg2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)arg1 == null) || ((object)arg2 == null))
            {
                return false;
            }
            
            if ((arg1.Name == arg2.Name) 
				&&(arg1.Ownership == arg2.Ownership)
				&&(arg1.Type == arg2.Type)
				&&(arg1.Hidden == arg2.Hidden)
				&&(arg1.Lock == arg2.Lock)
				&&(arg1.LockedUntil == arg2.LockedUntil)
				&&(arg1.ReconciledOn == arg2.ReconciledOn) )
			{
				return true;
			}
			return false;
		}
		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			Account rhsAccount = obj as Account;
			if ((System.Object)rhsAccount == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (this == rhsAccount);
		}

		public bool Equals(Account rhsAccount)
		{
			// If parameter is null return false:
			if ((object)rhsAccount == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (this == rhsAccount);
		}
		public static bool operator !=(Account arg1, Account arg2) { return !(arg1 == arg2); }
		public override int GetHashCode() {  return Name.GetHashCode(); }
			
		public string Name { get; set; }
				 		
		
		public bool Hidden { get; set; }


		
		public DateTime ReconciledOn { get; set; }


        public DateTime LockedUntil { get; set; }
        private eLock _Lock = eLock.Open;		
		public eLock Lock { get{return _Lock;} set{_Lock = value;} }
        public bool IsLocked(DateTime date)
        {
            if (_Lock == eLock.Open) return false;
            if (_Lock == eLock.Locked) return true;
            return (date < LockedUntil);
        }

		private eOwnership _ownership = eOwnership.Internal;		
		public eOwnership Ownership { get{return _ownership;} set{_ownership = value;} }	
		
		private eType _type = eType.Credit;		
		public eType Type { get{return _type;} set{_type = value;} }		

		public bool IsSystem()
		{
			return false;
		}
		
		public bool Internal 
		{
			get
			{
				return (Ownership == eOwnership.Internal);
			}
			set
			{
				if(value)
				{
					Ownership = eOwnership.Internal;
				}
				else
				{
					Ownership = eOwnership.External;
				}
			}
		}
		public bool External 
		{
			get
			{
				return (Ownership == eOwnership.External);
			}
			set
			{
				if(value)
				{
					Ownership = eOwnership.External;
				}
				else
				{
					Ownership = eOwnership.Internal;
				}
			}
		}

        public bool IsReconciled(DateTime dateTime)
        {
            if (dateTime <= ReconciledOn) return true;
            return false;
        }

		public XElement toXElement(AccountId id)
		{
			var res = new XElement("Account"
								, new XElement("Id", id.GetGuid())
								, new XElement("Name", Name)
								, new XElement("Ownership", Ownership)
								, new XElement("Type", Type)
								, new XElement("Hidden", Hidden)
								, new XElement("Lock", Lock)
								, new XElement("LockedUntil", LockedUntil)
								, new XElement("ReconciledOn", ReconciledOn));
			return res;
		}
	}
}
