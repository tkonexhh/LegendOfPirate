import sys
import os
import re
import shutil
import traceback
import Config
from Logger import Logger

from optparse import OptionParser
from optparse import OptionGroup

from GenSData import CsharpGenerator
from GenSData import CplusplusGenerator
from GenSData import TemplateTableParser


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


def format_class_name(src_class_name):
    class_name = src_class_name.capitalize()
    ret = ""
    i = 0
    while(i < len(class_name)):
        if (class_name[i] == '_'):
            if (i + 1 < len(class_name)):
                ret = ret + class_name[i + 1].upper()
            i = i + 2
        else:
            ret = ret + class_name[i]
            i = i + 1
    return ret
    
def format_dir_name(dir_path):
    dir_path = dir_path.replace("//", '/')
    dir_path = dir_path.replace("\\", '/')
    dir_path = dir_path.replace("\\\\", '/')
    dirs = dir_path.split('/')
    ret = ""
    for i in range(0, len(dirs)):
        if (len(dirs[i]) > 0):
            ret = ret + format_class_name(dirs[i])
        if (i != len(dirs) -1):
            ret = ret + '/'
    return ret

def check_filename_is_legal(file_name):
    return re.match("[a-z]+$|[a-z][a-z_]+[a-z]$", file_name) != None


def GenCodeSingleFile(filePath, outDir, out_rel_dir=None, target="csharp", errorMsgList=None):
    file_name = os.path.basename(filePath)
    if (file_name.endswith(".xlsx") == False):
        return

    if (Config.CheckNeedOutputCode(file_name) == False):
        print "Ingore " + file_name
        return
    if (check_filename_is_legal(file_name[:-5]) == False):
        errorMsgList.append(
            ("table %s  file_name ilegal: Only Contain \"a-z, _\"  Example: skill_effect.xlsx " % (file_name)))
        return
    if not os.access(outDir, os.F_OK):
        MakeDirPv(outDir)
    try:
        # output c#
        if (target == "csharp"):
            gen_dir=outDir + "/Generate/"
            extend_dir=outDir + "/Extend/"
            if (out_rel_dir != None):
                format_dir = format_dir_name(out_rel_dir)
                #print out_rel_dir, format_dir
                gen_dir = gen_dir + format_dir
                extend_dir = extend_dir + format_dir
            if not os.access(gen_dir, os.F_OK):
                MakeDirPv(gen_dir)
            if not os.access(extend_dir, os.F_OK):
                MakeDirPv(extend_dir)
            generator=CsharpGenerator()
            generator.GenerateToFile(filePath, gen_dir, extend_dir)

        if (target == "cplusplus"):
            # output c++
            generator=CplusplusGenerator()
            generator.GenerateToFile(filePath, outDir)
        print ("OutputCode %s Success" % (file_name))
    except Exception, e:
        print e
        print traceback.format_exc()
        errorMsg=("GenSData %s Fail" % (file_name))
        Logger.e(errorMsg)
        if (errorMsgList != None):
            errorMsgList.append(errorMsg)
            return
    if (errorMsgList == None):
        print("Congratulations!")


def GenCodeDir(inDir, outDir, target):
    errorMsgList=[]
    print ("Running..., Please wait a moment")
    for top, dirs, nodirs in os.walk(inDir):
        for file_name in nodirs:
            if (file_name.endswith(".xlsx") and file_name[:2] != "~$"):
                filePath=os.path.join(top, file_name)
                rel_path=os.path.relpath(top, inDir)
                GenCodeSingleFile(filePath, outDir, rel_path,
                                  target, errorMsgList)
    for errMsg in errorMsgList:
        Logger.e(errMsg)
    if (len(errorMsgList) > 0):
        Logger.e("Output Code Error List above")
        assert(False)
    else:
        print("All Success, Congratulations!")


def ParserArgs():
    usage='Usage:  OutputCode -t [csharp|cplusplus] -i [FILE|DIR] -o OUTPUT_FILE_PATH'
    parser=OptionParser(usage)

    parser.add_option('-o', '--output',
                      action='store', dest='output',
                      metavar='DIR', help='Output to directory')
    parser.add_option('-t', '--target',
                      action='store', dest='target',
                      choices=["csharp", "cplusplus"],
                      default="csharp",
                      metavar='csharp|cplusplus', help='You Must Selecct Output Target (csharp or cplusplus)')
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
        GenCodeSingleFile(option.input, option.output, None, option.target)
    elif (os.path.isdir(option.input)):
        GenCodeDir(option.input, option.output, option.target)

    pass

reload(sys)
sys.setdefaultencoding('utf-8')
if __name__ == '__main__':
    (succ, option, args) = ParserArgs()
    if (succ):
        Run(option, args)
    else:
        Logger.e("Parser command args error")
