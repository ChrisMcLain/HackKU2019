using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HackKU2019.Models
{
    public class ResultsModel
    {
        public IUser User { get; set; }
        public List<Warning> Warnings { get; set; }
    }
}