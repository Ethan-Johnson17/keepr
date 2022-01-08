using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetById(int id)
    {
      try
      {
        return Ok(_vs.GetById(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.creatorId = userInfo.Id;
        Vault vault = _vs.CreateVault(newVault);
        vault.Creator = userInfo;
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault update, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.creatorId = userInfo.Id;
        update.id = id;
        Vault updated = _vs.Edit(update);
        return Ok(updated);
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
        _vs.Delete(id, userInfo.Id);
        return Ok("Succesfully Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // #region VaultKeep
    [HttpGet("{id}/keeps")]
    public ActionResult<List<VaultKeepViewModel>> GetByVault(int id)
    {
      try
      {
        List<VaultKeepViewModel> vaulk = _vks.GetByVault(id);
        return Ok(vaulk);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // #endregion
  }
}