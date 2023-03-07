using AutoMapper;
using HogwartsPotionsBackend.Models.DTOs;
using HogwartsPotionsBackend.Models.Entities;

namespace HogwartsPotionsBackend.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Ingredient, IngredientDTO>().ReverseMap();
        CreateMap<Ingredient, NewIngredientDTO>().ReverseMap();

        CreateMap<Potion, PotionDTO>().ReverseMap();

        CreateMap<Recipe, RecipeDTO>().ReverseMap();
        CreateMap<Recipe, NewRecipeDTO>().ReverseMap();

        CreateMap<Room, RoomDTO>().ReverseMap();
        CreateMap<Room, NewRoomDTO>().ReverseMap();

        CreateMap<Student, StudentDTO>().ReverseMap();
    }
}
