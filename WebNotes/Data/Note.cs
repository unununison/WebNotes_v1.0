using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Data
{
    public class Note{
        public int Id { get; set; }
        public string Note_head { get; set; } // заголовок
        public string Note_text { get; set; } // сама заметка


        public int? TagId { get; set; } //id привязанного тега
        public Tag Tag { get; set; }
    }
}
