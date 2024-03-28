using Patches.assets;

namespace PatchTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUpdateMinor()
        {
            //Arrange
            VersionEngine versionEngine = new VersionEngine(VersionEngine.ReleaseType.Minor);
            String expected = "2";

            //Act
            String incrementCheck = versionEngine.UpdateMinor(1);
            
            //Assert
            Assert.AreEqual(expected, incrementCheck);
        }

        [TestMethod]
        public void TestUpdatePatch()
        {
            //Arrange
            VersionEngine versionEngine = new VersionEngine(VersionEngine.ReleaseType.Patch);
            String expected = "1";

            //Act
            String incrementCheck = versionEngine.UpdatePatch(1);

            //Assert
            Assert.AreEqual(expected, incrementCheck);
        }

        [TestMethod]
        public void TestMinorUpdateSetPatchToZero()
        {
            //Arrange
            VersionEngine versionEngine = new VersionEngine(VersionEngine.ReleaseType.Patch);
            String expected = "0";

            //Act
            versionEngine.UpdateMinor(1);
            String checkPatch = versionEngine.ModifiedPatch;
            
            //Assert
            Assert.AreEqual(expected, checkPatch);
        }




    }


}