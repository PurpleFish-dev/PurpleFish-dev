using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolStripCustomCtrls
{
    class UniqueString
    {
        public UniqueString(string sOriginal)
        {
            _original = sOriginal;
            _existings = new List<string>();
        }

        public UniqueString(string sOriginal, string[] Existings)
        {
            _original = sOriginal; 
            _existings.AddRange(Existings);
        }

        public void AddExisting(string sExisting)
        {
            _existings.Add(sExisting);
        }

        public bool GetUniqueString(out string sUnique)
        {
            if (_existings.Contains(_original) == false)
            {
                sUnique = _original;
                return false;
            }  
            
            //remove brackets and contents
            string originalNoBracketSub = _original;
            int nIndexOfLastOpenBracket = _original.LastIndexOf("(");
            int nIndexOfLastCloseBracket = _original.LastIndexOf(")");

            if ((nIndexOfLastOpenBracket != -1) &&
                (nIndexOfLastCloseBracket != -1) &&
                (nIndexOfLastOpenBracket < nIndexOfLastCloseBracket))
            {
                originalNoBracketSub = _original.Remove(nIndexOfLastOpenBracket);
            }

            if (_existings.Contains(originalNoBracketSub) == false)
            {
                sUnique = originalNoBracketSub;
                return true;
            }  
            
            int nCopyNum = 2;
            while (_existings.Contains(originalNoBracketSub + "(" + nCopyNum.ToString() + ")"))
            {
                nCopyNum++;
            }
            sUnique = originalNoBracketSub + "(" + nCopyNum.ToString() + ")";
            return true;
        }

        private string _original;
        private List<string> _existings;
    }
}
