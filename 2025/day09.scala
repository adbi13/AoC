import math._
import scala.collection.mutable.ListBuffer

@main
def day09(): Unit =
    val path: os.Path = os.pwd / "day09.txt"
    val coordinates: Array[Array[Long]] = os.read.lines(path).toArray.map(_.split(",").map(_.toLong))

    val areas: ListBuffer[(Int, Int, Long)] = ListBuffer() 

    for i <- 0 to coordinates.length - 2
        j <- i + 1 to coordinates.length - 1 do
        val area: Long = abs(1 + coordinates(i)(0) - coordinates(j)(0)) * abs(1 + coordinates(i)(1) - coordinates(j)(1))
        areas.append((i, j, area))
    
    println(areas.maxBy(_._3)._3)
