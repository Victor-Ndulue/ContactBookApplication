﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        [ForeignKey(nameof(Contact))]
        public int ContactId { get; set; }
    }
}
