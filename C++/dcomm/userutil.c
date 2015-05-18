#include "communicator.h"

int CheckUnique()
{
    return 1;
}

int SetUnique()
{
    return 0;
}

int UniqueProcess()
{
    if (!CheckUnique())
    {
        return 0;
    }

    SetUnique();

    return 1;
}