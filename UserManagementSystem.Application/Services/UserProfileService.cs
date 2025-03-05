using AutoMapper;
using UserManagementSystem.Application.Interfaces;
using UserManagementSystem.Domain.Entities;
using UserManagementSystem.Infrastructure.Interfaces;

namespace UserManagementSystem.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        public async Task<UserProfileResponse> GetProfileByUserIdAsync(int userId)
        {
            var profile = await _userProfileRepository.GetProfileByUserIdAsync(userId);
            return _mapper.Map<UserProfileResponse>(profile);
        }

        public async Task CreateProfileAsync(CreateUserProfileRequest request)
        {
            var userProfile = _mapper.Map<UserProfile>(request);
            await _userProfileRepository.CreateProfileAsync(userProfile);
        }

        public async Task UpdateProfileAsync(UpdateUserProfileRequest request)
        {
            var userProfile = _mapper.Map<UserProfile>(request);
            await _userProfileRepository.UpdateProfileAsync(userProfile);
        }

        public async Task DeleteProfileAsync(int userId)
        {
            var profile = await _userProfileRepository.GetProfileByUserIdAsync(userId);
            if (profile != null)
            {
                await _userProfileRepository.DeleteProfileAsync(profile);
            }
        }
    }
}
