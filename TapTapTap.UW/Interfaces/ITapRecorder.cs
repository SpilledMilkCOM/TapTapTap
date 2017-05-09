using System.Collections.Generic;

namespace SM.TapTapTap.Interfaces
{
	public interface ITapRecorder
	{
		IEnumerable<ITap> AllTaps { get; }

		/// <summary>
		/// Clear all of the recording data.
		/// </summary>
		void Clear();

		/// <summary>
		/// Register a tap "press"
		/// </summary>
		/// <param name="code"></param>
		void Press(string code = null);

		/// <summary>
		/// Register a tap "release"
		/// </summary>
		/// <param name="code"></param>
		void Release(string code = null);
	}
}