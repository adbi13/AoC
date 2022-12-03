module Day03

open System.IO

let linesList inputFile = 
    File.ReadAllLines inputFile

let lettersInBothParts (line: string) =
    let middle = line.Length / 2
    let firstHalf = line[..middle].ToCharArray()
    let secondHalf = line[middle..]
    Array.filter (fun (ch: char) -> secondHalf.Contains(ch)) firstHalf

let firstReoccurringLetter line =
    (lettersInBothParts line)[0]

let charCalculation operator (a: char) (b: char) =
    operator (int a) (int b)

let findReocurringLetters (lines: string array) =
    let first = lines[0].ToCharArray()
    let rest = lines[1..]
    Array.filter (fun (ch: char) -> Array.forall (fun (line: string) -> line.Contains(ch)) rest) first

let rec findCommonInGroups  (lines: string array) =
    match lines with
    | [||] -> []
    | lines when lines.Length <= 3 -> [ (findReocurringLetters lines)[0] ]
    | lines -> (findReocurringLetters lines[..2])[0] :: findCommonInGroups lines[3..]

let getPriority ch =
    match ch with
    | ch when 'a' <= ch && ch <= 'z' -> 1 + charCalculation (-) ch 'a'
    | ch when 'A' <= ch && ch <= 'Z' -> 27 + charCalculation (-) ch 'A'
    | _ -> invalidArg (nameof ch) "Invalid character has to be a letter."

let sumPriorities array =
    Seq.sumBy getPriority array

let result =
    linesList "inputs/03.txt" |> Array.map firstReoccurringLetter |> sumPriorities

let resultPartTwo =
    linesList "inputs/03.txt" |> findCommonInGroups |> sumPriorities
