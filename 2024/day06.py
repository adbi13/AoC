from queue import SimpleQueue

def init_directions():
    directions = SimpleQueue()
    directions.put((0, -1)) # up
    directions.put((1, 0)) # righ
    directions.put((0, 1)) # down
    directions.put((-1, 0)) # left
    return directions

def detect_cycle(area_map, start_x, start_y, start_orientation):
    directions = init_directions()
    orientation = directions.get()

    while orientation != start_orientation:
        directions.put(orientation)
        orientation = directions.get()

    actual_x = start_x
    actual_y = start_y
    next_x = actual_x + orientation[0]
    next_y = actual_y + orientation[1]

    skip_first = True

    while 0 <= next_x < len(area_map[0]) and 0 <= next_y < len(area_map):
        if (not skip_first and actual_x == start_x and actual_y == start_y
            and orientation == start_orientation):
            return True
        skip_first = False
        if area_map[next_y][next_x] == "#":
            directions.put(orientation)
            orientation = directions.get()
        else:
            actual_x, actual_y = next_x, next_y

        next_x = actual_x + orientation[0]
        next_y = actual_y + orientation[1]
    
    return False



def count_squares(area_map, start_x, start_y):
    directions = init_directions()
    orientation = directions.get()

    actual_x = start_x
    actual_y = start_y
    next_x = actual_x + orientation[0]
    next_y = actual_y + orientation[1]
    square_count = 1
    cycles_count = 0

    while 0 <= next_x < len(area_map[0]) and 0 <= next_y < len(area_map):
        if area_map[next_y][next_x] == "#":
            directions.put(orientation)
            orientation = directions.get()
        else:
            if area_map[next_y][next_x] != "X" and area_map[next_y][next_x] != "O":
                square_count += 1
                area_map[next_y][next_x] = "X"
            if area_map[next_y][next_x] != "O":
                prev_square = area_map[next_y][next_x]
                area_map[next_y][next_x] = "#"
                if detect_cycle(area_map, actual_x, actual_y, orientation):
                    cycles_count += 1
                    area_map[next_y][next_x] = "O"
                else:
                    area_map[next_y][next_x] = prev_square

            actual_x, actual_y = next_x, next_y

        next_x = actual_x + orientation[0]
        next_y = actual_y + orientation[1]

    return square_count, cycles_count

if __name__ == "__main__":
    with open("input06.txt") as input_file:
        area = [list(line.strip()) for line in input_file]
    for y, row in enumerate(area):
        if "^" in row:
            start_x = row.index("^")
            start_y = y
            area[start_y][start_x] = "X"
            break
    
    result, result2 = count_squares(area, start_x, start_y)
    print(f"Part 1: {result}")
    print(f"Part 2: {result2}")
