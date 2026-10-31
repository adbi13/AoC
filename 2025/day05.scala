def isFresh(ranges: Array[Array[BigInt]])(ingredient: BigInt): Boolean =
    ranges.exists(range => range(0) <= ingredient && ingredient <= range(1))

def adjustRanges(ranges: Array[Array[BigInt]]) =
    for i <- 0 to ranges.length - 2
        j <- i + 1 to ranges.length - 1 do
        if ranges(i)(0) >= ranges(j)(0) && ranges(i)(1) <= ranges(j)(1) then
            ranges(i) = Array(-1, -1)
        else if ranges(i)(0) <= ranges(j)(0) && ranges(i)(1) >= ranges(j)(1) then
            ranges(j) = Array(-1, -1)
        else if ranges(i)(0) >= ranges(j)(0) && ranges(i)(0) <= ranges(j)(1) then
            ranges(j)(1) = ranges(i)(0) - 1
        else if ranges(i)(1) >= ranges(j)(0) && ranges(i)(1) <= ranges(j)(1) then
            ranges(j)(0) = ranges(i)(1) + 1

@main
def day05(): Unit =
    val path: os.Path = os.pwd / "day05.txt"
    val Array(rangesFile, availableFile) = os.read(path).split("\n\n")
    val ranges = rangesFile.split("\n").map(_.split("-").map(BigInt(_)))
    val availableIngredients = availableFile.strip().split("\n").map(BigInt(_))
    adjustRanges(ranges)
    println(ranges.filter(_(0) != -1).map(range => range(1) - range(0) + 1).sum)
