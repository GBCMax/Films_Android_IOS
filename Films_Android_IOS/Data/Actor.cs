using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.Data
{
	public class Actor
	{
		[PrimaryKey, AutoIncrement]
		public int ActorId { get; set; }
		public string FullName { get; set; }
	}
}
