#define MARK_OPCLASS_PROCESS 0x1
#define MARK_OPCLASS_PACKET 0x2
#define MARK_OPCLASS_FILE 0x3
#define MARK_OPCLASS_REGISTRY 0x4

#define MARK_OPTYPE_CREATE 0x1
#define MARK_OPTYPE_DESTROY 0x2
#define MARK_OPTYPE_WRITE 0x3
#define MARK_OPTYPE_READ 0x4

#define MIN(a, b) (((a) < (b)) ? (a) : (b))

typedef struct _MARK_EVENT
{
    short szProcessName[32];
    short szUserName[32];
    short szImagePath[176];

    short szOperationPath[256];

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
    short szData[124];
} MARK_MESSAGE, *PMARK_MESSAGE;

typedef struct _MARK_PROCESS
{
    short szProcessName[32];
    short szUserName[32];
    short szImagePath[176];

    long pid;
    long ppid;
} MARK_PROCESS, *PMARK_PROCESS;

typedef struct _H_MARK_PROCESS
{
    MARK_PROCESS proc;
} H_MARK_PROCESS, *PH_MARK_PROCESS;

int HandleControlNotification(PMARK_MESSAGE msg);
int HandleInfoNotification(PMARK_MESSAGE msg);

int HandleProcessEvent(PMARK_EVENT evt);
int HandlePacketEvent(PMARK_EVENT evt);
int HandleRegistryEvent(PMARK_EVENT evt);
int HandleFileEvent(PMARK_EVENT evt);

int SendEvent(PMARK_EVENT evt);

PMARK_PROCESS FindLoadProcess(int pid); 
PMARK_PROCESS LoadProcessInfo(int pid);