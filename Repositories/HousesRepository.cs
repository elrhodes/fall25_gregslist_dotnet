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
        List<House> houses = _db.Query<House, Profile, House>(sql, (house, profile) =>
        {
            house.Creator = profile;
            return house;
        }).ToList();
        return houses;
    }
}