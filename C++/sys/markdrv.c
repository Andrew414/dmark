#include "markdrv.h"
#include "core.h"

NTSTATUS 
#pragma warning(suppress: 28101)
StartCommonService(
    IN OUT PDRIVER_OBJECT   DriverObject,
    IN PUNICODE_STRING      RegistryPath
)
{
    UNREFERENCED_PARAMETER(RegistryPath);

    DriverObject->DriverUnload = StopService;

    return STATUS_SUCCESS;
}

NTSTATUS
DriverEntry(
    IN OUT PDRIVER_OBJECT   DriverObject,
    IN PUNICODE_STRING      RegistryPath
    )
{
    
    NTSTATUS status = STATUS_SUCCESS;
    __asm int 3;

    if (!NT_SUCCESS(status = StartCommonService(DriverObject, RegistryPath)))
        return status;

    if (!NT_SUCCESS(status = StartProcessMonitoring(DriverObject, RegistryPath)))
        return status;
   
    if (!NT_SUCCESS(status = StartRegistryMonitoring(DriverObject, RegistryPath)))
        return status;

    if (!NT_SUCCESS(status = StartFileMonitoring(DriverObject, RegistryPath)))
        return status;

    if (!NT_SUCCESS(status = StartNetworkMonitoring(DriverObject, RegistryPath)))
        return status;

    return status;
}

int SendEvent(PMARK_EVENT evt)
{
#if 1
    KdPrint(("%x: PID:%6x, PPID:%6x, TID:%6x, OPERATION=%s.%s, FLAGS=%8x, USERNAME=%S, PATH=%S, IMAGE=%S, PROCESS=%S\n", 
        evt->time,
        evt->pid,
        evt->ppid,
        evt->tid,
        evt->opclass == MARK_OPCLASS_PROCESS ? "PROCESS " : evt->opclass == MARK_OPCLASS_REGISTRY ? "REGISTRY" : 
        evt->opclass == MARK_OPCLASS_PACKET  ? "NETWORK " : evt->opclass == MARK_OPCLASS_FILE     ? "FILE    " : "UNKNOWN ",
        evt->optype == MARK_OPTYPE_CREATE ? "CREATE " : evt->optype == MARK_OPTYPE_DESTROY ? "DESTROY" :
        evt->optype == MARK_OPTYPE_RENAME ? "RENAME " : evt->optype == MARK_OPTYPE_WRITE   ? "WRITE  " : "UNKNOWN",
        evt->flags,
        evt->szUserName,
        evt->szOperationPath,
        evt->szImagePath,
        evt->szProcessName
        ));
#else
    UNREFERENCED_PARAMETER(evt);
#endif
    return 0;
}

VOID
StopService(IN PDRIVER_OBJECT DriverObject)
{
    UNREFERENCED_PARAMETER(DriverObject);
    KdPrint(("Entered StopService\n"));

    StopFileMonitoring();
    StopRegistryMonitoring();
    StopNetworkMonitoring();
    StopProcessMonitoring();
    return;
}