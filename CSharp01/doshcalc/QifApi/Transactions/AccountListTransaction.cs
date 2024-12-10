using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace QifApi.Transactions
{
    /// <summary>
    /// An account list transaction. This is used to describe an account.
    /// </summary>
    public class AccountHeader : TransactionBase
    {
        public sealed class eType 
		{
			private static readonly Dictionary<string, eType> instance = new Dictionary<string,eType>();
			
			public static readonly eType None		= new eType (0, "None");
			public static readonly eType Bank		= new eType (1, "Bank");
			public static readonly eType Liability	= new eType (2, "Liability");
			public static readonly eType Asset		= new eType (3, "Asset");
			public static readonly eType CreditCard = new eType (4, "CreditCard");
			public static readonly eType Cash		= new eType (5, "Cash");
			
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

			internal static object Parse(string p)
			{
				throw new NotImplementedException();
			}
		}
		
		/// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        /// <value>The credit limit.</value>
        public decimal CreditLimit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the statement balance date.
        /// </summary>
        /// <value>The statement balance date.</value>
        private string _rawDate = "None";
		private DateTime _date;

        internal void RawDate(string p)
        {
            _rawDate = p;
        }
		
		public DateTime StatementBalanceDate
        {
            get{ Debug.Assert(_rawDate == null); return _date; }
            set{ _date = value; _rawDate = null; }
        }

        //internal void ParseStatementBalanceDate(ref QifDom.yearFormat yearFormat, ref QifDom.dayMonthFormat dayMonthFormat)
        //{
        //    if(_rawDate != null)
        //    {
        //        Debug.Assert( Common.UpdateDateFormatAndParse(_rawDate,  ref yearFormat, ref dayMonthFormat, ref _date) );
        //        _rawDate = null;
        //    }
        //}

        /// <summary>
        /// Gets or sets the statement balance.
        /// </summary>
        /// <value>The statement balance.</value>
        public decimal StatementBalance
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountHeader"/> class.
        /// </summary>
        public AccountHeader()
        {
            Name = "";
            Type = eType.None;
            Description = "";
        }

		public AccountHeader(AccountHeader.eType type)
        {
            Name = type.ToString();
            Type = type;
            Description = "";
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

		public bool Selected { get; set; }

        
    }
}
