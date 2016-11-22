using System;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DownloadImagesTest()
        {

            ScrapingBrowser Browser = new ScrapingBrowser();

            Uri uri = new Uri("http://pokemon.wikia.com/wiki/Category:Generation_I_Pok%C3%A9mon");

            WebPage PageResult = Browser.NavigateToPage(uri);

            HtmlNodeCollection pokeNodes = PageResult.Html.SelectNodes("//div[@id='gallery-0']//div[@class='wikia-gallery-item']");

            //venonat, venomoth, nidoran m/f, farfetch'd
            foreach (var poke in pokeNodes)
            {
                HtmlNode nameNode = poke.SelectNodes("div[@class='thumb']/div").First();
                Console.WriteLine(nameNode.Id);

            }


        }


        [TestMethod]
        public void DownloadFileTest()
        {

            WebClient webClient = new WebClient();
            string url =
                "http://vignette3.wikia.nocookie.net/pokemon/images/d/d3/049Venomoth.png/revision/latest/scale-to-width-down/200?cb=20140328194046";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(baseDirectory);
            DirectoryInfo parentDir = fileInfo.Directory.Parent;
            string parentDirName = parentDir.FullName;
            string fileName = parentDirName + @"\Downloads\Venomoth.png";
            webClient.DownloadFile(url, fileName);

        }


        [TestMethod]
        public void GetHtmlTest()
        {

            using (WebBrowser browser = new WebBrowser())
            {
                
                Uri uri = new Uri("http://www.pokemon.com/us/pokedex/");
                browser.Navigate(uri);
                browser.DocumentCompleted += browser_DocumentCompleted;


            }
        }

        [TestMethod]
        public void ResizeImagesTest()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(baseDirectory);
            DirectoryInfo parentDir = fileInfo.Directory.Parent;
            string filePath = parentDir.FullName + @"\Downloads\";

            foreach (var file in Directory.EnumerateFiles(filePath))
            {
                using (var img = Image.FromFile(file))
                {
                    Image thumbnail = img.GetThumbnailImage(128, 128, () => false, IntPtr.Zero);
                    string saveFilePath = filePath + @"y.png";
                    thumbnail.Save(saveFilePath, ImageFormat.Png);
                }
            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine("document finished loading");
     
        }
    }
}
