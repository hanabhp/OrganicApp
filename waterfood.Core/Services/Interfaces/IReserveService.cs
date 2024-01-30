using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Objects.Reserves;

namespace waterfood.Core.Services.Interfaces
{
    public interface IReserveService
    {
        Response ReserveAnItem(int itemId, int centerId, CurrentUser user);
        ReserveItemList CheckUserReservedItemsByQrToken(string qrToken,int centerOwnerId);
        Response SetItemStatusByCenterOwner(List<ReserveItemsCheckList> items , int userId);
        ItemsStatus GetAllItemStatuses();
    }
}
