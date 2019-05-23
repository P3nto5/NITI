using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesMvc.Models;

namespace SalesMvc.Controllers
{
    public class PDFController : Controller
    {
        private readonly SalesMvcContext _context;

        public PDFController(SalesMvcContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<int> PDFs = _context.PDFs.Select(x => x.Id).ToList();
            return View(PDFs);
        }
        [HttpPost]
        public IActionResult UploadPDF(IList<IFormFile> files)
        {
            IFormFile PDFenviado = files.FirstOrDefault();
            if(PDFenviado != null || PDFenviado.ContentType.ToLower().StartsWith("File/"))
            {
                MemoryStream memoryStream = new MemoryStream();
                PDFenviado.OpenReadStream().CopyTo(memoryStream);
                PDF PdfEntity = new PDF()
                {
                    Nome = PDFenviado.Name,
                    Dados = memoryStream.ToArray(),
                    ContentType = PDFenviado.ContentType
                };
                _context.PDFs.Add(PdfEntity);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public FileStreamResult VerPdf(int id)
        {
            PDF PDF = _context.PDFs.FirstOrDefault(m => m.Id == id);
            MemoryStream memoryStream = new MemoryStream(PDF.Dados);
            return new FileStreamResult(memoryStream, PDF.ContentType);

        }
    }
}