module Day05

open System
open System.IO
open System.Collections.Generic

let inputLines inputFile =
    File.ReadAllLines(inputFile)

let processLine (line:string) =
    Array.filter (fun x -> (String.IsNullOrWhiteSpace x) = false) (line.Split(" ->,".ToCharArray()))

let processInput (lines:string[]) =
    Array.map (fun (line:string) -> Array.map Int32.Parse (processLine line)) lines

let positions = new Dictionary<int * int, int>()

let processRow col1 col2 row =
    let colStep = if col1 > col2 then -1 else 1
    Seq.iter (fun x -> positions.[(row, x)] <- (positions.GetValueOrDefault((row, x), 0) + 1)) [col1..colStep..col2]

let processCol row1 row2 col =
    let rowStep = if row1 > row2 then -1 else 1
    Seq.iter (fun x -> positions.[(x, col)] <- (positions.GetValueOrDefault((x, col), 0) + 1)) [row1..rowStep..row2]

let processDiagonal row1 row2 col1 col2 =
    let rowStep = if row1 > row2 then -1 else 1
    let colStep = if col1 > col2 then -1 else 1
    Seq.iter2 (fun row col -> positions.[(row, col)] <- (positions.GetValueOrDefault((row, col), 0) + 1)) [row1..rowStep..row2] [col1..colStep..col2]

let saveLine (line:int[]) =
    if line[0] = line[2] then processRow line[1] line[3] line[0] 
    else if line[1] = line[3] then processCol line[0] line[2] line[1]
    else processDiagonal line[0] line[2] line[1] line[3]

let savePositions lines =
    Seq.iter saveLine lines

let runVents =
    let initPositions = savePositions (processInput (inputLines "05input.txt"))
    Seq.fold (fun count over -> if over > 1 then count + 1 else count) 0 [for value in positions -> value.Value]
