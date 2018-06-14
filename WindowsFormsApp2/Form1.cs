using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private int[] arr;
        private int count = 0; //для массива
        private Timer myTimer = new Timer();
        private Timer myTimer1 = new Timer();
        private string res;
        private string folderName = AppDomain.CurrentDomain.BaseDirectory+"textfile"; //Где создаем файлы
        string filename = AppDomain.CurrentDomain.BaseDirectory + "11.txt"; //Файл из которого читаем

        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;

            ReadAndCreateFiles(); // заполняем массив данными


            myTimer.Interval = 10000;
            myTimer1.Interval = 11000;
            myTimer.Tick += MyTimer_Tick;
            myTimer1.Tick += MyTimer1_Tick;

            myTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            if (arr.Length <= count)
            {
                myTimer.Enabled = false;
                myTimer1.Enabled = false;
                return;
            }

            
            MyProcClick();
            myTimer.Stop();
            myTimer1.Enabled = true;
            myTimer1.Start();
        }


        private void MyTimer1_Tick(object sender, EventArgs e)
        {
            if (arr.Length <= count)
            {
                myTimer.Enabled = false;
                myTimer1.Enabled = false;
                return;
            }
            GetResult();
            myTimer1.Stop();
            string path = folderName +"\\"+ arr[count]+".txt";
            File.WriteAllText(path, res+" "+ arr[count]);
            count++;
            myTimer.Enabled = true;
            myTimer.Start();

        }
        
       
        void MyProcClick()
        {
           
                webBrowser1.Document.GetElementById("ctl00_ctl00_ctl00_ctl00_LeftColumn_LeftColumn_LeftColumn_LeftColumn_GoToEvent").InnerText = arr[count].ToString();
                webBrowser1.Document.GetElementById("ctl00_ctl00_ctl00_ctl00_LeftColumn_LeftColumn_LeftColumn_LeftColumn_Go").InvokeMember("click");

            
        }

        void GetResult()
        {
            res= webBrowser1.Document
                .GetElementById("ctl00_ctl00_ctl00_ctl00_LeftColumn_LeftColumn_LeftColumn_LeftColumn_CustomValidator1")
                .Style;
        }


        void ReadAndCreateFiles()
        {
            


            string line;
            int counter = 0;
            StreamReader file;

            arr = new int[File.ReadLines(filename).Count()+1]; //задаем размерность массива, количество строк в файле 


            using (file = new StreamReader(filename))
            {
                while ((line = file.ReadLine()) != null)
                {
                    
                    string fileName = line + ".txt";
                    arr[counter++] = int.Parse(line);
                    string pathString = Path.Combine(folderName, fileName);
                    if (!Directory.Exists(folderName))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderName);
                    }

                    if (!File.Exists(pathString))
                    {
                        var h = File.Create(pathString);
                        h.Close();
                    }
                }
            }
        }
    }
}
