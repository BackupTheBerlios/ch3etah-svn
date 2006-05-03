namespace Ch3Etah.BugTracker
{
	public class TrackerItemStatus
	{
		public readonly string ID;
		public readonly string Name;
		public readonly bool IsClosed;
		
		public TrackerItemStatus(string id, string name, bool isClosed)
		{
			ID = id;
			Name = name;
			IsClosed = isClosed;
		}
		
		
		private static TrackerItemStatus[] _staticStatusList;
		
		public static TrackerItemStatus[] GetStaticStatusList()
		{
			if (_staticStatusList == null)
			{
				_staticStatusList = new 
					TrackerItemStatus[] {
						new TrackerItemStatus("1", "Open", false)
						, new TrackerItemStatus("2", "Closed", false)
						, new TrackerItemStatus("3", "Deleted", false)
						, new TrackerItemStatus("4", "Pending", false)
					};
			}
			return _staticStatusList;
		}

		public static TrackerItemStatus FindByID(string id)
		{
			foreach (TrackerItemStatus status in GetStaticStatusList())
			{
				if (status.ID == id)
				{
					return status;
				}
			}
			return null;
		}
		
		public static TrackerItemStatus FindByName(string name)
		{
			foreach (TrackerItemStatus status in GetStaticStatusList())
			{
				if (status.Name == name)
				{
					return status;
				}
			}
			return null;
		}

		public override string ToString()
		{
			return this.Name;
		}

	}
}
