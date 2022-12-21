module Day14

open System.IO

let saveRock (plan: char array array) (fromPosition: int array) (toPosition: int array) =
    if fromPosition[0] = toPosition[0] then
        let row = fromPosition[0]
        let minCol = min fromPosition[1] toPosition[1]
        let maxCol = max fromPosition[1] toPosition[1]
        for col in [minCol..maxCol] do
            plan[row][col] <- '#'
    else
        let col = fromPosition[1]
        let minRow = min fromPosition[0] toPosition[0]
        let maxRow = max fromPosition[0] toPosition[0]
        for row in [minRow..maxRow] do
            plan[row][col] <- '#'
    toPosition

let saveLine plan (line: string array) =
    line
    |> Array.map (fun line -> Array.map int (line.Split(",")))
    |> Array.reduce (saveRock plan)
    |> ignore
    plan

let saveInput plan inputFile =
    inputFile
    |> File.ReadAllLines
    |> Array.map (fun line -> line.Split(" -> "))
    |> Array.map (saveLine plan)
    |> ignore
    for row in [0..999] do
        plan[row][163] <- '#'
    plan

let moveSand (plan: char array array) (position: int * int) =
    let row = fst position
    let col = snd position
    if plan[row][col + 1] = '.' then
        row, col + 1
    elif plan[row - 1][col + 1] = '.' then
        row - 1, col + 1
    elif plan[row + 1][col + 1] = '.' then
        row + 1, col + 1
    else
        row, col

let addSandUnit (plan: char array array) (startPosition: int * int) =
    let mutable actualPosition = startPosition
    let mutable nextPosition = moveSand plan actualPosition
    let mutable lastUnit = false
    if actualPosition = nextPosition then
        true
    else
        while actualPosition <> nextPosition do
            actualPosition <- nextPosition
            nextPosition <- moveSand plan actualPosition
            if snd nextPosition >= 199 || snd nextPosition <= 0 then
                lastUnit <- true
                actualPosition <- nextPosition
        if not lastUnit then
            plan[fst actualPosition][snd actualPosition] <- 'o'
        lastUnit

let result =
    let mutable slice = [| for _ in [1..1000] -> [| for _ in [1..200] -> '.'|] |]
    slice <- saveInput slice "inputs/14.txt"
    let mutable counter = 0
    while not (addSandUnit slice (500, 0)) do
        counter <- counter + 1
    counter + 1
