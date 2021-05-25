﻿using System.Text.Json;

namespace DocumentRegistry.Web.Models
{
    public class ResponseModel
    {
        public static T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}