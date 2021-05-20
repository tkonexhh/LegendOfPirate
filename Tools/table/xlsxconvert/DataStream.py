#!/usr/bin/python
# -*- coding=utf-8 -*-


import os
import struct
from Logger import Logger

FIXED_HEADER_LEN = 28
FILE_TAG_LEN = 4
INT0TYPE = 0
INT1TYPE = 1
INT2TYPE = 2
INT3TYPE = 3
INT4TYPE = 4
FLOATTYPE = 5
STRINGTYPE = 6


class DataStreamWriter(object):
    """docstring for DataStream
         int0, 0\
          char, 1\
          int2, 2\
          int3, 3\
          int4, 4\
          float 5\
          string 6\
        """

    def __init__(self, filePath):
        self.__filePath = filePath
        self.__fd = open(filePath, 'wb')
        self.__schemeLen = 0
        self.__FieldTypeRowOff = 0
        self.__FieldNameowOff = 0
        self.__row = 0
        self.__col = 0
        self.__curLen = 0

        self.__InitHead()

    def __InitHead(self):
        "TAG:LRGB"
        ""
        self.__fd.write(struct.pack("c", 'L'))
        self.__fd.write(struct.pack("c", 'R'))
        self.__fd.write(struct.pack("c", 'G'))
        self.__fd.write(struct.pack("c", 'B'))
        self.__fd.write(struct.pack(">i", 0))  # all size 数据大小
        self.__fd.write(struct.pack(">i", 0))  # scheme len 数据头大小
        self.__fd.write(struct.pack(">i", 0))  # col 列数
        self.__fd.write(struct.pack(">i", 0))  # row 行数
        self.__fd.write(struct.pack(">i", 0))  # 字段类型偏移量
        self.__fd.write(struct.pack(">i", 0))  # 字段名字偏移量
        self.__curLen = self.__fd.tell()

    # 重写字段头
    def __WriteHead(self):
        self.__fd.seek(FILE_TAG_LEN)
        self.__fd.write(struct.pack(">i", self.__curLen))  # all size 数据大小
        # scheme len 数据头大小
        self.__fd.write(struct.pack(">i", self.__schemeLen))
        self.__fd.write(struct.pack(">i", self.__col))  # col 列数
        self.__fd.write(struct.pack(">i", self.__row))  # row 数据行数
        self.__fd.write(struct.pack(">i", self.__FieldTypeRowOff))  # 字段类型偏移量
        self.__fd.write(struct.pack(">i", self.__FieldNameowOff))  # 字段名字偏移量

    def RecordFieldTypeRowOff(self):
        self.__FieldTypeRowOff = self.__curLen

    def RecordFieldNameRowOff(self):
        self.__FieldNameowOff = self.__curLen

    def RecordCol(self, col):
        self.__col = col

    def EndWriteSheme(self):
        # print self.__curLen
        self.__schemeLen = self.__curLen - FIXED_HEADER_LEN
        pass

    def EndRow(self):
        self.__row += 1

        pass

    def WriteField(self, val):
        if (type(val) == int):
            self.__curLen += self.WriteInt(val)
        elif (type(val) == float):
            self.__curLen += self.WriteFloat(val)
        elif (type(val) == bool):
            self.__curLen += self.WriteBool(val)
        elif (type(val) == unicode):
            self.__curLen += self.WriteString(val)
        elif (type(val) == str):
            self.__curLen += self.WriteString(val)
        # print val

    def WriteVector2(self, strV):
        str_list = strV.split(',')
        x = 0
        y = 0
        if (len(strV) > 0):
            if (len(str_list) >= 1):
                x = float(str_list[0])
            if (len(str_list) >= 2):
                y = float(str_list[1])
        self.__curLen += self.__WriteVector2(x, y)

    def WriteVector3(self, strV):
        str_list = strV.split(',')
        x = 0
        y = 0
        z = 0
        if (len(strV) > 0):
            if (len(str_list) >= 1):
                x = float(str_list[0])
            if (len(str_list) >= 2):
                y = float(str_list[1])
            if (len(str_list) >= 3):
                z = float(str_list[2])
        self.__curLen += self.__WriteVector3(x, y, z)

    def __WriteInt1(self, val):
        self.__fd.write(struct.pack("B", val & 0xff))
        return 1

    def __WriteInt2(self, val):
        self.__fd.write(struct.pack(">h", val))
        return 2

    def __WriteInt3(self, val):
        self.__fd.write(struct.pack("B", (val & 0xff0000) >> 16))
        self.__fd.write(struct.pack("B", (val & 0xff00) >> 8))
        self.__fd.write(struct.pack("B", val & 0xff))
        return 3

    def __WriteInt4(self, val):
        self.__fd.write(struct.pack(">i", val))
        return 4

    def WriteInt(self, val):
        if val == 0:  # 0 byte
            self.__fd.write(struct.pack("B", INT0TYPE))
            return 1
        elif (val >= -128 and val <= 127):  # 1 byte
            self.__fd.write(struct.pack("B", INT1TYPE))
            return self.__WriteInt1(val) + 1
        elif (val >= -32768 and val <= 32767):  # 2 byte
            self.__fd.write(struct.pack("B", INT2TYPE))
            return self.__WriteInt2(val) + 1
        elif (val >= -8388608 and val <= 8388607):  # 3 byte
            self.__fd.write(struct.pack("B", INT3TYPE))
            return self.__WriteInt3(val) + 1
        elif (val >= -2147483648 and val <= 2147483647):           # 4byte
            self.__fd.write(struct.pack("B", INT4TYPE))
            return self.__WriteInt4(val) + 1
        else:
            errMsg = ("Must be -2147483648 ~ 2147483647")
            Logger.e(errMsg)
            raise Exception(errMsg)

    def WriteFloat(self, val):
        self.__fd.write(struct.pack("B", FLOATTYPE))
        self.__fd.write(struct.pack("f", val))
        return 5

    def __WriteVector2(self, x, y):
        size = 0
        size += self.WriteFloat(x)
        size += self.WriteFloat(y)
        return size

    def __WriteVector3(self, x, y, z):
        size = 0
        size += self.WriteFloat(x)
        size += self.WriteFloat(y)
        size += self.WriteFloat(z)
        return size

    def WriteString(self, val):
        writeLen = 0
        sLen = len(val)
        if sLen == 0:  # 0 byte
            self.__fd.write(struct.pack("B", STRINGTYPE))
            writeLen += 1
        elif (sLen >= -128 and sLen <= 127):  # 1 byte
            self.__fd.write(struct.pack("B", STRINGTYPE | (INT1TYPE << 4)))
            writeLen += (self.__WriteInt1(sLen) + 1)
        elif (sLen >= -32768 and sLen <= 32767):  # 2 byte
            self.__fd.write(struct.pack("B", STRINGTYPE | (INT2TYPE << 4)))
            writeLen += (self.__WriteInt2(sLen) + 1)
        elif (sLen >= -8388608 and sLen <= 8388607):  # 3 byte
            self.__fd.write(struct.pack("B", STRINGTYPE | (INT3TYPE << 4)))
            writeLen += (self.__WriteInt3(sLen) + 1)
        elif (sLen >= -2147483648 and sLen <= 2147483647):           # 4byte
            self.__fd.write(struct.pack("B", STRINGTYPE | (INT4TYPE << 4)))
            writeLen += (self.__WriteInt4(sLen) + 1)
        else:
            errMsg = ("String Len too Long")
            Logger.e(errMsg)
            raise Exception(errMsg)
        if (sLen > 0):
            self.__fd.write(val)
        writeLen += sLen
        return writeLen

    def WriteBool(self, val):

        if (val):
            self.__fd.write(struct.pack("B", INT1TYPE))
            self.__WriteInt1(1)
            return 2
        else:
            self.__fd.write(struct.pack("B", INT0TYPE))
            return 1

    def Close(self):
        self.__WriteHead()
        self.__fd.close()
        pass


