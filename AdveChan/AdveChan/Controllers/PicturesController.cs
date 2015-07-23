using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdveChan.Helpers;

namespace AdveChan.Controllers
{
    public class PicturesController : Controller
    {
        //TO DO: clean code
        [HttpPost]
        public ActionResult LoadingPicture(HttpPostedFileBase image)
        {
            if (image!=null)
            {
                try
                {
                    var file = image;
                    var content = Server.MapPath("~/Images/");
                    var directoryName = string.Format("{0}{1}{2}", DateTime.Now.Day, DateTime.Now.Month,
                        DateTime.Now.Year);
                    var path = Path.Combine(content, directoryName);
                    var randomName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                    var fileName = randomName + ".jpeg";
                    var fileNameThumb = randomName + "_thumb.jpeg";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    var filePath = string.Format(@"{0}\{1}", path, fileName);
                    var filePathThumb = string.Format(@"{0}\{1}", path, fileNameThumb);
                    var imagefileBig = Image.FromStream(file.InputStream);
                    var imagefileThumb = ImageHelper.MakeThumb(imagefileBig, 240, 136);
                    imagefileBig.Save(filePath,ImageFormat.Jpeg);
                    imagefileThumb.Save(filePathThumb,ImageFormat.Jpeg);
                    TempData["ImageUrl"] = filePath;
                   // return Content(String.Format("/Images/{0}/{1}",directoryName,fileNameThumb));
                    return null;
                }
                catch (Exception exception)
                {

                  //  return Content("Error while loading picture");
                    return null;
                }
            }
            //return Content("Error! No file to load");
            return null;
        }
    }
}