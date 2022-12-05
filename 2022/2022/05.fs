module Day05

open System.IO

let rec getStackLines lines =
    match lines with
    | _::neck::tail when neck = "" -> [], tail
    | head::tail ->
        let stacks, manual = getStackLines tail
        head::stacks, manual
    | _ -> invalidArg (nameof lines) "Invalid input format"

let parseLine (line: string)  (stacks: char list array)=
    let mutable stack = 0
    for index in 1 .. 4 .. line.Length do
        if line[index] <> ' ' then
            stacks[stack] <- stacks[stack] @ [ line[index] ]
        stack <- stack + 1
    stacks

let readStackLines (lines: string list) =
    let mutable stacks = [| for _ in 1 .. 4 .. lines[0].Length -> [] |]
    for line in lines do
        stacks <- parseLine line stacks
    stacks

let makeMove (stacks: char list array) fromStack toStack =
    match stacks[fromStack] with
    | head::tail ->
        stacks[fromStack] <- tail
        stacks[toStack] <- head::stacks[toStack]
    | _ -> invalidArg (nameof fromStack) "Source stack is empty"

let makeAllMoves stacks (manualLines: string list) =
    for line in manualLines do
        let splittedManualLine =  line.Split(' ')
        for _ in 1..int splittedManualLine[1] do
            makeMove stacks (int splittedManualLine[3] - 1) (int splittedManualLine[5] - 1)
    stacks

let moveMultiple (stacks: char list array) count fromStack toStack=
    let movingPart = stacks[fromStack][..count-1]
    stacks[fromStack] <- stacks[fromStack][count..]
    stacks[toStack] <- movingPart @ stacks[toStack]

let makeAllMultipleMoves stacks (manualLines: string list) =
    for line in manualLines do
        let splittedManualLine =  line.Split(' ')
        moveMultiple stacks (int splittedManualLine[1]) (int splittedManualLine[3] - 1) (int splittedManualLine[5] - 1)
    stacks

let result =
    let stackLines, manualLines = "inputs/05.txt" |> File.ReadAllLines |> List.ofArray |> getStackLines
    let mutable stacks = readStackLines stackLines

    stacks <- makeAllMoves stacks manualLines

    Array.fold (fun actual (stack: char list) -> actual + string stack.Head) "" stacks

let resultPartTwo =
    let stackLines, manualLines = "inputs/05.txt" |> File.ReadAllLines |> List.ofArray |> getStackLines
    let mutable stacks = readStackLines stackLines

    stacks <- makeAllMultipleMoves stacks manualLines

    Array.fold (fun actual (stack: char list) -> actual + string stack.Head) "" stacks
