using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KangaModeling.Facade;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Caching;

namespace KangaModeling.Web.Controllers
{
    public class ApiController : Controller
    {
        private const string c_StyleDefault = c_StyleSketchy;

        private const string c_StyleSketchy = "sketchy";
        private const string c_StyleClassic = "classic";

        private static Dictionary<string, DiagramStyle> s_StyleMappings = new Dictionary<string, DiagramStyle>()
        {
            { c_StyleSketchy, DiagramStyle.Sketchy },
            { c_StyleClassic, DiagramStyle.Classic },
        };

        private const string c_TypeSequence = "sequence";

        private static Dictionary<string, DiagramType> s_TypeMappings = new Dictionary<string, DiagramType>()
        {
            { c_TypeSequence, DiagramType.Sequence },
        };

        //
        // GET: /Api/Get
        public ActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpNotFoundResult();
            }

            var buffer = HttpContext.Cache.Get(id) as byte[];
            if (buffer == null)
            {
                return new HttpNotFoundResult();
            }

            return new FileStreamResult(new MemoryStream(buffer), "image/png");
        }

        //
        // GET: /Api/Create
        public ActionResult Create(string source, string type, string style = c_StyleDefault)
        {
            source = Server.UrlDecode(source);

            return CreateDiagramCore(source, type, style,
             result =>
             {
                 using (MemoryStream temp = new MemoryStream())
                 {
                     var id = Guid.NewGuid().ToString();

                     result.Image.Save(temp, ImageFormat.Png);
                     byte[] imageData = temp.GetBuffer();

                     AddImageDataToCache(id, imageData);

                     return new JsonResult
                     {
                         JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                         Data = new
                         {
                             source = source,
                             errors = result.Errors.Select(error =>
                                new
                                {
                                    message = error.Message,
                                    token = new
                                    {
                                        line = error.TokenLine,
                                        start = error.TokenStart,
                                        end = error.TokenStart + error.TokenLength,
                                        value = error.TokenValue,
                                    }
                                }),
                             diagram = Url.Action("get", "api", new { Id = id }),
                         }
                     };
                 }
             });
        }

        //
        // GET: /Api/Js
        public ActionResult Js()
        {
            string jsPath = Server.MapPath("/Scripts/kanga.js");
            string js = System.IO.File.ReadAllText(jsPath);

            js = js.Replace("_KANGA_API_BASE_URI_", Url.Action(string.Empty, "api"));

            return new JavaScriptResult() { Script = js };
        }

        private ActionResult CreateDiagramCore(string text, string type, string style, Func<DiagramResult, ActionResult> resultHandler)
        {
            DiagramType diagramType;
            if (type == null || !s_TypeMappings.TryGetValue(type, out diagramType))
            {
                return new HttpStatusCodeResult(400);
            }

            DiagramStyle diagramStyle;
            if (style == null || !s_StyleMappings.TryGetValue(style, out diagramStyle))
            {
                return new HttpStatusCodeResult(400);
            }

            var arguments = new DiagramArguments(text, diagramType, diagramStyle);

            using (var result = DiagramFactory.Create(arguments))
            {
                return resultHandler(result);
            }
        }


        private void AddImageDataToCache(string id, byte[] imageData)
        {
            HttpContext.Cache.Add(id, imageData, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5), CacheItemPriority.Default, null);
        }
    }
}
