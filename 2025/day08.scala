import math._
import scala.collection.mutable.ListBuffer
import scala.util.boundary

@main
def day08(): Unit =
    val path: os.Path = os.pwd / "day08.txt"
    val coordinates: Array[Array[Int]] = os.read.lines(path).toArray.map(_.split(",").map(_.toInt))
    val circuits: Array[Int] = Array.fill(coordinates.length)(0)

    val distances: ListBuffer[(Int, Int, Double)] = ListBuffer() 

    for i <- 0 to coordinates.length - 2
        j <- i + 1 to coordinates.length - 1 do
        val distance: Double = sqrt(coordinates(j).zip(coordinates(i)).map((y, x) => pow(y - x, 2)).sum)
        distances.append((i, j, distance))
    println(distances.length)
    var maxCircuitId = 0
    var resultPart2 = 0L
    boundary {
        for distance <- distances.sortBy(_._3) do
            if circuits(distance._1) == 0 && circuits(distance._2) == 0 then
                maxCircuitId += 1
                circuits(distance._1) = maxCircuitId
                circuits(distance._2) = maxCircuitId
            else if circuits(distance._1) == 0 then
                circuits(distance._1) = circuits(distance._2)
            else if circuits(distance._2) == 0 then
                circuits(distance._2) = circuits(distance._1)
            else if circuits(distance._1) != circuits(distance._2) then
                var oldId = circuits(distance._1)
                for i <- 0 to circuits.length - 1 do
                    if circuits(i) == oldId then
                        circuits(i) = circuits(distance._2)

            if circuits.distinct.length == 1 then
                resultPart2 = coordinates(distance._1)(0).toLong * coordinates(distance._2)(0)
                boundary.break()
    }

    println(resultPart2)

    /* PART 01
    circuits
        .groupBy(identity)
        .foreach((key, values) => println(s"$key: ${values.length}"))

    val result = circuits
        .filter(_ > 0)
        .groupBy(identity)
        .map(_._2.length)
        .toArray
        .sorted
        .takeRight(3)
        .fold(1)(_ * _)
    
    println(result)
    */
