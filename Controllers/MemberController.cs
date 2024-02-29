using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class MemberController : Controller 
{
[HttpGet("[controller]/{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var member = new Member
        {
            Id = id,
            Name = "Jane",
            Membership = 123456,
        };
        return View("~/Views/Member/MemberDetails.cshtml", member);
    }
}