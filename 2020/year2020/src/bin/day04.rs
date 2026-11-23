use std::{collections::{HashMap, HashSet}, fs::self};

use regex::Regex;

fn parse_input_file(file_name: String) -> Result<Vec<HashMap<String, String>>, String> {
    let Ok(text) = fs::read_to_string(file_name) else {
        return Err("File not found.".to_string());
    };

    Ok(text.split("\n\n")
        .map(|line| line
            .split_whitespace()
            .map(|field| field
                .split_once(':')
                .unwrap())
            .map(|(field, value)| (field.to_string(), value.to_string()))
            .collect())
        .collect())
}

fn part01(file_name: String) -> Result<usize, String> {
    let documets_fields = parse_input_file(file_name)?;

    let expected_fields = HashSet::from([
        "byr",
        "iyr",
        "eyr",
        "hgt",
        "hcl",
        "ecl",
        "pid"
    ].map(|val| val.to_string()));

    Ok(documets_fields
        .iter()
        .map(|hash_map| hash_map
            .keys()
            .map(|key| key
                .to_string())
            .collect::<HashSet<_>>())
        .filter(|fields_set| *fields_set == expected_fields
            || fields_set.symmetric_difference(&expected_fields).collect::<Vec<_>>() == Vec::from(["cid"]))
        .count())
}

fn validate_field(field: &String, value: &String) -> bool {
    match field.as_str() {
        "byr" => {
            let re = Regex::new(r"^((19[2-9]\d)|(200[0-2]))$").unwrap();
            re.is_match(value)
        },
        "iyr" => {
            let re = Regex::new(r"^20((1\d)|(20))$").unwrap();
            re.is_match(value)
        },
        "eyr" => {
            let re = Regex::new(r"^20((2\d)|(30))$").unwrap();
            re.is_match(value)
        },
        "hgt" => {
            let re = Regex::new(r"^(1(([5-8]\d)|(9[0-3]))cm|(((59)|(6\d)|(7[0-6]))in))$").unwrap();
            re.is_match(value)
        },
        "hcl" => {
            let re = Regex::new(r"^#[0-9a-f]{6}$").unwrap();
            re.is_match(value)
        },
        "ecl" => {
            let re = Regex::new(r"^((amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth))$").unwrap();
            re.is_match(value)
        },
        "pid" => {
            let re = Regex::new(r"^\d{9}$").unwrap();
            re.is_match(value)
        },
        "cid" => true,
        _ => false
    }
}

fn part02(file_name: String) -> Result<usize, String> {
    let documets_fields = parse_input_file(file_name)?;

    let expected_fields = HashSet::from([
        "byr",
        "iyr",
        "eyr",
        "hgt",
        "hcl",
        "ecl",
        "pid"
    ].map(|val| val.to_string()));

    Ok(documets_fields
        .iter()
        .filter(|hash_map| hash_map
            .iter()
            .all(|(field, value)| validate_field(field, value)))
        .map(|hash_map| hash_map
            .keys()
            .map(|key| key.to_string())
            .collect::<HashSet<_>>())
        .filter(|fields_set| *fields_set == expected_fields
            || fields_set.symmetric_difference(&expected_fields).collect::<Vec<_>>() == Vec::from(["cid"]))
        .count())
}

fn main() -> Result<(), String> {
    let result = part02("inputs/day04.txt".to_string())?;
    println!("{}", result);

    Ok(())
}

#[test]
fn example_part1() -> Result<(), String> {
    let result = part01("inputs/day04example.txt".to_string())?;
    assert_eq!(result, 2);
    Ok(())
}

#[test]
fn example_part2() -> Result<(), String> {
    let result = part02("inputs/day04example2.txt".to_string())?;
    assert_eq!(result, 4);
    Ok(())
}

#[test]
fn example_part2_example1() -> Result<(), String> {
    let result = part02("inputs/day04example.txt".to_string())?;
    assert_eq!(result, 2);
    Ok(())
}

#[test]
fn example_part2_byr() {
    assert!(validate_field(&"byr".to_string(), &"2002".to_string()));
    assert!(!validate_field(&"byr".to_string(), &"2003".to_string()));
}

#[test]
fn example_part2_hgt() {
    assert!(validate_field(&"hgt".to_string(), &"60in".to_string()));
    assert!(!validate_field(&"hgt".to_string(), &"190in".to_string()));
    assert!(validate_field(&"hgt".to_string(), &"190cm".to_string()));
    assert!(!validate_field(&"hgt".to_string(), &"190".to_string()));
}

#[test]
fn example_part2_hcl() {
    assert!(validate_field(&"hcl".to_string(), &"#123abc".to_string()));
    assert!(!validate_field(&"hcl".to_string(), &"#123abz".to_string()));
    assert!(!validate_field(&"hcl".to_string(), &"123abc".to_string()));
}

#[test]
fn example_part2_ecl() {
    assert!(validate_field(&"ecl".to_string(), &"brn".to_string()));
    assert!(!validate_field(&"ecl".to_string(), &"wat".to_string()));
}

#[test]
fn example_part2_pid() {
    assert!(validate_field(&"pid".to_string(), &"000000001".to_string()));
    assert!(!validate_field(&"pid".to_string(), &"0123456789".to_string()));
}

#[test]
fn example_part2_doc1() {
    assert!(!validate_field(&"eyr".to_string(), &"1972".to_string()));
    assert!(validate_field(&"cid".to_string(), &"100".to_string()));
    assert!(validate_field(&"hcl".to_string(), &"#18171d".to_string()));
    assert!(validate_field(&"ecl".to_string(), &"amb".to_string()));
    assert!(!validate_field(&"hgt".to_string(), &"170".to_string()));
    assert!(!validate_field(&"pid".to_string(), &"186cm".to_string()));
    assert!(validate_field(&"iyr".to_string(), &"2018".to_string()));
    assert!(validate_field(&"byr".to_string(), &"1926".to_string()));
}
