using System.Text.Json;
using HTMX.Web.Models.Contacts;

namespace HTMX.Web.Services;

public class ContactService : IContactService
{
    private List<Contact> _contacts;

    private readonly JsonSerializerOptions jsonOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

    public ContactService()
    {
        var rawContacts = File.ReadAllText("Data/contacts.json");
        _contacts = JsonSerializer.Deserialize<List<Contact>>(rawContacts, jsonOptions) ?? [];
    }

    public Contact Get(int id)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            throw new Exception("Contact not found");
        }

        return contact;
    }

    public IEnumerable<Contact> GetAll()
    {
        return _contacts;
    }

    public IEnumerable<Contact> Search(string query)
    {
        return _contacts.Where(c =>
            c.First.Contains(query, StringComparison.OrdinalIgnoreCase)
            || c.Last.Contains(query, StringComparison.OrdinalIgnoreCase)
            || c.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
            || c.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
        );
    }

    public bool Create(ContactFormViewModel contact)
    {
        try
        {
            var newContact = new Contact
            {
                Id = _contacts.Max(c => c.Id) + 1,
                First = contact.First,
                Last = contact.Last,
                Phone = contact.Phone,
                Email = contact.Email
            };

            _contacts.Add(newContact);

            File.WriteAllText(
                "Data/contacts.json",
                JsonSerializer.Serialize(_contacts, jsonOptions)
            );

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Update(ContactFormViewModel contact)
    {
        var index = _contacts.FindIndex(c => c.Id == contact.Id);

        try
        {
            _contacts[index] = new Contact
            {
                Id = _contacts[index].Id,
                First = contact.First,
                Last = contact.Last,
                Phone = contact.Phone,
                Email = contact.Email
            };

            File.WriteAllText(
                "Data/contacts.json",
                JsonSerializer.Serialize(_contacts, jsonOptions)
            );

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            _contacts.RemoveAll(c => c.Id == id);

            File.WriteAllText(
                "Data/contacts.json",
                JsonSerializer.Serialize(_contacts, jsonOptions)
            );

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
