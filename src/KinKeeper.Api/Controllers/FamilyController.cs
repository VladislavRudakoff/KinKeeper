namespace KinKeeper.Api.Controllers;

/// <summary>
/// Контроллер позволяющий работать с семейным древом
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class FamilyController : ControllerBase
{
    /// <summary>
    /// Получить текущий профиль пользователя
    /// </summary>
    [HttpGet]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<UserProfile> GetCurrentProfile()
    {
        await Task.Yield();

        return new(Guid.NewGuid())
        {
            FullName = "Anonymous Person",
            Birthday = DateTimeOffset.MinValue
        };
    }
}
