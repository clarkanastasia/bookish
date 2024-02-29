using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;
public class MembersController : Controller 
{
private readonly Library myLibrary;
    Member member1 = new Member
        {
            MemberId = 1,
            Name = "Jane",
            MembershipNo = "123456",
        };

public MembersController(Library library)
{
    myLibrary = library;
}

[HttpGet("[controller]/all")]
    public IActionResult ListAll()
    {
        myLibrary.AddMember(member1);
        return View("~/Views/Member/All.cshtml", myLibrary);
    }

[HttpGet("[controller]/{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var member = myLibrary.GetMemberById(id);
        return View("~/Views/Member/MemberDetails.cshtml",member);;
    }
}