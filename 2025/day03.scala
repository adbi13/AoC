def getMaxJoltage(batteries: String): Int =
    val intBatteries = batteries.map(_.asDigit)
    val (maxVal, maxIndex) = intBatteries.zipWithIndex.dropRight(1).maxBy(_._1)
    val second = intBatteries.drop(maxIndex + 1).max
    maxVal * 10 + second

@main
def day03(): Unit =
    val path: os.Path = os.pwd / "day03.txt"
    val lines: Seq[String] = os.read.lines(path).map(_.strip())
    println(lines.map(getMaxJoltage).sum)
