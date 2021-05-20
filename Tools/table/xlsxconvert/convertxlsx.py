#!/usr/bin/python
# -*- coding:utf-8 -*-
#
import sys
import os
import re
import shutil
import traceback

from optparse import OptionParser
from optparse import OptionGroup

from Csv2CustomTxt import Csv2CustomTxt
from Csv2CustomLrgB import Csv2CustomLrgB
from Logger import Logger
import inspect
import os


def MakeDirPv(path):
    if len(path) == 0:
        return
    base_dir = os.path.dirname(path)
    if len(base_dir) == 0:
        base_dir = "."
    # print "MakeDirPv", path, base_dir
    if not os.path.exists(base_dir):
        MakeDirPv(base_dir)
    if not os.path.exists(path):
        os.mkdir(path)


def check_filename_is_legal(file_name):
    return True#re.match("[a-z]+$|[a-z][a-z_]+[a-z]$", file_name) != None


def ConvertSingleFile(inFilePath, outDir, outClientDir, outServerDir, fileFmt, errorMsgList=None):
    file_name = os.path.basename(inFilePath)
    if not os.access(outDir, os.F_OK):
        MakeDirPv(outDir)
    if (inFilePath.endswith(".xlsx") and file_name[:2] != "~$"):
        xlsx2csv = Csv2CustomTxt()
        if (fileFmt.strip() == "xc"):
            xlsx2csv = Csv2CustomLrgB()
        try:
            if (file_name == "language.xlsx"):
                xlsx2csv.ConvertToCsvFile(
                    inFilePath, outClientDir, outServerDir, True, outDir, fileFmt)
            else:
                xlsx2csv.ConvertToCsvFile(
                    inFilePath, outClientDir, outServerDir, False, outDir, fileFmt)
            print ("Convert To Txt %s Success" % (file_name))
        except Exception, e:
            #exstr = traceback.format_exc()
            errorMsg = unicode(e) + '\n' + \
                ("Convert To Txt %s Fail " % (file_name))
            Logger.e(errorMsg)
            if (errorMsgList != None):
                errorMsgList.append(errorMsg)
                return
        if (errorMsgList == None):
            print("Congratulations!")


def ConverXlsxDir(inDir, outDir, target, format):
    errorMsgList = []
    print ("Running..., Please wait a moment")
    for top, dirs, nodirs in os.walk(inDir):
        for file_name in nodirs:
            if (file_name.endswith(".xlsx") and file_name[:2] != "~$"):
                headDir, tailDir = os.path.split(top)
                outfileDir = os.path.join(outDir, tailDir)
                filePath = os.path.join(top, file_name)
                # print filePath
                outClientDir = None
                outServerDir = None
                target = "onlyclient"
                if (target == "onlyclient"):
                    outClientDir = outDir
                elif (target == "onlyserver"):
                    outServerDir = outDir
                elif (target == "all"):
                    outClientDir = outDir + "/client/"
                    outServerDir = outDir + "/server/" + tailDir + "/"
                if (len(tailDir) > 0):
                    if (check_filename_is_legal(tailDir) == False):
                        errorMsgList.append(
                            ("table %s parent dir name ilegal: Only Contain \"a-z, _\" " % (tailDir)))
                if (check_filename_is_legal(file_name[:-5]) == False):
                    errorMsgList.append(
                        ("table %s  file_name ilegal: Only Contain \"a-z, _\"  Example: skill_effect.xlsx " % (file_name[:-5])))
                ConvertSingleFile(filePath, outDir, outClientDir,
                                  outServerDir, "txt", errorMsgList)
    for errMsg in errorMsgList:
        Logger.e(errMsg)
    if (len(errorMsgList) > 0):
        Logger.e("Output Code Error List above")
        assert(False)
    else:
        print("All Success, Congratulations!")


def ParserArgs():
    usage = 'Usage:  OutputCode -t [csharp|cplusplus] -i [FILE|DIR] -o OUTPUT_FILE_PATH'
    parser = OptionParser(usage)

    parser.add_option('-o', '--output',
                      action='store', dest='output',
                      metavar='DIR', help='Output to directory')
    parser.add_option('-t', '--target',
                      action='store', dest='target',
                      choices=["onlyclient", "onlyserver", "all"],
                      default="all",
                      metavar='onlyclient|onlyserver|all', help='Output Client\'s txt or Server\'s txt')
    parser.add_option('-f', '--format',
                      action='store', dest='format',
                      choices=["txt", "xc"],
                      default="xc",
                      metavar='txt|xc', help='Select txt format or xc format')
    parser.add_option("-i", "--input", action="store",
                      metavar='FILE|DIR', dest="input",
                      help="Input a directory or file")

    (option, args) = parser.parse_args()

    if (option.input == None or option.output == None):
        parser.print_usage()
        return False, option, args

    if (option.input == None or not (os.path.isfile(option.input) or os.path.isdir(option.input))):
        parser.print_usage()
        return False, option, args

    return True, option, args


def Run(option, args):
    if (os.path.isfile(option.input)):
        ConvertSingleFile(option.input, option.output, option.output,
                          option.target, option.format)
    elif (os.path.isdir(option.input)):
        ConverXlsxDir(option.input, option.output,
                      option.target, option.format)
    pass
    
reload(sys)
sys.setdefaultencoding('utf-8')
if __name__ == '__main__':
    reload(sys)
    sys.setdefaultencoding('utf-8')
    (succ, option, args) = ParserArgs()
    if (succ):
        Run(option, args)
    else:
        Logger.e("Parser command args error")
