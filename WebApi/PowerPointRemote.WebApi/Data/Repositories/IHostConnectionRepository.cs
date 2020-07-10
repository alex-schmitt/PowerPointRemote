namespace PowerPointRemote.WebAPI.Data.Repositories
{
    public interface IHostConnectionRepository
    {
        void SetConnection(string channelId, string connectionId);
        void RemoveConnection(string channelId);
        string GetConnection(string channelId);
    }
}