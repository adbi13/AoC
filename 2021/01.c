#include <stdlib.h>
#include <stdio.h>

#define DEPTH 3

int lastMeasurmentsSum(const int* measurments, unsigned int actualIndex)
{
    int sum = 0;
    for (int actual = 0; actual < DEPTH; actual++)
    {
        sum += measurments[actualIndex];
        actualIndex = actualIndex == 0 ? DEPTH : actualIndex - 1;
    }
    return sum;
}

int main(int argc, char const *argv[])
{
    int measurments[DEPTH + 1] = { 0 };
    unsigned int actualIndex = 0;

    while (actualIndex < DEPTH)
    {
        if (scanf("%d", measurments + actualIndex) != 1)
        {
            fprintf(stderr, "Invalid input!\n");
            return 1;
        }
        actualIndex++;
    }

    int increased = 0;
    int previousIndex;

    while (scanf("%d", measurments + actualIndex) == 1)
    {
        previousIndex = actualIndex == 0 ? DEPTH : actualIndex - 1;
        if (lastMeasurmentsSum(measurments, actualIndex) > lastMeasurmentsSum(measurments, previousIndex))
        {
            increased++;
        }
        actualIndex = actualIndex == DEPTH ? 0 : actualIndex + 1;
    }

    printf("%d measurements larger than the previous %d: %d\n", DEPTH, DEPTH, increased);
    return 0;
}
