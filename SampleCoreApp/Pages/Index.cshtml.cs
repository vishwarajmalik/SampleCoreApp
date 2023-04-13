using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SampleCoreApp.Model;


namespace SampleCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Message = "";
        public string Error = "";
        //UserInfo UserInfo = new UserInfo();
        public void OnGet()
        {
        }


        public void OnPost()
        {
            try
            {
                string FirstName = Request.Form["FirstName"];
                string LastName = Request.Form["LastName"];

                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                {
                    Error = "FirstName and LastName are required fields";
                    return;
                }
                else
                {
                    UserInfo UserInfo = new UserInfo
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        FirstName = FirstName,
                        LastName = LastName,
                        CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ")
                    };

                    string UserJSON = JsonConvert.SerializeObject(UserInfo);

                    string FolderPath = (Directory.GetCurrentDirectory() + "\\UserData\\");
                    // Save user data in json file in UserData folder
                    // if UserData folder does not exists then create new folder
                    if (!Directory.Exists(FolderPath))
                    {
                        Directory.CreateDirectory(FolderPath);
                    }

                    System.IO.File.WriteAllText(FolderPath + "User.json", UserJSON);

                    Message = "User saved successfully.";
                }
            }
            catch (Exception ex)
            {

                Error = "Some error occured please try again.";

                _logger.Log(LogLevel.Error, ex, ex.ToString());
            }
        }


    }
}