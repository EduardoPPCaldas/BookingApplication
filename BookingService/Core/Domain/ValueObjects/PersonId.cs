using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.ValueObjects
{
    public class PersonId
    {
        public string? IdNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}