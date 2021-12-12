#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#define BINARY_LENGTH 12

void printBinary(unsigned long num)
{
    while (num)
    {
        if (num & 1)
        {
            printf("1");
        }   
        else
        {
            printf("0");
        }
        num >>= 1;
    }
    printf("\n");
}

int main(int argc, char const *argv[])
{
    char inputBinaryString[BINARY_LENGTH];
    unsigned long inputBinary;

    unsigned long mask = 1;
    int frequency[BINARY_LENGTH] = { 0 };

    while (scanf("%s", inputBinaryString) == 1)
    {
        inputBinary = strtoul(inputBinaryString, NULL, 2);
        for (size_t actualBit = 0; actualBit < BINARY_LENGTH; actualBit++)
        {
            if ((inputBinary & mask) != 0)
            {
                frequency[actualBit]++;
            }
            else
            {
                frequency[actualBit]--;
            }
            mask <<= 1;
        }
        mask = 1;
    }

    if (!feof(stdin))
    {
        fprintf(stderr, "Invalid format of input!\n");
        return EXIT_FAILURE;
    }

    unsigned long gammaRate = 0;
    unsigned long epsilonRate = 0;
    for (size_t actualBit = 0; actualBit < BINARY_LENGTH; actualBit++)
    {
        if (frequency[actualBit] > 0)
        {
            gammaRate |= mask;
        }
        else
        {
            epsilonRate |= mask;
        }
        mask <<= 1;
    }

    unsigned long result = gammaRate * epsilonRate;

    printf("Gamma rate: %d | ", gammaRate);
    printBinary(gammaRate);
    printf("Epsilon rate: %d | ", epsilonRate);
    printBinary(epsilonRate);
    printf("Result: %lu\n", result);
}
