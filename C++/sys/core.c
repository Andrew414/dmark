#include "core.h"

int HandleControlNotification(PMARK_MESSAGE msg)
{
    if (msg->code)
    {

    }
    return 0;
}

int HandleInfoNotification(PMARK_MESSAGE msg)
{
    msg;
    return 0;
}

int HandleProcessEvent(PMARK_EVENT evt)
{
    return SendEvent(evt);
}

int HandlePacketEvent(PMARK_EVENT evt)
{
    return SendEvent(evt);
}

int HandleRegistryEvent(PMARK_EVENT evt)
{
    return SendEvent(evt);
}

int HandleFileEvent(PMARK_EVENT evt)
{
    return SendEvent(evt);
}