def isFresh(ranges: Array[Array[BigInt]])(ingredient: BigInt): Boolean =
    ranges.exists(range => range(0) <= ingredient && ingredient <= range(1))

@main
def day05(): Unit =
    val path: os.Path = os.pwd / "day05.txt"
    val Array(rangesFile, availableFile) = os.read(path).split("\n\n")
    val ranges = rangesFile.split("\n").map(_.split("-").map(BigInt(_)))
    val availableIngredients = availableFile.strip().split("\n").map(BigInt(_))
    println(availableIngredients.count(isFresh(ranges)))


