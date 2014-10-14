using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace datavalidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Work.Run();
            Console.Read();
        }
    }
    public class Work
    {
        private static List<Scenic> scenicList = new List<Scenic>();
        public static void Run()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"../../" + "DataScraperWorks/full_data_new.xml");
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
            List<Scenic> tempScenicList = new List<Scenic>();
            List<Scenic> tempSameList = new List<Scenic>();
            foreach (Scenic tempScenic in scenicList)
            {
                int count = 0;
                foreach (Scenic scenic in scenicList)
                {

                    if (tempScenic.scenicName == scenic.scenicName)
                    {
                        //Console.WriteLine("Scenic: " + tempScenic.scenicName);
                        count++;
                    }
                }
                if (count < 2)
                {
                    tempScenicList.Add(tempScenic);
                }
                else
                {
                    tempSameList.Add(tempScenic);
                }
            }
            tempScenicList.Sort();
            Console.WriteLine("tempScenicList: " + tempScenicList.Count);
            saveXmlDoc(@"../../DataScraperWorks/", "final_data.xml", tempScenicList);
            saveXmlDoc(@"../../DataScraperWorks/", "finalSameList.xml", tempSameList);
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"../../" + "DataScraperWorks/tempOtherSameList.xml");
            //XmlNode scenicNode = xmlDoc.SelectSingleNode("scenic");
            //XmlNodeList items = scenicNode.ChildNodes;
            //foreach (XmlNode item in items)
            //{
            //    XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
            //    string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
            //    XmlNode scenicLngNode = item.SelectSingleNode("lng");
            //    string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
            //    XmlNode scenicLatNode = item.SelectSingleNode("lat");
            //    string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
            //    XmlNode scenicComeNode = item.SelectSingleNode("from");
            //    string scenicComeFrom = scenicComeNode != null ? scenicComeNode.InnerText : "null";
            //    Scenic scenic = new Scenic();
            //    scenic.scenicName = scenicName;
            //    scenic.scenicLat = scenicLat;
            //    scenic.scenicLng = scenicLng;
            //    scenic.comeFrom = scenicComeFrom;
            //    scenicList.Add(scenic);
            //}
            //Console.WriteLine("scenicList: " + scenicList.Count);
            //List<Scenic> tempScenicList = new List<Scenic>();
            //List<Scenic> tempSameList = new List<Scenic>();
            //foreach (Scenic tempScenic in scenicList)
            //{
            //    if (tempScenic.comeFrom == "mianbao1")
            //    {
            //        tempScenicList.Add(tempScenic);
            //    }
            //    else
            //    {
            //        tempSameList.Add(tempScenic);
            //    }
            //}
            //Console.WriteLine("tempScenicList: " + tempScenicList.Count);
            //saveXmlDoc(@"../../DataScraperWorks/", "tempMianBaoList.xml", tempScenicList);
            //saveXmlDoc(@"../../DataScraperWorks/", "tempNoMianBaoSameList.xml", tempSameList);
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"../../" + "DataScraperWorks/tempNoMianBaoSameList.xml");
            //XmlNode scenicNode = xmlDoc.SelectSingleNode("scenic");
            //XmlNodeList items = scenicNode.ChildNodes;
            //foreach (XmlNode item in items)
            //{
            //    XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
            //    string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
            //    XmlNode scenicLngNode = item.SelectSingleNode("lng");
            //    string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
            //    XmlNode scenicLatNode = item.SelectSingleNode("lat");
            //    string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
            //    XmlNode scenicComeNode = item.SelectSingleNode("from");
            //    string scenicComeFrom = scenicComeNode != null ? scenicComeNode.InnerText : "null";
            //    Scenic scenic = new Scenic();
            //    scenic.scenicName = scenicName;
            //    scenic.scenicLat = scenicLat;
            //    scenic.scenicLng = scenicLng;
            //    scenic.comeFrom = scenicComeFrom;
            //    scenicList.Add(scenic);
            //}
            //List<Scenic> scenicList1 = new List<Scenic>();
            //XmlDocument xmlDoc1 = new XmlDocument();
            //xmlDoc1.Load(@"../../" + "DataScraperWorks/tempMianBaoList.xml");
            //XmlNode scenicNode1 = xmlDoc1.SelectSingleNode("scenic");
            //XmlNodeList items1 = scenicNode1.ChildNodes;
            //foreach (XmlNode item in items1)
            //{
            //    XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
            //    string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
            //    XmlNode scenicLngNode = item.SelectSingleNode("lng");
            //    string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
            //    XmlNode scenicLatNode = item.SelectSingleNode("lat");
            //    string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
            //    XmlNode scenicComeNode = item.SelectSingleNode("from");
            //    string scenicComeFrom = scenicComeNode != null ? scenicComeNode.InnerText : "null";
            //    Scenic scenic = new Scenic();
            //    scenic.scenicName = scenicName;
            //    scenic.scenicLat = scenicLat;
            //    scenic.scenicLng = scenicLng;
            //    scenic.comeFrom = scenicComeFrom;
            //    scenicList1.Add(scenic);
            //}
            //Console.WriteLine("scenicList: " + scenicList.Count);
            //List<Scenic> tempScenicList = new List<Scenic>();
            //List<Scenic> tempSameList = new List<Scenic>();
            //foreach (Scenic tempScenic in scenicList)
            //{
            //    int count = 0;
            //    foreach (Scenic scenic in scenicList1)
            //    {

            //        if (tempScenic.scenicName == scenic.scenicName)
            //        {
            //            //Console.WriteLine("Scenic: " + tempScenic.scenicName);
            //            count++;
            //        }
            //    }
            //    if (count < 1)
            //    {
            //        tempScenicList.Add(tempScenic);
            //    }
            //}
            //Console.WriteLine("tempScenicList: " + tempScenicList.Count);
            //saveXmlDoc(@"../../DataScraperWorks/", "tempOtherSameList.xml", tempScenicList);
            ////saveXmlDoc(@"../../DataScraperWorks/", "tempSameList.xml", tempSameList);
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"../../" + "DataScraperWorks/tempOtherSameList.xml");
            //XmlNode scenicNode = xmlDoc.SelectSingleNode("scenic");
            //XmlNodeList items = scenicNode.ChildNodes;
            //foreach (XmlNode item in items)
            //{
            //    XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
            //    string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
            //    XmlNode scenicLngNode = item.SelectSingleNode("lng");
            //    string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
            //    XmlNode scenicLatNode = item.SelectSingleNode("lat");
            //    string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
            //    XmlNode scenicComeNode = item.SelectSingleNode("from");
            //    string scenicComeFrom = scenicComeNode != null ? scenicComeNode.InnerText : "null";
            //    Scenic scenic = new Scenic();
            //    scenic.scenicName = scenicName;
            //    scenic.scenicLat = scenicLat;
            //    scenic.scenicLng = scenicLng;
            //    scenic.comeFrom = scenicComeFrom;
            //    scenicList.Add(scenic);
            //}
            //Console.WriteLine("scenicList: " + scenicList.Count);
            //List<Scenic> tempScenicList = new List<Scenic>();
            //List<Scenic> tempSameList = new List<Scenic>();
            //foreach (Scenic tempScenic in scenicList)
            //{
            //    if (tempScenicList.Count == 0)
            //    {
            //        tempScenicList.Add(tempScenic);
            //    }
            //    else
            //    {
            //        int count = 0;
            //        foreach (Scenic scenic in tempScenicList)
            //        {

            //            if (tempScenic.scenicName == scenic.scenicName)
            //            {
            //                count++;
            //            }
            //        }
            //        if (count < 1)
            //        {
            //            tempScenicList.Add(tempScenic);
            //        }
            //    }
            //}
            //Console.WriteLine("tempScenicList: " + tempScenicList.Count);
            //saveXmlDoc(@"../../DataScraperWorks/", "tempOtherScenicList.xml", tempScenicList);
        }
        /// <summary>
        /// 数据整合
        /// </summary>
        private static void accountData()
        {
            DirectoryInfo rootDir = new DirectoryInfo(@"../../" + "DataScraperWorks");
            try
            {
                //遍历文件夹
                foreach (DirectoryInfo subDir in rootDir.GetDirectories())
                {
                    //遍历文件
                    foreach (FileInfo file in subDir.GetFiles("*.*"))
                    {
                        Console.WriteLine("File: " + file.Directory + "//" + file.ToString());
                        //读取xml文件
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(file.Directory + "//" + file.ToString());
                        XmlNode scenicNode = xmlDoc.SelectSingleNode("scenic");
                        if (scenicNode == null)
                        {
                            scenicNode = xmlDoc.SelectSingleNode("extraction").SelectSingleNode("scenic");
                        }
                        XmlNodeList items = scenicNode.ChildNodes;
                        foreach (XmlNode item in items)
                        {
                            XmlNode scenicNameNode = item.SelectSingleNode("scenicname");
                            if (scenicNameNode == null)
                            {
                                scenicNameNode = item.SelectSingleNode("secnicname");//mafengwo111 蚂蜂窝数据结构错了
                            }
                            string scenicName = scenicNameNode != null ? scenicNameNode.InnerText : "null";
                            if (scenicNameNode == null)
                            {
                                Console.WriteLine("Scenic: " + file.ToString());
                            }
                            //Console.WriteLine("Scenic: " + scenicName);
                            XmlNode scenicLngNode = item.SelectSingleNode("lng");
                            string scenicLng = scenicLngNode != null ? scenicLngNode.InnerText : "null";
                            //Console.WriteLine("ScenicLng: " + scenicLng);
                            XmlNode scenicLatNode = item.SelectSingleNode("lat");
                            string scenicLat = scenicLatNode != null ? scenicLatNode.InnerText : "null";
                            //Console.WriteLine("ScenicLat: " + scenicLat);
                            Scenic scenic = new Scenic();
                            scenic.scenicName = scenicName;
                            scenic.scenicLng = scenicLng;
                            scenic.scenicLat = scenicLat;
                            scenic.comeFrom = subDir.ToString();
                            scenicList.Add(scenic);
                        }

                    }
                }
                //数据排序
                scenicList.Sort();
                //保存所有数据
                saveXmlDoc(rootDir.FullName, "full_data.xml", scenicList);
                Console.WriteLine("Count: " + scenicList.Count.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }
        /// <summary>
        /// 存储xml数据
        /// </summary>
        /// <param name="saveDir"></param>
        /// <param name="fileName"></param>
        /// <param name="scenicList"></param>
        private static void saveXmlDoc(string saveDir,string fileName,List<Scenic> scenicList){
            //保存XML
            XmlDocument xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="utf-8"?>
            XmlDeclaration xmldecl;
            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmldecl);
            //加入一个根元素
            XmlElement xmlElement = xmlDoc.CreateElement("", "scenic", "");
            xmlDoc.AppendChild(xmlElement);
            for (int i = 0; i < scenicList.Count; i++)
            {
                XmlNode root = xmlDoc.SelectSingleNode("scenic");//查找<scenic> 
                XmlElement xeItem = xmlDoc.CreateElement("item");//创建一个<item>节点
                XmlElement xeName = xmlDoc.CreateElement("scenicname");
                xeName.InnerText = scenicList[i].scenicName;//设置文本节点
                xeItem.AppendChild(xeName);//添加到<item>节点中
                XmlElement xeLng = xmlDoc.CreateElement("lng");
                xeLng.InnerText = scenicList[i].scenicLng;//设置文本节点
                xeItem.AppendChild(xeLng);//添加到<item>节点中
                XmlElement xeLat = xmlDoc.CreateElement("lat");
                xeLat.InnerText = scenicList[i].scenicLat;//设置文本节点
                xeItem.AppendChild(xeLat);//添加到<item>节点中
                XmlElement xeFrom = xmlDoc.CreateElement("from");
                xeFrom.InnerText = scenicList[i].comeFrom;//设置文本节点
                xeItem.AppendChild(xeFrom);//添加到<item>节点中
                root.AppendChild(xeItem);//添加到<scenic>节点中
            }
            //保存创建好的XML文档
            xmlDoc.Save(saveDir+"//"+fileName);
        }
    }
}
