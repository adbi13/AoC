@main
def day06(): Unit =
    val path: os.Path = os.pwd / "day06.txt"
    val lines: Seq[String] = os.read.lines(path).map(_.strip())
    val problems: Array[(Array[Long], String)] = lines
        .map(_.split("\s+"))
        .toArray
        .transpose.map(line => (line.dropRight(1).map(_.toLong), line.last))
    
    val result = problems.map((numbers, operator) => operator match
        case "*" => numbers.fold(1L)((a, b) => a * b)
        case "+" => numbers.fold(0L)((a, b) => a + b)
    ).sum

    println(result)
