using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string c_TypeSequence = "sequence";

        private static readonly Dictionary<string, DiagramStyle> s_StyleMappings = new Dictionary<string, DiagramStyle>
        {
            { c_StyleSketchy, DiagramStyle.Sketchy },
            { c_StyleClassic, DiagramStyle.Classic },
        };

        private static readonly Dictionary<string, DiagramType> s_TypeMappings = new Dictionary<string, DiagramType>
        {
            { c_TypeSequence, DiagramType.Sequence },
        };

        //
        // GET: /Api
        public ActionResult Index()
        {
            return new EmptyResult();
        }

        //
        // GET: /Api/Js        
        public ActionResult Js()
        {
            string jsPath = Server.MapPath("~/Scripts/kanga.js");
            string js = System.IO.File.ReadAllText(jsPath);

            js = js.Replace("_KANGA_API_BASE_URI_", Url.Action(string.Empty, "api", null, "http"));

            return new JavaScriptResult { Script = js };
        }

        //
        // GET: /Api/Get        
        [AllowCrossSiteJson]
        public ActionResult Get(string id)
        {
            return CreateImageResult(id);
        }

        //
        // GET: /Api/Create
        [AllowCrossSiteJson]
        public ActionResult Create(string source, string type, string style = c_StyleDefault)
        {
            var diagramSource = Server.UrlDecode(source);

            DiagramType diagramType;
            if (type == null || !s_TypeMappings.TryGetValue(type, out diagramType))
            {
                return new HttpStatusCodeResult(400, string.Format("Invalid value '{0}' for parameter 'type'.", type));
            }

            DiagramStyle diagramStyle;
            if (style == null || !s_StyleMappings.TryGetValue(style, out diagramStyle))
            {
                return new HttpStatusCodeResult(400, string.Format("Invalid value '{0}' for parameter 'style'.", style));
            }

            var diagramArguments = new DiagramArguments(diagramSource, diagramType, diagramStyle);
            using (var diagramResult = DiagramFactory.Create(diagramArguments))
            {
                var id = Guid.NewGuid().ToString();

                SaveImage(diagramResult, id);

                return CreateResult(diagramResult, id);
            }
        }
        
        private void SaveImage(DiagramResult result, string imageId)
        {
            using (var imageStream = new MemoryStream())
            {
                result.Image.Save(imageStream, ImageFormat.Png);

                HttpContext.Cache.Add(
                    imageId,
                    imageStream.GetBuffer(),
                    null,
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(5),
                    CacheItemPriority.Default,
                    null);
            }
        }

        private ActionResult CreateResult(DiagramResult result, string id)
        {
            if (Request.AcceptTypes != null &&
                Request.AcceptTypes.Any(acceptType => acceptType.Contains("json")))
            {
                return CreateJsonResult(result, id);
            }

            return CreateImageResult(id);
        }

        private ActionResult CreateImageResult(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(400, "Missing parameter 'id'.");
            }

            var buffer = HttpContext.Cache.Get(id) as byte[];
            if (buffer == null)
            {
                return new HttpStatusCodeResult(400, string.Format("Invalid value '{0}' for parameter 'id'.", id));
            }

            return new FileStreamResult(new MemoryStream(buffer), "image/png");
        }

        private ActionResult CreateJsonResult(DiagramResult result, string id)
        {
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    source = result.Arguments.Source,
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
                    diagram = Url.Action("get", "api", new { Id = id }, "http"),
                    diagramWidth = result.Image.Width,
                    diagramHeight = result.Image.Height,
                }
            };
        }
    }
}
