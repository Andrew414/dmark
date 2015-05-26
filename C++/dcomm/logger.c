#include "communicator.h"
#include <stdio.h>

int SaveMessageToLog(PMARK_EVENT evt)
{
    printf("%x: PID:%6x, PPID:%6x, TID:%6x, OPERATION=%s.%s, FLAGS=%8x, USERNAME=%S, PATH=%S, IMAGE=%S, PROCESS=%S\n",
        evt->time,
        evt->pid,
        evt->ppid,
        evt->tid,
        evt->opclass == MARK_OPCLASS_PROCESS ? "PROCESS " : evt->opclass == MARK_OPCLASS_REGISTRY ? "REGISTRY" :
        evt->opclass == MARK_OPCLASS_PACKET ? "NETWORK " : evt->opclass == MARK_OPCLASS_FILE ? "FILE    " : "UNKNOWN ",
        evt->optype == MARK_OPTYPE_CREATE ? "CREATE " : evt->optype == MARK_OPTYPE_DESTROY ? "DESTROY" :
        evt->optype == MARK_OPTYPE_RENAME ? "RENAME " : evt->optype == MARK_OPTYPE_WRITE ? "WRITE  " : "UNKNOWN",
        evt->flags,
        evt->szUserName,
        evt->szOperationPath,
        evt->szImagePath,
        evt->szProcessName
        );

    return 1;
}