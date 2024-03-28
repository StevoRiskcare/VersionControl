using Patches.assets;
using static Patches.assets.VersionEngine;

internal class Program
{
    private static ReleaseType releaseType;

    /// <summary>
    /// Hit Run and input Minor or Patch followed by a path to a valid json file. See the ReadMe.txt instructions under
    /// Solution Items
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        Console.WriteLine("Please enter update type (Minor or Patch)");
        string patchType = Console.ReadLine();
        Console.WriteLine("Please enter version file location");
        string fileLocation = Console.ReadLine();

        patchType = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(patchType.ToLower());

        if (File.Exists(fileLocation))
        {
            try
            {
                ReleaseType releaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), patchType);
                VersionEngine version = new VersionEngine(releaseType, fileLocation);
                version.Release = releaseType;
                version.UpdateVersion();
                Console.WriteLine(releaseType);
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please select input Minor or Patch");
            }
        }
        else
        {
            Console.WriteLine("The file location does not exist");
        }
    }
}