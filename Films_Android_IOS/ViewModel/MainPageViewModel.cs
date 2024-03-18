using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Films_Android_IOS.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.ViewModel
{
	public partial class MainPageViewModel : ObservableObject
	{
		private string searchFilmString;
		public string SearchFilmString
		{
			get
			{
				return searchFilmString;
			}
			set
			{
				searchFilmString = value;
				SearchBy(searchFilmString);
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Data.Film> SearchBy(string search)
		{
			if (SearchFilmString != "")
			{
				Films = new ObservableCollection<Data.Film>(
					listOfFilms.Where(film =>
					film.Name.ToLower().Contains(search.ToLower()) ||
					film.Genre.ToLower().Contains(search.ToLower()) ||
					film.ActorsString.ToLower().Contains(search.ToLower())
					)
					);
			}
			else
			{
				Films = new ObservableCollection<Data.Film>(listOfFilms);
			}
			return Films;
		}

		private List<Data.Film> listOfFilms = new List<Data.Film>();

		[ObservableProperty]
		private ObservableCollection<Data.Film> films = new ObservableCollection<Data.Film>();

		[ObservableProperty]
		private ObservableCollection<Data.Actor> actors = new ObservableCollection<Data.Actor>();

		[ObservableProperty]
		private Data.Film selectedFilm;

		public MainPageViewModel()
		{
			FillDB();
			//ClearDB();
			LoadFilmAsync();
			DataUpdater.DataUpdated += LoadFilmAsync;
		}

		public async void ClearDB()
		{
			await Task.Run(() => App.Database.ClearDBAsync());
		}

		public async void FillDB()
		{
			await Task.Run(() => App.Database.FillDatabase());
		}
		public async void LoadFilmAsync()
		{
			List<Data.Film> filmsList = await App.Database.GetFilmAsync();
			listOfFilms.Clear();
			listOfFilms.AddRange(filmsList);
			ObservableCollection<Data.Film> filmsCollection = new ObservableCollection<Data.Film>(filmsList);
			Films = filmsCollection;
		}

		[RelayCommand]
		private async Task DeleteFilm(Data.Film film)
		{
			await App.Database.DeleteFilmAsync(film);
			await Application.Current.MainPage.DisplayAlert("Есть сэр", "Удалено", "Есть сэр");
			LoadFilmAsync();
		}

		[RelayCommand]
		private async Task CheckInfoFilm(Data.Film film)
		{
			var navigationParameter = new Dictionary<string, object>
			{
				{ "Films", film }
			};
			await Shell.Current.GoToAsync("//DefaultPage/FullInfoFilmPage", navigationParameter);
		}
	}
}
