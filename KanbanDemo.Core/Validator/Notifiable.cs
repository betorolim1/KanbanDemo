using KanbanDemo.Core.Validator.Interfaces;
using System.Collections.Generic;

namespace KanbanDemo.Core.Validator
{
    public class Notifiable : INotifiable
    {
        private List<string> _notifications = new List<string>();

        public bool IsValid => _notifications.Count == 0;

        public List<string> Notifications => _notifications;

        protected void AddNotifications(List<string> notifications)
        {
            if (notifications is null)
                return;

            _notifications.AddRange(notifications);
        }

        protected void AddNotification(string notification)
        {
            if (notification is null)
                return;

            _notifications.Add(notification);
        }
    }
}
