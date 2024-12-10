using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace QIFParser
{
	public class QIF
	{
		
		private List<Entry> _entries = new List<Entry>();//_tokens;
		string HeaderText;
		public bool Parse(string filename)
		{
			List<string> _lines = new List<string>();
			//open the file
			using(StreamReader fStream = File.OpenText(filename))
			{
				string line = null;
				while((line = fStream.ReadLine())!=null)
				{
					if(line.IndexOf(@"!Type:")== 0)
					{
						HeaderText = line.Remove(0, 6);
						break;
					}
				}

				while((line = fStream.ReadLine())!=null)
				{
					line = line.Trim();
					if(line == "^")
					{
						Entry entry = entryFactory(_lines);
						this._entries.Add(entry);
						_lines.Clear();
					}
					else
					{
						_lines.Add(line);
					}
				}		
			}

			//split into lines

			//split into tokens
			return true;
		}

		public bool Write(string filename)
		{
			List<string> _lines = new List<string>();
			_lines.Add(@"!Type:" + HeaderText);
			foreach(Entry entry in _entries)
			{
				entry.Write(_lines);
			}

			using(StreamWriter fStream = File.CreateText(filename))
			{
				foreach(string line in _lines)
				{
					fStream.WriteLine(line);
				}
			}
			return true;
		}

		private Entry entryFactory(List<string> lines)
		{
			Entry _entry = new Entry();
			foreach(string line in lines)
			{
				_entry.AddToken(tokenFactory(line));
			}
			return _entry;
		}

		private Token tokenFactory(string line)
		{
			//line.ElementAt//if()
			//{
			//}

			
			char s = line.ElementAt(0);
			line = line.Remove(0,1);
			switch(s)
			{
				case'D':
				{
					DateToken dt = new DateToken();
					bool bResult = dt.Parse(line);
					return dt;
				}
				case'T':
				{
					AmountToken dt = new AmountToken();
					bool bResult = dt.Parse(line);
					return dt;
				}
				case PayeeToken.TokDesc:
				{
					PayeeToken dt = new PayeeToken();
					bool bResult = dt.Parse(line);
					return dt;
				}
				default:
				{
					Debug.Assert(false);
					break;
				}
			}			
			return null;
		}
	}

	internal class Entry
	{
		private List<Token> _tokens = new List<Token>();//;

		internal void AddToken(Token token)
		{
			_tokens.Add(token);
		}

		internal bool Write(List<string> lines)
		{
			foreach(Token token in _tokens)
			{
				token.Write(lines);
			}
			lines.Add(@"^");
			return true;
		}
	}

	public abstract class Token
	{
		public abstract bool Write(List<string> lines);
	}

	public class DateToken : Token
	{
		public const char TokDesc = 'D';
		private DateTime _date = new DateTime();
		public bool Parse(string line)
		{
			try
			{			
				_date = DateTime.ParseExact(line, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
			}
			catch
			{
				try{
				_date = DateTime.ParseExact(line, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);

				}
				catch{
				return false;
				}
			}
			//_date.AddYears(-100);
			
			return true;
		}

		public override bool Write(List<string> lines)
		{			
			string line = "D" + _date.ToString("dd/MM/yy");
			lines.Add(line);
			return true;
		}
	}

	internal class PayeeToken : Token
	{
		public const char TokDesc = 'P';
		private string desc;
		
		public bool Parse(string line)
		{
			desc = line;
			return true;
		}

		public override bool Write(List<string> lines)
		{
			lines.Add(PayeeToken.TokDesc + desc);
			return false;
		}
	}

	internal class AmountToken : Token
	{
		private decimal _decimal;
		public bool Parse(string line)
		{
			bool bResult = decimal.TryParse(line, out _decimal);
			return bResult;
		}

		public override bool Write(List<string> lines)
		{
			lines.Add("T" + _decimal.ToString("#.00"));
			return false;
		}
	}
}
