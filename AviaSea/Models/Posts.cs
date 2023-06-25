using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AviaSea.Models
{
    public class Posts
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string NameUser { get; set; }
        public string FamilyUser { get; set; }
        public byte[] ImageUser { get; set; }
        public byte[] ImagePost
        {
            set
            {
                if (value !=null)
                {
                    MemoryStream memorystream = new MemoryStream();
                    memorystream.Write(value, 0, (int)value.Length);
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BitmapImage imgsource = new BitmapImage();
                    imgsource.BeginInit();
                    imgsource.StreamSource = memorystream;
                    imgsource.EndInit();
                    bitmapImage = imgsource;
                }
            }
        }
        public string info { get; set; }
        public int kolvo_see { get; set; }

        public new BitmapImage bitmapImage { get; set; }
    }
}
