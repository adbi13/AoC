module Day04

open System
open System.IO

type Bingo = (int * bool)[] list

let printRow row =
    let newline = printfn ""
    Seq.iter (fun (number, mark) -> printf $"| {number}:{mark} ") row

let printBingo (bingo:Bingo) =
    Seq.iter printRow bingo

let inputLines inputFile =
    File.ReadAllLines(inputFile)

let drawnNumbers (lines:string[]) =
    Array.map Int32.Parse (lines[0].Split(','))

let processBingoLine lineList (line:string) =
    lineList @ [Array.map (fun x -> (Int32.Parse x, false)) (Array.filter (fun x -> (String.IsNullOrWhiteSpace x) = false) (line.Split()))]

let processOneBingo (lines:string[]) actual =
    Array.fold processBingoLine [] lines[actual..(actual+4)]

let rec processBingoInput (lines:string[]) actual =
    if actual >= lines.Length then []
    else if lines[actual] = "" then processBingoInput lines (actual + 1)
    else [processOneBingo lines (actual)] @ processBingoInput lines (actual + 5)

let rowWin row =
    Array.fold (fun result (_, mark) -> result && mark) true row

let columnWin (bingo:Bingo) column =
    List.fold (fun result actual -> result && (snd (bingo[actual][column]))) true [0..4]

let bingoWin (bingo:Bingo) =
    (List.fold (fun result actual -> result || (rowWin actual)) false bingo)
    || (List.fold (fun result actual -> result || (columnWin bingo actual)) false [0..4])

let setMark drawnNumber (number, mark) =
    if number = drawnNumber then (number, true) else (number, mark)

let markDrawnNumber drawnNumber (bingo:Bingo) =
    List.map (fun line -> Array.map (setMark drawnNumber) line) bingo

let rec drawNumber bingoList (numberList:int[]) actual =
    let markedBingoList = List.map (markDrawnNumber numberList[actual]) bingoList
    let winningBingos = List.filter bingoWin markedBingoList
    if winningBingos.IsEmpty then drawNumber markedBingoList numberList (actual + 1)
    else (winningBingos, numberList[actual])

let runBingo =
    let lines = inputLines "04input.txt"
    drawNumber (processBingoInput lines 1) (drawnNumbers lines) 0

let rec drawNumberLosing bingoList (numberList:int[]) actual =
    let markedBingoList = List.map (markDrawnNumber numberList[actual]) bingoList
    let losingBingos = List.filter (fun bingo -> (bingoWin bingo) = false) markedBingoList
    if losingBingos.IsEmpty = false then drawNumberLosing losingBingos numberList (actual + 1)
    else (markedBingoList, numberList[actual])

let runLosingBingo =
    let lines = inputLines "04input.txt"
    drawNumberLosing (processBingoInput lines 1) (drawnNumbers lines) 0

let sumRow sum row =
    sum + (Array.fold (fun result (num, marked) -> if marked then result else result + num) 0 row)

let countPoints (bingo:Bingo) =
    List.fold sumRow 0 bingo
