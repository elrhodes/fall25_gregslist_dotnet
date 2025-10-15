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
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<House>> Create([FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            houseData.CreatorId = userInfo.Id;
            House house = _housesService.Create(houseData);
            return Ok(house);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}