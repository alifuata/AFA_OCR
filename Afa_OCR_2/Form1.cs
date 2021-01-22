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
        public Form1(string textToTranslate, string textLanguage, string targetLanguage, string translater)
        {
            InitializeComponent();
            textToTra = textToTranslate;
            tarLanguage = targetLanguage;
            textLang = textLanguage;
            this.translater = translater;

        }
        Dictionary<string, string> languages = new Dictionary<string, string>() { { "eng", "en," }, { "tur", "tr" }, { "fra", "fr" }, { "deu", "de" }, { "spa", "es" } };
        private string tarLanguage;
        private string textToTra;
        private string textLang;
        private string translater;
        private void Form1_Load(object sender, EventArgs e)
        {
            string modifiedText = textToTra.Replace(" ", "%20").Replace('&', ' ');
            string addressG = "https://translate.google.com/?sl=" + languages[textLang] + "&tl=" + languages[tarLanguage] + "&text=" + modifiedText + "&op=translate";
            string addressY = "https://ceviri.yandex.com.tr/?text=" + modifiedText + "&lang=" + languages[textLang] + "-" + languages[tarLanguage];
            string address = translater == "Google" ? addressG : addressY;
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
                dynamic node = htmldoc.getElementById("header") as dynamic;
                dynamic node1 = htmldoc.getElementById("dropOverlay") as dynamic;
                node.parentNode.removeChild(node);
                node1.parentNode.removeChild(node1);
            }
            catch (Exception)
            {

            }

        }
    }
}
