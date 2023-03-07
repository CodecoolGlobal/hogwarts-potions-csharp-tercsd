using HogwartsPotionsBackend.Models.Entities;
using System.Collections.Generic;

namespace HogwartsPotionsBackend.Models.DTOs
{
    public class RoomDTO
    {
        public long ID { get; set; }

        public int Capacity { get; set; }

        public IReadOnlyList<StudentDTO> Residents { get; set; }
    }
}
