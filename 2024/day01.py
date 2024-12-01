first_list = []
second_list = []

with open("input01.txt") as input_file:
    for line in input_file:
        line = line.strip()
        numbers = line.split()
        first_list.append(int(numbers[0]))
        second_list.append(int(numbers[1]))

first_list.sort()
second_list.sort()

total_distance = 0
for first, second in zip(first_list, second_list):
    total_distance += abs(second - first)

print(f"Part 1: {total_distance}")

number_counts = dict()
for number in second_list:
    number_counts[number] = number_counts.get(number, 0) + 1

similarity_score = 0
for number in first_list:
    similarity_score += number * number_counts.get(number, 0)

print(f"Part 2: {similarity_score}")
