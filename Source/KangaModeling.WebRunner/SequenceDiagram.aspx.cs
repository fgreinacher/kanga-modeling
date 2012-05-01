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
			string codeText = Request["code"] ?? string.Empty;

			var arguments = new DiagramArguments(codeText, DiagramType.Sequence, DiagramStyle.Sketchy);
			var result = DiagramFactory.Create(arguments);
			using (result)
			{
				Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
				Response.ContentType = "image/png";

                MemoryStream temp = new MemoryStream();
                result.Image.Save(temp, ImageFormat.Png);
                byte[] buffer = temp.GetBuffer();
                Response.OutputStream.Write(buffer, 0, buffer.Length);
			}
		}
	}
}
