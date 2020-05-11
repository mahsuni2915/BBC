﻿using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class Content : EntityBase<int>
    {
        public string ContentText { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string MainImage { get; set; }

        [ForeignKey("TarifandReceteId")]
        public int TarifandReceteId { get; set; }
        public TarifAndRecete TarifAndRecete { get; set; }
        
    }
}
