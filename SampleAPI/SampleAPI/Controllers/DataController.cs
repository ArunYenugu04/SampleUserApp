using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.DataLayer;
using SampleAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data>>> GetAll()
        {
            var users = await _context.Data
                .Include(u => u.Address)
                .Include(u => u.telephones)
                .Include(u => u.UserInstitutions)
                .ThenInclude(ui => ui.Institutions)
                .ToListAsync();

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Data>> GetById(int id)
        {
            var user = await _context.Data
                .Include(u => u.Address)
                .Include(u => u.telephones)
                .Include(u => u.UserInstitutions)
                    .ThenInclude(ui => ui.Institutions)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Data>> Create(Data newUser)
        {
            newUser.Id = 0;

            if (newUser.Address != null)
            {
                newUser.Address.Id = 0;
                newUser.Address.Data = null;
            }

            if (newUser.telephones != null)
            {
                newUser.telephones.Id = 0;
                newUser.telephones.Data = null;
            }

            if (newUser.UserInstitutions != null)
            {
                foreach (var ui in newUser.UserInstitutions)
                {
                    ui.Id = 0;
                    ui.Data = null;
                    ui.Institutions = null; 
                }
            }

            _context.Data.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Data updatedUser)
        {
            var user = await _context.Data.FindAsync(id);
            if (user == null)
                return NotFound();

            if (user.Address != null)
            {
                user.Address.Data = null;
            }

            if (user.telephones != null)
            {
                user.telephones.Data = null;
            }

            if (user.UserInstitutions != null)
            {
                foreach (var ui in user.UserInstitutions)
                {
                    ui.Data = null;
                    ui.Institutions = null; 
                }
            }

            user.Firstname = updatedUser.Firstname;
            user.Lastname = updatedUser.Lastname;
            user.Email = updatedUser.Email;
            user.Birthdate = updatedUser.Birthdate;
            user.Address = updatedUser.Address;
            user.telephones = updatedUser.telephones;
            //user.UserInstitutions = updatedUser.UserInstitutions;


            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}