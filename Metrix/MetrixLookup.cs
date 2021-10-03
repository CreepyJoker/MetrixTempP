using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Web.Caching;
using System.Timers;
using System.Web;
using Metrix.Cmp;
using System.Diagnostics;

namespace Metrix
{
    public partial class MetrixLookup : Form
    {
        public MetrixLookup()
        {
            InitializeComponent();
        }
        public static string userData;
        public static string userName;
        private static string gameDirectory = "";
        private static string gameRegion = "";
        private string cacheFile = @"C:\metrixcache.metx";
        private System.Timers.Timer timerUpdatePlayerData = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);

        public static DataTable playerData;
        private Stopwatch antiSpamSearch = new Stopwatch();

        //private string hostPath = "http://minimarkt.ro/lookupPlayer.php"; // here you set the domain name when you buy it. the path should be the same.
        private string hostDomainName = "https://metrixstats.000webhostapp.com/"; // main domain
        private void MetrixLookup_Load(object sender, EventArgs e)
        {
            //mlk_searchbtn1.BackgroundImage = Properties.Resources.redBtnNormal;
            WowInfo.MTX_InitEncountersDictionaries();
            mlk_searchbtn1.TabStop = false;
            mlk_searchbtn1.FlatStyle = FlatStyle.Flat;
            mlk_searchbtn1.FlatAppearance.BorderSize = 0;
            mlk_searchbtn1.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            mlk_searchbtn1.Width = 79;
            mlk_searchbtn1.Height = 23;
            //mlk_searchbtn1.MouseDown += (s, ea) => { mlk_searchbtn1.BackgroundImage = Properties.Resources.redBtnDown; };
            //mlk_searchbtn1.MouseUp += (s, ea) => { mlk_searchbtn1.BackgroundImage = Properties.Resources.redBtnNormal; };
            //button1.Hide();

            if (Environment.MachineName == "WORKSTATIONEDU" || Environment.MachineName == "DESKTOP-SLBR22B" || Environment.MachineName == "DESKTOPSLBR22B")
            {
                button1.Show();
            }
        }

