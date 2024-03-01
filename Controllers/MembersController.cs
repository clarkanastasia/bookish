using Bookish.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;
public class MembersController : Controller 
{    
    private readonly Library myLibrary;

    Member member1 = new Member{
        MemberId = 1,
        Name = "Jane Smith",
        MembershipNo = "123456"
    };
    
    public MembersController(Library library)
    {
        myLibrary = library;
    }

    [HttpGet("[controller]/all")]
    public IActionResult ListAll()
    {
        myLibrary.AddMember(member1);
        return View("~/Views/Member/MembersList.cshtml", myLibrary);
    }

    [HttpGet("[controller]/{memberId}")]
    public IActionResult GetById([FromRoute]int memberId)
    {
        var member = myLibrary.GetMemberById(memberId);

        return View("~/Views/Member/MemberDetails.cshtml", member);
    }

    [HttpGet("[controller]/AddMember")]
    public IActionResult AddMemberForm()
    {
        return View("~/Views/Member/AddMember.cshtml");
    }

    [HttpPost("[controller]/Add")]
    public IActionResult AddMember([FromForm] string name, [FromForm] string membershipNo)
    {
        var newMember = new Member{
            MemberId = myLibrary.Members.Count + 1,
            Name = name, 
            MembershipNo = membershipNo,
        };
        myLibrary.AddMember(newMember);
        return Redirect("all");
    }

    [HttpGet("[controller]/{id}/EditMember")]
    public IActionResult EditMemberForm([FromRoute] int id)
    {
        var existingMember = myLibrary.GetMemberById(id);
        return View("~/Views/Member/EditMember.cshtml", existingMember);
    }

    [HttpPost("[controller]/{id}/Edit")]
    public IActionResult EditBook([FromRoute] int id, [FromForm] string name, [FromForm] string membershipNo)
    {
        var existingMember = myLibrary.GetMemberById(id);
        existingMember.Name = name;
        existingMember.MembershipNo = membershipNo;
        return Redirect("/");
    }
}
