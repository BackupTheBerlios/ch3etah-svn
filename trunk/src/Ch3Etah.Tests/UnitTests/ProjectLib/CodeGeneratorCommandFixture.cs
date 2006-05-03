using NUnit.Framework;

namespace Ch3Etah.Tests.UnitTests.ProjectLib
{
	[TestFixture]
	public class CodeGeneratorCommandFixture
	{
		[Test]
		public void TestOutputSingleFile_SelectMetadataFiles()
		{
			Assert.Fail("This part of the app needs to be refactored so that it's more testable.");
			//Bug Tracker ID: 1479979
			// The user needs to be able to specify which files 
			// will be included when "Single Output" is chosen.
			// 1. Create command object w/ selected metadata files
			// 2. Change command to "Single Output"
			Assert.Fail("Make sure metadata files are still selected.");
			Assert.Fail("Make sure template gets passes only selected files, and not all the files from the project.");
			Assert.Fail("In single output mode, the template should have access to 'selected file' AND 'project files'.");
		}
	}
}
