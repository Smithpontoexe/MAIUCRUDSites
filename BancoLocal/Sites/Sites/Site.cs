﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Sites
{
    [Table("Sites")]
    public class Site
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        [MaxLength(250),Unique]
        public string Endereco { get; set; }

    }
}
