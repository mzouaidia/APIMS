using MonitoringStations.Core.Interfaces;
using MonitoringStations.Core.Services;
using MonitoringStations.Core.ViewModels;
using MonitoringStations.Domain.Models;
using MVCGrid.Models;
using MVCGrid.NetCore;
using System;

namespace MonitoringStations.WebUI.Grids
{
    public static class GridStations
    {
        private static IStationService _service;

        public static MVCGridBuilder<Station> Init()
        {
            //_service = new StationService();

            var colDef = new ColumnDefaults()
            {
                EnableSorting = true
            };

            return new MVCGridBuilder<Station>(colDef)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(sorting: true, defaultSortColumn: "LastModify", defaultSortDirection: SortDirection.Dsc)
                .WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
                .AddColumns(cols =>
                {
                    cols.Add("Id").WithValueExpression((p, c) => p.Id.ToString()).WithAllowChangeVisibility(true);
                    cols.Add("Hostname").WithHeaderText("Name").WithVisibility(true, true).WithValueExpression(p => p.Hostname);
                    cols.Add("MacAddress").WithHeaderText("Mac Address").WithVisibility(true, true).WithValueExpression(p => p.MacAddress);
                    cols.Add("AddressIp").WithHeaderText("Address Ip").WithVisibility(true, true).WithValueExpression(p => p.AddressIp);
                    cols.Add("StoreNumber").WithHeaderText("Store Number").WithVisibility(true, true).WithValueExpression(p => p.StoreNumber);
                    cols.Add("LastModify").WithHeaderText("Last Update").WithVisibility(true, true).WithValueExpression(p => p.LastModify.ToLongDateString());
                })
                .WithRetrieveDataMethod(stationsLoadData);
        }

        private static QueryResult<Station> stationsLoadData(GridContext arg)
        {
            var result = new QueryResult<Station>();
            var viewModelData = new StationsViewModel();
            viewModelData = _service.GetStations();
            result = (QueryResult<Station>)viewModelData.Stations;

            return result;
        }
    }
}
