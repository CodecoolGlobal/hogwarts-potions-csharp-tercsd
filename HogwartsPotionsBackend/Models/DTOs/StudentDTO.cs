using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Models.Enums;

namespace HogwartsPotionsBackend.Models.DTOs
{
    public class StudentDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public HouseType HouseType { get; set; }

        public PetType PetType { get; set; }

        public long? RoomID { get; set; }
    }
}
