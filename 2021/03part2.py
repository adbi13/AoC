from sys import argv

def findUnique(binaryList, mostCommon=True):
    bitIndex = 0

    while len(binaryList) > 1 and len(binaryList[0]) > bitIndex:
        frequence = 0
        for number in binaryList:
            if number[bitIndex] == "1":
                frequence += 1
            else:
                frequence -= 1
        
        mostCommonBit = "0" if frequence < 0 else "1"
        notMostCommonBit = "0" if mostCommonBit == "1" else "1"
        
        toDelete = []

        for number in binaryList:
            if number[bitIndex] != (mostCommonBit if mostCommon else notMostCommonBit):
                toDelete.append(number)
        
        for number in toDelete:
            binaryList.remove(number)

        bitIndex += 1

if __name__ == '__main__':
    inputFile = open(argv[1], "r")

    binaryNumbers = [line.strip() for line in inputFile.readlines()]

    inputFile.close()

    oxygen = binaryNumbers.copy()
    co2 = binaryNumbers.copy()

    findUnique(oxygen)
    findUnique(co2, mostCommon=False)

    result = int(oxygen[0], base=2) * int(co2[0], base=2)
    print(f"Result: {result}")
