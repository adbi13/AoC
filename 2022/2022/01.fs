module Day01

open System.IO

let linesList inputFile = 
    List.ofArray (File.ReadAllLines inputFile)


let countElfCalories (caloriesList: int list) line =
    match line with
    | "" -> 0 :: caloriesList
    | _ -> caloriesList.Head + int line :: caloriesList.Tail


let countAllElvesCalories lineList =
    List.fold countElfCalories [ 0 ] lineList


let result =
    List.max (countAllElvesCalories (linesList "inputs/01.txt"))


let rec sumFirstNElements (caloriesList: int list) n =
    match n with
    | 0 -> 0
    | _ -> caloriesList.Head + sumFirstNElements caloriesList.Tail (n - 1)


let resultPartTwo =
    let sortedCaloriesList = List.sortDescending (countAllElvesCalories (linesList "inputs/01.txt"))
    sumFirstNElements sortedCaloriesList 3
