﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteraktivnaMapaEvenata.Models
{
    public class Customer
    {
        public enum Genders { Male, Female, Unspecified }

        public int CustomerId { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Genders Gender { get; set; }

        public ICollection<Owner> FollowedOwners { get; set; }

        public ICollection<Promotion> FollowedPromotions { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Customer> Friends { get; set; }

        public ICollection<Flag> Flags { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
