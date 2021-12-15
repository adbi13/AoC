module Day08

open System
open System.IO
open System.Collections.Generic

let inputLines inputFile =
    File.ReadAllLines(inputFile)

let processLine (line:string) =
    Array.map (fun (word: string) -> word.ToCharArray() |> Array.sort |> set) (Array.filter (fun x -> (String.IsNullOrWhiteSpace x) = false) (line.Split(' ')))

let processInput (lines:string[]) part =
    Array.map (fun (line:string) -> processLine ((line.Split('|'))[part])) lines

let getNumberSet (numbers:Dictionary<Set<char>, int>) (number:int) =
    Seq.fold (fun result (keyValue: KeyValuePair<Set<char>,int>) -> if keyValue.Value = number then keyValue.Key else result) Set.empty numbers

let saveNumber (numbers:Dictionary<Set<char>, int>) (numberSet:Set<char>) =
    if numberSet.Count = 2 then numbers[numberSet] <- 1
    else if numberSet.Count = 3 then numbers[numberSet] <- 7
    else if numberSet.Count = 4 then numbers[numberSet] <- 4
    else if numberSet.Count = 5 then
        if ((getNumberSet numbers 7).IsSubsetOf numberSet) then
            numbers[numberSet] <- 3
        else if ((Set.difference (getNumberSet numbers 4) (getNumberSet numbers 1)).IsSubsetOf numberSet) then
            numbers[numberSet] <- 5
        else
            numbers[numberSet] <- 2
    else if numberSet.Count = 6 then
        if ((getNumberSet numbers 3).IsSubsetOf numberSet) then
            numbers[numberSet] <- 9
        else if ((getNumberSet numbers 7).IsSubsetOf numberSet) then
            numbers[numberSet] <- 0
        else numbers[numberSet] <- 6
    else if numberSet.Count = 7 then numbers[numberSet] <- 8

let saveNumbers (line:Set<char>[]) =
    let numbers = new Dictionary<Set<char>, int>()
    let _ = Array.iter (fun x -> saveNumber numbers x) (Array.sortBy (fun x -> x.Count) line)
    numbers

let sumNumbers (numbersDict:Dictionary<Set<char>, int>) (line:Set<char>[]) =
    fst (Array.fold (fun (sum, position) number -> (sum + (numbersDict[number] * position), position / 10)) (0, 1000) line)

let runNumbers =
    let lines = inputLines "08input.txt"
    let pattern = processInput lines 0
    let numberToCount = processInput lines 1
    Array.fold2 (fun sum actualPattern actualNumbers -> sum + (sumNumbers (saveNumbers actualPattern) actualNumbers)) 0 pattern numberToCount
