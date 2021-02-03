using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Data
{
    public class Notification
    {
        public int Id { get; set; }
        public string Notification_text { get; set; } 
        public DateTime Notification_data{ get; set; } 
    }
}
