package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func byteIsDigit(character byte) bool {
	return '0' <= character && character <= '9'
}

func readNumber(line string, index int) int {
	for index > 0 && byteIsDigit(line[index - 1]) {
		index--
	}
	var numString strings.Builder
	for index < len(line) && byteIsDigit(line[index]) {
		numString.WriteByte(line[index])
		index++
	}
	number, _ := strconv.Atoi(numString.String())
	return number
}

func getGearRatio(prev string, actual string, next string, index int) int {
	var count = 0
	var gearRatio = 1
	if index > 0 {
		if byteIsDigit(prev[index - 1]) && !byteIsDigit(prev[index]) {
			count++;
			gearRatio *= readNumber(prev, index - 1)
		}
		if byteIsDigit(actual[index - 1]){
			count++;
			gearRatio *= readNumber(actual, index - 1)
		}
		if byteIsDigit(next[index - 1]) && !byteIsDigit(next[index]) {
			count++;
			gearRatio *= readNumber(next, index - 1)
		}
	}
	if byteIsDigit(prev[index]){
		count++;
		gearRatio *= readNumber(prev, index)
	}
	if byteIsDigit(next[index]){
		count++;
		gearRatio *= readNumber(next, index)
	}
	if index < len(actual) - 1 {
		if byteIsDigit(prev[index + 1]) && !byteIsDigit(prev[index]) {
			count++;
			gearRatio *= readNumber(prev, index + 1)
		}
		if byteIsDigit(actual[index + 1]){
			count++;
			gearRatio *= readNumber(actual, index + 1)
		}
		if byteIsDigit(next[index + 1]) && !byteIsDigit(next[index]) {
			count++;
			gearRatio *= readNumber(next, index + 1)
		}
	}

	if count == 2 {
		return gearRatio
	}
	return 0
}

func processLineDay03Part2(prev string, actual string, next string) int {
	var lineSum = 0
	for index, character := range actual {
		if character == '*' {
			lineSum += getGearRatio(prev, actual, next, index)
		}
	}
	return lineSum
}

func isSymbol(character byte) bool {
	return !byteIsDigit(character) && character != '.'
}

func processLineDay03(prev string, actual string, next string) int {
	var lineSum = 0
	var isPart = false
	var numString strings.Builder
	for index := 0; index < len(actual); index++ {
		if '0' <= actual[index] && actual[index] <= '9' {
			if numString.Len() == 0 && index > 0 {
				isPart = isSymbol(prev[index - 1]) ||
					isSymbol(actual[index - 1]) ||
					isSymbol(next[index - 1])
			}

			numString.WriteByte(actual[index])
			isPart = isPart || isSymbol(prev[index])
			isPart = isPart || isSymbol(next[index])
		} else if actual[index] == '.' {
			if numString.Len() > 0 {
				isPart = isPart || isSymbol(prev[index])
				isPart = isPart || isSymbol(next[index])
				if isPart {
					number, _ := strconv.Atoi(numString.String())
					lineSum += number
				}
			}
			numString.Reset()
			isPart = false
		} else {
			isPart = true
			if numString.Len() > 0 {
				number, _ := strconv.Atoi(numString.String())
				lineSum += number
				numString.Reset()
			}
		}
	}
	if isPart && numString.Len() > 0 {
		number, _ := strconv.Atoi(numString.String())
		lineSum += number
	}
	return lineSum
}

func day03() {
	file, err := os.Open("input03.txt")

	if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

	scanner := bufio.NewScanner(file)
	var result = 0
	scanner.Scan()
	var prev string
	var actual = "............................................................................................................................................"
	var next = scanner.Text()

	for scanner.Scan() {
		prev = actual
		actual = next
		next = scanner.Text()
		result += processLineDay03Part2(prev, actual, next)
	}

	prev = actual
	actual = next
	next = "............................................................................................................................................"
	result += processLineDay03Part2(prev, actual, next)

	fmt.Println(result)
}
