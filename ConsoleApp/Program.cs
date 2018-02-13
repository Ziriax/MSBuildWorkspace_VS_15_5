using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async Task Run()
        {
            var path = Path.Combine("MainProject", "MainProject.csproj");
            while (!File.Exists(path) && path.Length < 255)
            {
                path = Path.Combine("..", path);
            }

            using (var workspace = MSBuildWorkspace.Create())
            {
                workspace.WorkspaceFailed += Workspace_WorkspaceFailed;
                var project = await workspace.OpenProjectAsync(path);
                var compilation = await project.GetCompilationAsync();
                var diagnostics = compilation.GetDiagnostics();
                Console.WriteLine(string.Join(Environment.NewLine, diagnostics));
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        private static void Workspace_WorkspaceFailed(object sender, Microsoft.CodeAnalysis.WorkspaceDiagnosticEventArgs e)
        {
            Console.WriteLine(e.Diagnostic);
        }
    }
}
