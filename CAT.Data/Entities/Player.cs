using System;
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
        public int PlayerId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string PlayerFirstName { get; set; }

        [Required]
        public string PlayerLastName { get; set; }

        [Required]
        public PlayerPosition PositionOfPlayer { get; set; }

        [Required]
        public string PlayerTeam { get; set; }

        public virtual ICollection<Moment> Moments { get; set; } = new List<Moment>();
    }

    public enum PlayerPosition
    {
        Center = 1,
        PowerForward,
        SmallForward,
        PointGuard,
        ShootingGuard
    }
}
