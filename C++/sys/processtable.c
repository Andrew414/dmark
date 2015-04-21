#include "processtable.h"
#include "core.h"

typedef enum _H_CELL_STATE
{
    FREE,
    OCCUPIED,
    DELETED
} H_CELL_STATE, *PH_CELL_STATE;

typedef struct _H_MARK_PROCESS
{
    MARK_PROCESS proc;
    H_CELL_STATE state;
    long         pid;
} H_MARK_PROCESS, *PH_MARK_PROCESS;

#define PROC_TABLE_SIZE 65536
static H_MARK_PROCESS s_table[PROC_TABLE_SIZE] = { 0 };
static int s_load = 0;

unsigned short hash(int value)
{
    return value & 0xFFFF;
}

PMARK_PROCESS FindKey(int key)
{
    int hashvalue = hash(key);
    int hitcount = 0;
    while ((s_table[hashvalue].state != FREE) && (hitcount < PROC_TABLE_SIZE))
    {
        if (s_table[hashvalue].pid == key && s_table[hashvalue].state == OCCUPIED)
        {
            return &(s_table[hashvalue].proc);
        }
        hashvalue = (hashvalue + 1) % PROC_TABLE_SIZE;
        hitcount++;
    }

    return 0;
}

int InsertValue(int key, PMARK_PROCESS pProc /*will be copied*/)
{
    if (s_load == PROC_TABLE_SIZE)
    {
        return 0;
    }

    int hashvalue = hash(key);
    
    while ((s_table[hashvalue].state == OCCUPIED))
    {
        hashvalue = (hashvalue + 1) % PROC_TABLE_SIZE;
    }

    s_table[hashvalue].state = OCCUPIED;
    s_table[hashvalue].pid = key;

    s_table[hashvalue].proc.pid = key;
    s_table[hashvalue].proc.ppid = pProc->ppid;

    MarkCopyMemory(s_table[hashvalue].proc.szImagePath, pProc->szImagePath, sizeof(pProc->szImagePath));
    MarkCopyMemory(s_table[hashvalue].proc.szProcessName, pProc->szProcessName, sizeof(pProc->szProcessName));
    MarkCopyMemory(s_table[hashvalue].proc.szUserName, pProc->szUserName, sizeof(pProc->szUserName));

    return 1;
}

int DeleteKey(int key)
{
    int hashvalue = hash(key);
    int hitcount = 0;

    while ((s_table[hashvalue].state != FREE) && (hitcount < PROC_TABLE_SIZE))
    {
        if (s_table[hashvalue].pid == key && s_table[hashvalue].state == OCCUPIED)
        {
            s_table[hashvalue].state = DELETED;
            return 1;
        }
        hashvalue = (hashvalue + 1) % PROC_TABLE_SIZE;
        hitcount++;
    }

    return 0;
}

int DeleteProcess(int pid)
{
    return DeleteKey(pid);
}

int AddProcess(PMARK_PROCESS pProc)
{
    return InsertValue(pProc->pid, pProc);
}

PMARK_PROCESS FindLoadProcess(int pid)
{
    PMARK_PROCESS pProc = FindKey(pid);
    if (pProc)
    {
        return pProc;
    }

    LoadProcess(pid);

    return FindKey(pid);
}