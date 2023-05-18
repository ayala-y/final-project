using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

public partial class Skirt
{
    public int Id { get; set; }

    public string? ImgName { get; set; }

    public double? WaistCircumference { get; set; }

    public double? HipCircumference { get; set; }

    public double? SkirtLength { get; set; }

    public double? HeightHip { get; set; }

    public string? SkirtCutImgName { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    [NotMapped]
    public IFormFile files { get; set; }
}
