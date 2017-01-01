using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ACC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type your Adidas website country code:");
            string code = Console.ReadLine().ToUpper();
            string address = Helpers.getAddress(code);

            if(address == null)
            {
                Helpers.quit(String.Format("Could not find Adidas website for country code '{0}'", code));
            }

            Console.WriteLine("Searching captchas products on {0}", address);

            string[] populars = Helpers.getPopular(code);

            foreach(string popular in populars)
            {
                string newaddress = String.Format("http://www.{0}/{1}", address, popular);

                //using (WebClient client = new WebClient())
                //{
                    //try
                    //{
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Searching for captchas : {0}", newaddress);
                        Console.ResetColor();

                        //client.Headers.Add("user-agent", Helpers.getUserAgent());
                        
                        //string source_string = client.DownloadString(newaddress);
                        string source_string = Helpers.getSourceCode(newaddress);

                        if (source_string == null)
                            Helpers.quit();

                        string[] source = source_string.Split('\n');
                        List<string> products = Helpers.getProducts(source);

                        Console.ForegroundColor = ConsoleColor.White;
                        foreach(string product in products)
                        {
                            string sitekey = Helpers.getSitekey(String.Format("http://www.{0}/{1}.html", address, product));

                            if (sitekey != null)
                            {
                                string file = String.Format("{0}\\sitekey.txt",AppDomain.CurrentDomain.BaseDirectory);
                                System.IO.File.WriteAllText(file, sitekey);
                                Console.WriteLine("http://www.{0}/{1}.html has captcha, check sitekey.txt : {2}", address, product, sitekey);
                            }
                        }
                        Console.ResetColor();

                        
                   // }
                    /*catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.ProtocolError) 
                            Helpers.quit(String.Format("Coud not reach {0} : {1} / Code: {2} / Description : {3}", newaddress, e.Message, ((HttpWebResponse)e.Response).StatusCode, ((HttpWebResponse)e.Response).StatusDescription));
                        else
                            Helpers.quit(String.Format("Coud not reach {0} - {1}:{2} ", newaddress, e.Status.ToString(), e.Message));

                    }

                }*/
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Helpers.quit("Done.");
        }
        
    }
}
