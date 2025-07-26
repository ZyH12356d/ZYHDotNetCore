using Newtonsoft.Json;
using System.Linq;
using ZYHDotNetCore.BirdAPI.Models.BirdModel;

namespace ZYHDotNetCore.BirdAPI.Endpoints.BirdEndpoints
{
    public static class BirdEndpoint
    {
        public static void MapBirdEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/birds" , ()  =>
            {
                string filePath = "wwwroot/Data/Birds.json";
                var jsonStr = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                if (result == null || result.Tbl_Bird == null)
                {
                    return Results.NotFound("No birds found.");
                }
                else
                {
                    return Results.Ok(result.Tbl_Bird);
                }
            });

            app.MapPost("/birds" , (Tbl_Bird responseModel)  =>
            {
                string filePath = "wwwroot/Data/Birds.json";
                var jsonStr = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                responseModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1; // Assign new ID
                result.Tbl_Bird.Add(responseModel);

                var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(filePath, updatedJsonStr);

                return Results.Created($"/birds/{responseModel.Id}", responseModel);
            });

            app.MapGet("/birds/{id:int}", (int id) =>
            {
                string filePath = "wwwroot/Data/Birds.json";
                var jsonStr = File.ReadAllText(filePath);
                var items = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);
                var result = items.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (result == null)
                {
                    return Results.NotFound($"Bird with ID {id} not found.");
                }
                else
                {
                    return Results.Ok(result);
                }
            });
        }
    }
}
