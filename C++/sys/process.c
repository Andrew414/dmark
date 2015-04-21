#include "nonpnp.h"
#include "core.h"
#include "processtable.h"

//#include <time.h>

int DeleteProcess(int pid);
int AddProcess(PMARK_PROCESS pproc);

#pragma warning(suppress: 6262)
VOID ProcessCreationCallback(
    _Inout_   PEPROCESS              Process,
    _In_      HANDLE                 ProcessId,
    _In_opt_  PPS_CREATE_NOTIFY_INFO CreateInfo
    )
{
    UNREFERENCED_PARAMETER(Process);

    MARK_PROCESS NewProc = { 0 };
    MARK_EVENT NewEvent = { 0 };

    if (!CreateInfo)
    {
#if 0
        KdPrint(("Process %d (%x) finished\n", ProcessId, ProcessId));
#endif
        PMARK_PROCESS pProc = FindLoadProcess((int)ProcessId);

        RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
        RtlCopyMemory(NewEvent.szOperationPath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
        RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
        RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

        NewEvent.flags = 0;
        NewEvent.time = 0;// time(0);
        NewEvent.pid = (long)ProcessId;
        NewEvent.ppid = pProc ? pProc->ppid : 0;
        NewEvent.tid = -1;

        NewEvent.opclass = MARK_OPCLASS_PROCESS;
        NewEvent.optype = MARK_OPTYPE_DESTROY;

        HandleProcessEvent(&NewEvent);

        DeleteProcess((int)ProcessId);
        return;
    }

    RtlCopyMemory(NewProc.szImagePath, CreateInfo->ImageFileName->Buffer, MIN(CreateInfo->ImageFileName->Length, sizeof(NewProc.szImagePath)));
    RtlCopyMemory(NewProc.szProcessName, CreateInfo->CommandLine->Buffer, MIN(CreateInfo->CommandLine->Length, sizeof(NewProc.szProcessName)));
    RtlCopyMemory(NewProc.szUserName, L"Unknown", sizeof(L"Unknown"));

    NewProc.pid = (long)ProcessId;
    NewProc.ppid = (long)CreateInfo->ParentProcessId;

    AddProcess(&NewProc);

#if 0
    KdPrint(("Process %d (%x) \"", ProcessId, PidCopy));
    PrintCUnicodeString(CreateInfo->ImageFileName);
    KdPrint(("\" started by PID %d\n", CreateInfo->ParentProcessId));
#endif

    RtlCopyMemory(NewEvent.szImagePath, NewProc.szImagePath, sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath, NewProc.szImagePath, sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szProcessName, NewProc.szProcessName, sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, NewProc.szUserName, sizeof(NewEvent.szUserName));

    NewEvent.flags = CreateInfo->Flags;
    NewEvent.time = 0; // time(0);
    NewEvent.pid = NewProc.pid;
    NewEvent.ppid = NewProc.ppid;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_PROCESS;
    NewEvent.optype = MARK_OPTYPE_CREATE;

    HandleProcessEvent(&NewEvent);
}

NTSTATUS
#pragma warning(suppress: 28101)
StartProcessMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath)
{
    UNREFERENCED_PARAMETER(DriverObject);
    UNREFERENCED_PARAMETER(RegistryPath);

    NTSTATUS status;
    status = PsSetCreateProcessNotifyRoutineEx(ProcessCreationCallback, FALSE);
    KdPrint(("Status for CreateProcessNotifyRoutineEx is %d : %x\n", status, status));

    return status;
}

NTSTATUS
StopProcessMonitoring()
{
    NTSTATUS status;
    status = PsSetCreateProcessNotifyRoutineEx(ProcessCreationCallback, TRUE);
    KdPrint(("Status for CreateProcessNotifyRoutineEx(T) is %d : %x\n", status, status));
    return STATUS_SUCCESS;
}