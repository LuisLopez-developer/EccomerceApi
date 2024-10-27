namespace AplicationLayer.UserPeople
{
    public class IsUserLinkedToPersonUseCase
    {
        private readonly IUserRepository _userRepository;

        public IsUserLinkedToPersonUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ExecuteAsync(string userId)
        {
            return await _userRepository.IsUserLinkedToPersonAsync(userId);
        }

    }
}
