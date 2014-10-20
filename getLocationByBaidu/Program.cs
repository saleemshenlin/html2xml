using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace getLocationByBaidu
{
    class Program
    {
        static void Main(string[] args)
        {
            Work.Run();
            Console.Read();
        }
    }

    class Work
    {
        private static List<Scenic> scenicList = new List<Scenic>();
        private static List<string> noResultList = new List<string>();
        private static List<string> moreResultList = new List<string>();
        private static HttpWebRequest httpWebRequest;
        private static HttpWebResponse httpWebResponse;
        public static void Run()
        {
            ReadXml("data_with_location(xiu).xml");
            ReadXml("data_without_location(xiu).xml");
            List<Scenic> withLocationList = new List<Scenic>();
            List<Scenic> withoutLocationList = new List<Scenic>();
            foreach(Scenic scenic in scenicList){
                if(scenic.scenicLat == "null"){
                    withoutLocationList.Add(scenic);
                    Console.WriteLine("info: without " + scenic.scenicLat);
                }else{
                    withLocationList.Add(scenic);
                    Console.WriteLine("info: with " + scenic.scenicLat);
                }
            }
            withLocationList.Sort();
            withoutLocationList.Sort();
            saveXml("data_with_location_final.xml", withLocationList);
            Console.WriteLine("info: data_with_location " + withLocationList.Count);
            saveXml("data_without_location_final.xml", withoutLocationList);
            Console.WriteLine("info: data_without_location " + withoutLocationList.Count);
        }

        public static void ReadXml(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"../../" + "DataScraperWorks/" + filename);
            XmlNode scenicNode = xmlDoc.SelectSingleNode("scenic");
            XmlNodeList items = scenicNode.ChildNodes;
            foreach (XmlNode item in items)
            {
                XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
                string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
                XmlNode scenicLngNode = item.SelectSingleNode("lng");
                string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
                XmlNode scenicLatNode = item.SelectSingleNode("lat");
                string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
                XmlNode scenicComeNode = item.SelectSingleNode("from");
                string scenicComeFrom = scenicComeNode != null ? scenicComeNode.InnerText : "null";
                Scenic scenic = new Scenic();
                scenic.scenicName = scenicName;
                scenic.scenicLat = scenicLat;
                scenic.scenicLng = scenicLng;
                scenic.comeFrom = scenicComeFrom;
                scenicList.Add(scenic);
            }
            Console.WriteLine("scenicList: " + scenicList.Count);
        }
        public static void saveXml(string fileName, List<string> list)
        {
            //保存XML
            XmlDocument xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="utf-8"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            XmlElement xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < list.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = list[i];//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(@"../../" + "DataScraperWorks/data_result" + "//" + fileName);
        }
        public static void saveXml(string fileName, List<Scenic> list)
        {
            //保存XML
            XmlDocument xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="utf-8"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            XmlElement xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < list.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = list[i].scenicName;//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                XmlElement xeLng = xmlDoc.CreateElement("lng");
                xeLng.InnerText = list[i].scenicLng;//设置文本节点
                xeItem.AppendChild(xeLng);//添加到<item>节点中
                XmlElement xeLat = xmlDoc.CreateElement("lat");
                xeLat.InnerText = list[i].scenicLat;//设置文本节点
                xeItem.AppendChild(xeLat);//添加到<item>节点中
                XmlElement xeFrom = xmlDoc.CreateElement("from");
                xeFrom.InnerText = list[i].comeFrom;//设置文本节点
                xeItem.AppendChild(xeFrom);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(@"../../" + "DataScraperWorks/data_result" + "//" + fileName);
        }
        public static void GetLocationByBaidu(string queryText)
        {
            string baiduUri = "http://api.map.baidu.com/place/v2/search?ak=tL14LtUikdTRTyoVuGEIXHbs&output=xml&scope=1&page_size=10&region=上海";
            //string baiduAK = "tL14LtUikdTRTyoVuGEIXHbs";
            string queryStr = "&query=" + queryText;
            //string page_size = "&page_size=10";
            string page_num = "&page_num=";
            if (scenicList.Count > 0)
            {
                try
                {
                    Uri uri = new Uri(baiduUri + queryStr + page_num + 0);
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
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strResult);
                    XmlNode rootNode = xmlDoc.SelectSingleNode("PlaceSearchResponse");
                    XmlNode totalNode = rootNode.SelectSingleNode("total");
                    if (int.Parse(totalNode.InnerText) == 0)
                    {
                        noResultList.Add(queryText);
                    }
                    else if (int.Parse(totalNode.InnerText) == 1)
                    {
                        string filePath = @"../../" + "DataScraperWorks/data_result/" + queryText + ".xml";
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("Error:当前文件已经存在！");
                            return;
                        }
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(strResult);
                        sw.Flush();
                        Console.WriteLine("info:已经完成文件写入！");
                    }
                    else if (int.Parse(totalNode.InnerText) > 1)
                    {
                        string filePath = @"../../" + "DataScraperWorks/data_result/data_more/" + queryText + ".xml";
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("Error:当前文件已经存在！");
                            return;
                        }
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(strResult);
                        sw.Flush();
                        Console.WriteLine("info:已经完成文件写入！");
                    }

                    sr.Close();

                    //sw.Close();
                    //fs.Close();
                    //Console.WriteLine(strResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
        }
        public static void insertLocationToXmlFromBaidu()
        {
            string baiduUri = "http://api.map.baidu.com/place/v2/search?ak=tL14LtUikdTRTyoVuGEIXHbs&output=xml&scope=1&page_size=10&region=上海";
            //string baiduAK = "tL14LtUikdTRTyoVuGEIXHbs";
            //string page_size = "&page_size=10";
            string page_num = "&page_num=";
            int count = 0;
            foreach (Scenic scenic in scenicList)
            {
                if (scenic.scenicLng == "null")
                {
                    try
                    {
                        string queryStr = "&query=" + scenic.scenicName;
                        Uri uri = new Uri(baiduUri + queryStr + page_num + 0);
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
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(strResult);
                        XmlNode rootNode = xmlDoc.SelectSingleNode("PlaceSearchResponse");
                        XmlNode totalNode = rootNode.SelectSingleNode("total");
                        XmlNode resultsNode = rootNode.SelectSingleNode("results");
                        if (int.Parse(totalNode.InnerText) == 0)
                        {
                            noResultList.Add(scenic.scenicName);
                        }
                        else if (int.Parse(totalNode.InnerText) == 1)
                        {
                            XmlNode resultNode = resultsNode.SelectSingleNode("result");
                            XmlNode locationNode = resultNode.SelectSingleNode("location");
                            XmlNode lngNode = locationNode.SelectSingleNode("lng");
                            scenic.scenicLng = lngNode.InnerText;
                            XmlNode latNode = locationNode.SelectSingleNode("lat");
                            scenic.scenicLat = latNode.InnerText;
                            Console.WriteLine("info:已经完成写入！");
                        }
                        else if (int.Parse(totalNode.InnerText) > 1)
                        {
                            XmlNodeList resultsNodeList = resultsNode.ChildNodes;
                            foreach (XmlNode resultNode in resultsNodeList)
                            {
                                string scenicName = resultNode.SelectSingleNode("name").InnerText;
                                if(scenicName==scenic.scenicName){
                                    XmlNode locationNode = resultNode.SelectSingleNode("location");
                                    XmlNode lngNode = locationNode.SelectSingleNode("lng");
                                    scenic.scenicLng = lngNode.InnerText;
                                    XmlNode latNode = locationNode.SelectSingleNode("lat");
                                    scenic.scenicLat = latNode.InnerText;
                                    break;
                                }
                            }
                            Console.WriteLine("info:已经完成写入！");
                        }

                        sr.Close();

                        //sw.Close();
                        //fs.Close();
                        //Console.WriteLine(strResult);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.ToString());
                    }
                }
                count++;
                Console.WriteLine("info: " + count.ToString());
            }
        }
    }
}
