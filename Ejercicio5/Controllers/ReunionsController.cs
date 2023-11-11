using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ejercicio5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio5.Controllers 
{
    [Route("api/reuniones")]
    [ApiController]
    public class ReunionsController : ControllerBase 
    {
        private readonly Ejercicio5DbContext _context;

        public ReunionsController(Ejercicio5DbContext context)
        {
            _context = context;
        }
    }
}
