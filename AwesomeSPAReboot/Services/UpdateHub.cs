using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace AwesomeSPAReboot.Services
{
    public class UpdateHub : Hub
    {
        private static Dictionary<string, ScheduleJob> _scheduledSearches = new Dictionary<string, ScheduleJob>();
        private static List<string> _recentSearches = new List<string>(); 
        private object _lock = new object();

        public void ListenToSearch(string searchTerm)
        {
            StopExistingSchedule();
            //var searchRepository = new SearchRepository();
            var schedule = new ScheduleJob(() => ScheduledNotification(searchTerm, Clients.Caller));
            schedule.Start(TimeSpan.FromSeconds(20).TotalMilliseconds);
            lock (_lock)
            {
                _scheduledSearches.Add(Context.ConnectionId, schedule);
                //searchRepository.SaveSearch(new Search{Term = searchTerm, TimeStamp = DateTime.Now});
                _recentSearches.Add(searchTerm);
            }
            //return Clients.All.updateSearchTerms(searchRepository.GetAll().Select(s => s.Term).Distinct().Take(20));
        }

        private void ScheduledNotification(string searchTerm, dynamic caller)
        {
            IImagesService imagesService = new ImagesService();
            var serializedData = JsonConvert.SerializeObject(imagesService.GetImagesFromTag(searchTerm));
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