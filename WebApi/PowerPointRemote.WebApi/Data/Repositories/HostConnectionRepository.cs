using System.Collections.Generic;

namespace PowerPointRemote.WebAPI.Data.Repositories
{
    public class HostConnectionRepository : IHostConnectionRepository
    {
        public HostConnectionRepository()
        {
            Data = new Dictionary<string, string>();
        }

        private Dictionary<string, string> Data { get; }

        public void SetConnection(string channelId, string connectionId)
        {
            Data[channelId] = connectionId;
        }

        public void RemoveConnection(string channelId)
        {
            Data.Remove(channelId);
        }

        public string GetConnection(string channelId)
        {
            Data.TryGetValue(channelId, out var connectionId);
            return connectionId;
        }
    }
}