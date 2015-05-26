#include "precomp.h"

#define ETH_TYPE_IPV4 0x0008
#define ETH_TYPE_IPV6 0xDD86

#define IP_PROTO_ICMP 0x01
#define IP_PROTO_IGMP 0x02
#define IP_PROTO_TCP  0x06
#define IP_PROTO_UDP  0x11

typedef struct _ETH_HDR
{
    BYTE    baSrcMAC[6];
    BYTE    baDstMAC[6];
    UINT16  uEthType;
} ETH_HEADER, *PETH_HEADER;

typedef struct _ETH_PACKET
{
    ETH_HEADER header;
    PVOID      pData;
} ETH_PACKET, *PETH_PACKET;

typedef struct _IP_HDR
{
    BYTE	bHeaderLen : 4;
    BYTE	bVersion : 4;
    BYTE	bECN : 3;
    BYTE	bDSCP : 5;
    UINT16  uTotalLen;
    UINT16  uID;
    UINT16  uFragOffset : 13;
    UINT16  uFlags : 3;
    BYTE    bTTL;
    BYTE    bProto;
    UINT16  uCheckSum;
    DWORD   dwSrcIP;
    DWORD   dwDstIP;
} IP_HEADER, *PIP_HEADER;

typedef struct _IP_PACKET
{
    IP_HEADER header;
    PVOID     pData;
} IP_PACKET, *PIP_PACKET;

typedef struct _ICMP_HDR
{
    BYTE    bType;
    BYTE    bCode;
    UINT16  uCheckSum;
    DWORD   dwHeaderData;
} ICMP_HEADER, *PICMP_HEADER;

typedef struct _ICMP_PACKET
{
    ICMP_HEADER header;
    PVOID       pData;
} ICMP_PACKET, *PICMP_PACKET;

typedef struct _TCP_HDR
{
    UINT16 uSrcPort;
    UINT16 uDstPort;

    DWORD  dwSeqNum;
    DWORD  dwAckNum;

    UINT16 uFlags : 6;
    UINT16 uReserved : 6;
    UINT16 uLen : 4;

    UINT16 uWindow;
    UINT16 uCheckSum;
    UINT16 uQosLabel;
} TCP_HEADER, *PTCP_HEADER;

typedef struct _TCP_PACKET
{
    TCP_HEADER header;
    PVOID      pData;
} TCP_PACKET, *PTCP_PACKET;

typedef struct _UDP_HDR
{
    UINT16 uSrcPort;
    UINT16 uDstPort;
    UINT16 uLen;
    UINT16 uCheckSum;
} UDP_HEADER, *PUDP_HEADER;

typedef struct _UDP_PACKET
{
    UDP_HEADER header;
    PVOID      pData;
} UDP_PACKET, *PUDP_PACKET;
