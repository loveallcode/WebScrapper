using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
//using System.Net;

 
namespace WebScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //string urlAddress = "http://google.com";
            string urlAddress = "http://AdvantageAvenue.com";

            var htmlcode1 = GetHTMLfromURLmeth1(urlAddress);
            var htmlcode2 = GetHTMLfromURLmeth2(urlAddress);

            System.Console.WriteLine(htmlcode1);
            System.Console.ReadKey();
            System.Console.WriteLine(htmlcode2);
            System.Console.ReadKey();
        }

        public static string GetHTMLfromURLmeth1(string urlAdd)
        {
            string htmlCode = string.Empty;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(urlAdd);
            }
            return htmlCode;
        }

        public static string GetHTMLfromURLmeth2(string urlAdd)
        {
            string htmlCode = string.Empty;
            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAdd);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //The remote server returned an error: (407) Proxy Authentication Required.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                htmlCode = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            return htmlCode;

            //this code may need the following in the App.config
            /*
               <system.net>
                  <defaultProxy useDefaultCredentials="true" >
                  </defaultProxy>
               </system.net>
            */
        }

        //Code to read HTML below ...
        /// Look into AngleSharp
        /// Look into HTMLAgilityPack.
        /// You can also look into using Fizzler or CSQuery depending on your needs for selecting the elements from the retrieved page.
        /// Using LINQ or Regular Expressions is just too error prone, especially when the HTML is malformed, missing closing tags, have nested child elements etc.
        ///FizzlerEx is a JQuery/CSS3-selectors implementation for .NET, based on HtmlAgilityPack and the original Fizzler project.

    }
}