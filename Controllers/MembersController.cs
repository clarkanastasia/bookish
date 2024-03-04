using Bookish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class MembersController : Controller 
{
    private readonly Library myLibrary;

    public MembersController(Library library)
    {
        myLibrary = library;
    }

    [HttpGet("[controller]/ListAll")]
    public IActionResult ListAll()
    {
        var members = myLibrary.Members.ToList();
        
        var viewModel = new MembersView
        {
            Members = members,
        };
        return View(viewModel);
    }

    [HttpGet("[controller]/{memberId}")]
    public IActionResult List([FromRoute]int memberId)
    {
        var member = myLibrary.Members.FirstOrDefault(member => member.MemberId == memberId);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    [HttpGet("[controller]/AddMember")]
    public IActionResult AddMember()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddMember([FromForm] Member member)
    {
        myLibrary.Members.Add(member);
        myLibrary.SaveChanges();
        return RedirectToAction("ListAll");
    }

    [HttpGet("[controller]/{id}/EditMember")]
    public IActionResult EditMember([FromRoute] int id)
    {
        var existingMember = myLibrary.Members.FirstOrDefault(member => member.MemberId == id);
        if (existingMember == null)
        {
            return NotFound();
        }
        return View(existingMember);
    }

    [HttpPost("[controller]/{id}/EditMember")]
    public IActionResult EditMember([FromRoute] int id, [FromForm] string name, [FromForm] string membershipNo)
    {
        var existingMember = myLibrary.Members.FirstOrDefault(member => member.MemberId == id);
        if (existingMember == null)
        {
            return NotFound();
        }
        existingMember.Name = name;
        existingMember.MembershipNo = membershipNo;
        myLibrary.SaveChanges();
        return RedirectToAction("ListAll");
    }

    [HttpGet("[controller]/{memberId}/DeleteMember")]
    public IActionResult DeleteMember([FromRoute] int memberId)
    {
        var existingMember = myLibrary.Members.FirstOrDefault(member => member.MemberId == memberId);
        if (existingMember == null)
        {
            return NotFound();
        }
        myLibrary.Members.Remove(existingMember);
        myLibrary.SaveChanges();
        return RedirectToAction("ListAll");
    }
}