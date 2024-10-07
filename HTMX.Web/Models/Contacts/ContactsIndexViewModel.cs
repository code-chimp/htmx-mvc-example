namespace HTMX.Web.Models.Contacts;

public class ContactsIndexViewModel
{
    public IEnumerable<Contact> Contacts { get; set; }
    public string QueryStringValue { get; set; }
}