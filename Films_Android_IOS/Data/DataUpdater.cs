using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films_Android_IOS.Data
{
	public class DataUpdater
	{
		public delegate void DataUpdatedEventHandler();
		public static event DataUpdatedEventHandler DataUpdated;
		public static void InformAboutDataUpdatedEvent()
		{
			DataUpdated?.Invoke();
		}
	}
}
