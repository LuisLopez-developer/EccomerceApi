using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.UserPeople
{
    public class AddPeopleUseCase<TDTO>
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IMapper<TDTO, People> _mapper;

        public AddPeopleUseCase(
            IPeopleRepository peopleRepository,
            IMapper<TDTO, People> mapper)
        {
            _peopleRepository = peopleRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO peopleDTO)
        {
            var people = _mapper.ToEntity(peopleDTO);

            if (await _peopleRepository.ExistByDNIAsync(people.DNI))
                throw new ValidationException("Ya existe una persona con ese DNI.");

            await _peopleRepository.AddAsync(people);
        }
    }
}
