using Xunit;
using Contacts_Manager;
namespace Contacts_Manager_Test
{
    public class UnitTest1
    {
        [Fact]
        public void CanAddNewContact()
        {
            // Arrange
            string name = "Rawan";
            string category = "software";

            // Act
            var updatedContacts = Program.AddContact(name, category);

            // Assert
            Assert.Contains($"{name} ({category})", updatedContacts);
        }
        [Fact]
        public void CanRemoveContact()
        {
            // Arrange
            string name = "rawan";
            string category = "sof";
            Program.AddContact(name, category); // Add the contact first

            // Act
            var updatedContacts = Program.RemoveContact(name);

            // Assert
            Assert.DoesNotContain($"{name} ({category})", updatedContacts);
        }

        [Fact]
        public void CanViewAllContacts()
        {
            // Arrange
            string name1 = "ahmmad";
            string category1 = "bussnis";
            Program.AddContact(name1, category1);

            string name2 = "anas";
            string category2 = "business";
            Program.AddContact(name2, category2);

            // Act
            var allContacts = Program.ViewAllContacts();

            // Assert
            Assert.Contains($"{name1} ({category1})", allContacts);
            Assert.Contains($"{name2} ({category2})", allContacts);
        }

        [Fact]
        public void RejectsAddingDuplicateContact()
        {
            // Arrange
            string name = "noor";
            string category = "software";
            Program.AddContact(name, category); // Add the contact first

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => Program.AddContact(name, category));
            Assert.Equal("Contact already exists", exception.Message);
        }
    }
    }
