#ifndef _NONPNP_H_
#define _NONPNP_H_

#include <ntddk.h>

#define NTSTRSAFE_LIB
//#include <ntstrsafe.h>
//#include "Trace.h" // contains macros for WPP tracing

#ifndef POOL_TAG
#define POOL_TAG 'kram'
#endif

DRIVER_INITIALIZE DriverEntry;
DRIVER_UNLOAD StopService;

VOID PrintCUnicodeString(PCUNICODE_STRING str);

NTSTATUS
StartRegistryMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath);

NTSTATUS
StartProcessMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath);

NTSTATUS
StartNetworkMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath);

NTSTATUS
StartFileMonitoring(
IN OUT PDRIVER_OBJECT   DriverObject,
IN PUNICODE_STRING      RegistryPath);

NTSTATUS
StopRegistryMonitoring();

NTSTATUS
StopProcessMonitoring();

NTSTATUS
StopNetworkMonitoring();

NTSTATUS
StopFileMonitoring();

#endif