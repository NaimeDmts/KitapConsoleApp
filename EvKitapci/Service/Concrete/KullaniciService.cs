using EvKitapci.Contexts;
using EvKitapci.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Concrete
{
    public class KullaniciService : BaseService<Kullanici>
    {
        public KullaniciService(AppDbContext context) : base(context)
        {
        }
    }
}
