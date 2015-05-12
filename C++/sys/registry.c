#include <Ntifs.h>
//#include "nonpnp.h"
#include "core.h"

LARGE_INTEGER RegCookie;

#pragma warning(suppress: 6262)
VOID HandleDeleteKeyEvent(PREG_DELETE_KEY_INFORMATION pDelInfo)
{
    MARK_EVENT NewEvent = { 0 };

    WCHAR buffer[sizeof(NewEvent.szOperationPath)] = { 0 };
    OBJECT_NAME_INFORMATION info = { 0 };
    unsigned long infoLen = sizeof(buffer);
    long pid = (long)PsGetCurrentProcessId();
    NTSTATUS status = 0;

    info.Name.Buffer = buffer;
    info.Name.MaximumLength = sizeof(buffer);
    info.Name.Length = 0;

    status = ObQueryNameString(pDelInfo->Object, &info, sizeof(buffer), &infoLen);

    PMARK_PROCESS pProc = FindLoadProcess(pid);

    RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath, NT_SUCCESS(status) ? info.Name.Buffer : L"Unknown", sizeof(NewEvent.szOperationPath));
    RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

    NewEvent.flags = 0;
    NewEvent.time = 0;// time(0);
    NewEvent.pid = (long)pid;
    NewEvent.ppid = pProc ? pProc->ppid : 0;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_REGISTRY;
    NewEvent.optype = MARK_OPTYPE_DESTROY;

    HandleRegistryEvent(&NewEvent);

    return;
}

#pragma warning(suppress: 6262)
VOID HandleDeleteKeyValueEvent(PREG_DELETE_VALUE_KEY_INFORMATION pDelInfo)
{
    MARK_EVENT NewEvent = { 0 };

    WCHAR buffer[sizeof(NewEvent.szOperationPath) / sizeof(WCHAR)] = { 0 };
    OBJECT_NAME_INFORMATION info = { 0 };
    unsigned long infoLen = sizeof(buffer);
    int pid = (long)PsGetCurrentProcessId();
    NTSTATUS status = 0;

    info.Name.Buffer = buffer;
    info.Name.MaximumLength = sizeof(buffer);
    info.Name.Length = 0;

    status = ObQueryNameString(pDelInfo->Object, &info, sizeof(buffer), &infoLen);

    PMARK_PROCESS pProc = FindLoadProcess(pid);

    RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath,
        (pDelInfo && pDelInfo->ValueName && pDelInfo->ValueName->Buffer) ? pDelInfo->ValueName->Buffer : L"Unknown",
        (pDelInfo && pDelInfo->ValueName && pDelInfo->ValueName->Buffer) ? MIN(sizeof(NewEvent.szOperationPath), pDelInfo->ValueName->Length) : sizeof(L"Unknown"));
    RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

    NewEvent.flags = 0;
    NewEvent.time = 0;// time(0);
    NewEvent.pid = (long)pid;
    NewEvent.ppid = pProc ? pProc->ppid : 0;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_REGISTRY;
    NewEvent.optype = MARK_OPTYPE_DESTROY;

    HandleRegistryEvent(&NewEvent);

    return;
}

#pragma warning(suppress: 6262)
VOID HandleSetValueEvent(PREG_SET_VALUE_KEY_INFORMATION pSetInfo)
{
    MARK_EVENT NewEvent = { 0 };

    long pid = (long)PsGetCurrentProcessId();

    PMARK_PROCESS pProc = FindLoadProcess(pid);

    RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath, 
        (pSetInfo && pSetInfo->ValueName && pSetInfo->ValueName->Buffer) ? pSetInfo->ValueName->Buffer : L"Unknown", 
        (pSetInfo && pSetInfo->ValueName && pSetInfo->ValueName->Buffer) ? MIN(sizeof(NewEvent.szOperationPath), pSetInfo->ValueName->Length) : sizeof(L"Unknown"));
    RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

    NewEvent.flags = 0;
    NewEvent.time = 0;// time(0);
    NewEvent.pid = (long)pid;
    NewEvent.ppid = pProc ? pProc->ppid : 0;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_REGISTRY;
    NewEvent.optype = MARK_OPTYPE_WRITE;

    HandleRegistryEvent(&NewEvent);

    return;
}

