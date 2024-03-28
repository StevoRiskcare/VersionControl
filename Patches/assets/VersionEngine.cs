using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonExtensionDataAttribute = Newtonsoft.Json.JsonExtensionDataAttribute;

namespace Patches.assets
{
    public class VersionEngine
    {
       
        public ReleaseType Release { get; set; }
        public String FilePath { get; set; }
        
        private String modifiedPatch;

        public String ModifiedPatch
        {
            get { return modifiedPatch; }
            set { modifiedPatch = value; }
        }

        private String modifiedMinor;
        public String ModifiedMinor
        {
            get { return modifiedMinor; }
            set { modifiedMinor = value; }
        }


        public VersionEngine(ReleaseType release, String filePath)
        {
            this.Release = release;
            this.FilePath = filePath;
        }

        public VersionEngine(ReleaseType release)
        {
            this.Release = release;
        }

        public void UpdateVersion()
        {
            
            string json = File.ReadAllText(this.FilePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string version = jsonObj["Version"];
            string[] versions = version.Split('.');
            this.ModifiedMinor = versions[1];
            this.ModifiedPatch = versions[2];

            switch (Release)
            {
                case ReleaseType.Minor:
                    {
                        this.ModifiedMinor = this.UpdateMinor(int.Parse(versions[1]));
                        break;
                    }
                case ReleaseType.Patch:
                    {
                        int incrementPatch = int.Parse(versions[2]);
                        incrementPatch++;
                        this.ModifiedPatch = this.UpdatePatch(incrementPatch);
                        
                        break;
                    }
                default: break;
            }


            jsonObj["Version"] = $"{versions[0]}.{modifiedMinor}.{modifiedPatch}";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(this.FilePath, output);
        }


        
        

        /// <summary>
        /// Accepts an integer variable and increments it. Results are persisted to modifiedMinor
        /// </summary>
        /// <param name="minor"></param>
        public string UpdateMinor(int minor)
        {
            minor++;
            int updateMinor = minor;

            this.ModifiedPatch = this.UpdatePatch();
            return updateMinor.ToString();


        }

        /// <summary>
        /// Accepts an integer variable returns a string
        /// </summary>
        /// <param name="patch"></param>
        public String UpdatePatch(int patch = 0)
        {
           
            int updatePatch = patch;

            return updatePatch.ToString();
        }

        public enum ReleaseType
        {
            Minor,
            Patch
        }

    }
}
