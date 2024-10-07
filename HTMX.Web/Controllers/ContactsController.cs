using HTMX.Web.Models.Contacts;
using HTMX.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HTMX.Web.Controllers;

[Route("[controller]")]
public class ContactsController : Controller
{
    private readonly ILogger<ContactsController> _logger;
    private readonly IContactService _service;

    public ContactsController(ILogger<ContactsController> logger, IContactService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        var queryString = Request.Query.ContainsKey("q") ? Request.Query["q"].ToString() : "";
        var vm = new ContactsIndexViewModel
        {
            Contacts = string.IsNullOrEmpty(queryString)
                ? _service.GetAll()
                : _service.Search(queryString),
            QueryStringValue = queryString
        };

        return View(vm);
    }

    [HttpGet("{id:int}")]
    public IActionResult View(int id)
    {
        var contact = _service.Get(id);
        return View(contact);
    }

    [HttpGet("{id:int}/edit")]
    public IActionResult GetEdit(int id)
    {
        var dao = _service.Get(id);
        var updateContact = new ContactFormViewModel
        {
            Id = dao.Id,
            First = dao.First,
            Last = dao.Last,
            Email = dao.Email,
            Phone = dao.Phone
        };
        return View("Update", updateContact);
    }

    [HttpPost("{id:int}/edit")]
    public IActionResult PostEdit(int id, ContactFormViewModel model)
    {
        model.Id = id;

        if (!ModelState.IsValid)
        {
            return View("Update", model);
        }

        var dupe = _service.GetAll()
            .FirstOrDefault(c => c.Email == model.Email && c.Id != model.Id);

        if (dupe != null)
        {
            ModelState.AddModelError("Email", "Email is already in use by another contact");
            return View("Update", model);
        }

        if (_service.Update(model))
        {
            return RedirectToAction("Index");
        }

        return View("Update", model);
    }

    [HttpGet("new")]
    public IActionResult GetNew()
    {
        var newContact = new ContactFormViewModel();

        return View("New", newContact);
    }

    [HttpPost("new")]
    public IActionResult PostNew(ContactFormViewModel newContact)
    {
        if (!ModelState.IsValid)
        {
            return View("New", newContact);
        }

        var contacts = _service.GetAll();

        if (contacts.Any(contact => contact.Email == newContact.Email))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View("New", newContact);
        }

        if (_service.Create(newContact))
        {
            return RedirectToAction("Index");
        }

        return View("New", newContact);
    }

    [HttpPost("{id:int}/delete")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction("Index");
    }
}
