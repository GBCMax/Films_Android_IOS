using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.Data
{
	public class Film
	{
		[PrimaryKey, AutoIncrement]
		public int FilmId { get; set; }
		public string Name { get; set; }
		public string Genre { get; set; }
		public string Description { get; set; }
		public string ActorsString {  get; set; }

		[OneToMany]
		public List<Actor> Actors { get; set; }
	}
}
