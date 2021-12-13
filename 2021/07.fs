module Day07

open System
open System.IO

let inputLine inputFile =
    (Array.map Int32.Parse ((File.ReadAllLines(inputFile)[0]).Split(','))) |> Array.toList

let median numbers =
    (List.sort numbers)[numbers.Length / 2]

let mean numbers =
    (List.fold (fun sum actual -> sum + actual) 0 numbers) / numbers.Length

let rec pathCost num =
    if num = 0 then 0 else num + pathCost (num - 1)

let sumDistance numbers point =
    List.fold (fun sum actual -> sum + (pathCost (abs (actual - point)))) 0 numbers

let runCrabs =
    let (inputNumbers:list<int>) = inputLine "07input.txt"
    sumDistance inputNumbers (mean inputNumbers)
