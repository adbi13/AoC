def createRanges(range: String): Iterable[BigInt] =
    val Array(lowerBound, upperBound) = range.split("-").map(BigInt(_))
    (lowerBound to upperBound)

def isValidId(id: BigInt): Boolean =
    val stringId: String = id.toString()
    val idLength: Int = stringId.length()
    idLength % 2 == 0
    && stringId.slice(0, idLength / 2) == stringId.slice(idLength / 2, idLength)

@main
def part01(): Unit =
    val path: os.Path = os.pwd / "day02.txt"
    val content: String = os.read(path).strip()
    println(content.split(",").flatMap(createRanges).filter(isValidId).sum)
