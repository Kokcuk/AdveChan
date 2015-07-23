using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AdveChan.Helpers
{
    public class ImageHelper
    {
        //TO DO: clean shitcode
        public static Image MakeThumb(Image image, int sizeW, int sizeH)
        {
            var height = image.Height;
            var width = image.Width;

            if (height > sizeH || width > sizeW)
            {
                if (height > width)
                {
                    double proportion = (double) height/(double) width;
                    height = sizeH;
                    width = (int) (height/proportion);
                    if (width > sizeW)
                    {
                        proportion = (double) width/(double) height;
                        width = sizeW;
                        height = (int) (width/proportion);
                    }
                }
                else
                {
                    double proprotion = (double) width/(double) height;
                    width = sizeW;
                    height = (int) (width/proprotion);
                    if (height > sizeH)
                    {
                        proprotion = (double) height/(double) width;
                        height = sizeH;
                        width = (int) (height/proprotion);
                    }
                }
                return new Bitmap(image, new Size(width, height));
            }
            return image;
        }
    }
}