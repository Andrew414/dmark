#include "communicator.h"

#include "..\sys\markusermode.h"

#include <Windows.h>
#include <Fltuser.h>
#include <stdio.h>

typedef struct _DRIVER_MESSAGE
{
    FILTER_MESSAGE_HEADER header;
    MARK_EVENT event;
} DRIVER_MESSAGE, *PDRIVER_MESSAGE;

int IsConnectionSuccessful(DRIVER_CONNECTION connection)
{
    HANDLE x = (HANDLE)connection;
    return (x != NULL && x != INVALID_HANDLE_VALUE);
}

DWORD WINAPI ProcessDriverMessages(_In_ LPVOID parameter)
{
    HANDLE port = (HANDLE)parameter;

    DRIVER_MESSAGE message = { 0 };

    while (1)
    {
        HRESULT res = FilterGetMessage((HANDLE)port, &(message.header), sizeof(message), NULL);

        if (S_OK == res)
        {
            ProcessMessage(&(message.event));
        }
        else
        {
            printf("Error 0x%x while receiving the message.\n");
        }
    }
}

DRIVER_CONNECTION ConnectToDriver()
{
    HANDLE hPort;
    FilterConnectCommunicationPort(PORT_NAME, FLT_PORT_FLAG_SYNC_HANDLE, NULL, 0, NULL, &hPort);

    HANDLE thread = CreateThread(NULL, 0, ProcessDriverMessages, hPort, 0, NULL);
    WaitForSingleObject(thread, INFINITE);
    return hPort;
}