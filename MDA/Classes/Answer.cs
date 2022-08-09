using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDA.Enums;

namespace MDA.Classes
{
    /// <summary>
    /// Ответы в меню
    /// </summary>
    public class Answer
    {
        public Mode Mode { get; set; }

        public int IdTable { get; set; }

        public uint CountOfPersons { get; set; }
    }
}
