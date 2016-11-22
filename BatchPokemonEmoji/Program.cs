using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Network;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace BatchPokemonEmoji
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filePath = DownloadImages();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(baseDirectory);
            DirectoryInfo parentDir = fileInfo.Directory.Parent.Parent;
            string filePath = parentDir.FullName + @"\Downloads\";

            ResizeImages(filePath);
            UploadImages();
        }

        static string DownloadImages()
        {
            ScrapingBrowser Browser = new ScrapingBrowser();
            WebClient webClient = new WebClient();

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileInfo fileInfo = new FileInfo(baseDirectory);
            DirectoryInfo parentDir = fileInfo.Directory.Parent.Parent;
            string parentDirName = parentDir.FullName;

            Uri uri = new Uri("http://pokemon.wikia.com/wiki/Category:Generation_I_Pok%C3%A9mon");

            WebPage PageResult = Browser.NavigateToPage(uri);

            HtmlNodeCollection pokeNodes = PageResult.Html.SelectNodes("//div[@id='gallery-0']//div[@class='wikia-gallery-item']");

            //venonat, venomoth, nidoran m/f, farfetch'd
            foreach (var poke in pokeNodes)
            {
                HtmlNode nameNode = poke.SelectNodes("div[@class='thumb']/div").First();
                string pokeName = nameNode.Id;
                HtmlNode imageNode = nameNode.SelectNodes("a/img").First();
                HtmlAttribute attribute = imageNode.Attributes.First(attr => attr.Name == "data-src");

                string dataUrl = attribute.Value;
                string fileName = parentDirName + @"\Downloads\" + pokeName + @".png";
                webClient.DownloadFile(dataUrl, fileName);
            }

            return parentDirName + @"\Downloads\";
        }

        static void ResizeImages(string parentPath)
        {
            //foreach (var file in Directory.EnumerateFiles(parentPath))
            //{
            //    using (var img = Image.FromFile(file))
            //    {
            //        Image thumbnail = img.GetThumbnailImage(128, 128, () => false, IntPtr.Zero);
            //        File.Delete(file);
            //        thumbnail.Save(file, ImageFormat.Png);
            //    }
            //}
        }

        static void UploadImages()
        {

        }
    }
}
