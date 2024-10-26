using AplicationLayer;
using EnterpriseLayer;
using Mappers.Dtos.Requests;

namespace Mappers
{
    public class AddPeopleMapper : IMapper<AddPeopleDTO, People>
    {
        public People ToEntity(AddPeopleDTO source)
        {
            return new People(source.DNI, source.Name, source.LastName, source.Address);
        }
    }

}
