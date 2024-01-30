using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Centers;
using waterfood.Core.Objects.Generals;

namespace waterfood.Core.Objects.Reserves
{
    
    public class ReserveItemsDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int ReserveId { get; set; }
        public int StatusRef { get; set; }

    }

    public class ReserveItemList : Response
    {
        public List<ReserveItemsDTO> ReserveItems { get; set; } = null!;
    }

    public class ReserveItemsCheckList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int ReserveRef { get;set; }
        public int StatusRef { get; set; }
    }

    public class ItemsStatusDTO
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
    }
    public class ItemsStatus : Response
    {
        public List<ItemsStatusDTO> Statuses { get; set; } = null!;
    }
   public class ReserveAnItemDTO
    {
        public int ItemId { get; set;}
        public int CenterId { get; set; }
    }
    public class QrTokenDTO
    {
        public string QeToken { get; set; } = null!;
    }
}
