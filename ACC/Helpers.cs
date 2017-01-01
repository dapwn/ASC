using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ACC
{
    class Helpers
    {
        public static string getAddress(string country)
        {
            Dictionary<string, string> marketDomainList = new Dictionary<string, string>();
            marketDomainList["AT"]="adidas.at";
            marketDomainList["AU"]="adidas.com.au";
            marketDomainList["BE"]="adidas.be";
            marketDomainList["BR"]="adidas.com.br";
            marketDomainList["CA"]="adidas.ca/en";
            marketDomainList["CF"]="adidas.ca";
            marketDomainList["CH"]="adidas.ch";
            marketDomainList["CL"]="adidas.cl";
            marketDomainList["CN"]="adidas.cn";
            marketDomainList["CO"]="adidas.co";
            marketDomainList["CZ"]="adidas.cz";
            marketDomainList["DE"]="adidas.de";
            marketDomainList["DK"]="adidas.dk";
            marketDomainList["EE"]="baltics.adidas.com";
            marketDomainList["ES"]="adidas.es";
            marketDomainList["FI"]="adidas.fi";
            marketDomainList["FR"]="adidas.fr";
            marketDomainList["GB"]="adidas.co.uk";
            marketDomainList["GR"]="adidas.gr";
            marketDomainList["HK"]="adidas.com.hk";
            marketDomainList["HU"]="adidas.hu";
            marketDomainList["IE"]="adidas.ie";
            marketDomainList["ID"]="adidas.co.id";
            marketDomainList["IN"]="adidas.co.in";
            marketDomainList["IT"]="adidas.it";
            marketDomainList["JP"]="japan.adidas.com";
            marketDomainList["KR"]="adidas.co.kr";
            marketDomainList["KW"]="mena.adidas.com";
            marketDomainList["MX"]="adidas.mx";
            marketDomainList["MY"]="adidas.com.my";
            marketDomainList["NG"]="global.adidas.com";
            marketDomainList["NL"]="adidas.nl";
            marketDomainList["NO"]="adidas.no";
            marketDomainList["NZ"]="adidas.co.nz";
            marketDomainList["OM"]="adidas.com.om";
            marketDomainList["PE"]="adidas.pe";
            marketDomainList["PH"]="adidas.com.ph";
            marketDomainList["PL"]="adidas.pl";
            marketDomainList["PT"]="adidas.pt";
            marketDomainList["QA"]="adidas.com.qa";
            marketDomainList["RU"]="adidas.ru";
            marketDomainList["SE"]="adidas.se";
            marketDomainList["SG"]="adidas.com.sg";
            marketDomainList["SK"]="adidas.sk";
            marketDomainList["TH"]="adidas.co.th";
            marketDomainList["TR"]="adidas.com.tr";
            marketDomainList["TW"]="adidas.com.tw";
            marketDomainList["US"]="adidas.com/us";
            marketDomainList["VE"]="latin-america.adidas.com";
            marketDomainList["VN"]="adidas.com.vn";
            marketDomainList["ZA"]="adidas.co.za";

            string wdgaf;

            if (marketDomainList.TryGetValue(country, out wdgaf))
                return marketDomainList[country];
            else
                return null;
        }

        public static string[] getPopular(string country)
        {
            string[] popular;

            if (country.ToUpper() == "US")
                popular = new string[] { "nmd", "ultraboost", "eqt?grid=true" };
            else if(country.ToUpper() == "CA")
                popular = new string[] { /*"nmd",*/ "ultra_boost", "equipment" };
            else
                popular = new string[] { "nmd", "ultra_boost", "equipment" };

            return popular;
        }
        
        public static string getUserAgent()
        {
            string[] useragents = new string[]{
            "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-us; Silk/1.0.141.16-Gen4_11004310) AppleWebkit/533.16 (KHTML, like Gecko) Version/5.0 Safari/533.16 Silk-Accelerated=true",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.111 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; tr-TR) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27",
            "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 7.0; InfoPath.3; .NET CLR 3.1.40767; Trident/6.0; en-IN)",
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.93 Safari/537.36"};

            Random randomUA = new Random();
            return useragents[randomUA.Next(useragents.Length)];
        }

        public static string getSourceCode(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.UserAgent = getUserAgent();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string data = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            return data;
        }

        public static string getpid(string[] split)
        {
            foreach(string s in split)
            {
                if (s.Contains("-buttons"))
                {
                    string[] spid = s.Split('-');
                    return spid[0];
                }
            }

            return null;
        }
        public static List<string> getProducts(string[] source = null)
        {
            if (source.Length < 1)
                quit("Error while reading source code");

            List<string> Products = new List<string>();

            for(int i = 0; i < source.Length;i++)
            {
                if (source[i].Contains("status:IN STOCK"))
                {
                    string pid = getpid(source[i+1].Split('"'));
                    Products.Add(pid);
                }
            }

            return Products;
        }
        public static string getSitekey(string newaddress)
        {
            /*using (WebClient client = new WebClient())
            {
                try
                {*/
                    //Console.WriteLine("Parsing {0}", newaddress);

                    string source_string = getSourceCode(newaddress);
                    string[] source = source_string.Split('\n');

                    for(int i = 0;i < source.Length;i++)
                    {
                        if(source[i].Contains("data-sitekey="))
                        {

                            string[] s1 = source[i].Split('"');
                            return s1[3];
                        }
                    }

                /*}
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.ProtocolError)
                        Helpers.quit(String.Format("Coud not reach {0} : {1} / Code: {2} / Description : {3}", newaddress, e.Message, ((HttpWebResponse)e.Response).StatusCode, ((HttpWebResponse)e.Response).StatusDescription));
                    else
                        Helpers.quit(String.Format("Coud not reach {0} - {1}:{2} ", newaddress, e.Status.ToString(), e.Message));

                    return null;
                }
            }*/

            return null;
        }
        public static void quit(string message=null)
        {
            if (message != null)
                Console.WriteLine(message);

            Console.ReadLine();
            System.Environment.Exit(0);
        }
    }
}
