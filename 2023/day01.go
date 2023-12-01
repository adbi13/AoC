package main

import (
	"bufio"
	"fmt"
	"os"
)

func day01() {
	file, err := os.Open("input01.txt")

	if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

	scanner := bufio.NewScanner(file)
	var result = 0
	for scanner.Scan() {
		var firstNum = -1
		var lastNum = -1
		line := scanner.Text()
		for i := 0; i < len(line); i++ {
			num := int(line[i] - '0')
			if num >= 0 && num <= 9 {
				if firstNum < 0 {
					firstNum = num * 10
				}
				lastNum = num
			}
		}
		result += firstNum + lastNum
	}

	fmt.Println(result)
}
