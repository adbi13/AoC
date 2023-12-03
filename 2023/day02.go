package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func processLineDay02(line string) int {
	game := strings.Split(line, ": ")
	gameId, _ := strconv.Atoi(strings.Split(game[0], " ")[1])

	for _, round := range strings.Split(game[1], "; ") {
		for _, colorCount := range strings.Split(round, ", ") {
			color := strings.Split(colorCount, " ")[1]
			count, _ := strconv.Atoi(strings.Split(colorCount, " ")[0])
			if (color == "red" && count > 12) || (color == "green" && count > 13) || (color == "blue" && count > 14) {
				return 0;
			}
		}
	}

	return gameId
}

func processLineDay02Part2(line string) int {
	game := strings.Split(line, ": ")
	// gameId, _ := strconv.Atoi(strings.Split(game[0], " ")[1])
	var colorCounts = map[string]int{
		"red": 0,
		"green": 0,
		"blue": 0,
	}

	for _, round := range strings.Split(game[1], "; ") {
		for _, colorCount := range strings.Split(round, ", ") {
			color := strings.Split(colorCount, " ")[1]
			count, _ := strconv.Atoi(strings.Split(colorCount, " ")[0])
			if count > colorCounts[color] {
				colorCounts[color] = count
			}
		}
	}

	return colorCounts["red"] * colorCounts["green"] * colorCounts["blue"]
}

func day02() {
	file, err := os.Open("input02.txt")

	if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

	scanner := bufio.NewScanner(file)
	var result = 0
	for scanner.Scan() {
		result += processLineDay02Part2(scanner.Text())
	}

	fmt.Println(result)
}
