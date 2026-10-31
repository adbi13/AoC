import scala.math.BigDecimal

def getMaxJoltage(batteries: String): Int =
    val intBatteries = batteries.map(_.asDigit)
    val (maxVal, maxIndex) = intBatteries.zipWithIndex.dropRight(1).maxBy(_._1)
    val second = intBatteries.drop(maxIndex + 1).max
    maxVal * 10 + second

def getMaxJoltage2(batteries: String): BigInt =
    val intBatteries = batteries.map(_.asDigit).map(BigInt(_))
    val highestDigits: Array[(BigInt, Int)] = Array.fill(12)((BigInt(0), 0))
    highestDigits(0) = intBatteries.zipWithIndex.dropRight(11).maxBy(_._1)
    for i <- 1 to 11 do
        highestDigits(i) = intBatteries
            .zipWithIndex
            .drop(highestDigits(i - 1)._2 + 1)
            .dropRight(11 - i)
            .maxBy(_._1)
    highestDigits
        .zipWithIndex
        .map((valueTuple, exponent) => valueTuple._1 * BigDecimal(10).pow(11 - exponent).toBigInt)
        .sum

@main
def day03(): Unit =
    val path: os.Path = os.pwd / "day03.txt"
    val lines: Seq[String] = os.read.lines(path).map(_.strip())
    println(lines.map(getMaxJoltage2).sum)
