#include <stdlib.h>
#include <stdio.h>
#include <string.h>

int main(int argc, char const *argv[])
{
    char command[8];
    int units;

    int depth = 0;
    int horizontalPosition = 0;
    int aim = 0;

    while(scanf("%s %d", command, &units) == 2)
    {
        if (strcmp(command, "forward") == 0)
        {
            horizontalPosition += units;
            depth += aim * units;
        }
        else if (strcmp(command, "down") == 0)
        {
            aim += units;
        }
        else if (strcmp(command, "up") == 0)
        {
            aim -= units;
        }
        else
        {
            fprintf(stderr, "Invalid command!");
            return EXIT_FAILURE;            
        }
    }

    if (!feof(stdin))
    {
        fprintf(stderr, "Invalid format of input!");
        return EXIT_FAILURE;
    }

    int result = horizontalPosition * depth;

    printf("Final horizontal position: %d\n"
            "Final depth: %d\n"
            "Result: %d\n", horizontalPosition, depth, result);

    return EXIT_SUCCESS;
}
