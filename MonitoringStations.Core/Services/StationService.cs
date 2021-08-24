using System;
using System.Linq;
using System.Collections.Generic;
using MonitoringStations.Core.Interfaces;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;
using MonitoringStations.DB.Context;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonitoringStations.Common.Enums;
using MonitoringStations.Common.Logs;

namespace MonitoringStations.Core.Services
{
    public class StationService : IStationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StationDto>> GetStations()
        {
            var stations = await _context.Stations.ToListAsync();
            var stationsDto = _mapper.Map<List<StationDto>>(stations);

            return stationsDto.ToList();
        }

        public string GetStationHostname(long stationId)
        {
            return GetStationById(stationId)?.Hostname ?? string.Empty;
        }

        public bool GetStationExist(string hostname, string macAddress, out StationDto station)
        {
            var stations = _context.Stations.FirstOrDefault(x => x.Hostname == hostname && x.MacAddress == macAddress);

            var result = _mapper.Map<StationDto>(stations);

            station = result ?? new StationDto();

            return result != null;
        }

        private ExistResult StationExist(Station station)
        {
            try
            {
                var stationId = _context?.Stations?.FirstOrDefault(x =>
                        x.Hostname == station.Hostname &&
                        x.MacAddress == station.MacAddress)?.Id ?? -1;

                var result = new ExistResult
                {
                    IsExist = stationId != -1,
                    Station = GetStationById(stationId),
                };

                return result;
            }
            catch(Exception ex)
            {
                new EventLogger().WriteLog(ex.Message);
                
                return new ExistResult
                {
                    IsExist = false,
                    Station = null
                };
            }
        }

        private async Task<OperationStateResult> InsertStation(Station station)
        {
            station.Id = DateTime.Now.Ticks;
            station.CreateDate = DateTime.Now;
            station.LastModify=DateTime.Now;

            await _context.Stations.AddAsync(station);
            await _context.SaveChangesAsync();

            var result = new OperationStateResult
            {
                OperationType = OperationTypes.Insert,
                RowId = station.Id
            };
            
            return result;
        }

        public int DeleteStation(long stationId)
        {
            var station = GetStationById(stationId);

            var result = (int)_context.Stations.Remove(station).State;
            _context.SaveChanges();

            return result;
        }

        public int DeleteStation(Station station)
        {
            var result = (int)_context.Stations.Remove(station).State;
            _context.SaveChanges();

            return result;
        }

        private async Task<OperationStateResult> UpdateStation(Station station)
        {
            try
            {
                var modifyRow = _context.Stations.FirstOrDefault(x => x.Id == station.Id);
                modifyRow.AddressIp = station.AddressIp;
                modifyRow.StoreNumber = station.StoreNumber;
                modifyRow.Zone = station.Zone;
                modifyRow.LastModify = DateTime.Now;
                await _context.SaveChangesAsync();

                var result = new OperationStateResult
                {
                    OperationType = OperationTypes.Update,
                    RowId = station.Id,
                    Msg = "OK"
                };

                return result;
            }
            catch (Exception ex)
            {
                return new OperationStateResult
                {
                    OperationType = OperationTypes.Update,
                    RowId = -1,
                    Msg = $"{ ex.Message }\n{ ex.InnerException?.Message ?? string.Empty }"
                };
            }
        }

        private Station GetStationById(long stationId)
        {
            var result = _context.Stations.FirstOrDefault(x => x.Id == stationId);

            if (result is null)
                throw new Exception("Station is not found !");

            return result;
        }
        public async Task<OperationStateResult> InsertUpdateStation(InputDataStationDto inputDataStationDto)
        {
            OperationStateResult result;
            var station = _mapper.Map<Station>(inputDataStationDto);
            var stationHist = _mapper.Map<StationHistory>(inputDataStationDto);
            var chk = StationExist(station);

            if (!chk.IsExist)
                result = await InsertStation(station);
            else
            {
                station.Id = chk.Station.Id;
                result = await UpdateStation(station);
            }

            stationHist.Station = chk.Station;
            InsertHistoryStation(stationHist);

            return result;
        }
          
        // Station history

        public IEnumerable<StationHistory> GetHistoryStationByMacAddress(string macAddress)
        {
            return _context.StationHistory.Where(x => x.Station.MacAddress == macAddress).ToList();
        }

        public IEnumerable<StationHistory> GetHistoryStation(long id)
        {
            return _context.StationHistory.Where(x => x.Station.Id == id).ToList();
        }

        public IEnumerable<StationHistory> GetHistoryStation(string name)
        {
            return _context.StationHistory.Where(x => x.Station.Hostname == name).ToList();
        }

        private async void InsertHistoryStation(StationHistory stationHistory)
        {
            stationHistory.Id = DateTime.Now.Ticks;
            stationHistory.CreateDate = DateTime.Now;

            await _context.StationHistory.AddAsync(stationHistory);
            await _context.SaveChangesAsync();
        }
    }
}