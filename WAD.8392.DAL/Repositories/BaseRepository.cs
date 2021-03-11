using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD._8392.DAL.Context;

namespace WAD._8392.DAL.Repositories
{
   public abstract class BaseRepository
    {
        protected readonly MusicInstrumentsDbContext _context;
        protected BaseRepository(MusicInstrumentsDbContext context)
        {
            _context = context;
        }
    }
}
