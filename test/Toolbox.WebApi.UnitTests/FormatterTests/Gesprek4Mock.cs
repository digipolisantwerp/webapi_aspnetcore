using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toolbox.WebApi.UnitTests.FormatterTests
{
    public class Gesprek
    {
        public DateTime GeplandOp { get; set; }
        public DateTime GeplandTot { get; set; }
        public string Plaats { get; set; }
        public string Titel { get; set; }
        //public List<Verslag> Verslagen { get; set; }
        public string Klant { get; set; }
        public int? GesprekIntakeId { get; set; }
        public int? GesprekInvullingId { get; set; }
    }
}
