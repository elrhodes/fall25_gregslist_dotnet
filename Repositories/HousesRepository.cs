

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
    internal House Create(House houseData)
    {
        string sql = @"
        INSERT INTO houses
        ( price, img_Url, description, bedrooms, bathrooms, sqft, creator_id)
        VALUES
        ( @Price, @ImgUrl, @Description, @Bedrooms, @Bathrooms, @SqFt, @CreatorId);

        SELECT 
        houses.*, 
        accounts.*
        FROM houses
        JOIN accounts ON houses.creator_id = accounts.id
        WHERE houses.id = LAST_INSERT_ID();
        ";
        House house = _db.Query(sql, (House house, Profile profile) =>
        {
            house.Creator = profile;
            return house;
        }, houseData).SingleOrDefault();
        return house;
    }
    //SECTION In this section, we do not need to do the auth0 select statements because we already did the author verification in the service layer.
    internal void Update(House houseData)
    {
        string sql = @"
        UPDATE houses
        SET
            price = @Price,
            img_url = @ImgUrl,
            description = @Description,
            bedrooms = @Bedrooms,
            bathrooms = @Bathrooms,
            sqft = @SqFt
        WHERE id = @Id;
            ";
        int rowsAffected = _db.Execute(sql, houseData);
        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows of data are now gone and that is not good!");
        }
    }

    internal void Delete(int houseId)
    {
        string sql = "DELETE FROM houses WHERE id = @HouseId LIMIT 1;";

        object param = new { HouseId = houseId };

        int rowsAffected = _db.Execute(sql, param);

        if (rowsAffected != 1)
        {
            throw new Exception(rowsAffected + " rows of data are now gone and that is not good!");
        }
    }
    //!SECTION 
}