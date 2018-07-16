using System;
using System.Collections.Generic;
using System.Text;

namespace ComicReaderApp.Models
{
    public class Settings
    {
        public static int DefaultComicSize { get { return 700; } }
        public static string ApiLocation { private set { } get { return "https://develop.de-baay.nl/ComicCloud/"; } }
        public static int PageLimit { private set; get; }
    }
}
