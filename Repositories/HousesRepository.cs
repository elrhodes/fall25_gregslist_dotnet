
namespace gregslist_api_dotnet.Repositories;

public class HousesRepository
{
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }
    internal List<House> GetAll()
    {//NOTE now we need to add the auth0 profile info to the houses, so we get the profile info from the accounts table and not just the creator_id
        string sql = @"
        SELECT
        h.*,
        a.*
        FROM houses h
        JOIN accounts a ON h.creator_id = a.id;
        ";
        // SECTION Dapper multi-mapping: here we are defining how the data is coming back from the query and how to map it to our models. It MUST be in the same order as the SELECT statement above. 
        List<House> houses = _db.Query(sql, (House house, Profile profile) =>
        {
            house.Creator = profile;
            return house;
        }).ToList();
        return houses;
    }
    // !SECTION

    internal House GetById(int houseId)
    {
        string sql = @"
        SELECT
        h.*,
        a.*
        FROM houses h
        JOIN accounts a ON h.creator_id = a.id
        WHERE h.id = @HouseId;
        ";
        object param = new { HouseId = houseId };
        House house = _db.Query(sql, (House house, Profile profile) =>
        {
            house.Creator = profile;
            return house;
        }, param).SingleOrDefault();
        return house;
    }
}