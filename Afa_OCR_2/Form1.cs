using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afa_OCR_2
{
    public partial class Form1 : Form
    {
        public Form1(string textToTranslate, string textLanguage, string targetLanguage)
        {
            InitializeComponent();
            textToTra = textToTranslate;
            tarLanguage = targetLanguage;
            textLang = textLanguage;
        }
        Dictionary<string, string> languages = new Dictionary<string, string>() { { "eng", "en," }, { "tur", "tr" }, { "fra", "fr" }, { "deu", "de" }, { "spa", "es" } };
        private string tarLanguage;
        private string textToTra;
        private string textLang;
        private void Form1_Load(object sender, EventArgs e)
        {
            string modifiedText = textToTra.Replace(" ", "%20").Replace('&',' ');
            string address = "https://translate.google.com/?sl=" + languages[textLang] + "&tl=" + languages[tarLanguage] + "&text=" + modifiedText + "&op=translate";
            //webBrowser1.Navigate("https://translate.google.com/?hl=tr&sl=en&tl=tr&text=kale&op=translate");
            webBrowser1.Navigate(address);
            webBrowser1.ScriptErrorsSuppressed = true;
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                dynamic htmldoc = webBrowser1.Document.DomDocument as dynamic;
                dynamic node = htmldoc.getElementById("gb") as dynamic;
                node.parentNode.removeChild(node);
            }
            catch (Exception)
            {

            }

        }
    }
}
