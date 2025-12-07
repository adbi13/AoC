def create_filesystem(disk_map):
    filesystem = []
    is_file = True
    file_id = 0
    for size in disk_map:
        for _ in range(int(size)):
            filesystem.append(file_id if is_file else None)
        if is_file:
            file_id += 1
        is_file = not is_file
    return filesystem

def rearrange_filesystem(filesystem):
    for i in range(len(filesystem)):
        if i >= len(filesystem):
            break
        while filesystem[i] is None:
            if i == len(filesystem) - 1:
                filesystem.pop()
                return
            filesystem[i] = filesystem.pop()


def rearrange_filesystem_whole_files(filesystem):
    file_id = None
    block_size = 0
    for i in range(len(filesystem) - 1, -1, -1):
        if filesystem[i] == file_id:
            block_size += 1
            continue
        if file_id is not None:
            space_size = 0
            space_start = None
            for j in range(i):
                if filesystem[j] is not None:
                    space_size = 0
                    space_start = None
                    continue
                if space_start is None:
                    space_start = j
                space_size += 1
                if space_size >= block_size:
                    for old, new in zip(range(i + 1, i + 1 + block_size),
                                        range(space_start, space_start + space_size)):
                        filesystem[new] = filesystem[old]
                        filesystem[old] = None
                    break
        file_id = filesystem[i]
        block_size = 1


def checksum(filesystem):
    result = 0
    for i, file_id in enumerate(filesystem):
        if file_id is None:
            continue
        result += i * file_id
    return result

if __name__ == "__main__":
    with open("input09.txt") as input_file:
        disk_map = input_file.read().strip()
    # filesystem = create_filesystem(disk_map)
    # rearrange_filesystem(filesystem)
    # result = checksum(filesystem)

    filesystem = create_filesystem(disk_map)
    rearrange_filesystem_whole_files(filesystem)
    result2 = checksum(filesystem)

    # print(filesystem)

    # print(f"Part 1: {result}")
    print(f"Part 2: {result2}")
