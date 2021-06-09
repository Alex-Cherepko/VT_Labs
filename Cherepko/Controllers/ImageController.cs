using CherepkoLib.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cherepko.Controllers
{
    public class ImageController:Controller
    {
        UserManager<ApplicationUser> userManager;
        IWebHostEnvironment env;
        public ImageController(UserManager<ApplicationUser> UserManager, IWebHostEnvironment Env)
        {
            userManager = UserManager;
            env = Env;
        }
        public async Task<FileResult> GetAvatar()
        {
            var user = await userManager.GetUserAsync(User);
            if (user.AvatarImage != null)
                return File(user.AvatarImage, "image/...");
            else
            {
                var avatarPath = "/Images/avatar.png";
                return File(env.WebRootFileProvider
                .GetFileInfo(avatarPath)
                .CreateReadStream(), "image/...");
            }
        }
    }
}
