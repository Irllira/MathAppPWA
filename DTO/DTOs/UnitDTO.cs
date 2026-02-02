using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class UnitDTO
    {
        public required int ID {  get; set; }
        public required string name {  get; set; }
        public string? description { get; set; }

        public required string educationLevel { get; set; }

 //       public virtual List<DefinitionDTO> definitions { get; set; } 

    }
}
