#!/usr/bin/python
# -*- coding:utf-8 -*-

import os
import sys
import csv
import codecs
import Config
import shutil
from DataStream import DataStreamReader
from DataStream import DataStreamWriter


class SplitLanguageLrgB():
    """docstring for SplitLanguageLrgB"""
    def __init__(self, srouceLrgbFilePath, outputDir):
        self._srcFilePath = srouceLrgbFilePath
        self._headTable = None
        self._outputDir = os.path.normpath(outputDir)
        
        print self._srcFilePath

    def _PaserScheme(self):
        dataR = DataStreamReader(self._srcFilePath)
        row = dataR.GetRowCount()
        col = dataR.GetColCount()
        print "_PaserScheme", row, col
        self._headTable = []
        for i in range(row):
            if i >= Config.filedNameLine:
                break
            for j in range(col):
                if len(self._headTable) <= i:
                    self._headTable.append([])
                self._headTable[i].append(dataR.ReadNext())

        print self._headTable
        dataR.Close()

    def _Output(self):
        dataR = DataStreamReader(self._srcFilePath)
        row = dataR.GetRowCount()
        col = dataR.GetColCount()
        outputDataW = {}
        for x in xrange(1,len(self._headTable[Config.filedNameLine - 1])):
                filedName = self._headTable[Config.filedNameLine - 1][x]
                belong = self._headTable[Config.belongLine - 1][x]
                if (belong == "N"):
                    continue
                fileDir = self._outputDir
                if os.access(fileDir, os.F_OK) == False:
                    os.makedirs(fileDir)
                print fileDir,x
                outputDataW[str(x)] = DataStreamWriter( fileDir + "/" + "Language_" + filedName + ".xc")
                outputDataW[str(x)].RecordCol(2)
        #print row, col
        for i in range(row):
            for j in range(col):
                if j == 0:
                    val = dataR.ReadNext()
                    for key in outputDataW:
                        #print value
                        if (i == Config.typeLine - 1):
                            outputDataW[key].RecordFieldTypeRowOff()
                        elif (i == Config.filedNameLine - 1):
                            outputDataW[key].RecordFieldNameRowOff()
                        #print i,j
                        outputDataW[key].WriteField(val)
                elif outputDataW.has_key(str(j)):
                    val = dataR.ReadNext()
                    outputDataW[str(j)].WriteField(val)

            if (i == Config.ignoreLine - 1):
                for key in outputDataW:
                    outputDataW[key].EndWriteSheme()
            for key in outputDataW:
                outputDataW[key].EndRow()
        for key in outputDataW:
            outputDataW[key].Close()
        dataR.Close()

    def _Split(self):
        self._PaserScheme()
        # 需要输出的文件fd
        print self._headTable[Config.filedNameLine - 1]
        self._Output()
    def  Run(self):
        self._Split()

        
