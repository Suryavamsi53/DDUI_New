using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using corewcfservice.model;
namespace corewcfservice.IServices
{
    [CoreWCF.ServiceContract]
    public interface IAddressService
    {
        [CoreWCF.OperationContract]
        Task<List<Address>> GetAddressesAsync();

        [CoreWCF.OperationContract]
        Task<Address> GetAddressAsync(int id);

        [CoreWCF.OperationContract]
        Task AddAddressAsync(Address address);

        [CoreWCF.OperationContract]
        Task UpdateAddressAsync(Address address);

        [CoreWCF.OperationContract]
        Task DeleteAddressAsync(int id);
    }
}
