using EvKitapci.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Interfaces
{
    public interface IKategoriService
    {
        IList<KitapDTO> KategoriVeIcindekiler();
    }
}
