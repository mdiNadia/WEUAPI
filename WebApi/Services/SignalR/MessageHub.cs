using Application.Helpers;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Services.SignalR
{
    public class MessageHub : Hub
    {

        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _tracker;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserAccessor _userAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public MessageHub(IMemoryCache memoryCache, IUserAccessor userAccessor, IUnitOfWork unitOfWork, IHubContext<PresenceHub> presenceHub,
            PresenceTracker tracker)
        {
            this._memoryCache = memoryCache;
            this._userAccessor = userAccessor;
            _unitOfWork = unitOfWork;
            _tracker = tracker;
            _presenceHub = presenceHub;

        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var username = httpContext.Request.GetCookie<string>("user");
            var groupName = GetGroupName(username.Trim(new char[] { '/', '"' }), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);
            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _unitOfWork.Messages.
                GetMessageThread(username.Trim(new char[] { '/', '"' }), otherUser);

            //if (_unitOfWork.HasChanges()) await _unitOfWork.CompleteAsync();
            await _unitOfWork.CompleteAsync();
            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string RecipientUsername, string Content)
        {
            //var username = Context.User.GetUsername();
            var httpContext = Context.GetHttpContext();
            var username = httpContext.Request.GetCookie<string>("user");
            if (username == RecipientUsername.ToLower())
                throw new HubException("You cannot send messages to yourself");

            var sender = await _userAccessor.GetUserByUsernameAsync(username.Trim(new char[] { '/', '"' }));
            var recipient = await _userAccessor.GetUserByUsernameAsync(RecipientUsername);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = Content
            };

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await _unitOfWork.Messages.GetMessageGroup(groupName);

            //این بلاک فقط برای اینه که مشخص بشه کاربر پیغام را سین کرده یا نه 
            //اگر سین نکرده بود میره به بلاک بعدی و براش نوتیفیکیشن میاد که پیغام جدید دارید
            if (group.Connections.Any(x => x.Username == recipient.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                //این قسمت فقط برای اینه که اگه کاربر در صفحه چت آنلاین و لاگین شده است یک نوتیفیکیشن براش بیاد که پیغام جدید دارید 
                var connections = await _tracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.UserName, knownAs = sender.KnownAs });
                }
                //این قسمت فقط برای اینه که اگه کاربر در صفحه چت آنلاین و لاگین شده است یک نوتیفیکیشن براش بیاد که پیغام جدید دارید 
            }

            _unitOfWork.Messages.AddMessage(message);

            var notification = new Domain.Entities.Notification()
            {
                CreationDate = DateTime.Now,
                Observer = (Profile)sender.Profiles,
                Target = (Profile)recipient.Profiles,
                NotificationType = NotificationType.message,
                Title = "New Message",
                Body = $"you hanve a new message from {sender.Profiles.First().Username}"
            };
            _unitOfWork.Notifications.Insert(notification);

            try
            {
                await _unitOfWork.CompleteAsync();
                await Clients.Group(groupName).SendAsync("ReceiveMessage", message.SenderUsername, message.Content);

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var httpContext = Context.GetHttpContext();
            var username = httpContext.Request.GetCookie<string>("user"); var group = await _unitOfWork.Messages.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, username);

            if (group == null)
            {
                group = new Group(groupName);
                _unitOfWork.Messages.AddGroup(group);
            }

            group.Connections.Add(connection);


            try
            {
                await _unitOfWork.CompleteAsync();
                return group;
            }
            catch (Exception err)
            {
                throw new HubException("Failed to join group");
            }
            throw new HubException("Failed to join group");
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _unitOfWork.Messages.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _unitOfWork.Messages.RemoveConnection(connection);

            try
            {
                await _unitOfWork.CompleteAsync();
                return group;
            }
            catch (Exception err)
            {
                throw new HubException("Failed to remove from group");

            }
            throw new HubException("Failed to remove from group");
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}_{other}" : $"{other}_{caller}";
        }
    }
}