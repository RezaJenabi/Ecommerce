using Infrastructure.Utilities.Commands;
using Infrastructure.Utilities.Common;
using System.Threading.Tasks;
using Warehouses.Commands.Warehouses;
using Warehouses.Domain.Models.WarehouseAggregate;
using Warehouses.Domain.Models.WarehouseAggregate.Repository;

namespace Warehouses.CommandsHandler.Warehouses
{
    public class CreateWarehouseHandler : MessageHandler<CreateWarehouse, Result>, ICreateWarehouseHandler
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateWarehouseHandler(IWarehouseRepository WarehouseRepository)
        {
            _warehouseRepository = WarehouseRepository;
        }

        public override async Task<Result> Handler(CreateWarehouse message)
        {
            var warehouse = new Warehouse(message.FirstName, message.LastName, message.Email, message.IsActive);
            await _warehouseRepository.AddAsync(warehouse);
            await _warehouseRepository.SaveEntitiesAsync();

            return new Result();
        }
    }

    public interface ICreateWarehouseHandler
    {
        Task<Result> Handler(CreateWarehouse message);
    }

}
