module Day06

open System
open System.IO

let inputLine inputFile =
    (Array.map Int32.Parse ((File.ReadAllLines(inputFile)[0]).Split(','))) |> Array.toList

let updateList (fishList:list<int64>) days =
    [for value in [0..8] -> if value = days then fishList[value] + int64 1 else fishList[value]]

let saveFishs line =
    List.fold updateList [for _ in [0..8] -> int64 0] line

let processDay (fishDays:list<int64>) =
    let newFish = fishDays[0]
    [for days in [1..8] -> if days = 7 then fishDays[7] + newFish else fishDays[days]] @ [newFish]

let rec dayCounter fishList days =
    if days = 0 then fishList else dayCounter (processDay fishList) (days - 1)

let runLanternFish =
    let init = saveFishs (inputLine "06input.txt")
    Seq.fold (fun sum x -> sum + x) (int64 0) (dayCounter init 256)
