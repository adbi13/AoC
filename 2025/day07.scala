def part1(lines: Array[Array[Char]]): Int =
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
    totalSplits

def part2(lines: Array[Array[Char]]): Long =
    val paths: Array[Array[Long]] = Array.fill(lines.length, lines(0).length)(0L)
    paths(0)(lines(0).indexOf('S')) = 1
    for y <- 1 to lines.length - 1
        x <- 0 to lines(0).length - 1 do
        if lines(y)(x) == '^' && paths(y - 1)(x) > 0 then
            if paths(y).isDefinedAt(x - 1) then
                paths(y)(x - 1) += paths(y - 1)(x)
            if paths(y).isDefinedAt(x + 1) then
                paths(y)(x + 1) += paths(y - 1)(x)
        if lines(y)(x) == '.' && paths(y - 1)(x) > 0 then
            paths(y)(x) += paths(y - 1)(x)

    paths.last.sum

@main
def day07(): Unit =
    val path: os.Path = os.pwd / "day07.txt"
    val lines: Array[Array[Char]] = os.read.lines(path).toArray.map(_.toArray)
    println(part2(lines))
