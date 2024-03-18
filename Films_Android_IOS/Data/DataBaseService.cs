using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.Data
{
	public class DataBaseService
	{
		private readonly SQLiteAsyncConnection? _database;

		public DataBaseService(string dbPath)
		{
			this._database = new SQLiteAsyncConnection(dbPath);
			this._database.CreateTableAsync<Film>();
			this._database.CreateTableAsync<Actor>();
		}
		public Task<List<Film>> GetFilmAsync()
		{
			return _database.Table<Film>().ToListAsync();
		}
		public Task<int> SaveFilmAsync(Film film)
		{
			return _database.InsertAsync(film);
		}
		public Task<int> UpdateFilmAsync(Film film)
		{
			return _database.UpdateAsync(film);
		}
		public Task<int> DeleteFilmAsync(Film film)
		{
			return _database.DeleteAsync(film);
		}
		public void ClearDBAsync()
		{
			_database.ExecuteAsync("DELETE FROM Film");
			_database.ExecuteAsync("DELETE FROM Actor");
		}
		public async void FillDatabase()
		{
			//ClearDBAsync();
			var actors = new List<Actor>
			{
				new Actor { FullName = "Арнольд Шварцнеггер" },
				new Actor { FullName = "Киллиан Мерфи" },
				new Actor { FullName = "Роберт Дауни - младший" },
				new Actor { FullName = "Киану Ривз" },
				new Actor { FullName = "Райан Гослинг" }
			};

			foreach (var actor in actors)
			{
				var existingActor = await _database.Table<Actor>().FirstOrDefaultAsync(x => x.FullName == actor.FullName);
				if (existingActor == null)
					await _database.InsertAsync(actor);
			}

			// Добавляем несколько фильмов и связываем их с актерами
			var films = new List<Film>
			{
				new Film
				{
					Name = "Терминатор",
					Genre = "Фантастика",
					Description = "История противостояния солдата Кайла Риза и киборга-терминатора, " +
					"прибывших в 1984-й год из пост-апокалиптического будущего, где миром правят " +
					"машины-убийцы, а человечество находится на грани вымирания.",
					ActorsString = string.Join(", ", actors.Take(1).Select(x => x.FullName)),
					Actors = actors.Take(1).ToList()
				},
				new Film
				{
					Name = "Оппенгеймер",
					Genre = "Триллер",
					Description = "История жизни американского физика Роберта Оппенгеймера, " +
					"который стоял во главе первых разработок ядерного оружия.",
					ActorsString = string.Join(", ", actors.Skip(1).Take(2).Select(x => x.FullName)),
					Actors = actors.Skip(1).Take(2).ToList()
				},
				new Film
				{
					Name = "Джон Уик 4",
					Genre = "Боевик",
					Description = "Джон Уик находит способ одержать победу над Правлением Кланов. " +
					"Однако, прежде чем он сможет заслужить свою свободу, ему предстоит сразиться " +
					"с новым врагом и его могущественными союзниками.",
					ActorsString = string.Join(", ", actors.Skip(3).Take(1).Select(x => x.FullName)),
					Actors = actors.Skip(3).Take(1).ToList()
				},
				new Film
				{
					Name = "Бегущий по лезвию 2049",
					Genre = "Фантастика",
					Description = "Продолжение культового фильма «Бегущий по лезвию», " +
					"действие которого разворачивается через несколько десятилетий.",
					ActorsString = string.Join(", ", actors.Skip(4).Take(1).Select(x => x.FullName)),
					Actors = actors.Skip(4).Take(1).ToList()
				}
			};

			foreach (var film in films)
			{
				var existingFilm = await _database.Table<Film>().FirstOrDefaultAsync(x => x.Name == film.Name);
				if (existingFilm == null)
					await _database.InsertAsync(film);
			}
		}
	}
}
