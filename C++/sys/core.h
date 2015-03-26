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

    long reserved1;
    long reserved2;
    long reserved3;
} MARK_EVENT, *PMARK_EVENT;

typedef struct _MARK_MESSAGE
{
    short code;
    short info;
    short reserved1;
    short reserved2;
    short szImagePath[124];
} MARK_MESSAGE, *PMARK_MESSAGE;