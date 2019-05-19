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
    public class ImagensController : Controller
    {
        private readonly SalesMvcContext _context;

        public ImagensController(SalesMvcContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<int> imagens = _context.Imagens.Select(x => x.Id).ToList();
            return View(imagens);
        }
        [HttpPost]
        public IActionResult UploadImagem(IList<IFormFile> arquivos)
        {
            IFormFile imagemEnviada = arquivos.FirstOrDefault();
            if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                imagemEnviada.OpenReadStream().CopyTo(ms);

                Imagem imagemEntity = new Imagem()
                {
                    Nome = imagemEnviada.Name,
                    Dados = ms.ToArray(),
                    ContentType = imagemEnviada.ContentType
                };
                _context.Imagens.Add(imagemEntity);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public FileStreamResult VerImagem(int id)
        {
            Imagem imagem = _context.Imagens.FirstOrDefault(m => m.Id == id);
            MemoryStream ms = new MemoryStream(imagem.Dados);
            return new FileStreamResult(ms, imagem.ContentType);
        }
    }
}