using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;

namespace html2xml.Controllers
{
    public class HomeController : Controller
    {
        private HttpWebRequest httpWebRequest;
        private HttpWebResponse httpWebResponse;
        public ActionResult Index()
        {
            string title = "";
            for (int num = 1; num <= 64; num++)
            {
                string strURL;
                strURL = "http://lvyou.baidu.com/shanghai/jingdian/#" + num;
                Uri uri = new Uri(strURL);
                WebRequest webRequest = WebRequest.Create(uri);
                httpWebRequest = (HttpWebRequest)webRequest;
                // 发送请求信息
                WebResponse webResponse = httpWebRequest.GetResponse(); // 获得响应信息
                httpWebResponse = (HttpWebResponse)webResponse;
                // 获得从当前Internet资源返回的响应流数据
                Stream stream = httpWebResponse.GetResponseStream();
                // 利用获得的响应流和系统缺省编码来初始化StreamReader实例。
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                string strResult = sr.ReadToEnd();  //从响应流从读取数据
                sr.Close();
                Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
                NodeFilter filterDiv = new AndFilter(new TagNameFilter("a"), new HasAttributeFilter("class", "nslog"));
                NodeList nodeList = parser.Parse(filterDiv);

                if (nodeList.Count > 0)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        ITag tempTag = nodeList[i] as ITag;
                        if (tempTag.Attributes["TITLE"] != null)
                        {
                            title += tempTag.Attributes["TITLE"].ToString() + ";";
                        }
                    }
                }
            }
            ViewBag.Message = title;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}