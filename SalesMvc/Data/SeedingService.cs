using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Models;
using SalesMvc.Models.Enums;
namespace SalesMvc.Data
{
    public class SeedingService
    {
        private SalesMvcContext _context;

        public SeedingService(SalesMvcContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if(_context.Department.Any() ||
                _context.Sellers.Any() ||
                _context.SalesRecords.Any())
            {
                return;
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Food");
            Department d5 = new Department(5, "Books");

            Seller s1 = new Seller(1, "Bob", "bob@gmail.com", 1000.0 ,new DateTime(1997, 4, 21), d1);
            Seller s2 = new Seller(2, "Maria", "maria@gmail.com", 1000.0 ,new DateTime(1996, 6, 19), d2);
            Seller s3 = new Seller(3, "Joao", "joao@gmail.com", 1000.0 ,new DateTime(1998, 9, 27), d3);
            Seller s4 = new Seller(4, "Samuel", "samuel@gmail.com", 1000.0 ,new DateTime(1999, 10, 2), d4);
            Seller s5 = new Seller(5, "Donald", "donald@gmail.com", 1000.0 ,new DateTime(1995, 1, 24), d5);
            Seller s6 = new Seller(6, "Ruan", "ruan@gmail.com", 1000.0 ,new DateTime(1997, 10, 26), d2);

            SalesRecord sr1 = new SalesRecord(1, new DateTime(2019, 09, 25), 11000.0, SalesStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2018, 09, 25), 21000.0, SalesStatus.Canceled, s3);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2017, 09, 25), 31000.0, SalesStatus.Pending, s6);
            SalesRecord sr4 = new SalesRecord(4, new DateTime(2016, 09, 25), 1000.0, SalesStatus.Pending, s2);
            SalesRecord sr5 = new SalesRecord(5, new DateTime(2015, 09, 25), 500.0, SalesStatus.Canceled, s4);

            _context.Department.AddRange(d1, d2, d3, d4, d5);
            _context.Sellers.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecords.AddRange(sr1, sr2, sr3, sr4, sr5);

            _context.SaveChanges();
        }
    }
}
