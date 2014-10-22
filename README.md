html2xml
========

旅游景点数据抓取【C#】 

通过newtonsoft.json解析Json

通过Winista.HtmlParser解析Html

从以下网站抓取上海景点数据，并保存成xml的格式：

携程	757	http://you.ctrip.com/sight/shanghai2.html \m

去哪儿	372	http://travel.qunar.com/p-cs299878-shanghai-jingdian

同程旅游	1937	http://go.ly.com/gonglve/shi-jingdian-shanghai-3100/

蚂蜂窝	486	http://www.mafengwo.cn/jd/10099/gonglve.html

途牛网	579	http://www.tuniu.com/guide/d-shanghai-2500/jingdian/

艺龙旅游	161	http://trip.elong.com/shanghai/jingdian/

驴妈妈	35	http://www.lvmama.com/lvyou/scenery/d-shanghai79.html

百度旅游	1015	http://lvyou.baidu.com/shanghai/jingdian/

蝉游记	185	http://chanyouji.com/search/attractions?q=%E4%B8%8A%E6%B5%B7

面包旅行	742	http://breadtrip.com/scenic/3/12/sight/

抓取数据后，通过datavalidation文件内的程序进行整合，并且去重。


