using backend.Dtos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

List<TrackDto> tracks = [
  new (1, "Seima", "Metal"),  
  new (2, "dipa", "Pop"),  
  new (3, "Sasdeima", "Rock"),  
];

app.MapGet("/tracks", () => tracks);

app.MapGet("/tracks/{id}", (int id) => 
{
    return tracks.Find(track => track.Id == id);
}).WithName("GetTrack");

app.MapPost("/tracks", (TrackCreateDto trackCreate) =>
{
  TrackDto t = new (tracks.Count + 1, trackCreate.Name, trackCreate.Genre);
  tracks.Add(t);
  return Results.CreatedAtRoute("GetTrack", new {id = t.Id}, t);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
