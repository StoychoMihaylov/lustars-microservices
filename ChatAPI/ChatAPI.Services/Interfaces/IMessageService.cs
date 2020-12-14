using ChatAPI.Data.Entities;
using System.Threading.Tasks;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task CreateNewMessage(ChatMessage message);
    }
}
