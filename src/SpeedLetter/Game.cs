using System;
using System.Collections.Generic;

namespace SpeedLetter
{
    class Game
    {
        public Difficulty GameDifficulty { get; set; }
        public int Rounds { get; set; }
        public bool IsActive { get; set; }
        public string CurrentLetter { get; set; }
        public List<Player> players = new();

        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }
    }
}
