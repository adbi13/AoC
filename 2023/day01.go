package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

func processLine(line string) int {
	numStrings := []string {
		"one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
		"1", "2", "3", "4", "5", "6", "7", "8", "9",
	}

	var firstNum int
	var lastNum int
	var minPosition = math.MaxInt
	var maxPosition int = -1

	for numIndex, numString := range numStrings {
		firstIndex := strings.Index(line, numString)
		if firstIndex != -1 && firstIndex < minPosition {
			minPosition = firstIndex
			if numIndex < 9 {
				firstNum = numIndex + 1
			} else {
				firstNum, _ = strconv.Atoi(numString)
			}
		}

		lastIndex := strings.LastIndex(line, numString)
		if lastIndex != -1 && lastIndex > maxPosition {
			maxPosition = lastIndex
			if numIndex < 9 {
				lastNum = numIndex + 1
			} else {
				lastNum, _ = strconv.Atoi(numString)
			}
		}
	}

	return firstNum * 10 + lastNum
}

func day01() {
	file, err := os.Open("input01.txt")

	if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

	scanner := bufio.NewScanner(file)
	var result = 0
	for scanner.Scan() {
		result += processLine(scanner.Text())
	}

	fmt.Println(result)
}
