namespace fall25_gregslist_dotnet.Controllers;

[ApiController]
[Route("api/houses")]

public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;

    public HousesController(HousesService hs)
    {
        _housesService = hs;
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

}