﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Data
{
    public interface IEntity
    {
        object Id { get; set; }
    }
}
