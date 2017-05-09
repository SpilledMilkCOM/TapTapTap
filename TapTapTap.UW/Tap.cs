using System;
using SM.TapTapTap.Interfaces;

namespace SM.TapTapTap
{
	public class Tap : ITap
	{
		public Tap()
		{
			Pressed = DateTime.Now;
		}

		public string Code { get; set; }

		public TimeSpan? Duration
		{
			get
			{
				TimeSpan? result = null;

				if (Released.HasValue)
				{
					result = Released.Value.Subtract(Pressed);
				}

				return result;
			}
		}

		public bool IsLocked { get; set; }

		public DateTime Pressed { get; set; }

		public DateTime? Released { get; set; }
	}
}