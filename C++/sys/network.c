#include "nonpnp.h"

NTSTATUS
#pragma warning(suppress: 28101)
StartNetworkMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath)
{
    UNREFERENCED_PARAMETER(DriverObject);
    UNREFERENCED_PARAMETER(RegistryPath);

    return STATUS_SUCCESS;
}

NTSTATUS
StopNetworkMonitoring()
{
    return STATUS_SUCCESS;
}