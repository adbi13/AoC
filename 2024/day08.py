from itertools import combinations

def get_antennas(antennas_map):
    positions = dict()
    for y, row in enumerate(antennas_map):
        for x, value in enumerate(row):
            if value != ".":
                positions[value] = positions.get(value, []) + [(x, y)]
    return positions

def get_antinodes(antennas, max_x, max_y, resonant_harmonics=False):
    positions = set()
    for antenna_positions in antennas.values():
        if resonant_harmonics:
            positions.update(antenna_positions)
        for first, second in combinations(antenna_positions, 2):
            diff_vector = (second[0] - first[0], second[1] - first[1])

            antinode = (first[0] - diff_vector[0], first[1] - diff_vector[1])
            while 0 <= antinode[0] < max_x and 0 <= antinode[1] < max_y:
                positions.add(antinode)
                if not resonant_harmonics:
                    break
                antinode = (antinode[0] - diff_vector[0], antinode[1] - diff_vector[1])

            antinode = (second[0] + diff_vector[0], second[1] + diff_vector[1])
            while 0 <= antinode[0] < max_x and 0 <= antinode[1] < max_y:
                positions.add(antinode)
                if not resonant_harmonics:
                    break
                antinode = (antinode[0] + diff_vector[0], antinode[1] + diff_vector[1])

    return positions

def count_antinodes_positions(antennas_map, resonant_harmonics=False):
    antennas = get_antennas(antennas_map)
    antinodes = get_antinodes(antennas, len(antennas_map[0]), len(antennas_map), resonant_harmonics)
    return len(antinodes)

if __name__ == "__main__":
    with open("input08.txt") as input_file:
        antennas_map = [line.strip() for line in input_file]

    result = count_antinodes_positions(antennas_map)
    result2 = count_antinodes_positions(antennas_map, True)

    print(f"Part 1: {result}")
    print(f"Part 2: {result2}")
