﻿using Microsoft.EntityFrameworkCore;
using SalesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesMvcContext _context;

        public DepartmentService(SalesMvcContext context)
        {
            this._context = context;
        }
        public async Task<List<Department>> FindAllAsync()
        {
            return await  _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}