#pragma warning(suppress: 6262)
VOID HandleRenameEvent(PREG_RENAME_KEY_INFORMATION pRenameInfo)
{
    MARK_EVENT NewEvent = { 0 };
    
    long pid = (long)PsGetCurrentProcessId();
    PMARK_PROCESS pProc = FindLoadProcess(pid);

    RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath,
        (pRenameInfo && pRenameInfo->NewName && pRenameInfo->NewName->Buffer) ? pRenameInfo->NewName->Buffer : L"Unknown",
        (pRenameInfo && pRenameInfo->NewName && pRenameInfo->NewName->Buffer) ? MIN(sizeof(NewEvent.szOperationPath), pRenameInfo->NewName->Length) : sizeof(L"Unknown"));
    RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

    NewEvent.flags = 0;
    NewEvent.time = 0;// time(0);
    NewEvent.pid = (long)pid;
    NewEvent.ppid = pProc ? pProc->ppid : 0;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_REGISTRY;
    NewEvent.optype = MARK_OPTYPE_RENAME;

    HandleRegistryEvent(&NewEvent);

    return;
}

#pragma warning(suppress: 6262)
VOID HandleCreateKeyEvent(PREG_CREATE_KEY_INFORMATION_V1 pCreateInfo)
{
    if (!(pCreateInfo->DesiredAccess & KEY_WRITE || 
          pCreateInfo->DesiredAccess & KEY_SET_VALUE || 
          pCreateInfo->DesiredAccess & KEY_CREATE_SUB_KEY))
    {
        return;
    }
    MARK_EVENT NewEvent = { 0 };

    long pid = (long)PsGetCurrentProcessId();
    PMARK_PROCESS pProc = FindLoadProcess(pid);

    RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
    RtlCopyMemory(NewEvent.szOperationPath,
        (pCreateInfo && pCreateInfo->CompleteName && pCreateInfo->CompleteName->Buffer) ? pCreateInfo->CompleteName->Buffer : L"Unknown",
        (pCreateInfo && pCreateInfo->CompleteName && pCreateInfo->CompleteName->Buffer) ? MIN(sizeof(NewEvent.szOperationPath), pCreateInfo->CompleteName->Length) : sizeof(L"Unknown"));
    RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
    RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

    NewEvent.flags = 0;
    NewEvent.time = 0;// time(0);
    NewEvent.pid = (long)pid;
    NewEvent.ppid = pProc ? pProc->ppid : 0;
    NewEvent.tid = -1;

    NewEvent.opclass = MARK_OPCLASS_REGISTRY;
    NewEvent.optype = MARK_OPTYPE_DESTROY;

    HandleRegistryEvent(&NewEvent);

    return;
}

NTSTATUS RegCallback(
    _In_      PVOID CallbackContext,
    _In_opt_  PVOID Argument1,
    _In_opt_  PVOID Argument2
    )
{
#if 1
    UNREFERENCED_PARAMETER(CallbackContext);
#else
    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "RegCallback called for %x : %x : %x\n", CallbackContext, Argument1, Argument2));
#endif

    if (!Argument2)
    {
        return STATUS_SUCCESS;
    }

    __try
    {

        switch ((int)Argument1)
        {
        case RegNtPreDeleteKey:
            HandleDeleteKeyEvent(Argument2);
            break;
        case RegNtPreDeleteValueKey:
            HandleDeleteKeyValueEvent(Argument2);
            break;
        case RegNtPreSetValueKey:
            HandleSetValueEvent(Argument2);
            break;
        case RegNtPreRenameKey:
            HandleRenameEvent(Argument2);
            break;
        case RegNtPreCreateKeyEx:
            HandleCreateKeyEvent(Argument2);
            break;
        default:
            break;
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return STATUS_SUCCESS;
    }

    return STATUS_SUCCESS;
}


NTSTATUS
#pragma warning(suppress: 28101)
StartRegistryMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath)
{
    UNREFERENCED_PARAMETER(DriverObject);
    UNREFERENCED_PARAMETER(RegistryPath);

    NTSTATUS status;

    DECLARE_CONST_UNICODE_STRING(szAltitude, L"268400");

    status = CmRegisterCallbackEx(RegCallback, &szAltitude, &DriverObject, NULL, &RegCookie, NULL);
    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "Status for CmRegisterCallbackEx is %d : %x\n", status, status));

    return status;
}

NTSTATUS
StopRegistryMonitoring()
{
    NTSTATUS status;
    status = CmUnRegisterCallback(RegCookie);
    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "Status for CmUnRegisterCallback is %d : %x\n", status, status));
    return STATUS_SUCCESS;
}