using System.Collections.Generic;

namespace MineSweeper.Application.ViewModels
{
    public class VisitCellResultViewModel
    {
        public VisitCellResultViewModel()
        {
            Cells = new List<CellViewModel>();
        }

        public bool HasMine { get; set; }

        public bool IsGameDone { get; set; }

        public bool IsWinner { get; set; }

        public List<CellViewModel> Cells { get; set; }

        public List<CellViewModel> Mines { get; set; }
    }
}