class DataStreamReader(object):
    """docstring for DataStream
         int0, 0\
          char, 1\
          int2, 2\
          int3, 3\
          int4, 4\
          float 5\
          string 6\
        """

    def __init__(self, filePath, buffer=None):
        self.__filePath = filePath
        self.__fileSize = os.path.getsize(self.__filePath)
        self.__fd = open(filePath, 'rb')
        self.__ReadHead()

    def __del__(self):
        if (self.__fd != None):
            self.__fd.close()

    def __ReadHead(self):
        "TAG:LRGB"
        ""
        self.__fd.seek(FILE_TAG_LEN)
        self.__allLen = self.__ReadInt4()
        self.__schemeLen = self.__ReadInt4()
        self.__col = self.__ReadInt4()
        self.__row = self.__ReadInt4()
        self.__FieldTypeRowOff = self.__ReadInt4()
        self.__FieldNameRowOff = self.__ReadInt4()

    # 获取行数
    def GetRowCount(self):
        return self.__row

    # 获取列数
    def GetColCount(self):
        return self.__col

    def SkipScheme(self):
        self.__fd.seek(FIXED_HEADER_LEN + self.__schemeLen)

    def HasNext(self):
        if (self.__fd.tell() >= self.__fileSize):
            return False
        else:
            return True
        pass

    def ReadNext(self):
        if (self.HasNext() == False):
            print "EEEEEEE"
        typeVal = self.__ReadInt1()
        aTag = typeVal & 0xf
        # print "aTag:", aTag
        if aTag == INT0TYPE:
            return self.__ReadInt0()
        elif aTag == INT1TYPE:
            return self.__ReadInt1()
        elif aTag == INT2TYPE:
            return self.__ReadInt2()
        elif aTag == INT3TYPE:
            return self.__ReadInt3()
        elif aTag == INT4TYPE:
            return self.__ReadInt4()
        elif aTag == FLOATTYPE:
            return self.__ReadFloat()
        elif aTag == STRINGTYPE:
            bTag = (typeVal & 0xf0) >> 4
            return self.__ReadString(bTag)
        pass

    def Console(self):
        while (self.HasNext()):
            print self.ReadNext()

    def ConsoleF(self):
        print self.__allLen
        print "Row ", self.GetRowCount(), " Col ", self.GetColCount()
        fd = open("./a.txt", 'w+')
        for i in range(0, self.GetRowCount()):
            for j in range(0, self.GetColCount()):
                fd.write(str(self.ReadNext()))
                fd.write("\t")
            fd.write("\n")
        fd.close()

    def __ReadInt0(self):
        return 0

    def __ReadInt1(self):
        val = self.__fd.read(1)
        iVal,  = struct.unpack("B", val)
        return iVal

    def __ReadInt2(self):
        val = self.__fd.read(2)
        iVal, = struct.unpack('>h', val)
        return iVal

    def __ReadInt3(self):
        val = self.__fd.read(3)
        # 判断符号位
        a,  = struct.unpack('B', val[0])
        b,  = struct.unpack('B', val[1])
        c,  = struct.unpack('B', val[2])
        iVal = 0
        if (a & 0x80 == 0):
            iVal = 0
        else:
            iVal = -16777216
        iVal |= a << 16
        iVal |= b << 8
        iVal |= c
        return iVal

    def __ReadInt4(self):
        val = self.__fd.read(4)
        iVal, = struct.unpack(">i", val)
        return iVal

    def __ReadFloat(self):
        bVal = self.__fd.read(4)
        fVal, = struct.unpack(">f", bVal)
        return fVal

    def __ReadString(self, bTag):
        sLen = 0
        if bTag == INT0TYPE:
            sLen = self.__ReadInt0()
        elif bTag == INT1TYPE:
            sLen = self.__ReadInt1()
        elif bTag == INT2TYPE:
            sLen = self.__ReadInt2()
        elif bTag == INT3TYPE:
            sLen = self.__ReadInt3()
        elif bTag == INT4TYPE:
            sLen = self.__ReadInt4()

        if (sLen > 0):
            return self.__fd.read(sLen)
        else:
            return ""

    def __ReadBool(self, val):
        bVal, = struct.unpack("?", bVal)
        return bVal
        pass

    def Close(self):
        self.__fd.close()
        self.__fd = None
        pass

if __name__ == '__main__':
    dataW = DataStreamWriter("F://b.bin")
    dataW.WriteField("序号")
    dataW.WriteField("键值")
    dataW.WriteField("键值")
    dataW.EndRow()
    dataW.WriteField("A")
    dataW.WriteField("A")
    dataW.WriteField("A")
    dataW.EndRow()
    dataW.RecordFieldTypeRowOff()
    dataW.WriteField("int")
    dataW.WriteField("string")
    dataW.WriteField("string")
    dataW.EndRow()
    dataW.RecordFieldNameRowOff()
    dataW.WriteField("Id")
    dataW.WriteField("Key")
    dataW.WriteField("Value")
    dataW.EndRow()
    dataW.EndWriteSheme()
    dataW.WriteField(1)
    dataW.WriteField("HpLv")
    dataW.WriteField("500")
    dataW.EndRow()
    dataW.WriteField(2)
    dataW.WriteField("AttLv")
    dataW.WriteField("100")
    dataW.EndRow()
    dataW.WriteField(129)
    dataW.WriteField("DefLv")
    dataW.WriteField("100")
    dataW.EndRow()
    dataW.Close()

    dataR = DataStreamReader("E://a.bin", None)
    dataR.ConsoleF()
    dataR.Close()
