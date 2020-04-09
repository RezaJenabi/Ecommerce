using Infrastructure.Domain.Repositories;
using System.Collections.Generic;
using Warehouses.Domain.Models.WarehouseAggregate;
using Warehouses.Domain.Models.WarehouseAggregate.Repository;
using Warehouses.Domain.WarehouseDbContext;

namespace Warehouses.Repository
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {

        public WarehouseRepository(WarehouseDbContext WarehouseManagementDbContext)
            : base(WarehouseManagementDbContext)
        {
        }

        public List<Warehouse> GetAllWarehouses()
        {

            return null;
        }
    }
}
