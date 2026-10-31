def part1(lines: Seq[String]): Long =
    val problems: Array[(Array[Long], String)] = lines
        .map(_.split("\s+"))
        .toArray
        .transpose.map(line => (line.dropRight(1).map(_.toLong), line.last))
    
    problems.map((numbers, operator) => operator match
        case "*" => numbers.fold(1L)((a, b) => a * b)
        case "+" => numbers.fold(0L)((a, b) => a + b)
    ).sum

def part2(lines: Seq[String]): Long =
    val problems: Array[Array[Char]] = lines
        .map(_.toArray)
        .toArray
        .transpose
    
    var currentOperator: Char = ' '
    var problemResult: Long = 0L
    var total: Long = 0L
    for problem <- problems do
        if problem.last != ' ' then
            currentOperator = problem.last
            problemResult = currentOperator match
                case '*' => 1
                case '+' => 0

        if problem.forall(_ == ' ') then
            total += problemResult
        else
            val number = problem.dropRight(1).mkString.strip.toLong
            if currentOperator == '+' then
                problemResult += number
            else if currentOperator == '*' then
                problemResult *= number
    
    total + problemResult
    
@main
def day06(): Unit =
    val path: os.Path = os.pwd / "day06.txt"
    val lines: Seq[String] = os.read.lines(path)

    println(part2(lines))
