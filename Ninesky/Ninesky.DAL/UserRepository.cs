using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninesky.IDAL;
using Ninesky.Models;

namespace Ninesky.DAL
{
    public class UserRepository : BaseRepository<User>, InterfaceUserRepository
   {
    }


}
