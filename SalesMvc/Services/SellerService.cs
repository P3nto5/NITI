using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Models;
namespace SalesMvc.Services
{
    public class SellerService
    {
        private readonly SalesMvcContext _context;

        public SellerService(SalesMvcContext context)
        {
            this._context = context;
        }
        public List<Seller> findAll()
        {
            return _context.Sellers.ToList();
        }
    }
}
