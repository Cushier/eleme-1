using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using http;
using System.Text.RegularExpressions;
using System.Web;
using TextMpt;
namespace eleme
{
    //By:北辰    QQ：1489154212
    //QQ交流群:303544938
    //饿了么一键最佳手气网页端:http://dahaiyidi.cn/eleme/login.html

    public partial class Form1 : Form
    {
        
        class Eleme : e_http
        {
            const string eleme_url = "https://h5.ele.me/restapi/marketing/themes/1969/group_sns/";
            GetText text = new GetText();
            public int Packets_number(string sn)
            {
                int number;
                string data= base.HttpGet(eleme_url + sn,"");
                data= text.TextMiddle(data, "\"lucky_number\": ", ", \"",0);

                int.TryParse(data, out number);
                return number;
            }
            public int Packets_Receive(string openid,string sign, string SID,string sn){
                int number;
                string url = "https://h5.ele.me/restapi/marketing/promotion/weixin/" + openid;
                string data = "{\"method\":\"phone\",\"group_sn\":\"" + sn + "\",\"sign\":\"" + sign + "\"}";
                data = HttpPost(url,data,"SID="+SID);
                number = text.TextOccurrences(data, "sns_username");
                Console.WriteLine("当前已经领取" + number);
                return number;

            }
            public int E_Add(string openid,string sign,string SID) {
                ListViewItem listView = new ListViewItem();
                string url = "https://h5.ele.me/restapi/marketing/promotion/weixin/" + openid;
                string data = "{\"method\":\"phone\",\"group_sn\":\"10eda62ae81180d3\",\"sign\":\"" + sign + "\"}";
             //   Console.WriteLine(url + "  " + data );
                data = HttpPost(url, "{\"method\":\"phone\",\"group_sn\":\"11013feca39138a2\",\"sign\":\""+sign+"\"}", "SID="+SID);
             //   Console.WriteLine(eleme_url);
                return text.TextOccurrences(data, "account");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Eleme eleme = new Eleme();
            GetText text = new GetText();
            int number = 0;
            int number2 = 0;
            string sn = text.TextMiddle(Packets.Text, "sn=", "&theme_id", 0);
            if (sn == "")
            {
                MessageBox.Show("红包连接错误，请重新输入!");

            }
            else {
                int Packets_number = eleme.Packets_number(sn)-1;
                if (listView1.Items.Count < Packets_number)
                {

                    MessageBox.Show("小号数量不足,请您添加！");
                }
                else {
                    for (int i = 0; i < listView1.Items.Count; i++) {
                        string openid = listView1.Items[i].SubItems[1].Text;
                        string sign = listView1.Items[i].SubItems[2].Text;
                        string SID = listView1.Items[i].SubItems[3].Text;
                         number = eleme.Packets_Receive(openid, sign, SID, sn);
                        if (number > number2){

                            number2 = number;
                        }
                        if (number2 == Packets_number)
                        {
                            MessageBox.Show("当前红包总数量为" + Packets_number+1 + "已领取数量" + number2 + "请您手动领取最大手气红包");
                            break;
                        }
                        if (number2 > Packets_number) {

                            MessageBox.Show("最大手气已经被领取");
                            break;
                        }

                        if (i+1 == listView1.Items.Count) {
                            MessageBox.Show("当前红包总数量为" + Packets_number+1 + "已领取数量" + number2 );
                        }
                    }

                }

            }
          
        }

        private void insertt(object sender, EventArgs e)
        {
            ListViewItem listView = new ListViewItem();
            GetText text = new GetText();
            Eleme eleme = new Eleme();
            string  data = System.Web.HttpUtility.UrlDecode(cookie.Text, System.Text.Encoding.UTF8)+";";
            int ID = listView1.Items.Count + 1;
            string openid = text.TextMiddle(data, "\"openid\":\"", "\",",0);
            string sign = text.TextMiddle(data, "\"eleme_key\":\"", "\",",0);
            string SID = text.TextMiddle(data, "SID=", ";",0);
            if (eleme.E_Add(openid, sign, SID) == 0)
            {
                MessageBox.Show("小号没有绑定手机号或者cookie不完整!");
            }
            else {
                ID = listView1.Items.Count + 1;
                listView.Text = ID.ToString();
                listView.SubItems.Add(openid);
                listView.SubItems.Add(sign);
                listView.SubItems.Add(SID);
                listView1.Items.Add(listView);
                MessageBox.Show("添加成功！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (path.Substring(path.Length - 4) == ".txt")
            {
                GetText text = new GetText();
                string data = text.GetTxt(path);
                int line = text.TextOccurrences(data, Environment.NewLine);
                string[] data2 = text.DivisionText(data, Environment.NewLine);
                for (int i = 0; i < line; i++)
                {
                    try
                    {
                        ListViewItem listView = new ListViewItem();
                        string[] data3 = text.DivisionText(data2[i], "----");
                        int ID = listView1.Items.Count + 1;
                        listView.Text = ID.ToString();
                        listView.SubItems.Add(data3[0]);
                        listView.SubItems.Add(data3[1]);
                        listView.SubItems.Add(data3[2]);
                        listView1.Items.Add(listView);
                        //    Console.WriteLine(data3[1]);
                    }
                    catch {
                        MessageBox.Show("请拖入程序保存的cookie文件!");
                        break;
                    }
               }
                    
                }

            
            else {
                MessageBox.Show("请拖入.txt文本");
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;                                                              //重要代码：表明是所有类型的数据，比如文件路径
             else
          e.Effect = DragDropEffects.None;

        }

        private void cookie_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                GetText text = new GetText();
                string path = AppDomain.CurrentDomain.BaseDirectory + "cookie.txt";
                string data = "";
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    Console.WriteLine(listView1.Items[i].SubItems[1].Text);
                    string openid = listView1.Items[i].SubItems[1].Text;
                    string sign = listView1.Items[i].SubItems[2].Text;
                    string SID = listView1.Items[i].SubItems[3].Text;
                    data = data + openid + "----" + sign + "----" + SID + Environment.NewLine;


                }
                text.WriteOutText(path, data);
                MessageBox.Show("cookie小号已经保存到路径   " + path);
            }
            catch {
                MessageBox.Show("保存cookie小号失败,请检查是否有读写权限!");

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/hibeiche/eleme");

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://v.youku.com/v_show/id_XMzYxNzIzMjM0OA==.html?spm=a2hzp.8244740.0.0");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://dahaiyidi.cn/eleme/login.html");
        }
    }
}
