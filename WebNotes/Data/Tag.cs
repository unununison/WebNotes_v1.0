using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Tag_text { get; set; }
        
        public IEnumerable<Note> Notes { get; set; }
    }
}
