// ILookupService.cs
using System.Collections.Generic;
using System.ServiceModel;

using corewcfservice.model;

namespace corewcfservice.IServices{

[CoreWCF.ServiceContract]
public interface ILookupService
{
    [CoreWCF.OperationContract]
    List<Lookup> GetLookups();

    [CoreWCF.OperationContract]
    Lookup GetLookup(int id);

    [CoreWCF.OperationContract]
    void AddLookup(Lookup lookup);

    [CoreWCF.OperationContract]
    void UpdateLookup(Lookup lookup);

    [CoreWCF.OperationContract]
    void DeleteLookup(int id);
}
}