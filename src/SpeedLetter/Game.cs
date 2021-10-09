using System;
using System.Collections.Generic;

namespace SpeedLetter
{
    class Game
    {
        public int TotalRounds = 10;
        public int CurrentRound { get; set; }
        public bool IsActive { get; set; }
        public string CurrentLetter { get; set; }
    }
}
