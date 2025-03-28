using Microsoft.AspNetCore.Mvc;
using TFMessage.models;
using TFMessage.write.services;

namespace TFMessage.write.controllers;

public class WriteController (IWriteService writeService):ControllerBase 
{

  [HttpPost]
  [Route("/write/post")]
  public async Task<IActionResult> Add([FromBody] MessageDTO model)
  {
    if (!ModelState.IsValid){
      return BadRequest("Invalid model");
    }

    bool succeded = await writeService.CreatePost(model.GetMessage()); 

    if (!succeded)
    {
      return BadRequest("Failed insertion");
    }
    return Ok();
  } 
}
