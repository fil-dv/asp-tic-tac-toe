namespace Web_Tic_tac_toe.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        public int GameID { get; set; }

        [Required]
        [StringLength(255)]
        public string GameState { get; set; }
    }
}
