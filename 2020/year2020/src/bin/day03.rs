use std::{fs::File, io::{BufRead, BufReader}};

fn count_trees(tree_map: &Vec<Vec<char>>, x_step: usize, y_step: usize) -> u64 {
    let mut x = 0;
    let mut y = 0;
    let mut tree_count = 0;
    let line_len = tree_map[0].len();

    while y < tree_map.len() {
        if tree_map[y][x] == '#' {
            tree_count += 1;
        }
        x = (x + x_step) % line_len;
        y += y_step;
    }

    tree_count
}

fn part01(file_name: String) -> Result<u64, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let tree_map: Vec<Vec<char>> = reader
        .lines()
        .map(|line| line
            .unwrap()
            .chars()
            .collect())
        .collect();

    Ok(count_trees(&tree_map, 3, 1))
}

fn part02(file_name: String) -> Result<u64, String> {
    let Ok(file) = File::open(file_name) else {
        return Err("File not found.".to_string());
    };
    let reader = BufReader::new(file);
    let tree_map: Vec<Vec<char>> = reader
        .lines()
        .map(|line| line
            .unwrap()
            .chars()
            .collect())
        .collect();



    Ok(count_trees(&tree_map, 1, 1)
        * count_trees(&tree_map, 3, 1)
        * count_trees(&tree_map, 7, 1)
        * count_trees(&tree_map, 5, 1)
        * count_trees(&tree_map, 1, 2))
}

fn main() -> Result<(), String> {
    let result = part02("inputs/day03.txt".to_string())?;
    println!("{}", result);

    Ok(())
}

#[test]
fn example_part1() -> Result<(), String> {
    let result = part01("inputs/day03example.txt".to_string())?;
    assert_eq!(result, 7);
    Ok(())
}

#[test]
fn example_part2() -> Result<(), String> {
    let result = part02("inputs/day03example.txt".to_string())?;
    assert_eq!(result, 336);
    Ok(())
}
