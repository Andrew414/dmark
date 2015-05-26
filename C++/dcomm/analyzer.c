#include "communicator.h"

#include <Windows.h>

static HANDLE pipe = NULL;

void CreateNeededInfo()
{
    pipe = CreateFile("\\\\.\\pipe\\dcommconnection", GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);
}

int SendMessageToAnalyzer(PMARK_EVENT event)
{
    if (!pipe || INVALID_HANDLE_VALUE == pipe)
    {
        CreateNeededInfo();
        if (!pipe || INVALID_HANDLE_VALUE == pipe)
            return 0;
    }

    DWORD written = 0;

    WriteFile(pipe, event, sizeof(MARK_EVENT), &written, NULL);
}