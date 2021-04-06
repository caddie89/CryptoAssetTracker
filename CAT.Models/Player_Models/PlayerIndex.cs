using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT.Models
{
    public class PlayerIndex
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

        [Display(Name = "Full Name")]
        public string PlayerFullName
        {
            get
            {
                var playerFullName = $"{PlayerFirstName} {PlayerLastName}";
                return playerFullName;
            }
        }

        //public string Position
        //{
        //    get
        //    {
        //        int positionValue = (int)PositionOfPlayer;
        //        string positionType = Enum.GetName(typeof(PlayerPosition), positionValue);

        //        switch (positionValue)
        //        {
        //            case 1:
        //                positionType = "Center";
        //                break;
        //            case 2:
        //                positionType = "Power Forward";
        //                break;
        //            case 3:
        //                positionType = "Small Forward";
        //                break;
        //            case 4:
        //                positionType = "Point Guard";
        //                break;
        //            case 5:
        //                positionType = "Shooting Guard";
        //                break;
        //        }
        //        return positionType;
        //    }
        //}
    }

    public enum PlayerPosition
    {
        [Display(Name = "Center")]
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
