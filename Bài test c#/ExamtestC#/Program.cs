using System;
using System.Collections;

namespace ExamtestC_
{
    class Program
    {
        static Hashtable contacts = new Hashtable();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Contact Manager Menu:");
                Console.WriteLine("1. Add new contact");
                Console.WriteLine("2. Find a contact by name");
                Console.WriteLine("3. Display contacts");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddNewContact();
                        break;
                    case "2":
                        FindContactByName();
                        break;
                    case "3":
                        DisplayContacts();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void AddNewContact()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            if (!contacts.ContainsKey(name))
            {
                contacts.Add(name, new Contact(name, phone));
                Console.WriteLine("Contact added successfully.");
            }
            else
            {
                Console.WriteLine("A contact with this name already exists.");
            }
        }

        static void FindContactByName()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            if (contacts.ContainsKey(name))
            {
                Contact contact = (Contact)contacts[name];
                Console.WriteLine($"Phone number: {contact.Phone}");
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }

        static void DisplayContacts()
        {
            Console.WriteLine("Address Book");
            Console.WriteLine("Contact Name\tPhone number");

            foreach (DictionaryEntry entry in contacts)
            {
                Contact contact = (Contact)entry.Value;
                Console.WriteLine($"{contact.Name}\t{contact.Phone}");
            }
        }
    }
}
