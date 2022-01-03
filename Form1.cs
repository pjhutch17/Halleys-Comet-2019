/* Halley's Comet
' Adventure Game
' Written by P Hutchison 27/01/86
' for TI-99/4A console in TI Ext.Basic.
' Updated for VB 2005 on 23/04/08 for Windows Vista.
' Updated for C# in VS 2019 on Windows 10.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Halleys_Comet_2019
{
    public partial class HC_Form : Form
    {
        public string[,] LocationText=null;
        public string[] LocList=null;
        public Int32 Mins, Hour, Dead, LocationNum;
        public Int16[] myFlags; // using char array instead of string.

        public HC_Form()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Start_Game();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        void Start_Game()
        {
            StreamReader file1;
            Int16 n = 0;
            // Init arrays
            myFlags = new Int16[17];
            LocList = new string[5];
            LocationText = new string[100, 3];

            // Read in files in to memory
            Mins = 0;
            Dead = 0;
            Hour = 3;
            LocationNum = 6;
            //Flags - 16 flags set to 0.
            for (n = 0; n <= 16; n++)
                myFlags[n] = 0;
            string LocLine;

            n = 1;
            Console.WriteLine("Reading location file");
            file1 = new StreamReader("../../HC_Location.csv");
            while (!file1.EndOfStream)
            {
                //Console.WriteLine("n = " + n.ToString());
                LocLine = file1.ReadLine();
                LocList = LocLine.Split(',');
                LocationText[n, 1] = LocList[0];
                LocationText[n, 2] = LocList[1];
                //Console.WriteLine(LocList[0] + ", " + LocList[1]);
                if (LocationText[n, 1] == "END" | LocationText[n, 1] == "")
                    break;
                n++;
            }
            file1.Close();
            //  Show first location
            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);

            //Console.WriteLine("Location = " + LocationNum);
            //Console.WriteLine("Description = " + LocationText[LocationNum, 1]);

        }

        /// <summary>
        /// Display available directions - NSEW
        /// </summary>
        /// <param name="LocText"></param>
        void ShowDirections(string LocText)
        {
            string DirectionText = "";
            LocText = LocText + "";

            if (GetDir(LocText, 1)>0) DirectionText = DirectionText + "North";
            if (GetDir(LocText, 3)>0) DirectionText = DirectionText + "South";
            if (GetDir(LocText, 5)>0) DirectionText = DirectionText + "West";
            if (GetDir(LocText, 7)>0) DirectionText = DirectionText + "East";
            Directions.Text = DirectionText;
        }

        /// <summary>
        /// Get Direction
        /// </summary>
        /// <param name="LocText"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        Int16 GetDir(string LocText, Int16 Num)
        {
            LocText = LocText + "";
            if (LocText != null & LocText.Length > 0)
                return Convert.ToInt16(LocText.Substring(Num, 2));
            else
                return 0;
        }

        /// <summary>
        /// Command processing when OK clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            Process_Command();
        } 

        /// <summary>
        /// Process user command.
        /// </summary>
        void Process_Command()
        {
            string cmd, obj;
            string[] cmdParams;

            cmdParams = new string[2];
            // Interpret user command
            txt_Additional.Text = "";
            cmd = UserCmd.Text.ToLower();
            obj = ""; // Object in command
            if (cmd.IndexOf(' ') > 0)
            {
                cmdParams = cmd.Split(' '); // Split it into seperate words
                if (cmdParams[1] != "") obj = cmdParams[1];
                cmd = cmdParams[0]; // Actual command
            }

            switch (cmd)
            {
                case "north":
                    GoNorth(); // 420
                    break;
                case "south":
                    GoSouth();  // 450
                    break;
                case "west":
                    GoWest();   // 480
                    break;
                case "east":    // 520
                    GoEast();
                    break;
                case "up":
                case "climb":   // 560
                    ClimbUp();
                    break;
                case "down":
                case "descend": // 610
                    ClimbDown();
                    break;
                case "open":
                case "unlock":
                case "enter": // 660
                    OpenItem(obj);
                    break;
                case "fix":
                case "mend":
                case "replace": // 710
                    FixItem(obj);
                    break;
                case "take":
                case "get": //740
                    TakeItem(obj);
                    break;
                case "attack":
                case "kill":
                case "destroy": // 850
                    Attack(obj);
                    break;
                case "examine":  // 900
                    ExamineItem(obj);
                    break;
                case "time": // 970
                    ShowTime();
                    break;
                case "insert":  // 990
                    InsertItem(obj);
                    break;
                case "place":
                case "put":  // 1020
                    PutItem(obj);
                    break;
                case "pay":  // 1060
                    PayForItem(obj);
                    break;
                case "give":  // 1090
                    GiveItem(obj);
                    break;
                case "smash":
                case "break":  // 1130
                    BreakItem(obj);
                    break;
                case "inventory":  // 1170
                    InventoryList();
                    break;
                default: // 360
                    txt_Additional.Text = "Pardon?";
                    break;
            } // End switch
            if (Dead > 0 | Hour >= 12)
            {
                txt_Additional.Text = "Sorry. You or dead or out of time.";
                //cmd_OK.Enabled = false; // Block further cmds
            }
        }

        /// <summary>
        /// Go north direction
        /// </summary>
        void GoNorth()
        {
            string LocText;
            LocText = LocationText[LocationNum, 1];
            if (GetDir(LocText, 1) > 0) {
                LocationNum = GetDir(LocText, 1);
                CalcTime(5);
            }
            else
                txt_Additional.Text = "This way is blocked!";
            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);
        }

        /// <summary>
        /// Calculate time upto 12 PM
        /// </summary>
        /// <param name="AddMins"></param>
        void CalcTime (Int32 AddMins)
        {
            Mins = Mins + AddMins;
            if (Mins >= 60)
            {
                Hour = Hour + 1;
                Mins = Mins - 60;
            }
            if (Hour >=12)
            {
                txt_Additional.Text = "Out of time. The evil Sauron is free and Earth is doomed. You have failed.";
                // cmd_OK.Enabled = false; // Block further commands
            }
        }

        /// <summary>
        /// Go south direction
        /// </summary>
        void GoSouth()
        {
            string LocText="";

            LocText = LocationText[LocationNum, 1];
            if (GetDir(LocText, 3) > 0)
            {
                LocationNum = GetDir(LocText, 3);
                CalcTime(5);
            }
            else if (LocationNum == 61 & TestFlag(15) == 0)
                txt_Additional.Text = "This way is blocked!";
            else if (LocationNum == 70 & TestFlag(16) == 0) 
                txt_Additional.Text = "This way is blocked!";
            else
                txt_Additional.Text = "This way is blocked!";

            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);
        }

        /// <summary>
        /// Get flag value
        /// </summary>
        /// <param name="FlagNum">Flag to get</param>
        /// <returns>Flah value</returns>
        Int16 TestFlag(Int16 FlagNum)
        {
            return myFlags[FlagNum];
        }

        /// <summary>
        /// Set a flag to 1
        /// </summary>
        /// <param name="FlagNum"></param>
        void SetFlag(Int16 FlagNum)
        {
            myFlags[FlagNum] = 1;
        }

        /// <summary>
        /// Go west direction
        /// </summary>
        void GoWest()
        {
            string LocText;

            LocText = LocationText[LocationNum, 1];
            if (GetDir(LocText, 5) > 0)
            {
                if (GetDir(LocText, 5) == 7)
                {
                    txt_Location.Text = LocationText[7, 2];
                    Dead = 1;
                    return;
                }
                else
                {
                    LocationNum = GetDir(LocText, 5);
                    CalcTime(5);
                }
            }
            else
                txt_Additional.Text = "This way is blocked!";

            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);
        }

        /// <summary>
        /// Go east direction
        /// </summary>
        void GoEast()
        {
            string LocText;

            LocText = LocationText[LocationNum, 1];
            if (GetDir(LocText, 7) == 0)
                txt_Additional.Text = "This way is blocked!";
            else if (LocationNum == 15 & TestFlag(10) == 0)
                txt_Additional.Text = "This way is blocked!";
            else if (LocationNum == 28 & TestFlag(12) == 0)
                txt_Additional.Text = "This way is blocked!";
            else if (LocationNum == 70 & TestFlag(16) == 0)
                txt_Additional.Text = "This way is blocked!";
            else if (LocationNum == 30 & TestFlag(11) == 0)
                txt_Additional.Text = "This way is blocked!";
            else {
                LocationNum = GetDir(LocText, 7);
                CalcTime(5);
            }

            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);
        }

        /// <summary>
        /// Climb up a ladder, rope, stairs etc.
        /// </summary>
        void ClimbUp()
        {
            string LocText;
            LocText = LocationText[LocationNum, 1];
            CalcTime(7);
            if ((LocationNum == 52 | LocationNum == 57) & TestFlag(9) == '1')
            {
                txt_Additional.Text = "You climb out of the quarry using the rope.";
                LocationNum = LocationNum - 1;
            }
            else if (LocationNum == 52 & TestFlag(9) == 0)
            {
                txt_Additional.Text = "You climb the ladder but it breaks and you fall to your death!";
                Dead = 1;
            }
            else if (LocationNum == 9)
            {
                txt_Additional.Text = "You climb the stairs up onto the balcony.";
                LocationNum = 4;
            }
            else
                txt_Additional.Text = "There is nothing to climb here!";

            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);

        }

        /// <summary>
        /// Climb down a ladder, rope, stairs etc.
        /// </summary>
        void ClimbDown()
        {
            string LocText;

            LocText = LocationText[LocationNum, 1];
            CalcTime(7);
            if (LocationNum == 4) {
                txt_Additional.Text = "You descend to the ground floor.";
                LocationNum = 9;
            }
            else if ((LocationNum == 51 | LocationNum == 56) & TestFlag(9) == 1) {
                txt_Additional.Text = "You climb into the quarry using the rope!";
                LocationNum = LocationNum + 1;
            }
            else if (LocationNum == 51 & TestFlag(9) == 0) {
                txt_Additional.Text = "You climb down the ladder but it breaks & you fall to your death!";
                Dead = 1;
            }
            else
                txt_Additional.Text = "You cannot do down here!";

            txt_Location.Text = LocationText[LocationNum, 2];
            ShowDirections(LocationText[LocationNum, 1]);
        }

        /// <summary>
        /// Open doors, safe, gate etc.
        /// </summary>
        /// <param name="obj"></param>
        void OpenItem(string obj)
        {
            CalcTime(2);
            if (LocationNum == 2 & TestFlag(1) == 1 & (obj == "door" | obj == "house"))
            {
                txt_Additional.Text = "The door opens with a creak.";
                SetFlag(10);
            }
            else if (LocationNum == 20 & obj == "safe")
            {
                txt_Additional.Text = "You twiddle with it but the alarm goes off, and the owner catches you. Game over";
                Dead = 1;
            }
            else if (LocationNum == 28 & TestFlag(3) == 1)
            {
                txt_Additional.Text = "You insert the card the the gate opens.";
                SetFlag(12);
            }
            else
                txt_Additional.Text = "What is there to open?";        
        }

        /// <summary>
        /// Fix a lorry etc.
        /// </summary>
        /// <param name="obj">Item to fix</param>
        void FixItem(string obj)
        {
            if (LocationNum == 39 & TestFlag(6) == 1 & obj == "tyre") {
                txt_Additional.Text = "You jack up the lorry & fix it, the driver awards you with money.";
                SetFlag(5);
            }
            else
                txt_Additional.Text = "There is nothing to fix.";
        }


        /// <summary>
        /// Pick up items in given location
        /// </summary>
        /// <param name="obj">Item to take</param>
        void TakeItem(string obj)
        {
            if (LocationNum == 1 & obj == "key") {
                SetFlag(1);
                txt_Additional.Text = "You put the key in your pocket.";
                CalcTime(1);
            }
            else if (LocationNum == 8 & obj == "jack") {
                SetFlag(6);
                txt_Additional.Text = "You pick up the jack.";
                CalcTime(1);
            }
            else if (LocationNum == 10 & obj == "mirror") {
                SetFlag(2);
                txt_Additional.Text = "You put the mirror in your backpack.";
                CalcTime(1);
            }
            else if (LocationNum == 16 & obj == "card") {
                SetFlag(3);
                txt_Additional.Text = "You put the card in your pocket.";
                CalcTime(1);
            }
            else if (LocationNum == 17 & obj == "meat") {
                SetFlag(4);
                txt_Additional.Text = "You put the meat in your backpack.";
                CalcTime(1);
            }
            else if (LocationNum == 31 & obj == "axe" & TestFlag(13) > 0) {
                SetFlag(7);
                txt_Additional.Text = "You put the axe in your backpack.";
                CalcTime(1);
            }
            else if (LocationNum == 35 & obj == "crystal" & TestFlag(14) > 0) {
                SetFlag(8);
                txt_Additional.Text = "You put the crystal in your backpack.";
                CalcTime(1);
            }
            else if (LocationNum == 49 & obj == "rope") {
                SetFlag(9);
                txt_Additional.Text = "You put the rope in your backpack.";
                CalcTime(1);
            }
            else if (LocationNum == 49 & obj == "money") {
                txt_Additional.Text = "You take the money but its owner comes in and bashes you. Game over";
                Dead = 1;
            }
            else
                txt_Additional.Text = "That can't be taken!";
        }

        /// <summary>
        /// Attack dogs, druids, etc
        /// </summary>
        /// <param name="obj">Thing to attack</param>
        void Attack(string obj)
        {
            CalcTime(3);
            if (LocationNum == 30 & obj == "dogs") {
                txt_Additional.Text = "You attack them but they overwhelm you and & kill you.";
                Dead = 1;
            }
            else if (LocationNum == 70 & obj == "druid" & TestFlag(2) == 0) {
                txt_Additional.Text = "You attack him but he is quicker and stabs you. Game over";
                Dead = 1;
            }
            else if (LocationNum == 70 & obj == "druid" & TestFlag(2) > 0) {
                txt_Additional.Text = "You attack him but he casts a spell but it bounces off your mirror, killing him!";
                SetFlag(16);
            }
            else
                txt_Additional.Text = "You can't kill that, silly!";
        }

        /// <summary>
        /// Examine or look at an item
        /// </summary>
        /// <param name="obj">Item to examine</param>
        void ExamineItem(string obj)
        {
            CalcTime(1);
            if (LocationNum == 1 & obj == "desk")
                txt_Additional.Text = "Amongst the papers and drawings you find a key.";
            else if (LocationNum == 8 & (obj == "car" | obj == "boot"))
                txt_Additional.Text = "Inside the boot you find a spare type, a metal box & a jack.";
            else if (LocationNum == 16 & obj == "coat")
                txt_Additional.Text = "Inside a pocket you find an ID card.";
            else if (LocationNum == 20 & obj == "picture")
                txt_Additional.Text = "Behind the picture you find a safe.";
            else if ((LocationNum == 51 | LocationNum == 52) & obj == "ladder")
                txt_Additional.Text = "The ladder is wet and old.";
            else
                txt_Additional.Text = "You see a " + obj;
        }

        /// <summary>
        /// Display time on watch
        /// </summary>
        void ShowTime()
        {
            txt_Additional.Text = "On your watch it says " + String.Format("##", Hour) + ":" + String.Format("##", Mins);
        }

        /// <summary>
        /// Insert a card
        /// </summary>
        /// <param name="obj"></param>
        void InsertItem(string obj)
        {
            CalcTime(1);
            if (LocationNum == 28 & obj == "card" & TestFlag(3) > 0 )
            {
                txt_Additional.Text = "The gates swing open automatically.";
                SetFlag(12);
            }
            else
                txt_Additional.Text = "Insert what?";
        }

        /// <summary>
        /// Put items somewhere
        /// </summary>
        /// <param name="obj">Item to put</param>
        void PutItem(string obj)
        {
            CalcTime(1);
            if (LocationNum == 73 & obj == "crystal" & TestFlag(8) > 0)
            {
                txt_Additional.Text = "*** Congratulations ***. The evil Sauron has " +
                "fled and Halley's Comet passes gloriously overhead. Game over.";
                Hour = 12;
            }
            else
                txt_Additional.Text = "You can't put that here!";
        }

        /// <summary>
        /// Pay for something e.g. toll
        /// </summary>
        /// <param name="obj"></param>
        void PayForItem(string obj)
        {
            CalcTime(1);
            if (LocationNum == 61 & obj == "toll" & TestFlag(5) > 0)
            {
                txt_Additional.Text = "You give money to the man. 'You may cross, Sir.'";
                SetFlag(15);
            }
            else
                txt_Additional.Text = "You have nothing to give.";        
        }

        /// <summary>
        /// Give item e.g. meat, money
        /// </summary>
        /// <param name="obj">Item to give</param>
        void GiveItem(string obj)
        {
            CalcTime(1);
            if (LocationNum == 30 & obj == "meat" & TestFlag(4) > 0)
            {
                txt_Additional.Text = "The dogs happily chew at it, totally ignoring you!";
                SetFlag(11);
            }
            else if (LocationNum == 61 & obj == "money" & TestFlag(5) > 0)
            {
                txt_Additional.Text = "The man accepts the money & disappears.";
                SetFlag(15);
            }
            else
                txt_Additional.Text = "Give what, silly?";
        }

        /// <summary>
        /// Break an item e.g glass
        /// </summary>
        /// <param name="obj">Item to break</param>
        void BreakItem(string obj)
        {
            CalcTime(3);
            if (LocationNum == 31 & obj == "glass")
            {
                txt_Additional.Text = "You break the glass with a nearby chair.";
                SetFlag(14);
            }
            else
                txt_Additional.Text = "It doesn't break.";
        }

        /// <summary>
        /// List items you are carrying
        /// </summary>
        void InventoryList()
        {
            string[] items =  { "key", "mirror", "card", "meat", "money", "jack", "axe", "crystal", "rope"};
            string itemList="";
            Int16 n, c=0;

            for (n=1; n<=9; n++)
            {
                if (TestFlag(n) > 0)
                {
                    itemList = itemList + items[n - 1] + " ";
                    c++;
                }                
            }
            if (c == 0) itemList = "Nothing";
            txt_Additional.Text = itemList;
            CalcTime(3);
        }
    } // end class
} // end namespace
