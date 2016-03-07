﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public interface IDbBugs
    {
        Level GetLevelWithBugs(int id);
        Level GetLevel(int id);
        List<Scoreboard> GetScoreBoard();
        List<Bug> GetBugList();
        List<Bug> GetBugListFromLevel(int levelId);
        List<Level> GetAllLevels();
        void AddToScoreboard(string name, int score);
    }
}
