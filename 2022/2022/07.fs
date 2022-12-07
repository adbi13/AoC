module Day07

open System.IO
open System.Collections.Generic

type Node =
    | File of size: int
    | Directory of parent: Node option * content: Dictionary<string, Node>

    member this.Size =
        match this with
        | File size -> size
        | Directory (content = c) -> Seq.fold (fun size (node: Node) -> size + node.Size) 0 c.Values

let root = Directory(parent = None, content = new Dictionary<string, Node>())

let directoryFromString cdString directory =
    let (Directory (parent, content)) = directory
    match cdString with
    | "/" -> root
    | ".." -> parent.Value
    | next -> content.Item next

let rec readFileTree (lines: string list list) actualDirectory =
    if not lines.IsEmpty then
        let (Directory (_, content)) = actualDirectory
        match lines.Head with
        | head::neck::_ when head = "$" && neck = "ls" -> readFileTree lines.Tail actualDirectory
        | head::neck::tail when head = "$" && neck = "cd" -> readFileTree lines.Tail (directoryFromString tail.Head actualDirectory)
        | head::neck::_ when head = "dir" ->
            content.Add(neck, Directory(Some(actualDirectory), new Dictionary<string, Node>()))
            readFileTree lines.Tail actualDirectory
        | head::neck::_ ->
            if not (content.ContainsKey(neck)) then
                content.Add(neck, File(int head))
                readFileTree lines.Tail actualDirectory
        | _ -> invalidArg (nameof lines) "Bad input"

let rec countSizes (node: Node) =
    let nodeSize = if node.Size <= 100000 then node.Size else 0
    match node with
    | File _ -> 0
    | Directory (content = c) -> nodeSize + Seq.fold (fun size (node: Node) -> size + countSizes node) 0 c.Values

let rec chooseMinimalEnough size1 size2=
    let smaller = min size1 size2
    let bigger = max size1 size2
    if 30000000 < 70000000 + smaller - root.Size then
        smaller
    else
        bigger

let rec findSmallestDirectory node minimalSize =
    match node with
    | File _ -> minimalSize
    | Directory (content = c) -> Seq.fold (fun minimum (node: Node) -> chooseMinimalEnough minimum (findSmallestDirectory node node.Size)) minimalSize c.Values

let inputLines =
    "inputs/07.txt" |> File.ReadAllLines |> List.ofArray |> List.map (fun str -> List.ofArray(str.Split(' ')))

let result =
    readFileTree inputLines root
    countSizes root

let resultPartTwo =
    readFileTree inputLines root
    findSmallestDirectory root root.Size
