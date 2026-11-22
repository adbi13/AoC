use std::{fs::File, io::{BufRead, BufReader}};

fn validate_line(line: &str) -> Result<bool, String> {
    let mut line_split = line.split(": ");
    let (Some(policy), Some(password)) = (line_split.next(), line_split.next()) else {
        return Err("Invalid line format.".to_string());
    };

    let mut policy_split = policy.split(' ');
    let (Some(range), Some(character)) = (policy_split.next(), policy_split.next()) else {
        return Err("Invalid policy format.".to_string());
    };

    let mut range_split = range.split('-');
    let (Some(lower), Some(upper)) = (range_split.next(), range_split.next()) else {
        return Err("Invalid range format.".to_string());
    };

    let Ok(lower) = lower.parse::<u32>() else {
        return Err("Lower bound is not a number.".to_string());
    };

    let Ok(upper) = upper.parse::<u32>() else {
        return Err("Upper bound is not a number.".to_string());
    };

    let Some(character) = character.chars().nth(0) else {
        return Err("Invalid char to look for.".to_string());
    };

    Ok((lower..=upper).contains(&(password.chars().filter(|ch| *ch == character).count() as u32)))
}

fn part01(file_name: String) -> Result<usize, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let result = reader
        .lines()
        .map(|line| line
            .unwrap())
        .filter(|line| validate_line(line).unwrap())
        .count();

    Ok(result)
}

fn validate_line_part2(line: &str) -> Result<bool, String> {
    let mut line_split = line.split(": ");
    let (Some(policy), Some(password)) = (line_split.next(), line_split.next()) else {
        return Err("Invalid line format.".to_string());
    };

    let mut policy_split = policy.split(' ');
    let (Some(range), Some(character)) = (policy_split.next(), policy_split.next()) else {
        return Err("Invalid policy format.".to_string());
    };

    let mut range_split = range.split('-');
    let (Some(lower), Some(upper)) = (range_split.next(), range_split.next()) else {
        return Err("Invalid range format.".to_string());
    };

    let Ok(lower) = lower.parse::<u32>() else {
        return Err("Lower bound is not a number.".to_string());
    };

    let Ok(upper) = upper.parse::<u32>() else {
        return Err("Upper bound is not a number.".to_string());
    };

    let Some(character) = character.chars().nth(0) else {
        return Err("Invalid char to look for.".to_string());
    };

    let result1 = password.chars().nth(lower as usize - 1).unwrap_or('#') == character;
    let result2 = password.chars().nth(upper as usize - 1).unwrap_or('#') == character;

    match (result1, result2) {
        (true, false) | (false, true) => Ok(true),
        _ => Ok(false)
    }
}

fn part02(file_name: String) -> Result<usize, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let result = reader
        .lines()
        .map(|line| line
            .unwrap())
        .filter(|line| validate_line_part2(line).unwrap())
        .count();

    Ok(result)
}

fn main() -> Result<(), String> {
    let result = part02("inputs/day02.txt".to_string())?;
    println!("{}", result);

    Ok(())
}

#[test]
fn example_part1() -> Result<(), String> {
    let result = part01("inputs/day02example.txt".to_string())?;
    assert_eq!(result, 2);
    Ok(())
}

#[test]
fn example_part2() -> Result<(), String> {
    let result = part02("inputs/day02example.txt".to_string())?;
    assert_eq!(result, 1);
    Ok(())
}
