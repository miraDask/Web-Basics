namespace BattleCards.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Card
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Range(0, int.MaxValue)]
        public int Attack { get; set; }

        [Range(0, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<UserCard> UsersCards { get; set; }
    }
}
