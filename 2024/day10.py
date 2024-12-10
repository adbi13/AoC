def trailhead_peaks(topographic_map, x, y, peaks):
    actual_height = topographic_map[y][x]
    if actual_height == 9:
        peaks.add((x, y))
    directions = [(-1, 0), (1, 0), (0, 1), (0, -1)]
    for direction in directions:
        next_x = x + direction[0]
        next_y = y + direction[1]
        if (0 <= next_x < len(topographic_map[0])
            and 0 <= next_y < len(topographic_map)
            and topographic_map[next_y][next_x] == actual_height + 1):
            trailhead_peaks(topographic_map, next_x, next_y, peaks)

def trailhead_score(topographic_map, x, y):
    peaks = set()
    trailhead_peaks(topographic_map, x, y, peaks)
    return len(peaks)

def trailhead_rating(topographic_map, x, y):
    actual_height = topographic_map[y][x]
    if actual_height == 9:
        return 1
    directions = [(-1, 0), (1, 0), (0, 1), (0, -1)]
    result = 0
    for direction in directions:
        next_x = x + direction[0]
        next_y = y + direction[1]
        if (0 <= next_x < len(topographic_map[0])
            and 0 <= next_y < len(topographic_map)
            and topographic_map[next_y][next_x] == actual_height + 1):
            result += trailhead_rating(topographic_map, next_x, next_y)
    return result

if __name__ == "__main__":
    with open("input10.txt") as input_file:
        topographic_map = [[int(value) for value in line.strip()] for line in input_file]
    
    result = 0
    result2 = 0
    for y, row in enumerate(topographic_map):
        for x, value in enumerate(row):
            if value == 0:
                result += trailhead_score(topographic_map, x, y)
                result2 += trailhead_rating(topographic_map, x, y)
    
    print(f"Part 1: {result}")
    print(f"Part 2: {result2}")
