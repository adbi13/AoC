use std::{fs::File, io::{BufRead, BufReader}};

fn part01(file_name: String) -> Result<u64, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let num_lines: Vec<u64> = reader
        .lines()
        .map(|line| line
            .unwrap()
            .parse::<u64>()
            .unwrap())
        .collect();

    for (i, num1) in num_lines.iter().enumerate() {
        for num2 in num_lines.iter().skip(i + 1) {
            if num1 + num2 == 2020 {
                return Ok(num1 * num2);
            }
        }
    }

    Err("Result not found.".to_string())
}

fn part02(file_name: String) -> Result<u64, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let num_lines: Vec<u64> = reader
        .lines()
        .map(|line| line
            .unwrap()
            .parse::<u64>()
            .unwrap())
        .collect();

    for (i, num1) in num_lines.iter().enumerate() {
        for (j, num2) in num_lines.iter().enumerate().skip(i + 1) {
            for num3 in num_lines.iter().skip(j + 1) {
                if num1 + num2 + num3 == 2020 {
                    return Ok(num1 * num2 * num3);
                }
            }
        }
    }

    Err("Result not found.".to_string())
}

fn main() -> Result<(), String> {
    let result = part02("inputs/day01.txt".to_string())?;
    println!("{}", result);

    Ok(())
}

#[test]
fn example_part1() -> Result<(), String> {
    let result = part01("inputs/day01example.txt".to_string())?;
    assert_eq!(result, 514579);
    Ok(())
}

#[test]
fn example_part2() -> Result<(), String> {
    let result = part02("inputs/day01example.txt".to_string())?;
    assert_eq!(result, 241861950);
    Ok(())
}
