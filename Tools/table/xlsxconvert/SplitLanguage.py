#!/usr/bin/python
# -*- coding:utf-8 -*-

import os
import sys
import csv
import codecs
import Config
import shutil


class SplitLanguage():
    """docstring for SplitLanguage"""

    def __init__(self, srouceTxtFilePath, outputDir):
        self._txtFilePath = srouceTxtFilePath
        self._headTable = None
        self._outputDir = os.path.normpath(outputDir)
        print self._txtFilePath

    def _PaserHead(self):
        txtFd = codecs.open(self._txtFilePath, 'r', "utf_8_sig")
        csvReader = csv.reader(txtFd, delimiter='\t')
        self._headTable = []
        i = 0
        for row in csvReader:
            j = 0
            if i >= Config.filedNameLine:
                break
            for record in row:
                if len(self._headTable) <= i:
                    self._headTable.append([])
                self._headTable[i].append(record)
                j = j + 1
            i = i + 1
        print self._headTable
        txtFd.close()

    def _Output(self):
        txtFd = codecs.open(self._txtFilePath, 'r', "utf_8_sig")
        csvReader = csv.reader(txtFd, delimiter='\t')
        outputFd = {}
        for x in xrange(1, len(self._headTable[Config.filedNameLine - 1])):
            filedName = self._headTable[Config.filedNameLine - 1][x]
            belong = self._headTable[Config.belongLine - 1][x]
            if (belong == "N"):
                continue
            fileDir = self._outputDir
            if os.access(fileDir, os.F_OK) == False:
                os.makedirs(fileDir)
            print fileDir, x
            outputFd[str(x)] = codecs.open(fileDir + "/" +
                                           "Language_" + filedName + ".txt", "w+", "utf_8_sig")
            # print outputFd, len(outputFd)
        i = 0
        for row in csvReader:
            j = 0
            for record in row:
                value = unicode(record, 'utf-8')
                if j == 0:
                    for key in outputFd:
                        # print value
                        outputFd[key].write(("%s" % (value)))
                elif outputFd.has_key(str(j)):
                    outputFd[str(j)].write(Config.delimiter)
                    outputFd[str(j)].write(("%s" % (value)))
                    outputFd[str(j)].write("\r\n")
                j = j + 1
            i = i + 1
            if (i == Config.belongLine):
                continue
        for key in outputFd:
            outputFd[key].close()
        txtFd.close()

    def _Split(self):
        self._PaserHead()
        # 需要输出的文件fd
        print self._headTable[Config.filedNameLine - 1]
        self._Output()

    def Run(self):
        try:
            self._Split()
        except Exception, e:
            print e, "SplitLanguage"

reload(sys)
sys.setdefaultencoding("utf-8")
# splitLanguage = SplitLanguage("G:/proj_xx/source_code/client/trunk/project_nx/Assets/BuildRes/standalone/Config/Language.txt",\
#    "G:/proj_xx/source_code/client/trunk/project_nx/Assets/BuildRes/standalone/Config")
# splitLanguage.Run()
