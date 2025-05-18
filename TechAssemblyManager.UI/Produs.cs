﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssemblyManager.UI
{
    public class Produs
    {
        public string Nume { get; set; }
        public decimal Pret { get; set; }
        public Image Imagine { get; set; }
        public string Descriere { get; set; }
        public int ScorCritici { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public string Categorie { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public override string ToString()
        {
            return $"{Nume} - {Pret} RON";
        }
    }
}
