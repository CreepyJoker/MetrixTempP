using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Metrix.Cmp;
using System.Diagnostics;
//using MoonSharp.Interpreter;

namespace Metrix
{
    public partial class PlayerStats : Form
    {
        public PlayerStats()
        {
            InitializeComponent();
            
            //this.Text = "Metrix - User Stats for " + userName;
        }

        private string userData = MetrixLookup.userData;
        private int highestLevel;
        private string bestTimer;
        private int highestPoints;
        private int highestFailurePoints;
        private int highestStars;
        private string highestRole;
        private string totalPoints;
        private int refreshFormTimer = 10;
        private int highestPointsKey;
        private int highestFailurePointsKey;
        private int highestStarsKey;
        private int bestTimerKey;
        private string lastDungeonSelected = "";
        private int lastRunInt = 0;
        private bool failedAffixesSelected = false;

        protected virtual bool DoubleBuffered { get; set; }

        private Dictionary<string, Color> dictClassColors = new System.Collections.Generic.Dictionary<string, Color>();
        private void PlayerStats_Load(object sender, EventArgs e)
        {

            dictClassColors.Add("DRUID", Color.Orange);
            dictClassColors.Add("ROGUE", Color.Yellow);
            dictClassColors.Add("MAGE", Color.Aquamarine);
            dictClassColors.Add("SHAMAN", Color.Blue);
            dictClassColors.Add("DEMONHUNTER", Color.Purple);
            dictClassColors.Add("MONK", Color.LimeGreen);
            dictClassColors.Add("WARRIOR", Color.Brown);
            dictClassColors.Add("PRIEST", Color.White);
            dictClassColors.Add("HUNTER", Color.GreenYellow);
            dictClassColors.Add("WARLOCK", Color.CadetBlue);
            dictClassColors.Add("PALADIN", Color.Pink);
            dictClassColors.Add("DEATHKNIGHT", Color.Red);

            DoubleBuffered = true;
            if (userData == null)
            {
                Close();
            }
            labelDungeonName.Text = "";
            MTX_ResetUserInterface();
            //MTX_SetUserData("FH");
            //Console.WriteLine(MTX_GetUserDungeons());
            //Console.WriteLine(userData);
            this.BackgroundImage = Properties.Resources.Waycrest;
            foreach (var button in this.Controls.OfType<Button>())
            {
                //button.BackgroundImage = Properties.Resources.redBtnNormal;
                if (button.Name != "buttonHighestStats" && button.Name != "buttonFailedMechanics" && button.Name != "btnFailedAffixes")
                {
                    button.BackColor = Color.FromArgb(255, 17, 17, 17);
                    button.ForeColor = Color.White;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                    button.TabStop = false;
                    //button.FlatStyle = FlatStyle.Flat;
                    //button.FlatAppearance.BorderSize = 0;
                    button.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    button.Width = 63;
                    button.Height = 23;

                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 59, 59, 59);
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 59, 59, 59);
                }
                else
                {
                    button.BackColor = Color.FromArgb(255, 17, 17, 17);
                    button.ForeColor = Color.White;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                    button.TabStop = false;
                    button.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 59, 59, 59);
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 59, 59, 59);
                }
            }

            foreach(var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //labelCharName.Text = "Thealmighty-Silvermoon (EU) - Gnome Warrior";

        }

        private void MTX_SetDungeonFailedMechanics(string dungeonName, int runNumber)
        {
            dataGridView2.DataSource = Cmp.ConvertUserData.MTX_JsonFailedMech(userData, dungeonName, runNumber);
        }

        private void MTX_SetDungeonFailedAffixes(string dungeonName, int runNumber)
        {
            dataGridView2.DataSource = Cmp.ConvertUserData.MTX_JsonFailedAffixes(userData, dungeonName, runNumber);
        }

        private void MTX_SetUserData(string dungeonName)
        {
            try
            {
                MTX_ResetUserInterface();
                MTX_SetPlayerTotalPoints();
                DataTable playerData = Cmp.ConvertUserData.Tabulate(userData, dungeonName);

                dataGridView1.DataSource = playerData;

                for (int i = 0; i < playerData.Rows.Count; i++)
                {
                    for (int j = 0; j < playerData.Columns.Count; j++)
                    {

                        dataGridView1.Columns["RT"].HeaderText = "Run Timer";
                        dataGridView1.Columns["RT"].Visible = false;
                        dataGridView1.Columns["TD"].HeaderText = "Date";
                        dataGridView1.Columns["P"].HeaderText = "Points";
                        dataGridView1.Columns["FP"].HeaderText = "Failure Points";
                        dataGridView1.Columns["TB"].HeaderText = "Timer Bonus";
                        dataGridView1.Columns["UL"].HeaderText = "Stars";
                        dataGridView1.Columns["RA"].HeaderText = "Role";
                        dataGridView1.Columns["DT"].HeaderText = "Dungeon Timer";
                        dataGridView1.Columns["DT"].Visible = false;
                        dataGridView1.Columns["L"].HeaderText = "Level";
                        dataGridView1.Columns["IT"].HeaderText = "Timed Run";

                        if(dataGridView1.Rows[i].Cells["RA"].Value.ToString() == "DAMAGER")
                        {
                            dataGridView1.Rows[i].Cells["RA"].Value = "DPS";
                        }
                        

                        highestLevel = Convert.ToInt32(playerData.AsEnumerable().Max(row => row["L"]));

                        highestPoints = Convert.ToInt32(playerData.AsEnumerable().Max(row => row["P"]));
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["P"].Value != null)
                            {
                                if (Convert.ToInt32(row.Cells["P"].Value) == highestPoints)
                                {
                                    highestPointsKey = row.Index;
                                    break;
                                }
                            }
                        }

                        highestFailurePoints = Convert.ToInt32(playerData.AsEnumerable().Max(row => row["FP"]));
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["FP"].Value != null)
                            {
                                if (Convert.ToInt32(row.Cells["FP"].Value) == highestFailurePoints)
                                {
                                    highestFailurePointsKey = row.Index;
                                    break;
                                }
                            }
                        }



                        highestStars = Convert.ToInt32(playerData.AsEnumerable().Max(row => row["UL"]));
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["UL"].Value != null)
                            {
                                
                                if (Convert.ToInt32(row.Cells["UL"].Value) == highestStars)
                                {
                                    highestStarsKey = row.Index;
                                    break;
                                }
                            }
                        }


                        bestTimer = Convert.ToString(MTX_ConvertSecondsToHMS(Convert.ToInt32((playerData.AsEnumerable().Max(row => row["RT"])))));
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["RT"].Value != null)
                            {

                                if (Convert.ToString(row.Cells["RT"].Value) == bestTimer)
                                {
                                    bestTimerKey = row.Index;
                                    break;
                                }
                            }
                        }

                        foreach (DataGridViewBand band in dataGridView1.Columns)
                        {
                            band.ReadOnly = true;
                        }

                        if (dataGridView1.Rows.Count == 3)
                        {
                            dataGridView1.Height = 87;
                        }
                        else if(dataGridView1.Rows.Count == 2)
                        {
                            dataGridView1.Height = 65;
                        }
                        else if(dataGridView1.Rows.Count == 1)
                        {
                            dataGridView1.Height = 43;
                        }

                        //dataGridView1.Columns["P"].HeaderText = "Points";

                        
                        dataGridView1.AllowUserToAddRows = false;
                        //Console.WriteLine(x);
                        MTX_SetUserInterface();
                        DoubleBuffered = true;


                    }
                }
                //dataGridView1.Refresh();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public static string MTX_FindString(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        private void MTX_SetPlayerTotalPoints()
        {
            string tempTotalPoints = "";
            string playerName = "";
            dynamic stuff = JsonConvert.DeserializeObject(userData);
            tempTotalPoints = stuff.playerID.POP;
            playerName = stuff.playerID.PN + "-" + stuff.playerID.PR;
            string playerRegion = stuff.playerID.PRG;


            totalPoints = tempTotalPoints;
            labelPlayerPoints.Text = $"{totalPoints}";
            labelPlayerPoints.BackColor = Color.Black;
            PlayerStats.ActiveForm.Text = $"[Metrix] - Personal Stats for {playerName}";

            if (playerRegion != null)
            {
                if (playerRegion == "3")
                {
                    playerRegion = "EU";
                }
                else if(playerRegion == "2")
                {
                    playerRegion = "KR";
                }
                else if(playerRegion == "1")
                {
                    playerRegion = "US";
                }
                else if(playerRegion == "4")
                {
                    playerRegion = "TW";
                }
                else if(playerRegion == "5")
                {
                    playerRegion = "CH";
                }
            }

            var playerClass = stuff.playerID.PLC;
            var playerRace = stuff.playerID.PLR;
            var playerFaction = stuff.playerID.PF;

            if( playerRace == null )
            {
                playerRace = "N/A";
            }

            if (playerClass == null)
            {
                playerClass = "ROGUE";
            }

            labelCharName.Text = $"{playerName} ( {playerRegion} ) - {playerRace} {playerClass}";
            labelCharName.ForeColor = dictClassColors[playerClass.ToString()];
            labelCharName.BackColor = Color.Black;
            //labelCharName.BackColor = Color.Black;

            if (playerFaction == "Alliance")
            {
                factionBannerPBox.Image = Properties.Resources.allianceBnr;
                
            }
            else if (playerFaction == "Horde")
            {
                factionBannerPBox.Image = Properties.Resources.hordeBnr;
            }

        }

        private void MTX_ResetUserInterface()
        {
            labelSelectADungeonFirst.Show();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label3.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            dataGridView1.DataSource = null;
            dataGridView1.Hide();
            dataGridView2.Hide();
            buttonHighestStats.Hide();
            buttonFailedMechanics.Hide();
            btnFailedAffixes.Hide();

            btnRun1.Hide();
            btnRun2.Hide();
            btnRun3.Hide();

        }

        private void MTX_SetUserInterface()
        {

            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkGray), Color.FromKnownColor(KnownColor.Black));
            labelSelectADungeonFirst.Hide();
            dataGridView1.Show();
            label1.Text = $"Highest Key Level: {highestLevel}";
            label1.Show();

            label2.Text = $"Highest Stars: {highestStars} - Key Level: [{dataGridView1.Rows[highestStarsKey].Cells["L"].Value}]";
            label2.Show();

            label3.Text = $"Highest Points: {highestPoints} - Key Level: [{dataGridView1.Rows[highestPointsKey].Cells["L"].Value}]";
            label3.Show();

            label5.Text = $"Best Timer: {bestTimer} - Key Level: [{dataGridView1.Rows[bestTimerKey].Cells["L"].Value}]";
            label5.Show();

            label6.Text = $"Highest Failure Points: {highestFailurePoints} - Key Level: [{dataGridView1.Rows[highestFailurePointsKey].Cells["L"].Value}]";
            label6.Show();

            label7.Text = $"";
            label7.Show();

            buttonFailedMechanics.Show();
            buttonHighestStats.Show();
            btnFailedAffixes.Show();

        }

        private static string MTX_ConvertSecondsToHMS(int secs)
        {
            TimeSpan t = TimeSpan.FromSeconds(secs);
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)t.TotalHours,
                t.Minutes,
                t.Seconds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("AD");
            lastDungeonSelected = "AD";
            labelDungeonName.Text = "Atal'Dazar";
            this.BackgroundImage = Properties.Resources.AD;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("AD");

            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.Black;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkRed), Color.FromKnownColor(KnownColor.Black));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("UR");
            lastDungeonSelected = "UR";
            labelDungeonName.Text = "Underrot";
            this.BackgroundImage = Properties.Resources.UR;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("UR");

            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.GreenYellow;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.LightGray), Color.FromKnownColor(KnownColor.Black));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("FH");
            lastDungeonSelected = "FH";
            labelDungeonName.Text = "Freehold";
            this.BackgroundImage = Properties.Resources.Freehold;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("FH");

            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.Aqua;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkGray), Color.FromKnownColor(KnownColor.Black));
            //dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("ML");
            lastDungeonSelected = "ML";
            labelDungeonName.Text = "The Motherlode";
            this.BackgroundImage = Properties.Resources.Mothelode;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("ML");

            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.ForestGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkGreen), Color.FromKnownColor(KnownColor.LightSkyBlue));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("SS");
            lastDungeonSelected = "SS";
            labelDungeonName.Text = "Shrine of the Storm";
            this.BackgroundImage = Properties.Resources.SOTS;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("SS");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.LightYellow), Color.FromKnownColor(KnownColor.SeaShell));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("TD");
            lastDungeonSelected = "TD";
            labelDungeonName.Text = "Tol Dagor";
            this.BackgroundImage = Properties.Resources.Tol_Dagor;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("TD");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.Tomato), Color.FromKnownColor(KnownColor.Bisque));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("WM");
            lastDungeonSelected = "WM";
            labelDungeonName.Text = "Waycrest Manor";
            this.BackgroundImage = Properties.Resources.Waycrest;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("WM");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.Tomato), Color.FromKnownColor(KnownColor.LightYellow));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("SB");
            lastDungeonSelected = "SB";
            labelDungeonName.Text = "Siege of Boralus";
            this.BackgroundImage = Properties.Resources.SOB;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("SB");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.Black;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.Black), Color.FromKnownColor(KnownColor.Bisque));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("JY");
            lastDungeonSelected = "JY";
            labelDungeonName.Text = "Mechagon: Junkyard";
            this.BackgroundImage = Properties.Resources.Junkyard;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("JY");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.Black;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkBlue), Color.FromKnownColor(KnownColor.Black));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("WS");
            lastDungeonSelected = "WS";
            labelDungeonName.Text = "Mechagon: Workshop";
            this.BackgroundImage = Properties.Resources.WS;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("WS");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkBlue), Color.FromKnownColor(KnownColor.LightYellow));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("TS");
            lastDungeonSelected = "TS";
            labelDungeonName.Text = "Temple of Sethraliss";
            this.BackgroundImage = Properties.Resources.TOS;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("WS");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkBlue), Color.FromKnownColor(KnownColor.LightYellow));
        }


        private void button12_Click(object sender, EventArgs e)
        {
            MTX_SetUserData("KR");
            lastDungeonSelected = "KR";
            labelDungeonName.Text = "King's Rest";
            this.BackgroundImage = Properties.Resources.KR;
            Thread.Sleep(refreshFormTimer);
            //MTX_SetUserData("WS");
            /*foreach (var label in this.Controls.OfType<Label>())
            {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.LawnGreen;
            }*/
            MTX_SetFormatGrid(Color.FromKnownColor(KnownColor.DarkBlue), Color.FromKnownColor(KnownColor.LightYellow));
        }

        private void PlayerStats_Shown(object sender, EventArgs e)
        {
            MTX_SetPlayerTotalPoints();
        }



        private void MTX_SetFormatGrid(Color clrHeaders, Color clrCells)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            /*dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = clrHeaders;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.DefaultCellStyle.BackColor = Color.Transparent;
                col.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                col.DefaultCellStyle.ForeColor = clrCells;
            }*/
        }

        private void buttonHighestStats_Click(object sender, EventArgs e)
        {
            MTX_SetUserData(lastDungeonSelected);
            //MTX_SetUserInterface();
            dataGridView1.Show();
            dataGridView2.Hide();
        }

        private void buttonFailedMechanics_Click(object sender, EventArgs e)
        {
            failedAffixesSelected = false;

            MTX_ResetUserInterface();

            buttonFailedMechanics.Show();
            buttonHighestStats.Show();
            btnFailedAffixes.Show();

            MTX_SetDungeonFailedMechanics(lastDungeonSelected, 0);
            labelSelectADungeonFirst.Hide();

            dataGridView1.Hide();
            dataGridView2.Show();

            var howManyRunsToShow = Cmp.ConvertUserData.nrDungeonsFailed;
            
            if(howManyRunsToShow == 1)
            {
                btnRun1.Show();
            }
            else if(howManyRunsToShow == 2)
            {
                btnRun1.Show();
                btnRun2.Show();
            }
            else if(howManyRunsToShow == 3)
            {
                btnRun1.Show();
                btnRun2.Show();
                btnRun3.Show();
            }
        }

        private void btnRun1_Click(object sender, EventArgs e)
        {
            lastRunInt = 0;
            if (failedAffixesSelected)
            {
                MTX_SetDungeonFailedAffixes(lastDungeonSelected, lastRunInt);
            }
            else
            {
                MTX_SetDungeonFailedMechanics(lastDungeonSelected, lastRunInt);
            }

            if (dataGridView2.Columns.Count >= 1)
            {
               
                dataGridView2.Show();
            }
            else
            {
                dataGridView2.Hide();
            }
        }

        private void btnRun2_Click(object sender, EventArgs e)
        {
            lastRunInt = 1;
            if (failedAffixesSelected)
            {
                MTX_SetDungeonFailedAffixes(lastDungeonSelected, lastRunInt);
            }
            else
            {
                MTX_SetDungeonFailedMechanics(lastDungeonSelected, lastRunInt);
            }
            if (dataGridView2.Columns.Count >= 1)
            {
               
                dataGridView2.Show();
            }
            else
            {
                dataGridView2.Hide();
            }
        }

        private void btnRun3_Click(object sender, EventArgs e)
        {
            lastRunInt = 2;
            if (failedAffixesSelected)
            {
                MTX_SetDungeonFailedAffixes(lastDungeonSelected, lastRunInt);
            }
            else
            {
                MTX_SetDungeonFailedMechanics(lastDungeonSelected, lastRunInt);
            }
            if (dataGridView2.Columns.Count >= 1)
            {
                
                dataGridView2.Show();
            }
            else
            {
                dataGridView2.Hide();
            }
        }

        private void btnFailedAffixes_Click(object sender, EventArgs e)
        {
            
            failedAffixesSelected = true;
            MTX_ResetUserInterface();

            buttonFailedMechanics.Show();
            buttonHighestStats.Show();
            btnFailedAffixes.Show();

            var howManyRunsToShow = 3;

            if (howManyRunsToShow == 1)
            {
                btnRun1.Show();
            }
            else if (howManyRunsToShow == 2)
            {
                btnRun1.Show();
                btnRun2.Show();
            }
            else if (howManyRunsToShow == 3)
            {
                btnRun1.Show();
                btnRun2.Show();
                btnRun3.Show();
            }

            MTX_SetDungeonFailedAffixes(lastDungeonSelected, lastRunInt);
            labelSelectADungeonFirst.Hide();

            if (dataGridView2.Columns.Count >= 1)
            {
                dataGridView2.Show();
            }

            // to add toggling of the new failed affixes datagridview
        }

        
    }
}
