using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SM.Common;
using SM.Common.Models;
using SM.TapTapTap.Interfaces;

namespace SM.TapTapTap.UWP.ViewModels
{
	public class MainViewModel : ModelBase
	{
		private readonly ITapRecorder _tapRecorder;

		public MainViewModel()
		{
			_tapRecorder = new TapRecorder();
			Taps = new ObservableCollection<string>();

			PlayCommand = new RelayCommand(Play);
			TapCommand = new RelayCommand(Tap);
		}

		public int TapCount => _tapRecorder.AllTaps.Count();

		public ObservableCollection<string> Taps { get; private set; }

		public ICommand PlayCommand { get; private set; }

		public ICommand TapCommand { get; private set; }

		private void Play()
		{
			int index = 0;

			if (_tapRecorder.AllTaps.Any())
			{
				var first = _tapRecorder.AllTaps.First().Pressed;

				foreach (var tap in _tapRecorder.AllTaps)
				{
					Taps.Add($"Tap[{index++}] {tap.Pressed.Subtract(first).TotalMilliseconds:N0} ms");
				}
			}

			_tapRecorder.Clear();
			OnPropertyChanged(nameof(TapCount));
		}

		private void Tap()
		{
			_tapRecorder.Press();
			Taps.Clear();

			OnPropertyChanged(nameof(TapCount));
		}
	}
}