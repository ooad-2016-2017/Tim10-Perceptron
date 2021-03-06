﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraktivnaMapaEvenata.UWP.Models
{
    public class EventState
    {
        public int EventStateId { get; set; }
        
        public string Description { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
