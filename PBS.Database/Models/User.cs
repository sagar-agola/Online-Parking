using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (50)]
        public string FirstName { get; set; }

        [MaxLength (50)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength (10)]
        public string ContectNumber { get; set; }

        public bool IsActive { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        #region User Role
        public int RoleId { get; set; }
        public Role Role { get; set; }
        #endregion

        #region User Address
        public int AddressId { get; set; }
        public Address Address { get; set; }
        #endregion

        #region Navigational Properties
        public List<ParkingLot> ParkingLots { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<UserClaim> UserClaims { get; set; }
        #endregion
    }
}
