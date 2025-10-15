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
}
