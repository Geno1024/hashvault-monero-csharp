using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Helpers;
using System.Windows.Forms;

namespace HashVaultMonero
{
    public partial class Monero : Form
    {
        public Monero()
        {
            InitializeComponent();
        }
        
        private void UpdateOutput(object str)
        {
            if (Output.InvokeRequired)
            {
                Action<string> actionDelegate = (x) => { Output.Text = x.ToString(); };
                Output.Invoke(actionDelegate, str);
            }
            else
            {
                Output.Text = str.ToString();
            }
        }

        private void GetStats()
        {
            dynamic result = Json.Decode(new StreamReader(WebRequest.Create($"https://monero.hashvault.pro/api/miner/{Address.Text}/stats").GetResponse().GetResponseStream()).ReadToEnd());
            long paid = Convert.ToInt64(result.amtPaid);
            long due = Convert.ToInt64(result.amtDue);
            UpdateOutput($"Paid: {paid / 1000000000000.0}\n" +
                $"Due: {due / 1000000000000.0}\n" +
                $"Total: {(paid + due) / 1000000000000.0}\n" +
                $"Hash Rate: {result.hash}\n" +
                $"Total Hashes: {result.totalHashes}\n" +
                $"Valid Shares: {result.validShares}\n" +
                $"Invalid Shares: {result.invalidShares}");
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Output.Text = "Waiting...";
            new Thread(GetStats).Start();
        }
    }
}
