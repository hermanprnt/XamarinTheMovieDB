using System;

namespace XamarinTest01.Common.Models
{
    public class MovieCall
    {
        public string Type { set; get; }
        public int Page { set; get; }

        public MovieCall(string type, int page)
        {
            Type = type;
            Page = page;
        }
    }
}
