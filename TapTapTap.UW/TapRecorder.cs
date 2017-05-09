using System;
using System.Collections.Generic;
using System.Linq;
using SM.TapTapTap.Interfaces;

namespace SM.TapTapTap
{
	/// <summary>
	/// A class to record "taps", clicks, or press/release
	/// </summary>
	public class TapRecorder : ITapRecorder
	{
		private readonly List<ITap> _taps;

		public TapRecorder()
		{
			_taps = new List<ITap>();
		}

		public IEnumerable<ITap> AllTaps => _taps;

		public void Clear()
		{
			_taps.Clear();
		}

		public void Press(string code = null)
		{
			var found = Last(code);

			if (found != null)
			{
				// A press will lock the previous code.

				found.IsLocked = true;
			}

			_taps.Add(new Tap { Code = code });
		}

		public void Release(string code = null)
		{
			var found = Last(code);

			if (found != null)
			{
				found.Released = DateTime.Now;
				found.IsLocked = true;
			}
		}

		private ITap Last(string code)
		{
			return _taps.LastOrDefault(tap => tap.Code == code && !tap.IsLocked);
		}
	}
}