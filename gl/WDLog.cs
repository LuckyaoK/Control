using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class WDLog
    {
        private bool _IsEnableLog;
        private int _CrdIntpID;
        private static Dictionary<int, object> _logLock = new Dictionary<int, object>() { { 1, new object() }, { 2, new object() } };
        public WDLog(int crdIntpID, bool isEnableLog = false)
        {
            _CrdIntpID = crdIntpID;
            _IsEnableLog = isEnableLog;
        }

        public void Log(string msg)
        {
            if (!_IsEnableLog) return;
            lock (_logLock[_CrdIntpID])
            {
                string fileName = string.Format("{0}Interference{1}.txt", _CrdIntpID == 1 ? EnumTestGroup.LeftGroup : EnumTestGroup.RightGroup, DateTime.Now.ToString("yy-MM-dd"));
                string filePath = Path.Combine(EnvironmentConfig.EnvironmentConfigLogData, fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss fff"), msg));
                    }
                }
            }
        }
        enum EnumTestGroup
        {
            LeftGroup,
            RightGroup
        }
    }

    public class EnvironmentConfig
    {
        public static string EnvironmentConfigPathRoot = @"EnvironmentConfig\";
        public static string EnvironmentConfigLogData = @"EnvironmentConfig\";
        public static string OpalConfigFilePath = @"ConfigTable\";
        public static string OpalTestImageSavePath = @"ImageProcessResult\";
        public static string OpalTestingResultPath = @"TestingResult\";

        static EnvironmentConfig()
        {
            string strOutput;
            try
            {
                // config axes config file path, if folder path not exist, create it
                EnvironmentConfigPathRoot = System.IO.Directory.GetCurrentDirectory() + @"\EnvironmentConfig\";
                if (false == System.IO.Directory.Exists(EnvironmentConfigPathRoot))
                {
                    System.IO.Directory.CreateDirectory(EnvironmentConfigPathRoot);
                }

                // construct the opal test log data path
                EnvironmentConfigLogData = System.IO.Directory.GetCurrentDirectory() + @"\EnvironmentConfig\LogData\";
                if (false == System.IO.Directory.Exists(EnvironmentConfigLogData))
                {
                    System.IO.Directory.CreateDirectory(EnvironmentConfigLogData);
                }

                // opal configuration file path
                OpalConfigFilePath = EnvironmentConfigPathRoot + @"ConfigTable\";
                if (false == System.IO.Directory.Exists(OpalConfigFilePath))
                {
                    System.IO.Directory.CreateDirectory(OpalConfigFilePath);
                }

                OpalTestImageSavePath = System.IO.Directory.GetCurrentDirectory() + @"\ImageProcessResult\";
                if (false == System.IO.Directory.Exists(OpalTestImageSavePath))
                {
                    System.IO.Directory.CreateDirectory(OpalTestImageSavePath);
                }

            }
            catch (Exception ex)
            {
                strOutput = string.Format("WD.Automation.Config EnvironmentConfig failed, " + ex.ToString());
                throw new Exception(strOutput);
            }
        }
    }
}
