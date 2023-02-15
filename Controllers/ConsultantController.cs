using Arbetsprov_Bonus.Data;
using Arbetsprov_Bonus.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Arbetsprov_Bonus.Controllers;

[Route("[controller]")]
[ApiController]
public class ConsultantController : ControllerBase
{
    private readonly IConsultantRepository _consultantRepository;

    public ConsultantController(
        IConsultantRepository consultantRepository    
    )
    {
        _consultantRepository = consultantRepository;
    }

    [HttpGet("Get")]
    public IActionResult Get()
    {
        try
        {
            return Ok(_consultantRepository.Get());
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        if(_consultantRepository.CheckExists(id))
        {
            return Ok(_consultantRepository.GetById(id));
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("Add")]
    public IActionResult Add([FromBody] Consultant consultant)
    {
        if(!_consultantRepository.CheckExists(consultant.FirstName, consultant.LastName))
        {
            return Ok(_consultantRepository.Add(consultant));
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("Remove")]
    public IActionResult Remove(int id)
    {
        if(_consultantRepository.CheckExists(id))
        {
            _consultantRepository.Remove(id);
            return Ok(_consultantRepository.Get());
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("Update/{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Consultant consultant)
    {
        if(_consultantRepository.CheckExists(id))
        {
            consultant.Id = (uint)id;
            _consultantRepository.Update(consultant);
            return Ok(_consultantRepository.GetById(id));
        }
        else
        {
            return BadRequest();   
        }
    }
}
