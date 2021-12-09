using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderSystem
{
    public class AllVehicleInfo
    {
        public static Dictionary<string, VehicleInfo> allVehicleInfo = new Dictionary<string, VehicleInfo>();

        public static Dictionary<string, Version> getSelectedCodesAndVersions()
        {
            Dictionary<string, Version> selectedCodesAndVersions = new Dictionary<string, Version>();
            foreach (var vehicleInformation in AllVehicleInfo.allVehicleInfo)
            {
                foreach (var versionInformation in vehicleInformation.Value.allCodesAndVersions)
                {
                    if (versionInformation.Value.isSelected)
                    {
                        selectedCodesAndVersions[versionInformation.Key + " | " + vehicleInformation.Key] = versionInformation.Value.xmlVersion;
                    }
                }
            }
            return selectedCodesAndVersions;
        }


        public static List<string> getFinasNumbers()
        {
            List<string> finasNumbers = new List<string>();
            foreach (var vehicleInformation in AllVehicleInfo.allVehicleInfo)
            {
                finasNumbers.Add(vehicleInformation.Key);
            }
            return finasNumbers;
        }
    }
    public class VehicleInfo
    {
        public string finasNumber;
        public Dictionary<string, VersionInfo> allCodesAndVersions;
        
    }

    public class VersionInfo
    {
        public Version xmlVersion;
        //public Version inputVersion;
        public bool isSelected = false;
    }
}
