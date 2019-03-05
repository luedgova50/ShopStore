namespace ShopStore.WebMarket.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

    }
}
