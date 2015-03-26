#include "..\sys\core.h"
#include <stdio.h>

int SendEvent(PMARK_EVENT evt)
{
    printf("%S (%S) => %x%x : %S\n\n", evt->szProcessName, evt->szUserName, evt->opclass, evt->optype, evt->szOperationPath);
    return 0;
}