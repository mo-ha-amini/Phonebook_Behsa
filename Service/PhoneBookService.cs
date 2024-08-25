using Models;

using Repository;

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Service
{
    public class PhoneBookService
    {
        PhoneBookRespository _repo;
        public PhoneBookService()
        {
            _repo = new PhoneBookRespository();
        }

        public bool DeleteContact(string Id)
        {
            try
            {
                var contacts = _repo.GetContacts();
                var contactForDelete = contacts.FirstOrDefault(x => x.Id.ToString() == Id);
                contacts.Remove(contactForDelete);
                _repo.SaveContact(contacts);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting contact: {ex.Message}");
                return false;
            }
        }

        public Contact GetContactById(string id)
        {

            var contacts = GetContacts();
            var contact = contacts.FirstOrDefault(x => x.Id.ToString() == id.ToString());
            return contact;

            //if (contact == null)
            //{
            //    return null;
            //}
            //return contact;
        }

        public List<Contact> GetContacts()
        {
            return _repo.GetContacts();
        }

        public bool SaveContact(Contact model)
        {
            var contacts = GetContacts();

            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            contacts.Add(model);
            return _repo.SaveContact(contacts);
        }

        public bool EditContact(string selectedId, Contact contact)
        {
            var contacts = GetContacts();

            var contactForEdit = contacts.FirstOrDefault(x => x.Id.ToString() == selectedId);
            if (contactForEdit != null)
            {
                contacts.Remove(contactForEdit);
                contact.Id = Guid.Parse(selectedId);
                contacts.Add(contact);
                return _repo.SaveContact(contacts);
            }
            return false;
        }
    }
}
