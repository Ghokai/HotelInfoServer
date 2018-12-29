using System;
using HotelInfoServer.Models;
using System.Text;
using System.Reflection;

namespace HotelInfoServer.OutputWriters
{
    public class HotelInfoHTMLOutputWriter : HotelInfoOutputWriterBase
    {
        //format spesific file extension passed to base class for writing output
        public HotelInfoHTMLOutputWriter() : base("html")
        {

        }

        //overrides to abstract method for imlementing format spesific logic
        public override string GenerateOutputContent(HotelInfo[] hotelInfos)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<table>");

            //table head
            sb.Append("<tr>");
            PropertyInfo[] properties = typeof(HotelInfo).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                sb.Append("<th>" + property.Name + "</th>");
            }
            sb.Append("</tr>");

            //table body
            foreach (HotelInfo item in hotelInfos)
            {
                sb.Append("<tr>");
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    sb.Append("<td>" + prop.GetValue(item, null).ToString() + "</td>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }
    }
}
