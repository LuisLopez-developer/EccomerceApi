using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.UserPeople
{
    public class AddPeopleUseCase<TDTO>
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IMapper<TDTO, People> _mapper;
        private readonly IUserRepository _userRepository;

        public AddPeopleUseCase(
            IPeopleRepository peopleRepository,
            IMapper<TDTO, People> mapper,
            IUserRepository userRepository)
        {
            _peopleRepository = peopleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(TDTO peopleDTO, string userId)
        {
            // Paso 1: Verifica si el usuario ya está vinculado a una persona
            if (await _userRepository.IsUserLinkedToPersonAsync(userId))
                throw new ValidationException("El usuario ya está vinculado a una persona.");

            // Paso 2: Realiza el mapeo de DTO a entidad
            var people = _mapper.ToEntity(peopleDTO);

            // Paso 3: Verifica si ya existe una persona con el mismo DNI
            if (await _peopleRepository.ExistByDNIAsync(people.DNI))
                throw new ValidationException("Ya existe una persona con ese DNI.");

            // Paso 4: Agrega la nueva persona al repositorio
            var personId  = await _peopleRepository.AddAsync(people);

            // Paso 5: Vincula al usuario
            await _userRepository.LinkUserToPersonAsync(userId, personId);
        }
    }
}
