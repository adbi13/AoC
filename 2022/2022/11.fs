module Day11

#nowarn "3391"

type Monkey(id: int, items: int list, operation: bigint -> bigint, test: bigint -> int) =
    let mutable items = List.map (fun (item: int) -> bigint item) items
    let mutable (inspections: bigint) = bigint 0

    member this.addItem item =
        items <- items @ [ item ]

    member this.moveItems (monkeyArray: Monkey array) =
        for item in items do
            let inspectedItem = operation item
            let nextMonkey = test inspectedItem
            monkeyArray[nextMonkey].addItem(inspectedItem % bigint 9699690)
            inspections <- inspections + bigint 1
        items <- List.Empty
    
    member this.getInspections =
        inspections


let monkeys = [|
    new Monkey(0, [ 64 ], (fun old -> old * bigint 7), (fun item -> if item % bigint 13 = 0 then 1 else 3));
    new Monkey(1, [ 60; 84; 84; 65 ], (fun old -> old + bigint 7), (fun item -> if item % bigint 19 = 0 then 2 else 7));
    new Monkey(2, [ 52; 67; 74; 88; 51; 61 ], (fun old -> old * bigint 3), (fun item -> if item % bigint 5 = 0 then 5 else 7));
    new Monkey(3, [ 67; 72 ], (fun old -> old + bigint 3), (fun item -> if item % bigint 2 = 0 then 1 else 2));
    new Monkey(4, [ 80; 79; 58; 77; 68; 74; 98; 64 ], (fun old -> old * old), (fun item -> if item % bigint 17 = 0 then 6 else 0));
    new Monkey(5, [ 62; 53; 61; 8; 86 ], (fun old -> old + bigint 8), (fun item -> if item % bigint 11 = 0 then 4 else 6));
    new Monkey(6, [ 86; 89; 82 ], (fun old -> old + bigint 2), (fun item -> if item % bigint 7 = 0 then 3 else 0));
    new Monkey(7, [ 92; 81; 70; 96; 69; 84; 83 ], (fun old -> old + bigint 4), (fun item -> if item % bigint 3 = 0 then 4 else 5));
|]

let result =
    for _ in 1..10000 do
        for monkey in monkeys do
            monkey.moveItems monkeys
    
    let sortedMonkeys = Array.sortByDescending (fun (monkey: Monkey) -> monkey.getInspections) monkeys
    sortedMonkeys[0].getInspections * sortedMonkeys[1].getInspections
    