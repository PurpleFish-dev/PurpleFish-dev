using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

//using System.Text.Json;

namespace AccountsCore
{
    [Serializable]
    abstract public class NamedItem
	{
		protected string _name = null;
        protected bool _system;// { get; }			
		protected bool _obsolete; // { get;  }
		
		protected NamedItem() {}
		
		protected NamedItem(NamedItem arg)
		{
			this._name = arg._name;
            this._obsolete = arg._obsolete;
            this._system = arg._system;
		}
		
		protected NamedItem(string name)
		{
			_name = name;
		}

        protected NamedItem(string name, bool obsolete, bool system)
        {
            _name = name;
            _obsolete = obsolete;
            _system = system;
        }

        public string Name { get { return _name; } /*set { _name = value; }*/ }
        public bool Obsolete { get { return _obsolete; } }
        public bool System { get { return _system; } }

		public override string ToString()
		{
			return _name;
		}

        public static bool operator ==(NamedItem arg1, NamedItem arg2)
		{
            // If both are null, or both are same instance, return true.
            if ((object)arg1 == null && (object)arg2 == null)//if (System.Object.ReferenceEquals(arg1, arg2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)arg1 == null) || ((object)arg2 == null))
            {
                return false;
            }

            return (arg1.Name == arg2.Name) && (arg1.Obsolete == arg2.Obsolete) && (arg1.System == arg2.System);
		}

		public static bool operator !=(NamedItem A, NamedItem B)
		{
			return !(A == B);
		}	
		
	}

    //public class PropertyRO
    //{
    //    public TaxCode() { }
    //    public TaxCode(TaxCode arg) : base(arg) { }
    //    public TaxCode(string name) : base(name) { }
    //    public TaxCode(string name, bool system) : base(name, system) { }
    //}
    
    
    [Serializable]
    public sealed class TaxCode : NamedItem
    {
        public TaxCode() { }
        public TaxCode(TaxCode arg) : base(arg) { }
        public TaxCode(string name) : base(name) { }
        internal TaxCode(string name, bool obsolete, bool system) : base(name, obsolete, system) { }

		public XElement toXElement(TaxCodeId id)
		{
			return new XElement("TaxCode"
								, new XElement("Id", id.GetGuid())
								, new XElement("Name", _name)
								, new XElement("Obsolete", _obsolete));
		}
    }

    public class TaxCodeWR : NamedItem
    {
        public TaxCodeWR() { }
        public TaxCodeWR(TaxCode arg) : base(arg) { }
        public TaxCodeWR(string name) : base(name) { }
        internal TaxCodeWR(string name, bool obsolete, bool system) : base(name, obsolete, system) { }
        public new string Name { get { return _name; }  set { _name = value; } }
        public new bool Obsolete { set { _obsolete = value; }  get { return _obsolete; } }
        public new bool System { /*set { _system = value; }*/ get { return _system; } }
        public static implicit operator TaxCode(TaxCodeWR b)
        {
            // Code to convert the book into an XML structure
            return new TaxCode(b.Name, b.Obsolete, b.System);
        }
    }

    [Serializable]
    public sealed class Property : NamedItem
    {
        public Property() { }
        public Property(Property arg) : base(arg) { }
        public Property(string name) : base(name) { }
        internal Property(string name, bool obsolete, bool system) : base(name, obsolete, system) { }

		public XElement toXElement(PropertyId id)
		{
			return new XElement("Property"
								, new XElement("Id", id.GetGuid())
								, new XElement("Name", _name)
								, new XElement("Obsolete", _obsolete));
		}
	}

    public class PropertyWR : NamedItem
    {
        public PropertyWR() { }
        public PropertyWR(Property arg) : base(arg) { }
        public PropertyWR(string name) : base(name) { }
        internal PropertyWR(string name, bool obsolete, bool system) : base(name, obsolete, system) { }
        public new string Name { get { return _name; } set { _name = value; } }
        public new bool Obsolete { set { _obsolete = value; } get { return _obsolete; } }
        public new bool System { set { _system = value; } get { return _system; } }
        public static implicit operator Property(PropertyWR b)
        {
            // Code to convert the book into an XML structure
            return new Property(b.Name, b.Obsolete, b.System);
        }
    }



    [Serializable]
	public sealed class Payee : NamedItem
	{
		public Payee() { }
		public Payee(Payee arg) : base(arg) { }
		public Payee(string name) : base(name) { }
		internal Payee(string name, bool obsolete, bool system) : base(name, obsolete, system) { }

		public XElement toXElement(PayeeId id)
		{
			var res = new XElement("Payee"
									, new XElement("Id", id.GetGuid())
									, new XElement("Name", Name)
									, new XElement("Obsolete", Obsolete));
			return res;
		}
	}

	public class PayeeWR : NamedItem
    {
        public PayeeWR() { }
        public PayeeWR(Payee arg) : base(arg) { }
        public PayeeWR(string name) : base(name) { }
        internal PayeeWR(string name, bool obsolete, bool system) : base(name, obsolete, system) { }
        public new string Name { get { return _name; } set { _name = value; } }
        public new bool Obsolete { set { _obsolete = value; } get { return _obsolete; } }
        public new bool System { set { _system = value; } get { return _system; } }
        public static implicit operator Payee(PayeeWR b)
        {
            // Code to convert the book into an XML structure
            return new Payee(b.Name, b.Obsolete, b.System);
        }
    }
	
}

