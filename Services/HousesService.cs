
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
    internal House Create(House houseData)
    {
        House house = _repository.Create(houseData);
        return house;
    }

    internal House Update(int houseId, House houseData, Account userInfo)
    {
        House house = GetById(houseId);
        if (house.CreatorId != userInfo.Id)
        {
            throw new Exception($"You cannot deletee a house you did not create, {userInfo.Name.ToUpper()}!!!You'll be served here shortly!!");
        }
        // NOTE the ?? operator means if the thing on the left is null, use the thing on the right
        // NOTE for value types (int, bool, double, etc) we cannot use ?? because they cannot be null, so we use a conditional (ternary) operator
        // NOTE this is called a null-coalescing operator
        house.Description = houseData.Description ?? house.Description;
        house.ImgUrl = houseData.ImgUrl ?? house.ImgUrl;
        house.Price = houseData.Price ?? house.Price;
        house.Bedrooms = houseData.Bedrooms ?? house.Bedrooms;
        house.Bathrooms = houseData.Bathrooms ?? house.Bathrooms;
        house.SqFt = houseData.SqFt ?? house.SqFt;
        _repository.Update(house);
        return house;
        // NOTE we are returning the house object that we updated in memory, not the one from the database
    }

    internal string Delete(int houseId, Account userInfo)
    {
        House house = GetById(houseId); //NOTE we do get by Id so we are just gettting one of the houses on the list
        if (house.CreatorId != userInfo.Id)
        {
            throw new Exception($"You cannot delete a house you did not create, {userInfo.Name.ToUpper()}!!!You'll be served here shortly!!");
        }
        _repository.Delete(houseId);
        return $"Successfully deleted your house!";
    }
}
