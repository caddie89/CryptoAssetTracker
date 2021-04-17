using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Player_Models
{
    public class PlayerCreate
    {
        [Required]
        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

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
    }
}

