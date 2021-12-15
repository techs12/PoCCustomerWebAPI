using System;

namespace PocCustomer.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool IsValid(bool validateId = false)
        {
            bool res = validateId ? this.Id > 0 : true;

            if (string.IsNullOrEmpty(FirstName.Trim()) ||
                string.IsNullOrEmpty(LastName.Trim()) ||
                DateOfBirth > DateTime.Today)
                res = false;

            return res;
        }
    }
}
