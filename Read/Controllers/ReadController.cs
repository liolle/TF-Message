namespace TFMessage.read.controllers;
using Microsoft.AspNetCore.Mvc;
using TFMessage.read.service;

public class ReadController(IReadService readService) : ControllerBase 
{

  [HttpGet]
  [Route("/read/all")]
  public IActionResult GetAll()
  {
    return Ok(readService.GetAll());
  }

}
