using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AccountsCore
{
	public class ActionMaster
	{
        public ActionMaster() { }

        List<UserAction> _actions = new List<UserAction>();
		int _lastDone =-1;
		//int nMaxSize = 1000;
		//int nCurrentSize =0;

		public delegate void UndoDescriptionChangedHandler(object sender, string description);
		public event UndoDescriptionChangedHandler UndoDescriptionChanged;
		public delegate void CanUndoChangedHandler(object sender, bool canUndo);
		public event CanUndoChangedHandler CanUndoChanged;

		public delegate void RedoDescriptionChangedHandler(object sender, string description);
		public event RedoDescriptionChangedHandler RedoDescriptionChanged;
		public delegate void CanRedoChangedHandler(object sender, bool canRedo);
		public event CanRedoChangedHandler CanRedoChanged;

		public List<string> getActionReport()
		{
			List<string> result = new List<string>();
			for(int i = 0; i <= _lastDone; ++i)
			{
				result.Add(_actions[i].Description());
			}
			return result;
		}

        public void Clear() { _actions.Clear(); _lastDone = -1; }
		public void Do(UserAction action)
		{
			action.Do();
			++_lastDone;

			if(_lastDone < _actions.Count)
                _actions.RemoveRange(_lastDone, _actions.Count - _lastDone);

            _actions.Add(action);				
			Debug.Assert(_lastDone == (_actions.Count -1));

            //logging here

            //action events	
            UndoDescriptionChanged?.Invoke(this, UndoDescription());
            CanUndoChanged(this, CanUndo());

            RedoDescriptionChanged?.Invoke(this, RedoDescription());
            CanRedoChanged(this, CanRedo());
        }

		public bool CanUndo()
		{
			if (_lastDone >= 0)
			{
				return true;
			}
			return false;
		}
		
		public void Undo()
		{
			Debug.Assert(CanUndo());
			_actions[_lastDone].Undo();
			_lastDone--;
            //action events	
            UndoDescriptionChanged?.Invoke(this, UndoDescription());
            CanUndoChanged(this, CanUndo());

            RedoDescriptionChanged?.Invoke(this, RedoDescription());
            CanRedoChanged(this, CanRedo());
		}

		public bool CanRedo()
		{
			if (_lastDone < (_actions.Count -1))
			{
				return true;
			}
			return false;
		}
		
		public void Redo()
		{
			Debug.Assert(CanRedo());
			_lastDone++;
			_actions[_lastDone].Do();
            //action events	
            UndoDescriptionChanged?.Invoke(this, UndoDescription());
            CanUndoChanged(this, CanUndo());

            RedoDescriptionChanged?.Invoke(this, RedoDescription());
            CanRedoChanged(this, CanRedo());
		}

		public string UndoDescription()
		{
			if(CanUndo() == true)
			{
				return "undo:- " + _actions[_lastDone].Description();
			}
			return "No actions to Undo";
		}

		public string RedoDescription()
		{
			if(CanRedo() == true)
			{
				return "redo:- " + _actions[_lastDone +1].Description();
			}
			return "No actions to Redo";
		}
	}

	public abstract class UserAction 
	{
		public abstract void Do();
		public abstract void Undo();
		public abstract string Description();
	}
	
}
