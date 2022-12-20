module Day13

open System.IO

type Packet =
    | PacketList of content: Packet list
    | PacketInteger of value: int

let rec getContent (line: string) =
    if line.Length = 0 then
        [], ""
    else
        match line[0] with
        | ']' -> [], line[1..]
        | '[' ->
            let listContent, continueString = getContent line[1..]
            let restContent, restString = getContent continueString
            PacketList(listContent)::restContent, restString
        | ',' -> getContent line[1..]
        | '1' when line[1] = '0' ->
            let packetContent, restString = getContent line[2..]
            PacketInteger(10)::packetContent, restString
        | num ->
            let packetContent, restString = getContent line[1..]
            PacketInteger(int (num.ToString()))::packetContent, restString

let rec comparePackets a b =
    match a, b with
    | PacketInteger valueA, PacketInteger valueB -> valueA - valueB
    | PacketList contentA, PacketList contentB -> compareListContents contentA contentB 0
    | PacketInteger _, PacketList _ -> comparePackets (PacketList([ a ])) b
    | PacketList _, PacketInteger _ -> comparePackets a (PacketList([ b ]))

and compareListContents (listA: Packet list) (listB: Packet list) index =
    if index < listA.Length && index < listB.Length then
        if comparePackets listA[index] listB[index] = 0 then
            compareListContents listA listB (index + 1)
        else
            comparePackets listA[index] listB[index]
    elif index = listA.Length && index = listB.Length then
        0
    elif index = listA.Length then
        -1
    else
        1

let countRightOrdered actualIndex (packetPairs: Packet list array) =
    if compareListContents packetPairs[0] packetPairs[1] 0 < 0 then
        actualIndex + 1
    else
        0

let isDividerPacket packet =
    packet = PacketList([ PacketList([ PacketInteger(2) ]) ])
    || packet = PacketList([ PacketList([ PacketInteger(6) ]) ])

let result =
    "inputs/13.txt" |> File.ReadAllLines
    |> Array.filter (fun line -> line <> "")
    |> Array.map getContent
    |> Array.map fst
    |> Array.chunkBySize 2
    |> Array.mapi countRightOrdered
    |> Array.sum

let resultPartTwo =
    "inputs/13.txt" |> File.ReadAllLines
    |> Array.filter (fun line -> line <> "")
    |> List.ofArray
    |> (@) ["[[2]]"]
    |> (@) ["[[6]]"]
    |> List.map getContent
    |> List.map (fun packet -> (fst packet)[0])
    |> List.sortWith comparePackets
    |> List.mapi (fun index packet -> index + 1, packet)
    |> List.filter (fun packet -> isDividerPacket (snd packet))
    |> List.map fst
    |> List.fold (*) 1
