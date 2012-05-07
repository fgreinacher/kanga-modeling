using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using KangaModeling.Facade;

namespace KangaModelling.WebApplication
{
	public class SequenceDiagram : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request["code"] ?? string.Empty;
            string styleName = Request["style"] ?? string.Empty;

            var style = GetStyleByName(styleName.ToLower());

            var arguments = new DiagramArguments(code, DiagramType.Sequence, style);
            Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
            Response.ContentType = "image/png";
            using (var result = DiagramFactory.Create(arguments))
            using (MemoryStream temp = new MemoryStream())
            {
                result.Image.Save(temp, ImageFormat.Png);
                byte[] buffer = temp.GetBuffer();
                Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }

        private DiagramStyle GetStyleByName(string styleName)
        {
            switch (styleName)
            {
                case "sketchy":
                    return DiagramStyle.Sketchy;
                case "classic":
                    return DiagramStyle.Classic;
                default:
                    return DiagramStyle.Sketchy;
            }
        }
	}
}
