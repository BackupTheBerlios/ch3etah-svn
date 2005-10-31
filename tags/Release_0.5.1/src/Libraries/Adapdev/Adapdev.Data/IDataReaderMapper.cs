namespace Adapdev.Data
{
	using System.Collections;
	using System.Data;

	/// <summary>
	/// Summary description for IDataReaderMapper.
	/// </summary>
	public interface IDataReaderMapper
	{
		IList MapObjects(IDataReader dr);
	}
}