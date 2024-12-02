def is_safe(report):
    differences = [level1 - level2
                    for level1, level2 in zip(report[:-1], report[1:])]
    trend = differences[0] > 0
    for difference in differences:
        if abs(difference) > 3 or difference == 0:
            return False
        if (difference > 0) != trend:
            return False
    return True


def get_safe_count(reports, problem_dampener=False):
    safe_reports_count = 0

    for report in reports:
        if is_safe(report):
            safe_reports_count += 1
        elif problem_dampener:
            for i in range(len(report)):
                fixed_report = report[:i] + report[i + 1:]
                if is_safe(fixed_report):
                    safe_reports_count += 1
                    break
    
    return safe_reports_count


if __name__ == "__main__":
    with open("input02.txt") as input_file:
        reports = [[int(level.strip()) for level in line.split()] for line in input_file]

    print(f"Part 1: {get_safe_count(reports)}")
    print(f"Part 2: {get_safe_count(reports, True)}")
