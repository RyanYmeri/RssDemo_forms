<%@ WebHandler Language = "C#" Class="Handler" %>

using System;
using System.Web;
using System.Xml;

namespace RssDemo_forms
{
    public class IISHandler1 : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://msdn.microsoft.com/en-us/library/46c5ddfy.aspx
        /// </summary>
        #region IHttpHandler Members
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/vnd.google-earth.kml+xml";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=events.kml");

            XmlTextWriter kml = new XmlTextWriter(context.Response.OutputStream, System.Text.Encoding.UTF8);

            kml.Formatting = Formatting.Indented;
            kml.Indentation = 3;

            kml.WriteStartDocument();

            kml.WriteStartElement("kml", "http://www.opengis.net/kml/2.2");
            kml.WriteStartElement("Placemark");
            kml.WriteElementString("name", "Chicago");
            kml.WriteElementString("description", "description");

            kml.WriteStartElement("Point");

            kml.WriteElementString("coordinates", "41.8369, 87.6847");

            kml.WriteEndElement();// <point>
            kml.WriteEndElement();// <Placemark>
            kml.WriteEndElement();// <kml>

            kml.Close();
            //write your handler implementation here.
        }
        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }
        #endregion
    }
}
