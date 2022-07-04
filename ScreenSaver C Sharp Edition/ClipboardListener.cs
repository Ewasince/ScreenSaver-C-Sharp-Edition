using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ScreenSaver_C_Sharp_Edition
{
    class ClipboardListener
    {
        static SharpClipboard clipboard;
        static MyQueue queue;
        static string path = load_path();
        public static void initialize()
        {
            clipboard = new SharpClipboard();
            //clipboard.ObserveLastEntry = false;
            queue = new MyQueue(path);

            clipboard.ClipboardChanged += ClipboardChanged;
        }
        private static void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            try
            {
                if (e.ContentType == ContentTypes.Text)
                {
                    Console.WriteLine(clipboard.ClipboardText);
                }
                else if (e.ContentType == ContentTypes.Image)
                {
                    Image img = (Image)e.Content;
                    if (img == null)
                        return;
                    if (img.Width != 3200 || img.Height != 1088)
                        return;
                    save_image(img, get_name());
                }
            }
            catch (Exception ex) { };
        }

        static string load_path()
        {
            string str = "D:\\Images\\Скриншоты2";
            if (File.Exists("path"))
            {
                using (StreamReader sr = new StreamReader("path", Encoding.Default))
                {
                    str = sr.ReadLine();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("path", false, Encoding.Default))
                {
                    sw.WriteLine(str);
                }
            }
            return str;
        }
        static void save_image(Image image, string name)
        {
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 50L);
            image.Save(path + "\\" + name + ".jpg", myImageCodecInfo, myEncoderParameters);
        }
        static string get_name()
        {
            DateTime dTime = DateTime.Now;
            string name = String.Format("{6} {0}.{1}.{2} {3}-{4}-{5}", dTime.Day, dTime.Month, dTime.Year, dTime.Hour, dTime.Minute, dTime.Second, queue.getQueue());
            return name;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
