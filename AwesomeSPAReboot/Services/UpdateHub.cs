using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace AwesomeSPAReboot.Services
{
    public class UpdateHub : Hub
    {
        private readonly IImagesService _imagesService;
        private readonly ISearchRepository _searchRepository;

        private static readonly Dictionary<string, ScheduleJob> _scheduledSearches = new Dictionary<string, ScheduleJob>();
        private readonly object _lock = new object();

        public UpdateHub(IImagesService imagesService, ISearchRepository searchRepository)
        {
            _imagesService = imagesService;
            _searchRepository = searchRepository;
        }

        public void ListenToSearch(string searchTerm, int frequency)
        {
            StopExistingSchedule();
            var schedule = new ScheduleJob(() => ScheduledNotification(searchTerm, Clients.Caller));
            schedule.Start(TimeSpan.FromSeconds(frequency).TotalMilliseconds);
            lock (_lock)
            {
                _scheduledSearches.Add(Context.ConnectionId, schedule);
            }
        }

        public void PublishSearches()
        {
            var searches =
                _searchRepository.GetAll().GroupBy(s => s.Term).OrderByDescending(g => g.Count()).Select(g => g.Key);
            Clients.All.updateSearchTerms(searches);
        }

        public void Unsubscribe()
        {
            StopExistingSchedule();
        }

        private void ScheduledNotification(string searchTerm, dynamic caller)
        {
            var serializedData = JsonConvert.SerializeObject(_imagesService.GetImages(searchTerm, true));
            caller.update(serializedData);
        }

        private void StopExistingSchedule()
        {
           if(_scheduledSearches.ContainsKey(Context.ConnectionId))
           {
               lock(_lock)
               {
                   var oldSchedule = _scheduledSearches[Context.ConnectionId];
                   oldSchedule.Stop();
                   _scheduledSearches.Remove(Context.ConnectionId);
               }
           }
        }

        public override Task OnConnected()
        {
            return Clients.All.joined(Context.ConnectionId, DateTime.Now.ToString());
        }

        public override Task OnDisconnected()
        {
            return Clients.All.leave(Context.ConnectionId, DateTime.Now.ToString());
        }

        public override Task OnReconnected()
        {
            return Clients.All.rejoined(Context.ConnectionId, DateTime.Now.ToString());
        }
    }
}