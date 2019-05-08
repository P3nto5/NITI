using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesMvc.Services.Exceptions;

namespace SalesMvc.Services
{
    public class SellerService
    {
        private readonly SalesMvcContext _context;

        public SellerService(SalesMvcContext context)
        {
            this._context = context;
        }
        public async Task<List<Seller>> findAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }
        public async Task InsertAsync(Seller obj)
        {
           // obj.Department = _context.Department.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Sellers.FindAsync(id);
                _context.Sellers.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Cant delete seller because he/she has sales");
            }
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
