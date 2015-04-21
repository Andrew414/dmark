#ifndef _CORE_H_
#define _CORE_H_

#define MARK_OPCLASS_PROCESS 0x1
#define MARK_OPCLASS_FILE 0x2
#define MARK_OPCLASS_REGISTRY 0x3
#define MARK_OPCLASS_PACKET 0x4

#define MARK_OPTYPE_CREATE 0x1
#define MARK_OPTYPE_DESTROY 0x2
#define MARK_OPTYPE_WRITE 0x3
#define MARK_OPTYPE_RENAME 0x4

#define MIN(a, b) (((a) < (b)) ? (a) : (b))

typedef struct _MARK_EVENT
{
    unsigned short szProcessName[32];
    unsigned short szUserName[32];
    unsigned short szImagePath[176];

    unsigned short szOperationPath[256];

    long time;
    long flags;

    long pid;
    long ppid;
    long tid;

    long opclass;
    long optype;
    long reserved3;
} MARK_EVENT, *PMARK_EVENT;

typedef struct _MARK_MESSAGE
{
    short code;
    short info;
    short reserved1;
    short reserved2;
    unsigned short szData[124];
} MARK_MESSAGE, *PMARK_MESSAGE;

typedef struct _MARK_PROCESS
{
    unsigned short szProcessName[32];
    unsigned short szUserName[32];
    unsigned short szImagePath[176];

    long pid;
    long ppid;
} MARK_PROCESS, *PMARK_PROCESS;

int HandleControlNotification(PMARK_MESSAGE msg);
int HandleInfoNotification(PMARK_MESSAGE msg);

int HandleProcessEvent(PMARK_EVENT evt);
int HandlePacketEvent(PMARK_EVENT evt);
int HandleRegistryEvent(PMARK_EVENT evt);
int HandleFileEvent(PMARK_EVENT evt);

int SendEvent(PMARK_EVENT evt);

PMARK_PROCESS FindLoadProcess(int pid);

void MarkCopyMemory(void* dst, void* src, int bytecount);
void* MarkMalloc(int bytecount);
void MarkFree(void* memory);

int LoadProcess(int pid);

#endif