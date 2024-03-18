using Films_Android_IOS.Data;

namespace Films_Android_IOS
{
	public partial class App : Application
	{
		private const string DbName = "Films.db";
		private static string _dbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

		private static DataBaseService database;
		public static DataBaseService Database
		{
			get
			{
				if (database == null)
				{
					database = new DataBaseService(_dbPath);
				}
				return database;
			}
		}

		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}
