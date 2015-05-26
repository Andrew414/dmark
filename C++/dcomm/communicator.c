#include <string.h>
#include <stdio.h>
#include "communicator.h"

#define INSTALL_KEY "-install"
#define UNINSTALL_KEY "-uninstall"

g_OfflineMode = 1;
g_MonitorConnection = 0;

int main(int argc, char* argv[])
{
    if (!UniqueProcess())
    {
        printf("Not Unique!\n");
        return 1;
    }

    if (argc > 1 && !strcmp(argv[1], INSTALL_KEY))
    {
        InstallDriver();
    }
    else if (argc > 1 && !strcmp(argv[1], UNINSTALL_KEY))
    {
        UninstallDriver();
    }

    // DIRTY HACK
    CallbackMain();

    return 0;
}

DRIVER_CONNECTION connection;

int CallbackMain()
{
    printf("CM BEGIN!\n");
    connection = ConnectToDriver();
    printf("Connection %x!\n", connection);

    return !IsConnectionSuccessful(connection);
}

int ProcessMessage(PMARK_EVENT event)
{
    if (g_OfflineMode)
    {
        SaveMessageToLog(event);
    }

    if (g_MonitorConnection)
    {
        SendMessageToAnalyzer(event);
    }

    return 0;
}