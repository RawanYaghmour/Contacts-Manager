using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts_Manager
{
    public class Program
    {
        private static List<Contact> user = new List<Contact>();

        static void Main(string[] args)
        {
            ContactsManagerApp();
        }

        public static void ContactsManagerApp()
        {
            bool Exit = false;

            while (!Exit)
            {
                Thread.Sleep(1500);
                Console.WriteLine( "\n");
                Console.WriteLine("Welcome to Our \"Contact Manager Application\"\n");
                Console.WriteLine("Select an option:");

                Thread.Sleep(500);

                Console.WriteLine("1- Add a new contact");
                Console.WriteLine("2- Remove a contact");
                Console.WriteLine("3- View all contacts");
                Console.WriteLine("4- Search for a contact");
                Console.WriteLine("5- Clear all contacts");
                Console.WriteLine("6- Exit the Contact Manager Application");
                Console.Write("\nEnter your choice (1-6): ");

                string option = Console.ReadLine();


                switch (option)
                {
                    case "1":
                        AddContactHandler();
                        break;
                    case "2":
                        RemoveContactHandler();
                        break;
                    case "3":
                        ViewAllContactsHandler();
                        break;
                    case "4":
                        SearchContactHandler();
                        break;
                    case "5":
                        ClearContactsHandler();
                        break;
                    case "6":
                        Exit = true;
                        Console.WriteLine("\nExiting the Contact Manager.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter a number from 1 to 6.");
                        break;
                }
            }
        }

        public static void AddContactHandler()
        {
            Console.Write("\nEnter a Contact Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Category: ");
            string category = Console.ReadLine();

            try
            {
                var updatedContacts = AddContact(name, category);
                Console.WriteLine("Contact added successfully");
                ViewContacts(updatedContacts);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void RemoveContactHandler()
        {
            Console.Write("Enter the name of the contact you want to remove: ");
            string name = Console.ReadLine();

            try
            {
                var updatedContacts = RemoveContact(name);
                Console.WriteLine("Contact removed successfully");
                ViewContacts(updatedContacts);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void ViewAllContactsHandler()
        {
            var allContacts = ViewAllContacts();
            Console.WriteLine("Here are all the contacts: ");
            ViewContacts(allContacts);
        }

        public static void SearchContactHandler()
        {
            Console.Write("Enter the name of the contact you want to find: ");
            string name = Console.ReadLine();
            string result = SearchForContact(name);
            Console.WriteLine(result);
        }

        public static void ClearContactsHandler()
        {
            ClearContacts();
            Console.WriteLine("The contacts were cleared successfully");
        }

        public static List<string> AddContact(string name, string category)
        {
            ValidateName(name);

            if (user.Any(c => c.Name == name))
                throw new InvalidOperationException("Contact already exists");

            user.Add(new Contact(name, category));
            return user.Select(c => c.ToString()).ToList();
        }

        public static List<string> RemoveContact(string name)
        {
            var contact = FindContact(name);
            user.Remove(contact);
            return user.Select(c => c.ToString()).ToList();
        }

        public static List<string> ViewAllContacts()
        {
            return user.Select(c => c.ToString()).ToList();
        }

        public static string SearchForContact(string name)
        {
            var contact = FindContact(name, throwIfNotFound: false);
            return contact != null ? contact.ToString() : "The contact not found";
        }

        public static void ClearContacts()
        {
            user.Clear();
        }

        public static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Contact name cannot be empty, Please enter a valid name for the contact.");
        }

        public static Contact FindContact(string name, bool throwIfNotFound = true)
        {
            var contact = user.FirstOrDefault(c => c.Name == name);
            if (throwIfNotFound && contact == null)
                throw new KeyNotFoundException("Contact not found");
            return contact;
        }

        public static void ViewContacts(List<string> contacts)
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine(contact);
            }
        }
    }
    }

