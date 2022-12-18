module Day10

open System.IO

let checkpoints = [ 20 .. 40 .. 220 ]

let parseLines lines =
    lines |> List.ofArray |> List.map (fun (line: string) -> line.Split(' '))

let rec getSignalStrength register cycle (lines: string array list) =
    if lines.IsEmpty then
        0
    else
        let actualCycle = cycle + 1
        let addToRegister = if List.contains actualCycle checkpoints then register * actualCycle else 0
        match lines.Head with
        | [| "addx"; num |] -> addToRegister + getSignalStrength register actualCycle ([| "add"; num |]::lines.Tail)
        | [| "add"; num |] -> addToRegister + getSignalStrength (register + int num) actualCycle lines.Tail
        | [| "noop" |] -> addToRegister + getSignalStrength register actualCycle lines.Tail
        | _ -> invalidArg (nameof lines) "Invalid operation"

let isSpriteVisible spritePosition pixelPosition =
    abs (spritePosition - pixelPosition) < 2

let rec drawDisplay register cycle (lines: string array list) =
    let actualCycle = (cycle + 1) % 40
    if actualCycle = 1 then
        printfn ""
    if isSpriteVisible register cycle then
        printf "#"
    else
        printf "."
    if lines.IsEmpty then
        ignore
    else
        match lines.Head with
        | [| "addx"; num |] -> drawDisplay register actualCycle ([| "add"; num |]::lines.Tail)
        | [| "add"; num |] -> drawDisplay (register + int num) actualCycle lines.Tail
        | [| "noop" |] -> drawDisplay register actualCycle lines.Tail
        | _ -> invalidArg (nameof lines) "Invalid operation"


let result =
    "inputs/10.txt" |> File.ReadAllLines |> parseLines |> getSignalStrength 1 0

let resultPartTwo =
    "inputs/10.txt" |> File.ReadAllLines |> parseLines |> drawDisplay 1 0 |> ignore
