using Kthusid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrintServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "get,post")]
    public class PrintController : ApiController 
    { 
        // GET api/print/GetPrinters 
        [HttpGet]
        public IEnumerable<string> GetPrinters() 
        {
            var installedPrinters = new List<String>();
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                installedPrinters.Add(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
            }
            return installedPrinters;
        } 

        [HttpPost]
        public string Print(PrintPageSettings pageSettings)
        {
            if (pageSettings != null)
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PrinterSettings.PrinterName = pageSettings.PrinterName;
                PaperSize paperSize = GetPaperSize(pageSettings.PaperSize);
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintPage += (o, e) =>
                {
                    Graphics g = e.Graphics;
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.PageUnit = GraphicsUnit.Millimeter;
                    FontFamily fontFamily = new FontFamily(pageSettings.FontFamily);
                    Font font = new Font(fontFamily, pageSettings.FontSize, GetFontStyle(pageSettings.FontStyle));//, GraphicsUnit.Pixel);
                    //StringFormat.GenericDefault.LineAlignment = StringAlignment.Near;
                    g.DrawString(pageSettings.Data, font, Brushes.Black, new Point(pageSettings.XPos, pageSettings.YPos));// fontFamily.GetLineSpacing(FontStyle.Bold));
                };
                pd.Print();
                return pageSettings.Data;
            }
            return "-1";
        }


        private FontStyle GetFontStyle(string _style)
        {
            FontStyle style = FontStyle.Regular;
            switch (_style)
            {
                case "bold":
                    style = FontStyle.Bold;
                    break;
                case "italic":
                    style = FontStyle.Italic;
                    break;
                case "strikeout":
                    style = FontStyle.Strikeout;
                    break;
                case "underline":
                    style = FontStyle.Underline;
                    break;
            }
            return style;
        }

        private PaperSize GetPaperSize(string size)
        {
            PaperSize paperSize = null;
            switch (size)
            {
                //constructor "name", inch, inch
                case "A3":
                    paperSize = new PaperSize("A3", 1170, 1650);
                    break;
                case "A4":
                    paperSize = new PaperSize("A4", 830, 1170);
                    break;
                case "A5":
                    paperSize = new PaperSize("A5", 580, 830);
                    break;
                case "A6":
                    paperSize = new PaperSize("A6", 410, 580);
                    break;
                case "A7":
                    paperSize = new PaperSize("A7", 290, 410);
                    break;
                case "A8":
                    paperSize = new PaperSize("A8", 200, 290);
                    break;
                case "A9":
                    paperSize = new PaperSize("A9", 150, 200);
                    break;
                case "A10":
                    paperSize = new PaperSize("A10", 100, 150);
                    break;
                case "B3":
                    paperSize = new PaperSize("B3", 1390, 1970);
                    break;
                case "B4":
                    paperSize = new PaperSize("B4", 980, 1390);
                    break;
                case "B5":
                    paperSize = new PaperSize("B5", 690, 980);
                    break;
                case "B6":
                    paperSize = new PaperSize("B6", 490, 690);
                    break;
                case "B7":
                    paperSize = new PaperSize("B7", 350, 490);
                    break;
                case "B8":
                    paperSize = new PaperSize("B8", 240, 350);
                    break;
                case "B9":
                    paperSize = new PaperSize("B9", 170, 240);
                    break;
                case "B10":
                    paperSize = new PaperSize("B10", 120, 170);
                    break;
                case "C3":
                    paperSize = new PaperSize("C3", 1280, 1800);
                    break;
                case "C4":
                    paperSize = new PaperSize("C4", 900, 1280);
                    break;
                case "C5":
                    paperSize = new PaperSize("C5", 640, 900);
                    break;
                case "C6":
                    paperSize = new PaperSize("C6", 450, 640);
                    break;
                case "C7":
                    paperSize = new PaperSize("C7", 320, 450);
                    break;
                case "C8":
                    paperSize = new PaperSize("C8", 220, 320);
                    break;
                case "C9":
                    paperSize = new PaperSize("C9", 160, 220);
                    break;
                case "C10":
                    paperSize = new PaperSize("C10", 110, 160);
                    break;
                case "DL":
                    paperSize = new PaperSize("C10", 430, 860);
                    break;
                default:
                    paperSize = new PaperSize("A4", 830, 1170);
                    break;
            }
            return paperSize;
        }


        //// GET api/values/5 
        //public string Get(int id) 
        //{ 
        //    return "value"; 
        //} 

        //// POST api/values 
        //public void Post([FromBody]string value) 
        //{ 
        //} 

        //// PUT api/values/5 
        //public void Put(int id, [FromBody]string value) 
        //{ 
        //} 

        //// DELETE api/values/5 
        //public void Delete(int id) 
        //{ 
        //} 
    } 
}

