﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Mathematics
    {
   public static float GetPercent(float percent, float value)
    {
        return (value / 100) * percent;
    }
    public static float GetValueInPercent(float maxValue, float curentValue)
    {
        return (curentValue * 100) / maxValue;
    }
}

