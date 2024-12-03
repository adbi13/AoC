import re

def part01(text):
    result = 0
    regex = re.compile(r"mul\((\d+),(\d+)\)")

    for found_match in regex.finditer(text):
        result += int(found_match.group(1)) * int(found_match.group(2))
    
    return result


def part02(text):
    result = 0

    regex = re.compile(r"don't\(\)(?:(?:(?!do\(\)).)*mul\((\d+),(\d+)\))+", flags=re.DOTALL)
    text = regex.sub("XXX", text)

    regex = re.compile(r"mul\((\d+),(\d+)\)")
    for found_match in regex.finditer(text):
        result += int(found_match.group(1)) * int(found_match.group(2))
    
    return result



if __name__ == "__main__":
    with open("input03.txt") as input_file:
        text = input_file.read()

    print(f"Part 1: {part01(text)}")
    print(f"Part 2: {part02(text)}")
