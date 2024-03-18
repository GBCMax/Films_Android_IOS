using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.ViewModel
{
	[QueryProperty(nameof(Films), nameof(Films))]
	public partial class FullInfoFilmViewModel : ObservableObject
	{
		[ObservableProperty]
		private Data.Film films;
	}
}
