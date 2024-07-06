using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace stylepeek.Pages.Import;

public class Index : PageModel
{
    public List<string> Files => filenames;
    private static List<string> filenames = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetHtmlFiles(string folder = "")
    {
        Console.WriteLine(nameof(OnGetHtmlFiles));
        Console.WriteLine(folder);
        string actual_folder = folder.Replace("~", "/home/nick/");
        Directory.Exists(actual_folder).Dump("exists?");

        var grepper = new Grepper()
        {
            RootPath = actual_folder,
            FileSearchMask = "*.html",
            Recursive = false
        };

        filenames = grepper.GetFileNames().ToList();
        Console.WriteLine($"total filenames {filenames.Count}");
        filenames.Dump("files");

        return Partial("_ImportList", this);
    }
}