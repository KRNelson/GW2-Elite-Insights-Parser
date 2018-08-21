﻿using System;
using System.Collections.Generic;
using static LuckParser.Models.ParseModels.BoonSimulator;

namespace LuckParser.Models.ParseModels
{
    public class BoonSimulationItemDuration: BoonSimulationItem
    {
        private readonly ushort _src;
        private long _overstack;

        public BoonSimulationItemDuration(BoonStackItem other) : base(other.Start, other.BoonDuration)
        {
            _src = other.Src;
            _overstack = other.Overstack;
        }

        public override long GetDuration(ushort src, long start = 0, long end = 0)
        {
            return GetItemDuration(start, end);
        }
        public override void SetEnd(long end)
        {
            Duration = Math.Min(Math.Max(end - Start, 0),Duration);
        }

        public override List<ushort> GetSrc()
        {
            List<ushort> res = new List<ushort>
            {
                _src
            };
            return res;
        }

        public override int GetStack(long end)
        {
            return 1;
        }

        public override long GetOverstack(ushort src, long start = 0, long end = 0)
        {
            if (end > 0 && Duration > 0)
            {
                long dur = GetItemDuration(start, end);
                return (long)Math.Round((double)dur / Duration * _overstack);
            }
            return _overstack;
        }

        public override bool AddOverstack(ushort src, long overstack)
        {
            if (_src != src || Duration == 0)
            {
                return false;
            }
            _overstack += overstack;
            return true;
        }
    }
}
