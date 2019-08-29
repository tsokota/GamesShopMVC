using DAL;
using System.Collections.Generic;


namespace BusinessLogicLayer.Services.IServices
{
    public interface IShipperService
    {
        List<Shipper> AllShipper();
    }
}
