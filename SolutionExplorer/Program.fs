
open System
open System.IO
open System.Diagnostics

let isNull (x: string) = match box obj with | null -> true | _ -> false 

let toTestedInt (x: string) =
    if isNull(x) || x.Trim().Length = 0 then -1 else Convert.ToInt32(x)

[<EntryPoint>]
let main argv =
    let handled = false

    let folderPath = "C:\\Users\\mikei\\source\\repos\\"

    printfn $"Solutions in folder: {folderPath}\n"

    let files = Directory.GetFiles(folderPath, "*.sln", SearchOption.AllDirectories) 
                    |> Array.map Path.GetFileName 

    files |> Array.iteri (fun i v -> printfn $"{i}: {v}")

    let fullPaths = Directory.GetFiles(folderPath, "*.sln", SearchOption.AllDirectories) 
                    |> Array.map Path.GetFullPath 

    printfn $"\n\nType the number of the Solution you would like to run. . .\n"
    let line = Console.ReadLine()

    let index = toTestedInt(line)

    if (index = -1) then
        printfn $"{line} was unable to parse to an int. Please try again. . .\n"
    else 
        printfn $"Opening file. . .\n"

    let file = fullPaths.[index]

    let _ = Process.Start("explorer.exe", file)

    Environment.Exit(0)

    0 // return an integer exit code
