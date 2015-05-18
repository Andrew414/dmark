#include "markusermode.h"
#include "core.h"
#include <fltKernel.h>

extern PFLT_FILTER pFilter;

static PFLT_PORT sPort;
static PFLT_PORT cPort;

static BOOLEAN connected = 0;

NTSTATUS ConnectHandler(
    IN PFLT_PORT ClientPort,
    IN PVOID ServerPortCookie,
    IN PVOID ConnectionContext,
    IN ULONG SizeOfContext,
    OUT PVOID *ConnectionPortCookie
    )
{
    UNREFERENCED_PARAMETER(ServerPortCookie);
    UNREFERENCED_PARAMETER(ConnectionContext);
    UNREFERENCED_PARAMETER(SizeOfContext);

    cPort = ClientPort;
    *ConnectionPortCookie = ClientPort;

    connected = 1;

    return STATUS_SUCCESS;
}

VOID DisconnectHandler(_In_ PVOID p) { p; connected = 0; }

NTSTATUS StartConnection(
    IN OUT PDRIVER_OBJECT   DriverObject,
    IN PUNICODE_STRING      RegistryPath)
{
    UNREFERENCED_PARAMETER(RegistryPath);
    UNREFERENCED_PARAMETER(DriverObject);

    NTSTATUS status = 0;

    OBJECT_ATTRIBUTES attr = { 0 };
    UNICODE_STRING name = { 0 };
    RtlInitUnicodeString(&name, PORT_NAME);
    PSECURITY_DESCRIPTOR desc = { 0 };
    FltBuildDefaultSecurityDescriptor(&desc, FLT_PORT_ALL_ACCESS);

    attr.Attributes = OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE;
    attr.Length = sizeof(OBJECT_ATTRIBUTES);
    
    attr.ObjectName = &name;
    attr.SecurityDescriptor = desc;

    status = FltCreateCommunicationPort(
        pFilter,
        &sPort,
        &attr,
        NULL,
        ConnectHandler,
        DisconnectHandler,
        NULL /*MessageNotifyCallback*/,
        16
        );

    KdPrintEx((DPFLTR_DEFAULT_ID, DPFLTR_ERROR_LEVEL, "Status for creating the connection: 0x%x", status));
    return status;
}

NTSTATUS StopConnection()
{
    connected = 0;
    FltCloseCommunicationPort(sPort);

    return STATUS_SUCCESS;
}

void SendToUserMode(PMARK_EVENT evt)
{
    if (!connected)
        return;

    LARGE_INTEGER time = { 0 };
    time.QuadPart = 10 * 100;
    FltSendMessage(
        pFilter,
        &cPort,
        evt,
        sizeof(MARK_EVENT),
        NULL,
        0,
        &time
        );
}