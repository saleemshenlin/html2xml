using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;

namespace html2xml.Controllers
{
    public class HomeController : Controller
    {
        private HttpWebRequest httpWebRequest;
        private HttpWebResponse httpWebResponse;
        private XmlDocument xmlDoc;
        private XmlNode xmlNode;
        private XmlElement xmlElement;

        public ActionResult Index()
        {
            string title = "";
            List<string> secnicNameList = new List<string>();
            List<string> secnicLocList = new List<string>();
            for (int num = 71; num <= 81; num++)
            {
                string strTongCheng;
                strTongCheng = "http://go.ly.com/gonglve/NewDestinationCommon/DestinationSceneryListByAllTag?provinceid=&cityid=3100&countyid=&tagidlist=&pageindex=" + num + "&pagesize=24&sorttype=0&datatype=1";
                Uri uri = new Uri(strTongCheng);
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
                //通过Winista.HtmlParser解析Html

                Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
                NodeFilter filterDiv = new AndFilter(new TagNameFilter("div"), new HasAttributeFilter("class", "tuijianImg"));
                NodeList nodeList = parser.Parse(filterDiv);

                if (nodeList.Count > 0)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        ITag tempTag = nodeList[i] as ITag;
                        secnicNameList.Add(tempTag.Children[4].Children[0].GetText());
                        title += tempTag.Children[4].Children[0].GetText() + ";";
                    }
                }

                //通过newtonsoft.json解析Json
                //JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
                //IList<string> secnicNames = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("sname")).ToList();
                //IList<string> SecnicLocs = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("ext.map_info")).ToList();
                //for (int i = 0; i < secnicNames.Count; i++)
                //{
                //    secnicNameList.Add(secnicNames[i]);
                //    secnicLocList.Add(SecnicLocs[i]);
                //    title += secnicNames[i] + " ; ";
                //}

            }
            //保存XML
            xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < secnicNameList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = secnicNameList[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                //string strLng = secnicLocList[i].Split(',')[0];
                //string strLat = secnicLocList[i].Split(',')[1];
                //XmlElement xeLng = xmlDoc.CreateElement("lng");
                //xeLng.InnerText = strLng;//设置文本节点
                //xeItem.AppendChild(xeLng);//添加到<item>节点中
                //XmlElement xeLat = xmlDoc.CreateElement("lat");
                //xeLat.InnerText = strLat;//设置文本节点
                //xeItem.AppendChild(xeLat);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(Server.MapPath("data1.xml"));

            ViewBag.Message = title;

            return View();
        }

