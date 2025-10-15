
namespace gregslist_api_dotnet.Services;

public class HousesService
{
    private readonly HousesRepository _repository;

    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }
    internal List<House> GetAll()
    {
        return _repository.GetAll();
    }

    internal House GetById(int houseId)
    {
        House house = _repository.GetById(houseId);
        if (house == null)
        {
            throw new Exception("No house found with the id of: " + houseId);
        }
        return house;
    }
}
