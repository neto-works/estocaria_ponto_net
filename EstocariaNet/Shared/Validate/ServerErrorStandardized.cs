using Microsoft.AspNetCore.Mvc;

namespace EstocariaNet.Shared.Validate
{
    public class ServerErrorStandardized
    {
        public static IActionResult Error500(ControllerBase controller ,Exception ex){
            return controller.StatusCode(500, $"Internal server error : {ex.Message}");
        }
    }
}