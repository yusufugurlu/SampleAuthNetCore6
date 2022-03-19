using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Helper
{
    public class ErrorList
    {
        public static string ErrorListHtmlTag(List<string> errors)
        {
            string result = "<ul>";
            foreach (var error in errors)
            {
                result += "<li>" + error + "</li>";
            }
            result += "</ul>";
            return result;
        }

        public static string ErrorListNewLine(List<string> errors)
        {
            string result = "";
            int errorId = 1;
            foreach (var error in errors)
            {
                result += errorId.ToString() + " - " + error + "\n";
                errorId++;
            }
            return result;
        }
    }
}
