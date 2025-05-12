using System;

namespace TechAssemblyManager
{
    public class CerereService
    {
        public string Descriere { get; set; }
        public DateTime DataProgramare { get; set; }
        public string EmailUtilizator { get; set; }
        public string Status { get; set; } = "În așteptare";


        public override string ToString()
        {
            return $"{DataProgramare.ToShortDateString()} - {EmailUtilizator} [{Status}]: {Descriere}";
        }
    }
}