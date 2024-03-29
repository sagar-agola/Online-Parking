﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PBS.Database.Models
{
    public class ParkingLot
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (50)]
        public string Name { get; set; }

        public bool IsAproved { get; set; }

        public bool IsActive { get; set; }

        #region Parking Owner
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        #endregion

        #region Parking Address
        public int AddressId { get; set; }
        public Address Address { get; set; }
        #endregion

        #region Navigational Properties
        public List<ParkingLotImage> ParkingLotImages { get; set; }
        public List<Slot> Slots { get; set; }
        #endregion
    }
}
