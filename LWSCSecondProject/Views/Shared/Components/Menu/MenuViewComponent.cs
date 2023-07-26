using Microsoft.AspNetCore.Mvc;

namespace LWSCSecondProject.Views.Shared.Components.Menu
{
    public class MenuViewComponent :ViewComponent
    {
       

        public async Task<IViewComponentResult> InvokeAsync(string myAppName)
        {

            return await Task.FromResult<IViewComponentResult>(View(model:myAppName));
        }
    }
}
