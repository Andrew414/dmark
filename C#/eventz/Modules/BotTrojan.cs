﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class BotTrojan : IAnalyzer
    {
        private int[] BasicIps = { 0x00007401, 0x00003B02, 0x00F22205, 0x00004805, 0x0000B405, 0x0000040E, 0x0000810E, 0x0000A80E, 0x0030C00E, 0x0038C00E, 0x002B0B1F, 0x00C8DE1F, 0x00318B25, 0x00D89425, 0x0080012A, 0x00F81D2E, 0x0070942E, 0x00000831, 0x00E00B3D, 0x00FB2D3D, 0x00487A3E, 0x0098B63E, 0x00000F40, 0x00002C40, 0x00007040, 0x00807040, 0x00E0EA40, 0x00700B42, 0x00F0C642, 0x0040E742, 0x0070D143, 0x00D0D343, 0x0080D543, 0x00D0DA43, 0x00C04244, 0x00002046, 0x00100D48, 0x00607B4A, 0x00B81F4E, 0x00D31F4E, 0x00106E4F, 0x00306E4F, 0x0068AD4F, 0x00981651, 0x00277955, 0x00A0CA55, 0x00283756, 0x002A3756, 0x008C3756, 0x00D23756, 0x0070F357, 0x00108758, 0x00097259, 0x00617259, 0x00B56C5B, 0x00FEC35B, 0x0060C55B, 0x0028C65B, 0x007FC65B, 0x00A4C85B, 0x00F8C85B, 0x007CC95B, 0x00ECC95B, 0x0014CB5B, 0x0074CF5B, 0x0010D05B, 0x000CD15B, 0x002DD45B, 0x0068D45B, 0x0087D45B, 0x00C6D45B, 0x00C9D45B, 0x00DCD45B, 0x001DD55B, 0x0048D55B, 0x005DD55B, 0x005ED55B, 0x0079D55B, 0x007ED55B, 0x0094D55B, 0x00A7D55B, 0x00ACD55B, 0x00AED55B, 0x00AFD55B, 0x00D9D55B, 0x0003D85B, 0x0049D85B, 0x00A2D95B, 0x00F9D95B, 0x0023DC5B, 0x003EDC5B, 0x003FDC5B, 0x005ADC5B, 0x007EDC5B, 0x004DDF5B, 0x00E7DF5B, 0x0061E25B, 0x0084E45B, 0x00F8E55B, 0x006EE65B, 0x008FE65B, 0x0093E65B, 0x00FCE65B, 0x0028E75B, 0x0024EA5B, 0x00FFEA5B, 0x0002EB5B, 0x004DEB5B, 0x0078EC5B, 0x00C6ED5B, 0x00F9ED5B, 0x0052EE5B, 0x000FEF5B, 0x0018EF5B, 0x00EEEF5B, 0x00A5F05B, 0x00D9F25B, 0x0073F35B, 0x00F0AF5D, 0x00701A5E, 0x00793C5E, 0x007A3C5E, 0x00F73D5E, 0x00923F5E, 0x00933F5E, 0x00953F5E, 0x00963F5E, 0x00F03F5E, 0x00F33F5E, 0x00F43F5E, 0x00F53F5E, 0x00F63F5E, 0x00F73F5E, 0x00809A5E, 0x00F09E5E, 0x008CD75F, 0x0000D85F, 0x0000C065, 0x0000EC65, 0x0000F865, 0x0000FC65, 0x002C0267, 0x00440A67, 0x00D80C67, 0x004C1067, 0x0048F667, 0x0000606A, 0x00D05E6D, 0x0060C46D, 0x00802C6E, 0x00A0E86E, 0x00A01471, 0x00000872, 0x00855573, 0x00008074, 0x00009074, 0x00009274, 0x0098C574, 0x00402E79, 0x0000817A, 0x0060CA7A, 0x0000447C, 0x0000467C, 0x00009D7C, 0x00003A7D, 0x00000D80, 0x0000A880, 0x0000BF80, 0x00002F81, 0x00404C81, 0x0000C982, 0x0000DE82, 0x00009184, 0x0000E884, 0x00001286, 0x00001686, 0x00001786, 0x00002186, 0x00007F86, 0x0000AC86, 0x0000D186, 0x0000EF86, 0x0000C988, 0x0000E488, 0x0000E688, 0x00004C89, 0x00002B8A, 0x00002F8B, 0x0000A78B, 0x0000BC8B, 0x0000A78C, 0x0000AA8C, 0x0010888D, 0x0016888D, 0x001B888D, 0x0000318F, 0x0000408F, 0x0000878F, 0x0000BD8F, 0x0000CF90, 0x00000392, 0x00003293, 0x00006994, 0x00009A94, 0x0000B294, 0x0000F894, 0x00006D95, 0x00007695, 0x00408F95, 0x00000A96, 0x00007E96, 0x00008D96, 0x00007B97, 0x0000C097, 0x00B8ED97, 0x00008898, 0x00009398, 0x00000E99, 0x00000A9A, 0x0000B19B, 0x0000BE9B, 0x0000CC9B, 0x0000A29D, 0x0000BA9D, 0x0000C39D, 0x0000E29D, 0x0000E79D, 0x0000E89D, 0x0000369E, 0x0000559F, 0x00006F9F, 0x0000879F, 0x00008D9F, 0x0000DF9F, 0x0000E59F, 0x0000DEA0, 0x0000BDA1, 0x0000E8A1, 0x00007DA2, 0x00ECD3A2, 0x0004D9A2, 0x00132FA3, 0x0000B6A3, 0x0000FDA3, 0x00003CA4, 0x0000C0A5, 0x0000CDA5, 0x0000D1A5, 0x0000E1A5, 0x00C0E1A5, 0x00000DA7, 0x00001CA7, 0x00004AA7, 0x000057A7, 0x000061A7, 0x0000A2A7, 0x0000AFA7, 0x0000E0A7, 0x000081A8, 0x000043AA, 0x000071AA, 0x000072AA, 0x000078AA, 0x0000B3AA, 0x0000CDAD, 0x0008CDAD, 0x0010CDAD, 0x0018CDAD, 0x0020CDAD, 0x0028CDAD, 0x0030CDAD, 0x00A0F9AD, 0x00C088AE, 0x004067AF, 0x00002FB0, 0x00883DB0, 0x00656EB0, 0x004015B1, 0x001024B1, 0x00B09FB2, 0x0000ECB4, 0x008C0BB9, 0x008F0BB9, 0x006C18B9, 0x00E0BEBA, 0x0087F7BC, 0x00E6F7BC, 0x006705C0, 0x00191AC0, 0x00D41FC0, 0x001D28C0, 0x00992BC0, 0x009A2BC0, 0x009C2BC0, 0x00A02BC0, 0x00AF2BC0, 0x00B02BC0, 0x00B82BC0, 0x002736C0, 0x004936C0, 0x006E36C0, 0x001043C0, 0x00A043C0, 0x00F354C0, 0x005556C0, 0x004A58C0, 0x008E64C0, 0x002C65C0, 0x00B565C0, 0x00C865C0, 0x00F065C0, 0x00F865C0, 0x007070C0, 0x000385C0, 0x00C298C0, 0x00339EC0, 0x002CA0C0, 0x0013A2C0, 0x0040ABC0, 0x0031BEC0, 0x0057C5C0, 0x0078DBC0, 0x0080DBC0, 0x00C0DBC0, 0x00D0DBC0, 0x0020E5C0, 0x0042E7C0, 0x00BDEAC0, 0x0065F5C0, 0x008100C1, 0x009200C1, 0x00C007C1, 0x00D510C1, 0x009016C1, 0x007E17C1, 0x003019C1, 0x00401AC1, 0x00862BC1, 0x00D32EC1, 0x000C68C1, 0x002968C1, 0x005E68C1, 0x006E68C1, 0x00B068C1, 0x008D69C1, 0x009A69C1, 0x00B869C1, 0x00CF69C1, 0x00D269C1, 0x00F569C1, 0x00206AC1, 0x00106BC1, 0x00B26CC1, 0x000BA4C1, 0x00A7C8C1, 0x00F0E3C1, 0x00A6F3C1, 0x00B100C2, 0x00F500C2, 0x009801C2, 0x009F01C2, 0x00B801C2, 0x00DC01C2, 0x00F701C2, 0x00B91DC2, 0x007432C2, 0x009C36C2, 0x00A06EC2, 0x00FB7EC2, 0x00ED8CC2, 0x00409CC2, 0x0002F2C2, 0x003AF7C2, 0x009003C3, 0x00A105C3, 0x008D14C3, 0x00DE44C3, 0x006C4EC3, 0x00CC55C3, 0x00BE58C3, 0x000872C3, 0x005A95C3, 0x0039B6C3, 0x0038BFC3, 0x0066BFC3, 0x00B0E1C3, 0x00C5E2C3, 0x006D01C4, 0x00003FC4, 0x0000C1C4, 0x00000DC6, 0x00800EC6, 0x00A00EC6, 0x001014C6, 0x002017C6, 0x00202DC6, 0x00402DC6, 0x001030C6, 0x004038C6, 0x004039C6, 0x00463EC6, 0x004C3EC6, 0x00E060C6, 0x007563C6, 0x00DE66C6, 0x00D494C6, 0x004097C6, 0x009897C6, 0x00CDA0C6, 0x00D0A2C6, 0x00FFA7C6, 0x00C9A9C6, 0x0030B0C6, 0x00AFB1C6, 0x00B0B1C6, 0x00B4B1C6, 0x00D6B1C6, 0x0040B2C6, 0x0016B3C6, 0x0020B5C6, 0x0040B5C6, 0x0020B7C6, 0x0040B8C6, 0x00C1B8C6, 0x0019BAC6, 0x00D0BAC6, 0x0040BBC6, 0x00ADBEC6, 0x00D4C7C6, 0x00EDCAC6, 0x0000CCC6, 0x0040CDC6, 0x009805C7, 0x00E505C7, 0x001809C7, 0x00601AC7, 0x00891AC7, 0x00CF1AC7, 0x00FB1AC7, 0x009121C7, 0x00DE21C7, 0x008022C7, 0x00202EC7, 0x00F83AC7, 0x00663CC7, 0x003847C7, 0x00C047C7, 0x003754C7, 0x003854C7, 0x003C54C7, 0x004054C7, 0x00D057C7, 0x002058C7, 0x003058C7, 0x001059C7, 0x00C659C7, 0x00A378C7, 0x0020A5C7, 0x00C8A6C7, 0x0052B8C7, 0x00C0B9C7, 0x00C0C4C7, 0x00A0C6C7, 0x00B0C6C7, 0x00B8C6C7, 0x00BCC6C7, 0x0040C8C7, 0x0060D4C7, 0x0000DFC7, 0x0040E6C7, 0x0060E6C7, 0x0055E9C7, 0x0060E9C7, 0x008AF5C7, 0x0089F6C7, 0x00D5F6C7, 0x00D7F6C7, 0x0040F8C7, 0x0040F9C7, 0x00E0FDC7, 0x0020FEC7, 0x008003C8, 0x000016C8, 0x002069C8, 0x00C000CA, 0x002014CA, 0x004015CA, 0x004028CA, 0x006C3DCA, 0x000044CA, 0x0000B7CA, 0x000002CB, 0x000009CB, 0x00581FCB, 0x004622CB, 0x004722CB, 0x002613CC, 0x00202CCC, 0x00C02CCC, 0x00E02CCC, 0x001030CC, 0x00FF34CC, 0x001039CC, 0x00E44BCC, 0x00C650CC, 0x001056CC, 0x00C757CC, 0x00E059CC, 0x00806ACC, 0x00C06ACC, 0x00D06BCC, 0x00F47ECC, 0x009780CC, 0x00B480CC, 0x00A782CC, 0x00F093CC, 0x00E098CC, 0x00809BCC, 0x009BBBCC, 0x009CBBCC, 0x00A0BBCC, 0x00C0BBCC, 0x00E0BBCC, 0x00F0BBCC, 0x00F8BBCC, 0x00FCBBCC, 0x00FEBBCC, 0x00B8C2CC, 0x009FE1CC, 0x00D2E1CC, 0x0000ECCC, 0x0088EDCC, 0x00A8EDCC, 0x00E8EDCC, 0x00F0EDCC, 0x00AAEECC, 0x00B7EECC, 0x000089CD, 0x00688ECD, 0x000090CD, 0x00B090CD, 0x008097CD, 0x002D9FCD, 0x00AE9FCD, 0x00B49FCD, 0x004DA6CD, 0x0054A6CD, 0x0082A6CD, 0x00D3A6CD, 0x00B0ACCD, 0x00F4ACCD, 0x00A0AFCD, 0x0047BDCD, 0x0048BDCD, 0x0000CBCD, 0x00E0CBCD, 0x0086CFCD, 0x006BD2CD, 0x008BD2CD, 0x0080D6CD, 0x00E0E9CD, 0x00B9ECCD, 0x00BDECCD, 0x0000FDCD, 0x001D33CE, 0x000051CE, 0x00807BCE, 0x00C07FCE, 0x00BC82CE, 0x0000BDCE, 0x00E0C3CE, 0x001CC5CE, 0x001DC5CE, 0x004DC5CE, 0x0030C9CE, 0x0040CBCE, 0x0050D1CE, 0x00A0E0CE, 0x0000E2CE, 0x0020E2CE, 0x0040E3CE, 0x00C016CF, 0x008020CF, 0x00E02DCF, 0x00406ECF, 0x00606ECF, 0x00806ECF, 0x00C0B7CF, 0x0000BDCF, 0x00C0E2CF, 0x0060E6CF, 0x0000EACF, 0x0080FECF, 0x00A846D0, 0x00D04CD0, 0x008851D0, 0x00005AD0, 0x00605DD0, 0x005075D0, 0x002033D1, 0x008042D1, 0x00C05FD1, 0x008061D1, 0x000091D1, 0x0040B6D1, 0x00B0C6D1, 0x00606DD5, 0x00D06DD5, 0x00901ED8, 0x0070A2D8, 0x00C0D4D8, 0x00009DDC, 0x0080E7DE, 0x0000A8DF, 0x0000A9DF, 0x0000AADF, 0x0000ABDF, 0x0000ACDF, 0x0000ADDF, 0x0000C9DF, 0x0000FEDF };
        private int[] BasicMasks = { 0x0000FCFF, 0x0000FFFF, 0x00FEFFFF, 0x0000FCFF, 0x0000FCFF, 0x0000FCFF, 0x0000FFFF, 0x0000F8FF, 0x00F8FFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00F8FFFF, 0x0080FFFF, 0x00FCFFFF, 0x00F0FFFF, 0x0000FCFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00F8FFFF, 0x00F0FFFF, 0x0000FFFF, 0x0080FFFF, 0x00C0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FEFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00F0FFFF, 0x00FCFFFF, 0x0000FEFF, 0x0000FCFF, 0x0000FCFF, 0x0000FEFF, 0x0000FEFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x0000FCFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x0000FFFF, 0x00FFFFFF, 0x0000C0FF, 0x0000FEFF, 0x0000FEFF, 0x00F8FFFF, 0x00C0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x0000FEFF, 0x0000FEFF, 0x00C0FFFF, 0x00C0FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00C0FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00C0FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00FCFFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0080FFFF, 0x00C0FFFF, 0x0000FFFF, 0x0000FFFF, 0x00C0FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00E0FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00C0FFFF, 0x0000FFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x0000FCFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00FEFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00C0FFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x0000FFFF, 0x0000FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00C0FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00E0FFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00F8FFFF, 0x00F0FFFF, 0x00FCFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00FCFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00FEFFFF, 0x00FCFFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x0000FFFF, 0x00F0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00FEFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00F8FFFF, 0x00FCFFFF, 0x00FEFFFF, 0x00FFFFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00FCFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FCFFFF, 0x00FCFFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FEFFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x0000FFFF, 0x00FFFFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x0000FFFF, 0x00E0FFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00FFFFFF, 0x00F0FFFF, 0x00C0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x0080FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F8FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x00E0FFFF, 0x00E0FFFF, 0x00F0FFFF, 0x00FCFFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00F0FFFF, 0x00E0FFFF, 0x00C0FFFF, 0x0080FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF, 0x0000FFFF };

        public BotTrojan(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        private bool found = false;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Bot and trojan finder";
            description = "";
            version = "0.1";
            noSubscription = false;
        }

        public void Initialize()
        {

        }

        public void ProcessEvent(Event evt)
        {
            if (evt.OpClass != OperationClass.Packet || found)
            {
                return;
            }

            foreach (var i in BasicIps)
            {
                if (evt.OperationPath.Contains(i.ToString() + ":"))
                {
                    found = true;
                    resultDelegate(new Result("Trojan found", "A running trojan application \"" + evt.ProcessName + "\" is found on the PC", 0, 80, evt.id));
                }
            }
        }
    }
}
