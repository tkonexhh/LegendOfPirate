#!/usr/bin/python
# -*- coding:utf-8 -*-
#
import os
import sys
import codecs
import csv

from xlsx2csv import Xlsx2csv
import Config
from Logger import Logger
from SplitLanguage import SplitLanguage


class Csv2CustomTxt:

    def __init__(self):
        self.filedBelongs = {}
        self.filedTypes = {}
        self._filePath = ""
        self.sheetdelimiter = "---*****************************fengexian*******************************************************-----"

    def _checkBelongInvalid(self, value):
        cellVal = value.strip()
        if (cellVal == "A" or cellVal == "C" or cellVal == "N" or cellVal == "S" or cellVal == "K"):
            return True
        else:
            return False

    def _checkIsBaseType(self, cellType):
        cellType = cellType.strip().lower()
        if (cellType == "int" or cellType == "bool"
                or cellType == "string" or cellType == "float"
                or cellType == "byte"
                or cellType == "vector2" or cellType == "vector3"):
            return True
        return False

    def _parseFiledBelong(self, reader, belongLine):
        r = 1
        for row in reader:
            if (len(row) <= 0):
                continue
            if (r == belongLine):
                c = 1
                for record in row:
                    if (len(record) > 0):
                        value = unicode(record, 'utf-8').strip()
                        if (len(value.strip()) > 1):
                            errMsg = ("ERROR: Data:Invalid col = %s row = %d must be \"A\" or \"C\" or \"S\" or \"N\" in FilePath: %s" % (
                                c, Config.belongLine, self._filePath))
                            Logger.e(errMsg)
                            raise Exception(errMsg)
                        if (self._checkBelongInvalid(value)):
                            self.filedBelongs[c] = value.strip()
                        else:
                            errMsg = ("ERROR: Data:column = %s row = %d Data Invalid in FilePath: %s" % (
                                c, Config.belongLine, self._filePath))
                            Logger.e(errMsg)
                            raise Exception(errMsg)
                    else:
                        self.filedBelongs[c] = 'N'
                    c = c + 1
                return
            r = r + 1

    def _parseMembersType(self, reader, typeLine):
        r = 1
        for row in reader:
            if (len(row) <= 0):
                continue
            # print r, row
            if (r == typeLine):
                c = 1
                for record in row:
                    if (len(record) > 0):
                        value = unicode(record, 'utf-8').strip()
                        typeStr = value.strip()
                        if (self._checkIsNoneCol(c) == False):
                            if (self._checkIsBaseType(record)):
                                self.filedTypes[c] = typeStr
                            else:
                                print typeStr
                                errMsg = ("ERROR: Data:column = %s row = %d Data TypeError must be \"int\" or \"bool\" or \"string\" or \"float\" in FilePath: %s" % (
                                    c, Config.typeLine, self._filePath))
                                Logger.e(errMsg)
                                raise Exception(errMsg)
                    c = c + 1
                return
            r = r + 1
        print self.filedTypes

    def _donothing(self):
        return

    def _checkHasInvalidCharSymbol(self, value):
        return value.find('\t') != -1 or value.find('\r') != -1 or value.find('\n') != -1

    def _checkIsNoneCol(self, col):
        if (self.filedBelongs.has_key(col)):
            if (self.filedBelongs[col] == "N"):
                return True
            else:
                return False
        return True

    def _checkNeedWrite(self, col, belong):
        if (self.filedBelongs.has_key(col)):
            belongVal = self.filedBelongs[col]
            if (belong == "C"):
                return belongVal == "A" or belongVal == "C" or belongVal == "K"
            else:
                return belongVal == "A" or belongVal == "S" or belongVal == "K"
        else:
            print self.filedBelongs
            Logger.w(
                ("Warning: Not Find Head  %d Invalid in FilePath: %s" % (col, self._filePath)))
            return False
        return False

    def _checkFiledNameUnique(self, row, belong):
        c = 1
        keys = {}
        for record in row:
            orginValue = unicode(record, 'utf-8')
            value = orginValue.strip()
            if (len(orginValue) != len(value)):
                errMsg = ("Error: FiledName: %s has space   in %s" %
                          (orginValue, self._filePath))
                Logger.w(unicode(errMsg))
            if (self._checkNeedWrite(c, belong)):
                if (keys.has_key(value)):
                    errMsg = ("Error: Find has same FiledName: %s  in %s" %
                              (value, self._filePath))
                    Logger.e(unicode(errMsg))
                    raise Exception(errMsg)
                else:
                    keys[value] = 1
            c = c + 1

    def _convertToFile(self, reader, outDir, fileName, belong, isBom=False, fileFmt="txt"):
        if not os.access(outDir, os.F_OK):
            os.mkdir(outDir)
        outputFilePath = os.path.abspath(
            "%s/%s.%s" % (outDir, fileName, Config.fileExt))
        fd = codecs.open(outputFilePath, 'w', "utf-8")
        if (isBom):
            fd.write(codecs.BOM_UTF8)
        sheetNum = 0
        ingoreLine = 0
        r = 0
        sheetRowNum = 0
        needCheckIdInvalid = Config.CheckNeedCheckIdUnique(fileName)
        Ids = {}
        for row in reader:
            c = 1
            #print (row)
            if (len(row) > 0 and len(row[0]) > 0) and r != Config.belongLine - 1:
                if (row[0].find(self.sheetdelimiter) != -1):
                    ingoreLine = Config.ignoreLine
                    sheetNum = sheetNum + 1
                    sheetRowNum = 0
                    continue
                if (sheetNum > 1 and ingoreLine > 0):
                    ingoreLine = ingoreLine - 1
                    continue
                elif (sheetNum == 1 and r == Config.filedNameLine - 1):
                    self._checkFiledNameUnique(row, belong)
                Id = unicode(row[0], 'utf-8')

                # id不能有空格
                if (len(Id) != len(Id.strip())):
                    errMsg = ("Error:  Id: %s has space in %s" %
                              (Id, self._filePath))
                    Logger.e(unicode(errMsg))
                    raise Exception(errMsg)

                # id不能相同
                if (needCheckIdInvalid == True):
                    if (Ids.has_key(Id)):
                        errMsg = ("Error: Find has same Id: %s  in %s" %
                                  (Id, self._filePath))
                        Logger.e(unicode(errMsg))
                        raise Exception(errMsg)
                    else:
                        Ids[Id] = Id
                for record in row:
                    value = unicode(record, 'utf-8')
                    if (sheetNum == 0):
                        if r == Config.filedNameLine - 1:
                            value = value.strip()
                        elif r == Config.typeLine - 1:
                            value = value.strip().lower()
                    if (self._checkNeedWrite(c, belong)):
                        if (c != 1):
                            fd.write(Config.delimiter)
                        if (self._checkHasInvalidCharSymbol(value)):
                            rowStr = ""
                            for j in range(0, len(row)):
                                rowStr = rowStr + unicode(row[j], 'utf-8')
                            errMsg = ("Error: Find Invalid Char Data: %s  at sheetNum: %d row %d col %d in %s.xlsx" % (
                                rowStr, sheetNum, sheetRowNum, c, self._filePath))
                            Logger.e(unicode(errMsg))
                            raise Exception(errMsg)
                        if (self.filedTypes[c] == "int" and r > Config.ignoreLine):
                            if (value == "#N/A" or len(value) == 0):
                                value = "0"
                                Logger.w(
                                    "Warring Data:column = %s row = %d Data #N/A" % (c, r))
                            else:
                                value = unicode(
                                    str(int(round(float(value)))), 'utf-8')
                        elif (self.filedTypes[c] == "byte" and len(value) > 0 and r > Config.ignoreLine):
                            if (int(value) < 0 or int(value) > 255):
                                errMsg = (
                                    "byte Data must be [0-255]:column = %s row = %d Data #N/A" % (c, r))
                                Logger.e(unicode(errMsg))
                                raise Exception(errMsg)
                        fd.write(("%s" % (value)))
                    c = c + 1
                fd.write("\r\n")
            r = r + 1
            sheetRowNum = sheetRowNum + 1
        fd.close()
        return outputFilePath

    def ConvertToCsvFile(self, filePath, outClientDir, outServerDir, allSheet=False, tmpDir=None, fileFmt="txt"):
        self._filePath = os.path.abspath(filePath)
        baseName = os.path.basename(filePath)
        fileName, ext = os.path.splitext(baseName)
        tempTxt = os.path.abspath("./temp.%s" % (fileFmt))
        if (tmpDir != None):
            tempTxt = os.path.abspath("%s/temp.%s" % (tmpDir, fileFmt))

        if (os.access(tempTxt, os.F_OK) == True):
            os.remove(tempTxt)
        txtFd = codecs.open(tempTxt, 'w', "utf-8")
        xlsx2csv = Xlsx2csv(self._filePath, delimiter='\t', hyperlinks=False, dateformat=None,
                            sheetdelimiter=self.sheetdelimiter, skip_empty_lines=False, escape_strings=False, cmd=False)
        if (allSheet == False):
            xlsx2csv.convert(txtFd, 1)
        else:
            xlsx2csv.convert(txtFd, 0)
        txtFd.close()
        clientOutputFilePath = None
        serverOutputFilePath = None
        txtFd = codecs.open(tempTxt, 'r', "utf-8")
        try:
            with txtFd as csvfile:
                csvReader = csv.reader(csvfile, delimiter='\t')
                if (allSheet == False):
                    self._parseFiledBelong(csvReader, Config.belongLine)
                    csvfile.seek(0)
                    self._parseMembersType(csvReader, Config.typeLine)
                else:
                    self._parseFiledBelong(csvReader, Config.belongLine + 1)
                    csvfile.seek(0)
                    self._parseMembersType(csvReader, Config.typeLine + 1)

                # save client
                if (outClientDir != None):
                    csvfile.seek(0)
                    outDirC = outClientDir
                    if not os.path.exists(outDirC):
                        os.makedirs(outDirC)
                    clientOutputFilePath = self._convertToFile(
                        csvReader, os.path.abspath(outDirC), fileName, "C", True, fileFmt)
                    baseName = os.path.basename(filePath)
                    if baseName == "Language.xlsx":
                        splitLanguage = SplitLanguage(
                            clientOutputFilePath, outDirC)
                        splitLanguage.Run()
                        if os.path.exists(clientOutputFilePath):
                            os.remove(clientOutputFilePath)
                # save client
                if (outServerDir != None):
                    csvfile.seek(0)
                    outDirS = outServerDir
                    if not os.path.exists(outDirS):
                        os.makedirs(outDirS)
                    serverOutputFilePath = self._convertToFile(
                        csvReader, os.path.abspath(outDirS), fileName, "S", False, fileFmt)
        except Exception, e:
            raise e
        finally:
            txtFd.close()
        # print "DelFile", tempTxt
        if os.path.exists(tempTxt):
            os.remove(tempTxt)
        return clientOutputFilePath, serverOutputFilePath
