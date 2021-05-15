using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppSample.Pages
{
    public class CalcModel : PageModel
    {
        public string Op { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public int Result { get; private set; }
        public void OnGet(string op, int x, int y)
        {
            Op = op;
            X = x;
            Y = y;
            Result = Op switch
            {
                "add" => X + Y,
                "sub" => X - Y,
                "mul" => X * Y,
                "div" => X / Y,
                _ => X + Y
            };
        }
    }
}
