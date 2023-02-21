using DashboardAPI.Common;

namespace DashboardAPI.Repositories
{
    public interface IDistanceRepository
    {
        APIResponse GetDistanceByProviance();
        APIResponse GetDistanceBySite(int provianceId);
        APIResponse GetDistanceByDriver(int siteId);
    }
}
