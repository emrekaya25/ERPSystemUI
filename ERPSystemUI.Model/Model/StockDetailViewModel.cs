using ERPSystemUI.Model.DTO.ProcessType;
using ERPSystemUI.Model.DTO.Stock;
using ERPSystemUI.Model.DTO.StockDetail;
using ERPSystemUI.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.Model
{
    public class StockDetailViewModel
    {
        public List<StockDetailDTO> StockDetails { get; set; }
        public List<StockDTO> Stocks { get; set; }
        public List<UserDTO> Deliverers { get; set; }
        public List<UserDTO> Recievers { get; set; }
        public List<ProcessTypeDTO> ProcessTypes { get; set; }

    }
}
