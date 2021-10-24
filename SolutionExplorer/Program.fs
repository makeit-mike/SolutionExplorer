
open System
open System.IO
open System.Diagnostics

// BLOCK: BEGIN GLOBALS
let folderPath = "C:\\Users\\mikei\\source\\repos\\"
let defaultApp = "explorer.exe"

let _APP_SUCCESS = 0
let _APP_FAILURE = -1

let msg_GetSolutionIndex = "\n\nType the number of the Solution you would like to run. . .\n"
let msg_UnableToParseEx = "Unable to parse to an int. Please try again. . .\n"

// BLOCK: BEGIN FUNCTIONS
let log (x: string) = Console.WriteLine x

let isNull (x: string) = match box obj with | null -> true | _ -> false 

let toTestedInt (x: string) =
    if isNull(x) || x.Trim().Length = 0 then -1 else Convert.ToInt32(x)

let fileName (fullPath: string) =
    fullPath.Substring(fullPath.LastIndexOf('\\') + 1)

let rec getProperFileIndex(x: int): int =
    let input = toTestedInt (Console.ReadLine())
    if (input = -1 || input >= x) then
        log msg_UnableToParseEx
        getProperFileIndex(x)
    else
        input

let getFullFilePaths(folderPath: string, fileType: string) = 
     Directory.GetFiles(folderPath, fileType, SearchOption.AllDirectories) 
            |> Array.map Path.GetFullPath 
            |> Array.toList 
            |> List.sort

// BLOCK: BEGIN ENTRY POINT
[<EntryPoint>]
let main argv =
    try
        log $"Solutions in folder: {folderPath}\n"

        let fullPaths = getFullFilePaths(folderPath, "*.sln")

        fullPaths 
            |> List.iteri (fun i f -> log $"{i}: {fileName f}")

        log msg_GetSolutionIndex

        let _ = Process.Start(defaultApp, fullPaths.[getProperFileIndex(fullPaths.Length)])
        _APP_SUCCESS
    with ex ->
        printfn "%A" ex 
        _APP_FAILURE 