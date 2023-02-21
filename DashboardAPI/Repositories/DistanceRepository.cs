using DashboardAPI.Common;
using DashboardAPI.Model;
using System.Xml.Linq;

namespace DashboardAPI.Repositories
{
    public class DistanceRepository : IDistanceRepository
    {
        public APIResponse GetDistanceByProviance()
        {
            APIResponse api = new APIResponse();

            try
            {
                List<Distances> distances = JsonFileReader.Read<List<Distances>>(JsonFiles.Distance);
                List<Drivers> drivers = JsonFileReader.Read<List<Drivers>>(JsonFiles.Driver);
                List<Provinces> provinces = JsonFileReader.Read<List<Provinces>>(JsonFiles.Province);
                List<Sites> sites = JsonFileReader.Read<List<Sites>>(JsonFiles.Site);

                var distanceByProviance = (from p in provinces
                                           join s in sites on p.Id equals s.ProvinceId
                                           join d in drivers on s.Id equals d.SiteId
                                           join t in distances on d.Id equals t.DriverId
                                           select new { p.Id, p.Name, t.DistanceTravelled } into byProviance
                                           group byProviance by new { byProviance.Id, byProviance.Name } into grp
                                           select new
                                           {
                                               provinceId = grp.Key.Id,
                                               provinceName = grp.Key.Name,
                                               totalDistanceTravelled = grp.Sum(x => x.DistanceTravelled)
                                           } into result
                                           select result).ToList();

                api.result = distanceByProviance.ToArray();
                api.message = "";
                api.success = true;
            }
            catch (Exception ex)
            {
                api.message = ex.Message;
                api.success = false;
            }

            return api;
        }

        public APIResponse GetDistanceBySite(int provianceId)
        {
            APIResponse api = new APIResponse();

            try
            {
                List<Distances> distances = JsonFileReader.Read<List<Distances>>(JsonFiles.Distance);
                List<Drivers> drivers = JsonFileReader.Read<List<Drivers>>(JsonFiles.Driver);
                List<Sites> sites = JsonFileReader.Read<List<Sites>>(JsonFiles.Site);

                var distanceBySite = (from s in sites
                                      join d in drivers on s.Id equals d.SiteId
                                      join t in distances on d.Id equals t.DriverId
                                      where s.ProvinceId == provianceId
                                      select new { s.Id, s.Name, t.DistanceTravelled } into bySite
                                      group bySite by new { bySite.Id, bySite.Name } into grp
                                      select new
                                      {
                                          siteId = grp.Key.Id,
                                          siteName = grp.Key.Name,
                                          totalDistanceTravelled = grp.Sum(x => x.DistanceTravelled)
                                      } into result
                                      select result).ToList();

                api.result = distanceBySite.ToArray();
                api.message = "";
                api.success = true;
            }
            catch (Exception ex)
            {
                api.message = ex.Message;
                api.success = false;
            }

            return api;
        }

        public APIResponse GetDistanceByDriver(int siteId)
        {
            APIResponse api = new APIResponse();

            try
            {
                List<Distances> distances = JsonFileReader.Read<List<Distances>>(JsonFiles.Distance);
                List<Drivers> drivers = JsonFileReader.Read<List<Drivers>>(JsonFiles.Driver);

                var distanceByDriver = (from d in drivers
                                        join t in distances on d.Id equals t.DriverId
                                        where d.SiteId == siteId
                                        select new { d.Id, d.Name, t.DistanceTravelled } into byDriver
                                        group byDriver by new { byDriver.Id, byDriver.Name } into grp
                                        select new
                                        {
                                            driverId = grp.Key.Id,
                                            driverName = grp.Key.Name,
                                            totalDistanceTravelled = grp.Sum(x => x.DistanceTravelled)
                                        } into result
                                        select result ).ToList();

                api.result = distanceByDriver.ToArray();
                api.message = "";
                api.success = true;
            }
            catch (Exception ex)
            {
                api.message = ex.Message;
                api.success = false;
            }

            return api;
        }
    }
}
