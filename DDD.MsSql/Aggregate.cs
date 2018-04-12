using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.MsSql
{
    public class Aggregate
    {
        [Index]
        public Guid Id { get; set; }
        [Index]
        [MaxLength(100)]
        public string Type { get; set; }
        public string Events { get; set; }

    }
}