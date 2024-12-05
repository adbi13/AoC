DIRECTIONS = [
    (x, y) for x in [-1, 0, 1] for y in [-1, 0, 1] if x != 0 or y != 0
]

def count_words(table, x, y, word="XMAS"):
    count = 0
    for x_direction, y_direction in DIRECTIONS:
        letter_index = 0
        actual_x = x
        actual_y = y
        while 0 <= actual_x < len(table[0]) and 0 <= actual_y < len(table):
            if table[actual_y][actual_x] == word[letter_index]:
                actual_x += x_direction
                actual_y += y_direction
                letter_index += 1
                if letter_index >= len(word):
                    count += 1
                    break
            else:
                break
    return count

def is_x_word(table, x, y, word="MAS"):
    return (((table[y-1][x-1] == word[0] and table[y+1][x+1] == word[2])
            or (table[y-1][x-1] == word[2] and table[y+1][x+1] == word[0]))
            and ((table[y+1][x-1] == word[0] and table[y-1][x+1] == word[2])
            or (table[y+1][x-1] == word[2] and table[y-1][x+1] == word[0])))

if __name__ == "__main__":
    with open("input04.txt") as input_file:
        table = [line.strip() for line in input_file]
    result = 0
    result_part2 = 0
    for y in range(len(table)):
        for x in range(len(table[0])):
            if table[y][x] == "X":
                result += count_words(table, x, y)

    for y in range(1, len(table) - 1):
        for x in range(1, len(table[0]) - 1):
            if table[y][x] == "A" and is_x_word(table, x, y):
                result_part2 += 1

    print(f"Part 1: {result}")
    print(f"Part 2: {result_part2}")
