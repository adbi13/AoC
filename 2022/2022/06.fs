module Day06

open System.IO

let searchedMarkerLength = 14

let rec findMarker markerLenght actualPosition (line: char list) =
    match line with
    | head::neck::tail when markerLenght = 2 && head <> neck -> 1 + actualPosition
    | head::tail when not (List.contains head tail[..markerLenght - 2]) ->
        findMarker (markerLenght - 1) (actualPosition + 1) tail
    | _::tail -> findMarker searchedMarkerLength (actualPosition + 1) tail
    | _ -> invalidArg (nameof line) "Line does not contain marker"

let result =
    ("inputs/06.txt" |> File.ReadAllText).ToCharArray() |> List.ofArray |> findMarker searchedMarkerLength 1
