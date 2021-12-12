module Day01

open System.IO
open System

let measuresList inputFile = 
    Array.map int (File.ReadAllLines(inputFile))

let compareWithPrevious (count, a, b, c) actual =
    if actual + b + c > a + b + c then (count + 1, b, c, actual) else (count, b, c, actual)

let countOfGreaterThanPrevious measures =
    Array.fold compareWithPrevious (0, Int32.MaxValue / 3, Int32.MaxValue / 3, Int32.MaxValue / 3) measures

let fst4 (a, _, _, _) =
    a

let result =
    fst4 (countOfGreaterThanPrevious (measuresList "01input.txt"))
