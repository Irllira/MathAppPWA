using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class PagesDTO
    {

        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }

        public required string link { get; set; }

        public required int UnitID { get; set; }
    }

}
