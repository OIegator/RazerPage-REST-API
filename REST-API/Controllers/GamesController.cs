using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using REST_API.Data;
using REST_API.Models;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("Games")]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly DBContext _context;

        public GamesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("Get")]
        public IEnumerable<Game>? Index()
        {
            return _context.Games != null ?
                         _context.Games.ToList() : null;
        }

        [HttpGet, Route("GetLast")]
        public Game Index2()
        {
            return _context.Games != null ?
                         _context.Games.ToList().Last() : null;
        }


        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        // [ValidateAntiForgeryToken]
        public string Create([Bind("Id,genre_id,game_name")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                _context.SaveChangesAsync();
                return "succsess";
            }
            return "decline";
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut, Route("Edit")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,genre_id,game_name")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        //POST: Games/Delete/5
        [HttpDelete, Route("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'DBContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2));
        }

        private bool GameExists(int? id)
        {
          return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
