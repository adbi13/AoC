@main
def day07(): Unit =
    val path: os.Path = os.pwd / "day07.txt"
    val lines: Array[Array[Char]] = os.read.lines(path).toArray.map(_.toArray)
    var totalSplits = 0
    for y <- 1 to lines.length - 1
        x <- 0 to lines(0).length - 1 do
        if lines(y)(x) == '^' && lines(y - 1)(x) == '|' then
            if lines(y).isDefinedAt(x - 1) then
                lines(y)(x - 1) = '|'
            if lines(y).isDefinedAt(x + 1) then
                lines(y)(x + 1) = '|'
            totalSplits += 1
        if lines(y)(x) == '.' && (lines(y - 1)(x) == '|' || lines(y - 1)(x) == 'S') then
            lines(y)(x) = '|'
    println(totalSplits)
