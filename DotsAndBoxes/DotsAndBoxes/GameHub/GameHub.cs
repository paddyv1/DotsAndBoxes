using DotsAndBoxes.Shared.DTOs.Requests;
using Microsoft.AspNetCore.SignalR;

namespace DotsAndBoxes.GameHub
{
    public class GameHub : Hub
    {

        public async Task FindMatchAsync(FindMatchRequest matchRequest)
        {
            throw new NotImplementedException();
        }

        public async Task MakeMoveAsync(MakeMoveRequest moveRequest) 
        { 
            throw new NotImplementedException(); 
        }


        public async Task GetSnapshotAsync(GetSnapshotRequest snapshotRequest)
        {
            throw new NotImplementedException();
        }
    }
}
