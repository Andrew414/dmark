#include "..\sys\markusermode.h"

#define WIN32

#include <stdio.h>
#include <pcap.h>
#include "tcpip.h"
#include "communicator.h"



typedef const struct pcap_pkthdr PCAP_PKT_HEADER;
typedef PCAP_PKT_HEADER * PPCAP_PKT_HEADER;
typedef struct sockaddr_in SOCKADDR_IN;
typedef SOCKADDR_IN * PSOCKADDR_IN;

#define PUCHAR unsigned char *

BOOL CheckProtocol(UINT8 uProtocol)
{
    if (uProtocol == IP_PROTO_ICMP)
        return TRUE;
    if (uProtocol == IP_PROTO_TCP)
        return TRUE;
    if (uProtocol == IP_PROTO_UDP)
        return TRUE;

    return FALSE;
}

/* Callback function invoked by libpcap for every incoming packet */
VOID HandlePacket(PUCHAR pParam, PPCAP_PKT_HEADER pHeader, PUCHAR pData)
{
    PETH_HEADER pEth = (PETH_HEADER)pData;
    if (pEth->uEthType != ETH_TYPE_IPV4)
    {
        return;
    }

    PIP_HEADER pIp = (PIP_HEADER)(pEth + 1);

    if (pIp->bVersion != 4 || pIp->bHeaderLen != 5 || !CheckProtocol(pIp->bProto))
    {
        return;
    }

    MARK_EVENT evt = { 0 };
    evt.flags = 0;
    evt.opclass = MARK_OPCLASS_PACKET;
    evt.optype = MARK_OPTYPE_WRITE;
    evt.pid = 0;
    evt.ppid = 0;
    evt.szImagePath[0] = 0;
    evt.szOperationPath[0] = 0;
    evt.szProcessName[0] = 0;
    evt.szUserName[0] = 0;
    evt.tid = 0;
    evt.time = 0;

    ProcessMessage(&evt);
}

DWORD WINAPI ProcessNetworkMessages(_In_ LPVOID parameter)
{
    pcap_if_t *alldevs;
    pcap_if_t *d;
    int inum;
    int i = 0;
    pcap_t *adhandle;
    char errbuf[PCAP_ERRBUF_SIZE];

    if (pcap_findalldevs(&alldevs, errbuf) == -1)
    {
        return 1;
    }

    for (d = alldevs; d; d = d->next, i++) {}

    if (i == 0)
    {
        return -1;
    }
    inum = i;

    for (d = alldevs, i = 0; i < inum - 1; d = d->next, i++)
    {
        /* Open the device */
        /* Open the adapter */
        if ((adhandle = pcap_open_live(d->name,	// name of the device
            65536,			// portion of the packet to capture. 
            // 65536 grants that the whole packet will be captured on all the MACs.
            1,				// promiscuous mode (nonzero means promiscuous)
            1000,			// read timeout
            errbuf			// error buffer
            )) == NULL)
        {
            return -1;
        }

        struct pcap_addr * addrs = d->addresses;
        while (addrs)
        {
            addrs = addrs->next;
        }

        /* start the capture */
        pcap_loop(adhandle, 0, HandlePacket, NULL);

        pcap_close(adhandle);
        return 0;
    }

    pcap_freealldevs(alldevs);

    return 0;
}