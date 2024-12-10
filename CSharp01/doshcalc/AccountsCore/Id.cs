using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AccountsCore
{		
	/*
	 * public class Guid
	{
		private Guid _g;
		private static Guid _unassigned = new Guid("00000000-0000-0000-0000-000000000000");
		private Guid(Guid g) {_g = g; }

		public Guid(){_g = _unassigned;}		
		public static Guid Create(){ return new Guid(Guid.NewGuid()); }
		public static Guid Create(Guid g){ return new Guid(g); }
		public static Guid CreateUnAssigned(){ return new Guid(_unassigned); }
		public bool IsAssigned() { return ( _g != _unassigned ); }
		public static bool operator ==(Guid arg1, Guid arg2)
			{
				if(arg1._g == arg2._g) 					
				{
					return true;
				}
				return false;
			}
		public static bool operator !=(Guid arg1, Guid arg2) { return !(arg1 == arg2); }
		public bool Equals(Guid other) { return this == other; }
		public int GetHashColde() { return _g.GetHashCode(); }
		public Guid Guid {get { return _g; } }

	}
	 * */
	
	[Serializable]
    public class IdClassListReadOnly<TKey, type> : IEnumerable
	{
        protected Dictionary<TKey, type> dick = new Dictionary<TKey, type>();
        public type this[TKey id]
		{
			get { return dick[id]; }			
		}	

		public int Count { get { return dick.Count; } }

        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<TKey, type> item in dick)
            {
                // You could use conditional checks or other statements here for a higher
                // degree of control regarding what the enumerator returns.
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        
        
        
        //public IEnumerator<KeyValuePair<TKey, type>> GetEnumerator()
        //{
        //    return this.dick.GetEnumerator();
        //}

        //IEnumerator IEnumerable<KeyValuePair<TKey, type>>.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        public Dictionary<TKey, type>.KeyCollection Keys
        {
            get{ return dick.Keys; }
        }

        public Dictionary<TKey, type>.ValueCollection Values
        {
            get{ return dick.Values; }
        }
		
		
		
		
	}

	[Serializable]
    public class IdClassListWritable<TKey, type> : IdClassListReadOnly<TKey, type>, ISerializable where type : new()
	{
		
		public IdClassListWritable( )		{	
			
		}

		//Special constructor
		protected IdClassListWritable(SerializationInfo info, StreamingContext context) 
		{
            KeyValuePair<TKey, type>[] array = (KeyValuePair<TKey, type>[])info.GetValue("W"/*this.ToString()*/, typeof(KeyValuePair<TKey, type>[]));

            foreach (KeyValuePair<TKey, type> item in array)
			{
				dick.Add(item.Key, item.Value);
				
			}
			
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
            KeyValuePair<TKey, type>[] array = dick.ToArray();
            info.AddValue("W"/*this.ToString()*/, array, typeof(KeyValuePair<TKey, type>[]));
		}

        public void Add(TKey key, type item)
		{
			//Guid id = Guid.NewGuid();
            dick.Add(key, item);
		}
        new public type this[TKey id]
		{
			
            get { type value;
                dick.TryGetValue(id, out value);


                return value;  // dick[id];
            }
			set { dick[id] = value; }
		}

        public bool Remove(TKey key)
		{
			return dick.Remove(key);
		}

        public bool Replace(TKey id, type item, out type oldItem)
		{
			bool bFound = dick.TryGetValue(id, out oldItem);
			if(bFound)
			{
				dick[id] = item;
			}
			return bFound;
		}		
	}	
}
