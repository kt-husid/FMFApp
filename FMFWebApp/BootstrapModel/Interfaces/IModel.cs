using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootstrapWebApplication.Models
{
    public interface IModel
    {
        int Id { get; set; }
    }
}