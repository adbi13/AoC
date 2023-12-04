package main

import (
	"bufio"
	"fmt"
	"os"
	"slices"
	"strconv"
	"strings"
)

func getNumbers(line string) []int {
	var numList []int
	for _, numString := range strings.Split(line, " ") {
		if numString != "" {
			number, _ := strconv.Atoi(strings.TrimSpace(numString))
			numList = append(numList, number)
		}
	}
	return numList
}

func processLineDay04(line string) int {
	numbers := strings.Split(line, ": ")[1]
	winningPart := strings.Split(numbers, " | ")[0]
	numbersYouHavePart := strings.Split(numbers, " | ")[1]
	winningNumbers := getNumbers(winningPart)
	numbersYouHave := getNumbers(numbersYouHavePart)

	var result int = 1
	for _, number := range numbersYouHave {
		if slices.Contains(winningNumbers, number) {
			result <<= 1
		}
	}

	return result >> 1
}

func processLineDay04Part2(line string, counts []int) []int {
	numbers := strings.Split(line, ": ")[1]
	winningPart := strings.Split(numbers, " | ")[0]
	numbersYouHavePart := strings.Split(numbers, " | ")[1]
	winningNumbers := getNumbers(winningPart)
	numbersYouHave := getNumbers(numbersYouHavePart)

	var index int = 1
	for _, number := range numbersYouHave {
		if slices.Contains(winningNumbers, number) {
			counts[index] += counts[0];
			index++;
		}
	}

	return counts
}

func day04() {
	file, err := os.Open("input04.txt")

	if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

	scanner := bufio.NewScanner(file)
	var result = 0
	counts := []int{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
	for scanner.Scan() {
		counts = processLineDay04Part2(scanner.Text(), counts)
		result += counts[0]
		counts = append(counts[1:], 1)
	}

	fmt.Println(result)
}
