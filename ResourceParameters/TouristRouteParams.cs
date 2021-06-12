using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FakeXiecheng.API.ResourceParameters
{
    public class TouristRouteParams
    {
        public string Keyword { get; set; }
        public string OperatorType { get; set; }
        public int? RatingValue { get; set; }
        private string _rating;
        public string Rating { get { return _rating; } set {

                if (!string.IsNullOrWhiteSpace(value))
                {
                    //string rating // 小于lessThan, 大于largerThan, 等于equalTo lessThan3, largerThan2, equalTo5 
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    Match match = regex.Match(value);
                    if (match.Success)
                    {
                        OperatorType = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                    }
                }
                _rating = value;
            } }

    }
}
