using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeepViewModel>> Create([FromBody] VaultKeepViewModel NewVk)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        NewVk.creatorId = userInfo.Id;
        VaultKeepViewModel vaultkeep = _vks.CreateVaultKeep(NewVk);
        vaultkeep.Creator = userInfo;
        return Ok(vaultkeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}