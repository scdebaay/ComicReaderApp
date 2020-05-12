using ComicReaderApp.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicReaderApp.Models
{
    class ApiVersionModel
    {
        public string apiName { get; set; }
        public string apiVersion { get; set; }
        public string libraryName { get; set; }
        public string libraryVersion { get; set; }
    }
}
