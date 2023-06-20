using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIS_MAUI.Services
{
    public class DataTransferService
    {
        public Place TransferPlace { get; set; }
        public Firm TransferFirm { get; set; }
        public UnitType TransferUnitType { get; set; }
        public FullNameUnit TransferFullNameUnit { get; set; }
        public AccountingUnit TransferAccountingUnit { get; set; }
    }
}
