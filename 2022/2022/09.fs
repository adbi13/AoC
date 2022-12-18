module Day09

open System.IO

type Coordinates = { X: int; Y: int }

let parseInput inputFile =
    inputFile |> File.ReadAllLines |> Array.map (fun line -> line.Split(' '))

let touching (head: Coordinates) (tail: Coordinates) =
    tail.X <= head.X + 1 && tail.X >= head.X - 1 && tail.Y <= head.Y + 1 && tail.Y >= head.Y - 1

let intAverage intList =
    intList |> List.map float |> List.average |> int

let makeMoveInDirection (headPosition: Coordinates) (tailPosition: Coordinates) (direction: Coordinates) =
    let actualHeadPosition = { X = headPosition.X + direction.X; Y = headPosition.Y + direction.Y }
    if not (touching actualHeadPosition tailPosition) then
        if actualHeadPosition.X = tailPosition.X then
            actualHeadPosition, { X = tailPosition.X; Y = intAverage [ actualHeadPosition.Y; tailPosition.Y ] }
        elif actualHeadPosition.Y = tailPosition.Y then
            actualHeadPosition, { X = intAverage [ actualHeadPosition.X; tailPosition.X ]; Y = tailPosition.Y }
        else
            let updateX = if actualHeadPosition.X > tailPosition.X then 1 else -1
            let updateY = if actualHeadPosition.Y > tailPosition.Y then 1 else -1
            actualHeadPosition, { X = tailPosition.X + updateX; Y = tailPosition.Y + updateY}
    else
        actualHeadPosition, tailPosition

let makeNMovesInDirection n (headPosition: Coordinates) (tailPosition: Coordinates) (direction: Coordinates) =
    let mutable tailVisited = Set.empty
    let mutable actualHeadPosition = headPosition
    let mutable actualTailPosition = tailPosition
    for _ in 1..n do
        let loopHeadPosition, loopTailPosition = makeMoveInDirection actualHeadPosition actualTailPosition direction
        actualHeadPosition <- loopHeadPosition
        actualTailPosition <- loopTailPosition
        printfn $"X: {actualTailPosition.X} Y: {actualTailPosition.Y}"
        printfn $"X: {actualHeadPosition.X} Y: {actualHeadPosition.Y}"
        tailVisited <- tailVisited.Add(actualTailPosition)
    printfn ""
    actualHeadPosition, actualTailPosition, tailVisited

let makeMovesFromLine line headPosition tailPosition =
    match line with
    | [| "D"; count |] -> makeNMovesInDirection (int count) headPosition tailPosition { X = 0; Y = -1 }
    | [| "U"; count |] -> makeNMovesInDirection (int count) headPosition tailPosition { X = 0; Y = 1 }
    | [| "L"; count |] -> makeNMovesInDirection (int count) headPosition tailPosition { X = -1; Y = 0 }
    | [| "R"; count |] -> makeNMovesInDirection (int count) headPosition tailPosition { X = 1; Y = 0 }
    | _ -> invalidArg (nameof line) "Invalid move"

let makeAllMoves (lines: string array array) =
    let mutable tailVisited = Set.empty
    let mutable actualHeadPosition = { X = 0; Y = 0 }
    let mutable actualTailPosition = { X = 0; Y = 0 }
    for line in lines do
        let loopHeadPosition, loopTailPosition, newlyTailVisited = makeMovesFromLine line actualHeadPosition actualTailPosition
        actualHeadPosition <- loopHeadPosition
        actualTailPosition <- loopTailPosition
        tailVisited <- tailVisited + newlyTailVisited
    tailVisited

let result =
    ("inputs/09.txt" |> parseInput |> makeAllMoves).Count
