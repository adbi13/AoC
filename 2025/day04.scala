def removeRolls(rollsMap: Array[Array[Option[Int]]]): Int =
    var removed: Int = 0
    for y <- 0 to rollsMap.length - 1
        x <- 0 to rollsMap(0).length - 1 do
        if rollsMap(y)(x).isDefined then
            if rollsMap(y)(x).get < 4 then
                removed += 1
                rollsMap(y)(x) = None
            else
                rollsMap(y)(x) = Some(0)
    removed

def initNumbers(rollsMap: Array[Array[Option[Int]]]): Unit =
    for y <- 0 to rollsMap.length - 1
        x <- 0 to rollsMap(0).length - 1 do
        if rollsMap(y)(x).isDefined then
            if rollsMap.isDefinedAt(y - 1) && rollsMap(y - 1).isDefinedAt(x - 1) then
                rollsMap(y)(x) = rollsMap(y - 1)(x - 1) match
                    case None => rollsMap(y)(x)
                    case Some(count) =>
                        rollsMap(y - 1)(x - 1) = Some(count + 1)
                        Some(rollsMap(y)(x).get + 1)
            if rollsMap.isDefinedAt(y - 1) then
                rollsMap(y)(x) = rollsMap(y - 1)(x) match
                    case None => rollsMap(y)(x)
                    case Some(count) =>
                        rollsMap(y - 1)(x) = Some(count + 1)
                        Some(rollsMap(y)(x).get + 1)
            if rollsMap.isDefinedAt(y - 1) && rollsMap(y - 1).isDefinedAt(x + 1) then
                rollsMap(y)(x) = rollsMap(y - 1)(x + 1) match
                    case None => rollsMap(y)(x)
                    case Some(count) =>
                        rollsMap(y - 1)(x + 1) = Some(count + 1)
                        Some(rollsMap(y)(x).get + 1)
            if rollsMap(y).isDefinedAt(x - 1) then
                rollsMap(y)(x) = rollsMap(y)(x - 1) match
                    case None => rollsMap(y)(x)
                    case Some(count) =>
                        rollsMap(y)(x - 1) = Some(count + 1)
                        Some(rollsMap(y)(x).get + 1)
            

@main
def day04(): Unit =
    val path: os.Path = os.pwd / "day04.txt"
    val lines: Seq[String] = os.read.lines(path).map(_.strip())
    val rollsMap: Array[Array[Option[Int]]] = lines
        .map(_.map(_ match
            case '@' => Some(0)
            case '.' => None
        ).toArray).toArray
    var total = 0
    var removed = 1
    while removed > 0 do
        initNumbers(rollsMap)
        removed = removeRolls(rollsMap)
        total += removed
    println(total)
