
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

let readLine = Console.ReadLine()

let log (x: string) = Console.WriteLine x

let toInt(x: string) = Convert.ToInt32 x

let isNull (x: string) = match box obj with | null -> true | _ -> false 

let toTestedInt (x: string) =
    if isNull x || x.Trim().Length = 0 then -1 else toInt x

let fileName (fullPath: string) =
    fullPath.Substring(fullPath.LastIndexOf('\\') + 1)

let rec getIntInput(x: int): int =
    let inputStr = readLine
    let input = toTestedInt inputStr
    if (input = -1 || input >= x) then
        log msg_UnableToParseEx
        x |> getIntInput
    else
        input

let rec getIntInput_OptionalReturn(input: int, defaultReturn: int): int =  
    let input = readLine |> toTestedInt 
    if (input = -1 || input >= input) then defaultReturn else input

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

        fullPaths |> List.iteri (fun i f -> log $"{i}: {fileName f}")

        log "\nSearch by index (0) or by string (1) : Defualt 0."

        let searchByString = getIntInput_OptionalReturn(1, 0) = 1; // (Input, DefualtReturnValue)

        if (searchByString) then
            log "What filter would you like to apply?"
            let filterBy = Console.ReadLine().ToLower()
            fullPaths 
                |> Seq.where (fun x-> x.ToLower().Contains filterBy)
                |> Seq.sort
                |> Seq.toList
                |> List.iteri (fun i f -> log $"{i}: {fileName f}")

        log msg_GetSolutionIndex
        let _ = Process.Start(defaultApp, fullPaths.[getIntInput(fullPaths.Length)])
        _APP_SUCCESS
    with ex ->
        printfn "%A" ex 
        Console.WriteLine("Press enter to close this window.")
        Console.ReadLine()
        _APP_FAILURE 