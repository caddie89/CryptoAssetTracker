﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Data.Entities
{
    public class Player
    {
        [Key]
        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Username")]
        public Guid OwnerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string PlayerFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string PlayerLastName { get; set; }

        [Required]
        [Display(Name = "Position")]
        public PlayerPosition PositionOfPlayer { get; set; }

        [Required]
        [Display(Name = "Team")]
        public string PlayerTeam { get; set; }

        [Display(Name = "Full Name")]
        public string PlayerFullName
        {
            get
            {
                var playerFullName = $"{PlayerFirstName} {PlayerLastName}";
                return playerFullName;
            }
        }

        public virtual ICollection<Moment> Moments { get; set; } = new List<Moment>();
    }

    public enum PlayerPosition
    {
        Center = 1,
        [Display(Name = "Power Forward")]
        PowerForward,
        [Display(Name = "Small Forward")]
        SmallForward,
        [Display(Name = "Point Guard")]
        PointGuard,
        [Display(Name = "Shooting Guard")]
        ShootingGuard
    }
}
