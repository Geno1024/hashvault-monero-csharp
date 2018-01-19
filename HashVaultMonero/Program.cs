using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HashVaultMonero
{
    static class Program
    {

        public static void ExtractResourceToFile(string resourceName, string filename)
        {
            if (File.Exists(filename)) return;
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                if (s == null) return;
                var b = new byte[s.Length];
                s.Read(b, 0, b.Length);
                fs.Write(b, 0, b.Length);
            }
        }


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExtractResourceToFile("HashVaultMonero.Newtonsoft_Json.dll", Path.Combine(Path.GetTempPath(), "Newtonsoft_Json.dll"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Monero());
        }
    }
}
