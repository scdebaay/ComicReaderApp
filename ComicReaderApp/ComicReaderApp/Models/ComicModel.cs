using System;
using System.Collections.Generic;
using System.Text;

namespace ComicReaderApp.Models
{
    class ComicModel
    {
        public ComicModel()
        {
            //If current value for size property is higher than the defaultSize from the config, size property is set to the value input.
            if (Defaultsize < Size)
            {
                this.Size = Size;
            }
            else //The size is smaller than the default value and default value takes precedence.
            {
                this.Size = Defaultsize;
            }
            //Initialize integer to parse the defaultSize into.
            this.Defaultsize = 700 ;
            //Initialize page property to 0. Can be set externally.
            this.Page = 0;
        }
        private int Defaultsize { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
        //Derive Previous property from page and totalPages property. No page lower than 1.
        public int Previous
        {
            get
            {
                if (this.Page <= 0)
                {
                    return 0;
                }
                else
                {
                    return this.Page - 1;
                }
            }
        }
        //Derive Next property from page. There is no way of determining the total number of pages in a comic yet.
        public int Next
        {
            get
            {
                return this.Page + 1;
            }
        }
        public string RequestedFolder { get; set; }
        public string RequestedFile { get; set; }
        public string ComicName
        {
            get
            {
                return RequestedFile.Substring(0, RequestedFile.Length - 4);
            }
        }
    }
}
