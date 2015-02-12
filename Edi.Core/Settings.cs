using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Core
{
    public class Settings
    {
        /// <summary>
        /// The directory that the data files get dumped into
        /// </summary>
        public static string ImportDirectory
        {
            get { return ConfigurationManager.AppSettings["ImportDirectory"]; }
        }

        public static string EdiTextFileDirectory
        {
            get { return ConfigurationManager.AppSettings["EdiTextFileDirectory"]; }
        }
    }
}
