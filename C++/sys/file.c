//#include "nonpnp.h"
#include <fltKernel.h>

PFLT_FILTER pFilter;

VOID FltUnload()
{
    if (pFilter)
        FltUnregisterFilter(pFilter);
}

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
    UNREFERENCED_PARAMETER(CompletionContext);
    //    NTSTATUS status;
    PFILE_OBJECT FileObject;
    //    UNICODE_STRING fullPath;
    //    UNICODE_STRING processName;
    //    PWCHAR Volume;

    //    FLT_PREOP_CALLBACK_STATUS returnStatus = FLT_PREOP_SUCCESS_NO_CALLBACK;

    /* If this is a callback for a FS Filter driver then we ignore the event */
    if (FLT_IS_FS_FILTER_OPERATION(Data))
    {
        return FLT_PREOP_SUCCESS_NO_CALLBACK;
    }

    if (FltObjects->FileObject != NULL && Data != NULL) {
        FileObject = Data->Iopb->TargetFileObject;
        if (FileObject != NULL && Data->Iopb->MajorFunction == IRP_MJ_CREATE)
        {
            //            status = GetProcessImageName(&processName);
            // KdPrint(("FILE OPERATION Process: %ws\n", FileObject->FileName.Buffer));

        }
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

    { IRP_MJ_CREATE,
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

    sizeof(FLT_REGISTRATION),         //  Size
    FLT_REGISTRATION_VERSION,           //  Version
    0,                                  //  Flags

    Contexts,                               //  Context
    Callbacks,                          //  Operation callbacks

    (PFLT_FILTER_UNLOAD_CALLBACK)FilterUnload,                     //  FilterUnload

    (PFLT_INSTANCE_SETUP_CALLBACK)FilterLoad,                    //  InstanceSetup
    NULL,            //  InstanceQueryTeardown
    NULL,            //  InstanceTeardownStart
    NULL,         //  InstanceTeardownComplete

    NULL,                 //  GenerateFileName
    NULL            //  NormalizeNameComponent
};

NTSTATUS
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
    KdPrint(("Status for FltRegisterFilter is %x : %x\n", pFilter, status));
    if (pFilter)
        status = FltStartFiltering(pFilter);
    KdPrint(("Status for FltStartFiltering is %d : %x\n", status, status));
    return STATUS_SUCCESS;
}

NTSTATUS
StopFileMonitoring()
{
    FltUnload();
    return STATUS_SUCCESS;
}