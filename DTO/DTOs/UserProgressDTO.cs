using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs
{
    public class UserProgressDTO
    {
        public int Id { get; set; }
        public required string unitName { get; set; }
        public int AccountId { get; set; }
        public int good { get; set; }
        public int all { get; set; }

        public required string type { get; set; }
    }
}
