from typing import Dict, Deque, List, Set
from collections import deque

def visit(n: int, E: Dict[int, List[int]], relevant_nodes: Set[int],
          visited: Dict[int, bool], result: Deque[int]) -> None:
    if visited[n] or n not in relevant_nodes:
        return
    visited[n] = True
    for adjacent in E.get(n, []):
        if adjacent in relevant_nodes:
            visit(adjacent, E, relevant_nodes, visited, result)
    result.appendleft(n)

def sort_manual(manual: List[int]) -> List[int]:
    relevant_nodes = set(manual)
    visited = {node: False for node in relevant_nodes}
    topological_order = deque()
    for node in relevant_nodes:
        visit(node, edges, relevant_nodes, visited, topological_order)
    topological_order_indexes = {page: order for order, page in enumerate(topological_order)}
    return list(sorted(manual, key=lambda value: topological_order_indexes[value]))


if __name__ == "__main__":
    edges = dict()
    manuals = []
    with open("input05.txt") as input_file:
        for line in input_file:
            line = line.strip()
            if len(nodes := line.split("|")) > 1:
                from_node = int(nodes[0])
                to_node = int(nodes[1])
                edges[from_node] = edges.get(from_node, []) + [to_node]
            elif line != "":
                manuals.append([int(page) for page in line.split(",")])

    result = 0
    result2 = 0
    for manual in manuals:
        sorted_manual = sort_manual(manual)
        if manual == sorted_manual:
            result += manual[len(manual) // 2]
        else:
            result2 += sorted_manual[len(manual) // 2]

    print(f"Part 1: {result}")
    print(f"Part 2: {result2}")
