using AutoMapper;
using UserManagementSystem.Domain.Entities;

namespace UserManagementSystem.Application.Mappings
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            // Entity -> DTO
            CreateMap<UserProfile, UserProfileResponse>();

            // DTO -> Entity
            CreateMap<CreateUserProfileRequest, UserProfile>();
            CreateMap<UpdateUserProfileRequest, UserProfile>();
        }
    }
}
