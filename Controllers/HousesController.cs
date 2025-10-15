namespace fall25_gregslist_dotnet.Controllers;

[ApiController]
[Route("api/houses")]

public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;
    private readonly Auth0Provider _auth;

    public HousesController(HousesService hs, Auth0Provider auth)
    {
        _housesService = hs;
        _auth = auth;
    }

    [HttpGet]
    public ActionResult<List<House>> GetAll()
    {
        try
        {
            List<House> houses = _housesService.GetAll();
            return Ok(houses);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("{houseId}")]
    public ActionResult<House> GetById(int houseId)
    {
        try
        {
            House house = _housesService.GetById(houseId);
            return Ok(house);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}