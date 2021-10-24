
open System
open System.IO
open System.Diagnostics

let isNull (x: string) = match box obj with | null -> true | _ -> false 

let toTestedInt (x: string) =
    if isNull(x) || x.Trim().Length = 0 then -1 else Convert.ToInt32(x)

let fileName (fullPath: string) =
    fullPath.Substring(fullPath.LastIndexOf('\\') + 1)

let rec getProperFileIndex(x: int): int =
    let input = toTestedInt (Console.ReadLine())
    if (input = -1 || input >= x) then
        printfn "Unable to parse to an int. Please try again. . .\n"
        getProperFileIndex(x)
    else
        input

let getFullFilePaths(folderPath: string, fileType: string) = 
     Directory.GetFiles(folderPath, fileType, SearchOption.AllDirectories) 
            |> Array.map Path.GetFullPath 
            |> Array.toList 
            |> List.sort

[<EntryPoint>]
let main argv =
    try
        let folderPath = "C:\\Users\\mikei\\source\\repos\\" // Replace with your Source/Repo folder that has all your SLN files.

        printfn $"Solutions in folder: {folderPath}\n"

        let fullPaths = getFullFilePaths(folderPath, "*.sln")

        fullPaths 
            |> List.iteri (fun i f -> printfn $"{i}: {fileName f}")

        printfn $"\n\nType the number of the Solution you would like to run. . .\n"

        let _ = Process.Start("explorer.exe", fullPaths.[getProperFileIndex(fullPaths.Length)])
        0
    with ex ->
        printfn "%A" ex 
        -1 