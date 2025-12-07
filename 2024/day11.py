class Node():
    def __init__(self, value):
        self.value = value
        self.prev = None
        self.next = None

    def update(self):
        if self.value == 0:
            self.value = 1
        elif (digit_count := len(str(self.value))) % 2 == 0:
            divisor = 10 ** (digit_count // 2)
            tmp = self.next
            self.next = Node(self.value % divisor)
            if tmp is not None:
                tmp.prev = self.next
            self.next.next = tmp
            self.next.prev = self
            self.value = self.value // divisor
        else:
            self.value *= 2024

class LinkedList():
    def __init__(self):
        self.first = None
        self.last = None
    
    def append(self, value):
        new_node = Node(value)
        if self.first is None:
            self.first = new_node
        else:
            self.last.next = new_node
        new_node.prev = self.last
        self.last = new_node

    def count_values(self):
        actual_node = self.first
        result = 0
        while actual_node is not None:
            result += 1
            actual_node = actual_node.next
        return result

    def update(self):
        actual_node = self.last
        while actual_node is not None:
            actual_node.update()
            actual_node = actual_node.prev
        while self.last.next is not None:
            self.last = self.last.next

    def print(self):
        actual_node = self.first
        while actual_node is not None:
            print(f"{actual_node.value}", end=" ")
            actual_node = actual_node.next
        print()


if __name__ == "__main__":
    with open("input11.txt") as input_file:
        values = [int(value) for value in input_file.read().strip().split()]

    linked_list = LinkedList()
    for value in values:
        linked_list.append(value)
    
    linked_list.print()
    for _ in range(25):
        linked_list.update()
        # linked_list.print()
        print(linked_list.count_values())
    result = linked_list.count_values()

    print(f"Part 1: {result}")

    # for _ in range(50):
    #     linked_list.update()
    # result = linked_list.count_values()

    # print(f"Part 2: {result}")
