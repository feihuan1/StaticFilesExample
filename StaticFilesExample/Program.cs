// all file in solution used to be accessable by path request but its not secure, so now only wwwroot can beaccess by browser
// http://localhost:5252/doc.pdf   http://localhost:5252/img1.jpg   http://localhost:5252/sample.txt

// if change name of wwwroot, add option in builder below, also can configure multiple folder
using Microsoft.Extensions.FileProviders;

/// create wwwroot folder, only this name will show earth icon, but icon doesn't matter
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot"
});

var app = builder.Build();


// enable use wwwroot folder
app.UseStaticFiles();// work with web root path
// enable second folder
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        // contentRootPath is the path from root of pc to this project 
        //builder.Environment.ContentRootPath + "\\mywebroot"
        Path.Combine(builder.Environment.ContentRootPath, "mywebroot")
   )
});// work with "mywebroot path"

//app.MapGet("/", () => "Hello World!"); // same as useRouting and useEndpoints
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Hello");
    });
});

app.Run();
