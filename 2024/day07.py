from typing import List

def is_possible(result: int, values: List[int], concat=False):
    if len(values) == 1:
        return result == values[0]
    actual_value = values.pop()
    number_length = 10 ** len(str(actual_value))
    if (concat and result % number_length == actual_value
        and is_possible(result // number_length, values.copy(), concat)):
        return True
    if (result % actual_value == 0
        and is_possible(result // actual_value, values.copy(), concat)):
        return True
    return is_possible(result - actual_value, values, concat)

if __name__ == "__main__":
    result_sum = 0
    result_sum2 = 0
    with open("input07.txt") as input_file:
        for line in input_file:
            result, values = line.split(":")
            result = int(result)
            values = [int(value) for value in values.strip().split()]
            if is_possible(result, values.copy()):
                result_sum += result
            if is_possible(result, values, True):
                result_sum2 += result
    
    print(f"Part 1: {result_sum}")
    print(f"Part 2: {result_sum2}")
