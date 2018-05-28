using ChocOvation.Models;
using System.Collections.Generic;

namespace ChocOvation.ViewModels
{
    public class ChocoDoseViewModel
    {
        public Choco Choco { get; set; }
        public List<DoseFormViewModel> DosesViewModel { get; set; }
    }
}