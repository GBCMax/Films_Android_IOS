using Films_Android_IOS.View;
namespace Films_Android_IOS
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute($"{nameof(DefaultPage)}/{nameof(FullInfoFilmPage)}", typeof(FullInfoFilmPage));
		}
	}
}
