using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class DefinitionDTO
    {
        public required int ID { get; set; }
        public required string name {  get; set; }
        public string? type  { get; set; }
        public required string part1 { get; set; }
        public required string part2 { get; set; }
        public required string UnitName { get; set; }
    }
}
