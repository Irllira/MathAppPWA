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
        public string name {  get; set; }
    //    public string description { get; set; }
        public string? type  { get; set; }
        public string part1 { get; set; }
        public string part2 { get; set; }
        public string UnitName { get; set; }


    }
}
