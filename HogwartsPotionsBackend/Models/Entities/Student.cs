using HogwartsPotionsBackend.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotionsBackend.Models.Entities;

public class Student
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }

    public string Name { get; set; }

    public HouseType HouseType { get; set; }

    public PetType PetType { get; set; }

    public long? RoomID { get; set; }
    public Room Room { get; set; }
}
