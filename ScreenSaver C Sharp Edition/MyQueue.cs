using System;
using System.IO;
using System.Text;

namespace ScreenSaver_C_Sharp_Edition
{
    internal class MyQueue
    {
        private int queue = 0;
        string path;
        string filename;
        public int getQueue()
        {
            queue++;
            using (StreamWriter wr = new StreamWriter(filename, false, Encoding.Default))
            {
                wr.Write(queue.ToString());
            }
            return queue - 1;
        }

        public MyQueue(string path)
        {
            this.path = path;
            filename = path + "\\num";
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                {
                    queue = Convert.ToInt32(sr.ReadLine());
                }
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.WriteLine("0");
                }
            }
        }
    }
}