        public ActionResult Baidu()
        {
            string title = "";
            List<string> secnicNameList = new List<string>();
            List<string> secnicLocList = new List<string>();
            for (int num = 1; num <= 64; num++)
            {
                string strBaidu;
                strBaidu = "http://lvyou.baidu.com/destination/ajax/jingdian?format=ajax&surl=shanghai&cid=0&pn=" + num + "&t=1413166934484";
                Uri uri = new Uri(strBaidu);
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
                //通过Winista.HtmlParser解析Html

                //Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
                //NodeFilter filterDiv = new AndFilter(new TagNameFilter("a"), new HasAttributeFilter("class", "nslog"));
                //NodeList nodeList = parser.Parse(filterDiv);

                //if (nodeList.Count > 0)
                //{
                //    for (int i = 0; i < nodeList.Count; i++)
                //    {
                //        ITag tempTag = nodeList[i] as ITag;
                //        if (tempTag.Attributes["TITLE"] != null)
                //        {
                //            title += tempTag.Attributes["TITLE"].ToString() + ";";
                //        }
                //    }
                //}

                //通过newtonsoft.json解析Json
                JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
                IList<string> secnicNames = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("sname")).ToList();
                IList<string> SecnicLocs = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("ext.map_info")).ToList();
                for (int i = 0; i < secnicNames.Count; i++)
                {
                    secnicNameList.Add(secnicNames[i]);
                    secnicLocList.Add(SecnicLocs[i]);
                    title += secnicNames[i] + " ; ";
                }

            }
            //保存XML
            xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < secnicNameList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = secnicNameList[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                string strLng = secnicLocList[i].Split(',')[0];
                string strLat = secnicLocList[i].Split(',')[1];
                XmlElement xeLng = xmlDoc.CreateElement("lng");
                xeLng.InnerText = strLng;//设置文本节点
                xeItem.AppendChild(xeLng);//添加到<item>节点中
                XmlElement xeLat = xmlDoc.CreateElement("lat");
                xeLat.InnerText = strLat;//设置文本节点
                xeItem.AppendChild(xeLat);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(Server.MapPath("data.xml"));

            ViewBag.Message = title;

            return View();
        }

        public ActionResult TongCheng()
        {
            string title = "";
            List<string> secnicNameList = new List<string>();
            List<string> secnicLocList = new List<string>();
            for (int num = 1; num <= 20; num++)
            {
                string strTongCheng;
                strTongCheng = "http://go.ly.com/gonglve/NewDestinationCommon/DestinationSceneryListByAllTag?provinceid=&cityid=3100&countyid=&tagidlist=&pageindex=" + num + "&pagesize=24&sorttype=0&datatype=1";
                Uri uri = new Uri(strTongCheng);
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
                //通过Winista.HtmlParser解析Html

                Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
                NodeFilter filterDiv = new AndFilter(new TagNameFilter("div"), new HasAttributeFilter("class", "tuijianImg"));
                NodeList nodeList = parser.Parse(filterDiv);

                if (nodeList.Count > 0)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        ITag tempTag = nodeList[i] as ITag;
                        secnicNameList.Add(tempTag.Children[4].Children[0].GetText());
                        title += tempTag.Children[4].Children[0].GetText() + ";";
                    }
                }

                //通过newtonsoft.json解析Json
                //JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
                //IList<string> secnicNames = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("sname")).ToList();
                //IList<string> SecnicLocs = jo["data"]["scene_list"].Select(m => (string)m.SelectToken("ext.map_info")).ToList();
                //for (int i = 0; i < secnicNames.Count; i++)
                //{
                //    secnicNameList.Add(secnicNames[i]);
                //    secnicLocList.Add(SecnicLocs[i]);
                //    title += secnicNames[i] + " ; ";
                //}

            }
            //保存XML
            xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i=0; i < secnicNameList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = secnicNameList[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                //string strLng = secnicLocList[i].Split(',')[0];
                //string strLat = secnicLocList[i].Split(',')[1];
                //XmlElement xeLng = xmlDoc.CreateElement("lng");
                //xeLng.InnerText = strLng;//设置文本节点
                //xeItem.AppendChild(xeLng);//添加到<item>节点中
                //XmlElement xeLat = xmlDoc.CreateElement("lat");
                //xeLat.InnerText = strLat;//设置文本节点
                //xeItem.AppendChild(xeLat);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(Server.MapPath("data1.xml"));

            ViewBag.Message = title;

            return View();
        }

        public ActionResult BreadTrip()
        {
            string title = "";
            List<string> secnicNameList = new List<string>();
            List<string> secnicLocList = new List<string>();
            for (int num = 1; num <= 730; num += 18)
            {
                string strBaidu;
                strBaidu = "http://breadtrip.com/scenic/3/12/sight/more/?next_start=" + num;
                Uri uri = new Uri(strBaidu);
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
                //通过Winista.HtmlParser解析Html

                //Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
                //NodeFilter filterDiv = new AndFilter(new TagNameFilter("div"), new HasAttributeFilter("class", "tuijianImg"));
                //NodeList nodeList = parser.Parse(filterDiv);

                //if (nodeList.Count > 0)
                //{
                //    for (int i = 0; i < nodeList.Count; i++)
                //    {
                //        ITag tempTag = nodeList[i] as ITag;
                //        secnicNameList.Add(tempTag.Children[4].Children[0].GetText());
                //        title += tempTag.Children[4].Children[0].GetText() + ";";
                //    }
                //}

                //通过newtonsoft.json解析Json
                JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
                IList<string> secnicNames = jo["sights"].Select(m => (string)m.SelectToken("name")).ToList();
                IList<string> SecnicLng = jo["sights"].Select(m => (string)m.SelectToken("longitude")).ToList();
                IList<string> SecnicLat = jo["sights"].Select(m => (string)m.SelectToken("latitude")).ToList();
                for (int i = 0; i < secnicNames.Count; i++)
                {
                    secnicNameList.Add(secnicNames[i]);
                    secnicLocList.Add(SecnicLng[i] + "," + SecnicLat[i]);
                    title += secnicNames[i] + " ; ";
                }

            }
            //保存XML
            xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < secnicNameList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = secnicNameList[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                string strLng = secnicLocList[i].Split(',')[0];
                string strLat = secnicLocList[i].Split(',')[1];
                XmlElement xeLng = xmlDoc.CreateElement("lng");
                xeLng.InnerText = strLng;//设置文本节点
                xeItem.AppendChild(xeLng);//添加到<item>节点中
                XmlElement xeLat = xmlDoc.CreateElement("lat");
                xeLat.InnerText = strLat;//设置文本节点
                xeItem.AppendChild(xeLat);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(Server.MapPath("data1.xml"));

            ViewBag.Message = title;


            return View();
        }
        
        public ActionResult YiLong()
        {
            string title = "";
            List<string> secnicNameList = new List<string>();
            List<string> secnicLocList = new List<string>();
            //for (int num = 1; num <= 2 ; num++ )
            //{
            string strBaidu;
            strBaidu = "http://trip.elong.com/shanghai/jingdian/";
            Uri uri = new Uri(strBaidu);
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
            //通过Winista.HtmlParser解析Html

            Parser parser = Parser.CreateParser(strResult, "utf-8");  //utf-8
            NodeFilter filterDiv = new AndFilter(new TagNameFilter("div"), new HasAttributeFilter("class", "zyjd-li"));
            NodeList nodeList = parser.Parse(filterDiv);
            NodeList subNodeList = nodeList[0].Children[1].Children;

            if (subNodeList.Count > 0)
            {
                for (int i = 0; i < subNodeList.Count; i++)
                {
                    ITag tempTag = subNodeList[i] as ITag;
                    if (tempTag != null)
                    {
                        ITag titleTag = tempTag.Children[0] as ITag;
                        secnicNameList.Add(titleTag.GetAttribute("TITLE").ToString());
                        title += titleTag.GetAttribute("TITLE").ToString() + ";";
                    }
                }
            }

            //通过newtonsoft.json解析Json
            //JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
            //IList<string> secnicNames = jo["sights"].Select(m => (string)m.SelectToken("name")).ToList();
            //IList<string> SecnicLng = jo["sights"].Select(m => (string)m.SelectToken("longitude")).ToList();
            //IList<string> SecnicLat = jo["sights"].Select(m => (string)m.SelectToken("latitude")).ToList();
            //for (int i = 0; i < secnicNames.Count; i++)
            //{
            //    secnicNameList.Add(secnicNames[i]);
            //    secnicLocList.Add(SecnicLng[i]+","+SecnicLat[i]);
            //    title += secnicNames[i] + " ; ";
            //}

            //}
            //保存XML
            xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < secnicNameList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = secnicNameList[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                //string strLng = secnicLocList[i].Split(',')[0];
                //string strLat = secnicLocList[i].Split(',')[1];
                //XmlElement xeLng = xmlDoc.CreateElement("lng");
                //xeLng.InnerText = strLng;//设置文本节点
                //xeItem.AppendChild(xeLng);//添加到<item>节点中
                //XmlElement xeLat = xmlDoc.CreateElement("lat");
                //xeLat.InnerText = strLat;//设置文本节点
                //xeItem.AppendChild(xeLat);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(Server.MapPath("data1.xml"));

            ViewBag.Message = title;


            return View();
        }

    }
}