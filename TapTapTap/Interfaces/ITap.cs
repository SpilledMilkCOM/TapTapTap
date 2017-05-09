using System;

namespace SM.TapTapTap.Interfaces
{
	public interface ITap
	{
		/// <summary>
		/// The code or character pressed (can be null)
		/// </summary>
		string Code { get; set; }

		TimeSpan? Duration { get; }

		/// <summary>
		/// The class is locked and should not change.
		/// </summary>
		bool IsLocked { get; set; }

		/// <summary>
		/// The timestamp when tapped.
		/// </summary>
		DateTime Pressed { get; set; }

		/// <summary>
		/// The timestamp when the press is released.
		/// </summary>
		DateTime? Released { get; set; }
	}
}