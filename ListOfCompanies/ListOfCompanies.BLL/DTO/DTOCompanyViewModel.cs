﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.BLL.DTO
{
    public class DTOCompanyViewModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
