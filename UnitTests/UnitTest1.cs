using System;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DownloadImagesTest()
        {
            ScrapingBrowser Browser = new ScrapingBrowser
            {
                AllowAutoRedirect = true,
                AllowMetaRedirect = true
            };
            int x = 0;
            WebPage PageResult = Browser.NavigateToPage(new Uri("http://www.pokemon.com/us/pokedex/"));
            //HtmlNode ul = PageResult.Html.CssSelect("ul.results").First();
            //IEnumerable<HtmlNode> li = ul.Descendants();
            HtmlNode section = PageResult.Html.CssSelect("section.pokedex-results").First();
            foreach (var li in section.SelectNodes("ul"))
            {
                x++;
            }
        }
    }
}
