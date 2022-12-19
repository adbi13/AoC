module Day12

open System.IO

type Grid = (char * int) array array

let directions = [|
    (-1, 0);
    (1, 0);
    (0, 1);
    (0, -1);
|]

let initPositionLine (line: string) =
    line.ToCharArray() |> Array.map (fun ch -> (ch, System.Int32.MaxValue - 1))

let refreshPositionLine (line: (char * int) array) =
    Array.map (fun position -> (fst position, System.Int32.MaxValue - 1)) line

let processInput inputFile =
    inputFile |> File.ReadAllLines |> Array.map initPositionLine

let rec findCharPosition ch (grid: Grid) x y =
    if fst (grid[x][y]) = ch then
        x, y
    elif y + 1 = grid[x].Length && x + 1 = grid.Length then
        -1, -1
    elif y + 1 = grid[x].Length then
        findCharPosition ch grid (x + 1) 0
    else
        findCharPosition ch grid x (y + 1)

let minByDistance (grid: Grid) (a: int * int) (b: int * int) =
    if snd (grid[fst a][snd a]) < snd (grid[fst b][snd b]) then
        a
    else
        b

let isInGrid (grid: Grid) (position: int * int) =
    0 <= fst position && fst position < grid.Length
    && 0 <= snd position && snd position < grid[0].Length

let getElevation ch =
    match ch with
    | 'S' -> int 'a'
    | 'A' -> int 'a'
    | 'E' -> int 'z'
    | _ -> int ch

let checkElevationDifference (grid: Grid) (actual: int * int) (neighbor: int * int) =
    let actualChar = fst (grid[fst actual][snd actual])
    let neighborChar = fst (grid[fst neighbor][snd neighbor])
    getElevation actualChar >= getElevation neighborChar - 1

let dequeueMinimum (grid: Grid) (queue: (int * int) list) =
    let minimum = List.fold (minByDistance grid) queue.Head queue
    (List.filter (fun item -> item <> minimum) queue), minimum

let updatePosition (grid: Grid) (actual: int * int) (neighbor: int * int) =
    let actualDistance = snd (grid[fst actual][snd actual]) + 1
    let neighborDistance = snd (grid[fst neighbor][snd neighbor])
    if actualDistance < neighborDistance then
        grid[fst neighbor][snd neighbor] <- (fst (grid[fst neighbor][snd neighbor]), actualDistance)

let findShortestPath (grid: Grid) (inputQueue: (int * int) list) =
    let mutable queue = inputQueue
    while not queue.IsEmpty do
        let actualQueue, actual = dequeueMinimum grid queue
        queue <- actualQueue
        for direction in directions do
            let neighborX = fst actual + fst direction
            let neighborY = snd actual + snd direction
            let neighborPosition = (neighborX, neighborY)
            if isInGrid grid neighborPosition && checkElevationDifference grid actual neighborPosition then
                updatePosition grid actual neighborPosition
        
let result =
    let grid = processInput "inputs/12.txt"
    let mutable queue = []
    for x in 0..grid.Length - 1 do
        for y in 0..grid[0].Length - 1 do
            queue <- (x, y)::queue
    let startPosition = findCharPosition 'S' grid 0 0
    grid[fst startPosition][snd startPosition] <- ('S', 0)
    findShortestPath grid queue
    let endPosition = findCharPosition 'E' grid 0 0
    snd (grid[fst endPosition][snd endPosition])

let resultPartTwo =
    let mutable minimumSteps = 412 // distance from S
    let mutable grid = processInput "inputs/12.txt"
    let mutable queue = []
    let mutable stop = false
    while not stop do
        grid <- Array.map refreshPositionLine grid
        queue <- []
        for x in 0..grid.Length - 1 do
            for y in 0..grid[0].Length - 1 do
                queue <- (x, y)::queue
        let startPosition = findCharPosition 'a' grid 0 0
        if startPosition = (-1, -1) then
            stop <- true
        else
            grid[fst startPosition][snd startPosition] <- ('A', 0)
            findShortestPath grid queue
            let endPosition = findCharPosition 'E' grid 0 0
            minimumSteps <- min minimumSteps (snd (grid[fst endPosition][snd endPosition]))
    minimumSteps
