module Day02

open System.IO

type Shape =
    | Rock = 1
    | Paper = 2
    | Scissors = 3

type PlayerMove =
    | Player of Shape
    | Opponent of Shape
    
let compareShapes first second =
    match first, second with
    | a, b when a = b -> 3
    | Shape.Rock, Shape.Scissors
    | Shape.Paper, Shape.Rock
    | Shape.Scissors, Shape.Paper -> 0
    | _ -> 6

let points (player1, player2)=
    match player1, player2 with
    | Player shape1, Opponent shape2 -> compareShapes shape2 shape1 + int shape1
    | Opponent shape1, Player shape2 -> compareShapes shape1 shape2 + int shape2
    | _ -> invalidArg (nameof player1 + nameof player2) "Invalid player combination"

let shapeFromChar ch =
    match ch with
    | 'A' | 'X' -> Shape.Rock
    | 'B' | 'Y' -> Shape.Paper
    | 'C' | 'Z' -> Shape.Scissors
    | _ -> invalidArg (nameof ch) "Invalid input character"

let resultFromChar ch =
    match ch with
    | 'X' -> 0
    | 'Y' -> 3
    | 'Z' -> 6
    | _ -> invalidArg (nameof ch) "Invalid input character"

let pointsFromCombination opponent result =
    match opponent, result with
    | 'A', 'X' -> 3
    | 'A', 'Y' -> 4
    | 'A', 'Z' -> 8
    | 'B', 'X' -> 1
    | 'B', 'Y' -> 5
    | 'B', 'Z' -> 9
    | 'C', 'X' -> 2
    | 'C', 'Y' -> 6
    | 'C', 'Z' -> 7
    | _ -> invalidArg (nameof opponent) "Invalid input"

let pointsFromLine (line: string) =
    pointsFromCombination line[0] line[2]

let lineToMoves (line: string) =
    Opponent (shapeFromChar line[0]), Player (shapeFromChar line[2])

let movesList inputFile = 
    Array.map lineToMoves (File.ReadAllLines inputFile)

let result =
    Array.fold (fun score nextMove -> score + points nextMove) 0 (movesList "inputs/02.txt")

let resultPartTwo =
    Array.sumBy pointsFromLine (File.ReadAllLines "inputs/02.txt")
