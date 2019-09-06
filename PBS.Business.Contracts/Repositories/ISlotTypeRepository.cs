using PBS.Database.Models;
using System.Collections.Generic;

namespace PBS.Business.Contracts.Repositories
{
    public interface ISlotTypeRepository
    {
        List<SlotType> GetAll ();
    }
}
