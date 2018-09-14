using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
namespace TextMpt{
    class GetText{
        ///   <summary>   
        ///   取文本中间。   
        ///   </summary> 
        ///  <param name="AllText">原文本</param>
        ///   <param name="Front">左边文本</param> 
        ///   <param name="Behind">右边文本</param> 
        ///   <param name="startingpoint">起始位置</param>
        /// <returns>文本型</returns>
        public string TextMiddle(string AllText, string Front, string Behind, int startingpoint)   
        {

            int TextFront = AllText.IndexOf(Front, startingpoint);
            int TextBehind = AllText.IndexOf(Behind, TextFront + 1);
            if (TextFront < 0 || TextBehind < 0)
            {
                return "";
            }
            else
            {
                TextFront = TextFront + Front.Length;
                TextBehind = TextBehind - TextFront;
                if (TextFront < 0 || TextBehind < 0)
                {
                    return "";
                }
                return AllText.Substring(TextFront, TextBehind);
            }

        }
        ///   <summary>   
        ///   取文本重复次数   
        ///   </summary> 
        ///  <param name="data">原文本</param>
        ///   <param name="Text">查找文本</param> 
        /// <returns>整数型</returns>
        public int TextOccurrences(string data,string Text)   
        {           
         string data2 = data.Replace(Text, "");
            int number = data.Length - data2.Length;
            return number/Text.Length;
        }
        ///   <summary>   
        ///   读入文本   
        ///   </summary> 
        ///  <param name="file">路径</param> 
        /// <returns>文本型</returns>
        public string GetTxt(string file) {
            try
            {
                StreamReader SR = new StreamReader(file, Encoding.Default);
                string content = SR.ReadToEnd();
                SR.Dispose();
                return content;
            }
            catch {
                return "";
            }
        }
        ///   <summary>   
        ///   写出文件   
        ///   </summary> 
        ///  <param name="path">路径</param> 
        ///  <param name="data">数据</param> 
        /// <returns>逻辑型</returns>
        public bool WriteOutText(string path,string data) {
            try
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter text = new StreamWriter(file);
                text.Write(data);
                text.Close();
                file.Close();
                return true;
            }
            catch {
                return false;
            }


        }

        ///   <summary>   
        ///   分割文本   
        ///   </summary> 
        ///  <param name="data">原文本</param>
        ///  <param name="UDivisionText">用作分割文本</param>
        /// <returns>文本型</returns>
        public string[] DivisionText(string data,string UDivisionText) {
            string[] sArray = Regex.Split(data, UDivisionText, RegexOptions.IgnoreCase);
            return sArray;
        }
    }
    class encoded {




    }
}
