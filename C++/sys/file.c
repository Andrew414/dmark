//#include "nonpnp.h"
#include <fltKernel.h>
#include "core.h"

PFLT_FILTER pFilter;

VOID FltUnload()
{
    if (pFilter)
        FltUnregisterFilter(pFilter);
}

//_Dispatch_type_CreateClose
NTSTATUS
DispatchIRPs(
_In_ PDEVICE_OBJECT DeviceObject,
_In_ PIRP           Irp
)
{
    UNREFERENCED_PARAMETER(DeviceObject);
    Irp->IoStatus.Status = STATUS_SUCCESS;
    IoCompleteRequest(Irp,
        IO_NO_INCREMENT
        );
    return Irp->IoStatus.Status;
}

FLT_PREOP_CALLBACK_STATUS
PreFileOperationCallback(
_Inout_ PFLT_CALLBACK_DATA Data,
_In_ PCFLT_RELATED_OBJECTS FltObjects,
_Out_ PVOID *CompletionContext
)
{
    __try
    {

        CompletionContext = NULL;
        PFILE_OBJECT FileObject;

        if (FLT_IS_FS_FILTER_OPERATION(Data))
        {
            return FLT_PREOP_SUCCESS_NO_CALLBACK;
        }

        if (FltObjects->FileObject != NULL && Data != NULL) {
            FileObject = !Data ? 0 : !Data->Iopb ? 0 : Data->Iopb->TargetFileObject;
            if (FileObject != NULL && Data->Iopb->MajorFunction == IRP_MJ_WRITE)
            {
                MARK_EVENT NewEvent = { 0 };

                long pid = (long)PsGetCurrentProcessId();

                PMARK_PROCESS pProc = FindLoadProcess(pid);

                RtlCopyMemory(NewEvent.szImagePath, pProc ? pProc->szImagePath : L"Unknown", sizeof(NewEvent.szImagePath));
                RtlCopyMemory(NewEvent.szOperationPath,
                    FileObject->FileName.Buffer,
                    MIN(sizeof(NewEvent.szOperationPath), FileObject->FileName.Length));
                RtlCopyMemory(NewEvent.szProcessName, pProc ? pProc->szProcessName : L"Unknown", sizeof(NewEvent.szProcessName));
                RtlCopyMemory(NewEvent.szUserName, pProc ? pProc->szUserName : L"Unknown", sizeof(NewEvent.szUserName));

                NewEvent.flags = FileObject->Flags;

                NewEvent.time = 0;// time(0);
                NewEvent.pid = (long)pid;
                NewEvent.ppid = pProc ? pProc->ppid : 0;
                NewEvent.tid = (long)Data->Thread;

                NewEvent.opclass = MARK_OPCLASS_FILE;
                NewEvent.optype = MARK_OPTYPE_WRITE;


                HandleFileEvent(&NewEvent);
            }
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return FLT_PREOP_SUCCESS_NO_CALLBACK;
    }

    return FLT_PREOP_SUCCESS_NO_CALLBACK;
}

FLT_POSTOP_CALLBACK_STATUS PostFileOperationCallback(_Inout_ PFLT_CALLBACK_DATA Data,
    _In_ PCFLT_RELATED_OBJECTS FltObjects,
    _In_ PVOID CompletionContext,
    _In_ FLT_POST_OPERATION_FLAGS Flags)
{
    UNREFERENCED_PARAMETER(Data);
    UNREFERENCED_PARAMETER(FltObjects);
    UNREFERENCED_PARAMETER(CompletionContext);
    UNREFERENCED_PARAMETER(Flags);
    return FLT_POSTOP_FINISHED_PROCESSING;
}

const FLT_OPERATION_REGISTRATION Callbacks[] = {

    { IRP_MJ_WRITE,
    0,
    (PFLT_PRE_OPERATION_CALLBACK)PreFileOperationCallback,
    (PFLT_POST_OPERATION_CALLBACK)PostFileOperationCallback },

    { IRP_MJ_OPERATION_END }
};

const FLT_CONTEXT_REGISTRATION Contexts[] = {
    { FLT_CONTEXT_END }
};

NTSTATUS FilterLoad(_In_ PCFLT_RELATED_OBJECTS  FltObjects,
    _In_ FLT_INSTANCE_SETUP_FLAGS  Flags,
    _In_ DEVICE_TYPE  VolumeDeviceType,
    _In_ FLT_FILESYSTEM_TYPE  VolumeFilesystemType)
{
    UNREFERENCED_PARAMETER(Flags);
    UNREFERENCED_PARAMETER(FltObjects);
    UNREFERENCED_PARAMETER(VolumeFilesystemType);

    if (VolumeDeviceType == FILE_DEVICE_NETWORK_FILE_SYSTEM) {
        return STATUS_FLT_DO_NOT_ATTACH;
    }

    return STATUS_SUCCESS;
}

NTSTATUS FilterUnload(_In_ FLT_FILTER_UNLOAD_FLAGS Flags)
{
    UNREFERENCED_PARAMETER(Flags);
    return STATUS_SUCCESS;
}


CONST FLT_REGISTRATION FilterRegistration = {

    sizeof(FLT_REGISTRATION),                       //  Size
    FLT_REGISTRATION_VERSION,                       //  Version
    0,                                              //  Flags

    Contexts,                                       //  Context
    Callbacks,                                      //  Operation callbacks

    (PFLT_FILTER_UNLOAD_CALLBACK)FilterUnload,      //  FilterUnload

    (PFLT_INSTANCE_SETUP_CALLBACK)FilterLoad,       //  InstanceSetup
    NULL,                                           //  InstanceQueryTeardown
    NULL,                                           //  InstanceTeardownStart
    NULL,                                           //  InstanceTeardownComplete

    NULL,                                           //  GenerateFileName
    NULL                                            //  NormalizeNameComponent
};

NTSTATUS
#pragma warning(suppress: 28101)
StartFileMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath)
{
    UNREFERENCED_PARAMETER(RegistryPath);

    INT i;
    NTSTATUS status;

    for (i = 0; i < IRP_MJ_MAXIMUM_FUNCTION; i++)
    {
        DriverObject->MajorFunction[i] = DispatchIRPs;
    }

    status = FltRegisterFilter(DriverObject, &FilterRegistration, &pFilter);
    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "Status for FltRegisterFilter is %p : %x\n", pFilter, status));
    if (pFilter)
        status = FltStartFiltering(pFilter);
    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "Status for FltStartFiltering is %d : %x\n", status, status));
    return STATUS_SUCCESS;
}

NTSTATUS
StopFileMonitoring()
{
    FltUnload();
    return STATUS_SUCCESS;
}