using CAT.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models.Player_Models
{
    public class PlayerEdit
    {
        [Display(Name = "Player ID")]
        public int PlayerId { get; set; }

        [Display(Name = "First Name")]
        public string PlayerFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string PlayerLastName { get; set; }

        [Display(Name = "Position")]
        public PlayerPosition PositionOfPlayer { get; set; }

        [Display(Name = "Team")]
        public string PlayerTeam { get; set; }
    }
}
