//#include "nonpnp.h"
#include "core.h"
#include <ntifs.h>
#include "processtable.h"

#ifndef POOL_TAG
#define POOL_TAG 'kram'
#endif

void MarkCopyMemory(void* dst, void* src, int bytecount)
{
    RtlCopyMemory(dst, src, bytecount);
}
void* MarkMalloc(int bytecount)
{
    return ExAllocatePoolWithTag(PagedPool, bytecount, POOL_TAG);
}
void MarkFree(void* mem)
{
    ExFreePoolWithTag(mem, POOL_TAG);
}

//extern NTSTATUS NTAPI SeLocateProcessImageName(PEPROCESS Process, PUNICODE_STRING Name);

int AddProcess(PMARK_PROCESS pproc);
int LoadProcess(int pid)
{
    MARK_PROCESS Proc = { 0 };
    PEPROCESS wProc = { 0 };
    NTSTATUS status = 0;

    PUNICODE_STRING procName = { 0 };

    Proc.pid = pid;
    Proc.ppid = 0;

    status = PsLookupProcessByProcessId((HANDLE)pid, &wProc);
    
    if (NT_SUCCESS(status))
    {
        status = SeLocateProcessImageName(wProc, &procName);
    }

    MarkCopyMemory(Proc.szImagePath, NT_SUCCESS(status) ? procName->Buffer : L"Unknown", sizeof(Proc.szImagePath));
    MarkCopyMemory(Proc.szUserName, L"Unknown", sizeof(Proc.szUserName));
    MarkCopyMemory(Proc.szProcessName, L"Unknown", sizeof(Proc.szProcessName));

    ObDereferenceObject(wProc);
    
    return AddProcess(&Proc);
}

VOID PrintCUnicodeString(PCUNICODE_STRING str)
{
    short* c = (short*)str->Buffer;
    unsigned int i = 0;
    for (; *c && i <= str->Length; c++, i += 2)
    {
        KdPrint(("%C", *c));
    }
}

VOID
PrintChars(
_In_reads_(CountChars) PCHAR BufferAddress,
_In_ size_t CountChars
)
{
    if (CountChars) {

        while (CountChars--) {

            if (*BufferAddress > 31
                && *BufferAddress != 127) {

                KdPrint(("%c", *BufferAddress));

            }
            else {

                KdPrint(("."));

            }
            BufferAddress++;
        }
        KdPrint(("\n"));
    }
    return;
}

