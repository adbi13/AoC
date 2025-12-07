DIRECTIONS = [
    (-1, 0),
    (0, 1),
    (1, 0),
    (0, -1)
]

def flood_fill(garden, x, y, character):
    if garden[y][x] != character:
        return 0, 0
    area = 1
    perimeter = 0
    garden[y][x] = garden[y][x].lower()
    for x_dir, y_dir in DIRECTIONS:
        new_x = x + x_dir
        new_y = y + y_dir
        if 0 <= new_x < len(garden[0]) and 0 <= new_y < len(garden):
            if garden[new_y][new_x] == character:
                next_area, next_perimeter = flood_fill(garden, new_x, new_y, character)
                area += next_area
                perimeter += next_perimeter
            elif garden[new_y][new_x] != character.lower():
                perimeter += 1
        else:
            if new_x < 0 or new_x >= len(garden[0]):
                perimeter += 1
            if new_y < 0 or new_y >= len(garden):
                perimeter += 1
    return area, perimeter

if __name__ == "__main__":
    with open("input12.txt") as input_file:
        garden = [list(line.strip()) for line in input_file]

    result = 0
    for y, row in enumerate(garden):
        for x, value in enumerate(row):
            if value.isupper():
                area, perimeter = flood_fill(garden, x, y, value)
                result += area * perimeter
    
    print(f"Part 1: {result}")

