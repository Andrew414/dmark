#include "..\sys\core.h"

int InstallDriver();
int UninstallDriver();

#define DRIVER_CONNECTION void*

int IsConnectionSuccessful(DRIVER_CONNECTION connection);

DRIVER_CONNECTION ConnectToDriver();
void StartPacketCapture();

int CallbackMain();

int ProcessMessage(PMARK_EVENT event);

extern int g_OfflineMode;
extern int g_MonitorConnection;

int SendMessageToAnalyzer(PMARK_EVENT event);
int SaveMessageToLog(PMARK_EVENT event);

int UniqueProcess();