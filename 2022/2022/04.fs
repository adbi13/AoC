module Day04

open System.IO

let processInputTuples (linePart: string) =
    linePart.Split '-' |> Array.map int

let processInput lineArray =
    Array.map (fun (line: string) -> line.Split ',' |> Array.map processInputTuples) lineArray

let fullyContains (pairs: int array array) =
    let pair1 = pairs[0]
    let pair2 = pairs[1]
    (pair1[0] <= pair2[0] && pair2[1] <= pair1[1]) || (pair2[0] <= pair1[0] && pair1[1] <= pair2[1])

let countFullyContaingPairs pairs =
    (Array.filter fullyContains pairs).Length

let overlaps (pairs: int array array) =
    let pairSet1 = set [pairs[0][0]..pairs[0][1]]
    let pairSet2 = set [pairs[1][0]..pairs[1][1]]
    not (Set.intersect pairSet1 pairSet2).IsEmpty

let countOverlapingPairs pairs =
    (Array.filter overlaps pairs).Length

let result =
    "inputs/04.txt" |> File.ReadAllLines |> processInput |> countFullyContaingPairs

let resultPartTwo =
    "inputs/04.txt" |> File.ReadAllLines |> processInput |> countOverlapingPairs
