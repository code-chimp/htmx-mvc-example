using HTMX.Web.Models.Contacts;

namespace HTMX.Web.Services;

public interface IContactService
{
    Contact Get(int id);

    IEnumerable<Contact> GetAll();

    IEnumerable<Contact> Search(string query);

    bool Create(ContactFormViewModel contact);

    bool Update(ContactFormViewModel contact);

    bool Delete(int id);
}
