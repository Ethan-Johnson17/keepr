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
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep NewVk)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        NewVk.creatorId = userInfo.Id;
        VaultKeep vaultkeep = _vks.CreateVaultKeep(NewVk, userInfo.Id);
        vaultkeep.Creator = userInfo;
        return Ok(vaultkeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("Succesfully Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}