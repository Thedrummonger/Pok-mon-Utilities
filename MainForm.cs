using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IronOcr;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pokemon_Utils
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static Screen CaptureDisplay;
        const int ENUM_CURRENT_SETTINGS = -1;
        public MainForm CurrentForm;
        public bool ScanningScreen = false;
        public Regex trimmer = new Regex(@"\s\s+");

        #region FormControls
        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentForm = this;
            int Screencount = Screen.AllScreens.Count();
            NUDDisplay.Value = 1;
            NUDDisplay.Minimum = 1;
            NUDDisplay.Maximum = Screencount;

            label2.Text = "Current Display: " + Screen.AllScreens[0].DeviceName;

            NUDTolerance.Value = 0.8M;
            NUDTolerance.Minimum = 0.1M;
            NUDTolerance.Maximum = 1;
            NUDTolerance.Increment = 0.1M;
            NUDTolerance.DecimalPlaces = 1;
        }

        private void PreviewButtonClick(object sender, EventArgs e)
        {
            var image = Screenshot(Screen.AllScreens[(int)NUDDisplay.Value - 1]);

            Form ImageDisplay = new Form();
            ImageDisplay.Width = 960;
            ImageDisplay.Height = 540;
            ImageDisplay.BackgroundImage = image;
            ImageDisplay.BackgroundImageLayout = ImageLayout.Stretch;
            ImageDisplay.Show();

        }

        private void ChangeDisplay(object sender, EventArgs e)
        {
            label2.Text = "Current Display: " + Screen.AllScreens[(int)NUDDisplay.Value - 1].DeviceName;
        }

        private void ScanButtonClick(object sender, EventArgs e)
        {
            if (ScanningScreen)
            {
                Console.WriteLine("Scan already in process");
                return;
            }
            ScanningScreen = true;
            BTNScan.Text = "Scanning...";
            Font defaultFont = BTNScan.Font;
            BTNScan.Font = new Font(defaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);
            BTNScan.Enabled = false;
            Console.WriteLine("Scan Start");
            DoAsyncOCR();
        }
        public async void DoAsyncOCR()
        {
            var image = Screenshot(Screen.AllScreens[(int)NUDDisplay.Value - 1]);
            OcrResult Result = await Task.Run(() => new IronTesseract().Read(image));

            var s = trimmer.Replace(Result.Text, " ");
            s = s.Replace(".", "");
            string[] words = s.Split(' ');

            Console.WriteLine("Comparing Pokemon");
            List<string> FoundPKMN = new List<string>();
            foreach (var WordFromScreen in words.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                foreach (var PokemonName in PKMN.Names)
                {
                    double Simularity = CalculateSimilarity(WordFromScreen, PokemonName);
                    if (Simularity >= (double)NUDTolerance.Value && !FoundPKMN.Contains(PokemonName)) { FoundPKMN.Add(PokemonName); }
                }
            }
            FoundPKMN.Sort();
            FoundPKMN.Reverse();
            PrintResults(FoundPKMN);
        }

        public void PrintResults(List<string> FoundPKMN)
        {
            Console.WriteLine("Done");
            foreach (var i in FoundPKMN)
            {
                LBResults.Items.Insert(0, i);
            }
            BTNScan.Text = "Scan For Pokemon Data";
            Font defaultFont = BTNScan.Font;
            BTNScan.Font = new Font(defaultFont.FontFamily, DefaultFont.Size, FontStyle.Regular);
            BTNScan.Enabled = true;
            ScanningScreen = false;

        } 

        private void ResultsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (LBResults.SelectedIndex == -1) { return; }
            var pokemon = LBResults.SelectedItem.ToString();
            System.Diagnostics.Process.Start("https://bulbapedia.bulbagarden.net/wiki/" + pokemon + "#Type_effectiveness");
        }

        #endregion FormControls

        //====================================================================================================================

        #region Dekstop Screen Capture

        public static Bitmap Screenshot(Screen screen)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

            Bitmap screenshot;

            using (Bitmap bmp = new Bitmap(dm.dmPelsWidth, dm.dmPelsHeight))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(dm.dmPositionX, dm.dmPositionY, 0, 0, bmp.Size);
                screenshot = new Bitmap(bmp, 1024, 768);
                //bmp.Save(screen.DeviceName.Split('\\').Last() + ".png");
            }


            return screenshot;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        #endregion Dekstop Screen Capture

        //====================================================================================================================

        #region Text Similarity Testing

        public static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = LevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        public static int LevenshteinDistance(string source, string target)
        {
            // degenerate cases
            if (source == target) return 0;
            if (source.Length == 0) return target.Length;
            if (target.Length == 0) return source.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < source.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < target.Length; j++)
                {
                    var cost = (source[i] == target[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[target.Length];
        }

        #endregion Text Similarity Testing
    }
}