        private void mlk_searchbtn1_Click(object sender, EventArgs e)
        {
            if (antiSpamSearch.Elapsed.Seconds >= 10)
            {
                antiSpamSearch.Reset();
                antiSpamSearch.Stop();
            }
            if (!antiSpamSearch.IsRunning)
            {
                antiSpamSearch.Start();
            }
            else
            {
                MessageBox.Show("You have to wait " + (10 - antiSpamSearch.Elapsed.Seconds) + " seconds before you can perform another lookup.", "Metrix", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(metrixLookupTxtbox.Text != "")
            {
                string[] split = metrixLookupTxtbox.Text.Split('-');
                string pregion = gameRegion.Replace("\"", "");
                string prealm = "";
                string pname = "";
                try
                {
                    if (split[1].Length > 0)
                    {
                        pname = split[0];
                        prealm = split[1];
                        //Console.WriteLine($"Player Name: {pname} Player Realm: {prealm}");

                        WebRequest initreq = WebRequest.Create(hostDomainName + "lookupPlayer.php" + "?region=" + pregion + "&player=" + pname + "&realm=" + prealm);

                        initreq.Credentials = CredentialCache.DefaultCredentials;

                        
                        WebResponse details = initreq.GetResponse();
                        //Console.WriteLine(response);

                        using (Stream dataStream = details.GetResponseStream())
                        {
                            
                            StreamReader reader = new StreamReader(dataStream);
                            
                            string responseFromServer = reader.ReadToEnd();
                            
                            if (responseFromServer != "Player does not exist or has no info yet." && responseFromServer != "Access is denied." && responseFromServer != "Player does not exist." && responseFromServer != "Database connection failed.")
                            {
                                userData = responseFromServer;
                                PlayerStats frmn = new PlayerStats();
                                frmn.Show();

                                //Console.WriteLine(userData);
                                userName = metrixLookupTxtbox.Text;
                            }
                            else
                            {
                                MessageBox.Show(responseFromServer, "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        details.Close();

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message == "Unable to connect to the remote server")
                    {
                        MessageBox.Show("The webserver is currently unavailable. Please try again later.", "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if(ex.Message == "Index was outside the bounds of the array.")
                    {
                        MessageBox.Show("The lookup format should be: PlayerName-RealmName.\nPlease try again respecting the format requested.", "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Player name.", "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void setGameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // to be added
        }

        private string MTX_StitchData(string a, string b, string c)
        {
            string fin = "";

            if (a != "" && b != "" && c != "")
            {
                fin = ($"{a}{b}{c}");
                fin = fin.Replace('"', '\r');
                fin = fin.Replace("\r", "");
                fin = fin.Replace("\t", "");
                //Console.WriteLine(fin);
            }
            return fin;
        }

        private void MTX_SendUserData(string pathName)
        {
            //string userName = "";
            string userData = "";
            using (StreamReader sr = new StreamReader(pathName))
            {
                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null) 
                {
                    string c1 = "";
                    string c2 = "";
                    string c3 = "";
                    
                    if (line.Contains("MTX_ExportDB"))
                    {
                        if ((line = sr.ReadLine()) != null)
                        {
                            c1 = line;
                            c1 = c1.Replace(", -- [1]", "");
                            //Console.WriteLine(c1);
                            if ((line = sr.ReadLine()) != null)
                            {
                                c2 = line;
                                c2 = c2.Replace(", -- [2]", "");
                                //Console.WriteLine(c2);

                                if ((line = sr.ReadLine()) != null)
                                {
                                    c3 = line;
                                    c3 = c3.Replace(", -- [3]", "");
                                    //Console.WriteLine(c3);
                                    userData = ($"{Cmp.ConvertUserData.MTX_Arcan(MTX_StitchData(c1, c2, c3), Encoding.UTF8)}");
                                    //userData = '"' + userData + '"';
                                    //Console.WriteLine(userData);
                                    //playerData = Cmp.ConvertUserData.Tabulate(userData, "TD");
                                    //playerData = userData;
                                    //Console.WriteLine(playerData);

                                }
                            }
                        }
                        
                    }
                   
                }
            }

            string json = userData;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostDomainName +"syncUserdata.php");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(json);
            //request.SendChunked = true;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine("R : " + result);
                }
            }
        }


        private void selectChangeGameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tempDir = "";
            var tempRegion = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tempDir = folderBrowserDialog1.SelectedPath;

                if (!tempDir.Contains("retail"))
                {
                    MessageBox.Show("Please select a valid folder. You must select the `_retail_` subfolder as well.", "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Console.WriteLine(tempDir);
                

                if (!File.Exists(cacheFile))
                {
                    File.Create(cacheFile).Dispose();

                    string[] entries = Directory.GetFileSystemEntries(tempDir, "Config.wtf", SearchOption.AllDirectories);

                    foreach(var fisier in entries)
                    {
                        using (var sw = new StreamReader(fisier))
                        {
                            string line;
                            while((line = sw.ReadLine()) != null)
                            {
                                if (line.Contains("portal"))
                                {
                                    string[] lineSplitter = line.Split(' ');
                                    tempRegion = lineSplitter[2];
                                    Console.WriteLine(tempRegion);
                                }
                               
                            }
                            sw.Close();

                        }
                        
                    }

                    using (var sw = new StreamWriter(cacheFile))
                    {
                        sw.WriteLine(tempDir + "|" + tempRegion);
                        sw.Close();
                    }

                }
                if (File.Exists(cacheFile))
                {
                    string[] entries = Directory.GetFileSystemEntries(tempDir, "Config.wtf", SearchOption.AllDirectories);

                    foreach (var fisier in entries)
                    {
                        using (var sw = new StreamReader(fisier))
                        {
                            string line;
                            while ((line = sw.ReadLine()) != null)
                            {
                                if (line.Contains("portal"))
                                {
                                    string[] lineSplitter = line.Split(' ');
                                    tempRegion = lineSplitter[2];
                                    Console.WriteLine(tempRegion);
                                }

                            }
                            sw.Close();

                        }

                    }
                    using (var sw = new StreamWriter(cacheFile, false))
                    {
                        sw.WriteLine(tempDir + "|" + tempRegion);
                        sw.Close();
                    }
                    
                }
                gameDirectory = tempDir;
                gameRegion = tempRegion;
                MTX_UpdatePlayerData();
                timerUpdatePlayerData.Start();
            }
        }


        private void MTX_UpdatePlayerDataSch(object sender, ElapsedEventArgs e)
        {
            string path = gameDirectory;
            string[] entries = Directory.GetFileSystemEntries(path, "Metrix.lua", SearchOption.AllDirectories);

            foreach (var ent in entries)
            {
                MTX_SendUserData(ent);
                Thread.Sleep(100);
            }
            labelLatestUpdate.Text = "Last updated: " + System.DateTime.Now.ToString();
            label2.Text = "Your current game region is set to: " + gameRegion;

        }
        private void MTX_UpdatePlayerData()
        {
            string path = gameDirectory;
            string[] entries = Directory.GetFileSystemEntries(path, "Metrix.lua", SearchOption.AllDirectories);

            foreach (var ent in entries)
            {
                MTX_SendUserData(ent);
                Thread.Sleep(100);
            }
            labelLatestUpdate.Text = "Last updated personal data: " + System.DateTime.Now.ToString();
            label2.Text = "Your current game region is set to: " + gameRegion;
        }
        
        private void MetrixLookup_Shown(object sender, EventArgs e)
        {
            timerUpdatePlayerData.AutoReset = true;
            timerUpdatePlayerData.Elapsed += new ElapsedEventHandler(MTX_UpdatePlayerDataSch);

            if (File.Exists(cacheFile))
            {
                using (var sw = new StreamReader(cacheFile))
                {
                    string[] cacheData = sw.ReadLine().Split('|');
                    gameDirectory = cacheData[0];
                    gameRegion = cacheData[1];
                    sw.Close();
                    timerUpdatePlayerData.Start();
                    label2.Text = "Your current game region is set to: " + gameRegion;
                    Console.WriteLine("Works fine, idk what happens there...");
                }
                //Console.WriteLine(gameDirectory);
            }
            else
            {
                label2.Text = "REGION UNAVAILABLE. Please select a valid World of Warcraft game folder first.";
                MessageBox.Show("To properly use this application, please set the game folder. \n It must include the `_retail_` subfolder as well.", "[Metrix]", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MTX_UpdatePlayerData();
        }

        private void metrixLookupTxtbox_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                mlk_searchbtn1_Click(sender, e);
            }
        }

        private void checkForBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
