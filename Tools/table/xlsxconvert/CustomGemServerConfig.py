#!/usr/bin/python
# -*- coding:utf-8 -*-
import os
import sys
import shutil
import zipfile
import platform
import Xlsx2CsvDir
import codecs
import re

def UnZip(zip_path, out_dir):
    print "UnZip ", zip_path, " to ", out_dir
    zfile   = zipfile.ZipFile(zip_path, 'r')
    for filename in zfile.namelist():
        #parent dir not exist, create it
        parent_dir_path    = os.path.split(os.path.join(out_dir, filename))[0]
        if not os.path.exists(parent_dir_path):
            os.mkdir(parent_dir_path)
        #file is a dir, skip it
        if filename.endswith("/") or filename.endswith("\\"):
            continue
        data = zfile.read(filename)
        file = open(os.path.join(out_dir, filename), 'w+b')
        file.write(data)
        file.close()
    zfile.close()

def ZipDir(dir_path, zip_path):
    print "ZipDir ", dir_path, " to ", zip_path
    filelist = []
    if os.path.isfile(dir_path):
        filelist.append(dir_path)
    else :
        for root, dirs, files in os.walk(dir_path):
            for name in files:
                filelist.append(os.path.join(root, name))

    zf = zipfile.ZipFile(zip_path, "w", zipfile.zlib.DEFLATED)
    for tar in filelist:
        arcname = tar[len(dir_path):]
        #print arcname
        zf.write(tar,arcname)
    zf.close()

def CopyFile(srcPath, dstPath):
    print "CopyFile", srcPath, dstPath
    if os.path.exists(srcPath):
        shutil.copyfile(srcPath, dstPath)

def DelFile(filePath):
    print "DelFile", filePath
    if os.path.exists(filePath):
        os.remove(filePath)

def DelDirForce(dirPath):
    print "DelDirForce", dirPath
    if os.path.exists(dirPath):
        shutil.rmtree(dirPath)

def MakeDirForce(dirPath):
    print "MakeDirForce", dirPath
    if os.path.exists(dirPath):
        DelDirForce(dirPath)
    os.makedirs(dirPath)
    

def SvnSmartClean(dirPath):
    cmdStr      = ""
    if platform.system().lower() == "windows":
        cmdStr = "cd " + dirPath + " && svn st"
    else:
        cmdStr = "cd " + dirPath + "; svn st"
    print cmdStr
    output      = os.popen(cmdStr).read()
    lineList    = output.split("\n")
    delList     = []
    print "SvnSmartClean", dirPath
    print "output", output
    for line in lineList:
        line = line.strip()
        if line.startswith("?"):
            for pos in xrange(1, len(line)):
                if line[pos] != ' ' and line[pos] != '\t':
                    break
            path = line[pos:]
            path = dirPath + "/" + path
            delList.append(path)
    for path in delList:
        if os.path.exists(path):
            if os.path.isfile(path):
                os.remove(path)
                print "delete file", path
            else:
                dir.DelDirForce(path)
                print "delete dir", path

def GetSVNVersion(path):
    runStr = ("svn info --xml -r HEAD \"%s\"" %(path))
    outPut = os.popen(runStr).read()

    print runStr
    print outPut
    startIdx =  outPut.find("revision=\"") + len("revision=\"")
    print outPut[startIdx:]
    endIdx = 0
    for i in xrange(startIdx, len(outPut)):
        if outPut[i] == "\"":
            endIdx = i
            break
    return outPut[startIdx: endIdx]

def CleanTempDir(tmpDir):
    print "DelDirForce", tmpDir
    if os.path.exists(tmpDir):
        shutil.rmtree(tmpDir)
    pass

def OverrideFiles(tableDir, customDir):
    #当前工作配表
    currentFileDict = {}
    for top, dirs, nodirs in os.walk(tableDir):
        for file_name in nodirs:
            if (file_name.endswith(".xlsx") and file_name[:2] != "~$"):
                filePath = os.path.join(top, file_name)
                currentFileDict[file_name] = filePath

    #构建者上传的文件配表
    curstomTableDict = {}                  
    for top, dirs, nodirs in os.walk(customDir):
        for file_name in nodirs:
            if (file_name.endswith(".xlsx") and file_name[:2] != "~$"):
                filePath = os.path.join(top, file_name)
                curstomTableDict[file_name] = filePath
    #Override
    for file_name in curstomTableDict:
        if (currentFileDict.has_key(file_name)):
            DelFile(currentFileDict[file_name])
            CopyFile(curstomTableDict[file_name], currentFileDict[file_name])

def Xlsx2Txt(tableDir, outDir, branchName):
    Xlsx2CsvDir.Convert(tableDir, outDir, branchName)
    pass

#重写ServerConfigVersion.txt 打上svn版本号印记
def OverrideWriteServerConfigVersion(txtPath, svnRevision):
    fd = codecs.open(txtPath, 'w', "utf-8")
    fd.seek(0)
    fd.write(codecs.BOM_UTF8)
    fd.write("流水号\t更新服务器配置版本\r\n")
    fd.write("S\tS\r\n")
    fd.write("int\tstring\r\n")
    fd.write("Id\tUpdateVersion\r\n")
    fd.write(("1\t%s\r\n" %(svnRevision)))
    fd.close()

    
def Run(tableDir, targetTableDir, outputZipFile, tmpDir, customDirZip = None):
    assert(outputZipFile[-4:] == ".zip")
    svnRevision = GetSVNVersion(tableDir)
    outputZipFile = outputZipFile[:-4] + "_svnversion@" + str(svnRevision) + ".zip"
    print tableDir, customDirZip, targetTableDir, outputZipFile, tmpDir
    customDir = tmpDir + "/customTable"
    txtOutputDir = tmpDir +"/TxtOutput"
    txtOutputDir = tmpDir +"/TxtOutput"
    outputZipDir = os.path.dirname(outputZipFile)
    MakeDirForce(tmpDir)
    MakeDirForce(customDir)
    MakeDirForce(txtOutputDir)
    MakeDirForce(outputZipDir)
    SvnSmartClean(tableDir)

    if (customDirZip != None and len(customDirZip) > 0 and os.path.exists(customDirZip)):
        UnZip(customDirZip, customDir)
        OverrideFiles(tableDir + "/" + targetTableDir , customDir)
    Xlsx2Txt(tableDir, txtOutputDir, targetTableDir)
    OverrideWriteServerConfigVersion(txtOutputDir + "/server" + "/ServerConfigVersion.txt", svnRevision)
    ZipDir(txtOutputDir + "/server", outputZipFile)
    pass

if __name__ == '__main__':
    tableDir = sys.argv[1]
    targetTableDir = sys.argv[2]
    outputZipFile = sys.argv[3]
    tmpDir = sys.argv[4]
    customDirZip = None
    if (len(sys.argv) >= 6):
        customDirZip = sys.argv[5]
    Run(tableDir, targetTableDir, outputZipFile, tmpDir, customDirZip)