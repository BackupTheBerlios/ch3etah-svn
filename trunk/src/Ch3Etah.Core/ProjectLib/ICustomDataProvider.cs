using System;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Interface to be implemented by data sources
	/// stored as CustomDataItems in a project file.
	/// </summary>
	public interface ICustomDataProvider
	{
		
		string GetPersistentData();
		
		void RestorePersistentData(string data);
		
	}
}
