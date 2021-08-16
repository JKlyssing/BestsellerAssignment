using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BestsellerNET.Models;

namespace BestsellerNET.Services
{
    public class JsonProductService
    {
        //Contructer to keep webhostEnvironment and help navigate to data.json, ensuring consistency across platforms
        public JsonProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        //Path to data
        private string JsonFile
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "data.json"); }
        }

        //Return iterable enumerable of custom product class with products from data.json
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFile))
            {
                return JsonSerializer.Deserialize<Rootobject>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).Products;
            }
        }

        //Return custom Category object with all categories inside from data.json
        public Category GetCategories()
        {
            using (var jsonFileReader = File.OpenText(JsonFile))
            {
                return JsonSerializer.Deserialize<Rootobject>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).Categories;
            }
        }
    }
}
