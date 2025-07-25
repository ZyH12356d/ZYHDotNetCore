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
                if (result == null || result.Tbl_Bird == null || result.Tbl_Bird.Length == 0)
                {
                    return Results.NotFound("No birds found.");
                }
                else
                {
                    return Results.Ok(result.Tbl_Bird);
                }
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
