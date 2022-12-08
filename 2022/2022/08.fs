module Day08

open System.IO

let stringToIntArray (inputString: string) =
    Array.map (fun ch -> ch |> string |> int) (inputString.ToCharArray())

let parseTreesFromInput inputFile =
    inputFile |> File.ReadAllLines |> Array.map stringToIntArray

let coordinatesOutOfBounds (trees: int array array) row col =
    row < 0 || col < 0 || row = trees.Length || col = trees[0].Length

let rec findVisiblesInDirection (trees: int array array) row col dirX dirY maxHeight =
    if coordinatesOutOfBounds trees row col then
        Set.empty
    else
        let actualTree = trees[row][col]
        let visibleSet = findVisiblesInDirection trees (row + dirX) (col + dirY) dirX dirY (max actualTree maxHeight)
        if actualTree > maxHeight then
            visibleSet.Add(row, col)
        else 
            visibleSet

let rec treeVisibilityInDirection (trees: int array array) row col dirX dirY treeHeight =
    if coordinatesOutOfBounds trees row col then
        0
    elif trees[row][col] >= treeHeight then
        1
    else
        1 + treeVisibilityInDirection trees (row + dirX) (col + dirY) dirX dirY treeHeight

let result =
    let trees = parseTreesFromInput "inputs/08.txt"
    let mutable visibleTrees = Set.empty
    for col in 0..(trees[0].Length - 1) do
        visibleTrees <- visibleTrees + (findVisiblesInDirection trees 0 col 1 0 -1)
        visibleTrees <- visibleTrees + (findVisiblesInDirection trees (trees.Length - 1) col -1 0 -1)
    for row in 0..(trees.Length - 1) do
        visibleTrees <- visibleTrees + (findVisiblesInDirection trees row 0 0 1 -1)
        visibleTrees <- visibleTrees + (findVisiblesInDirection trees row (trees[0].Length - 1) 0 -1 -1)
    visibleTrees.Count

let resultPartTwo =
    let trees = parseTreesFromInput "inputs/08.txt"
    let mutable maxScore = 0
    for col in 0..(trees[0].Length - 1) do
        for row in 0..(trees.Length - 1) do
            let mutable treeScore = 1
            treeScore <- treeScore * (treeVisibilityInDirection trees (row + 1) col 1 0 (trees[row][col]))
            treeScore <- treeScore * (treeVisibilityInDirection trees (row - 1) col -1 0 (trees[row][col]))
            treeScore <- treeScore * (treeVisibilityInDirection trees row (col + 1) 0 1 (trees[row][col]))
            treeScore <- treeScore * (treeVisibilityInDirection trees row (col - 1) 0 -1 (trees[row][col]))
            maxScore <- max maxScore treeScore
    maxScore
