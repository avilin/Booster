﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public interface IScoreable
    {
        void IncrementScore(int score);
    }
}