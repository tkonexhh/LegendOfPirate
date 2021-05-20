commentLine = 1 
belongLine = 2 
typeLine = 3 
filedNameLine = 4 
ignoreLine = 3
delimiter = '\t'
hasquote = False
fileExt = "txt"

needVirtual = []
needCopyMethod = []

dontCheckIdUnique = ["action_timecut"]

dontOutputCode = ["const.xlsx", "language.xlsx"]

def CheckNeedOutputCode(file_name):
    if (dontOutputCode.count(file_name) > 0):
        return False
    return True

def CheckNeedCopyMethod(className):
    if (needCopyMethod.count(className) > 0):
        return True
    return False
    
def FormatGetPropName(propName):
    atIdx = propName.find('@')
    if (atIdx != -1):
        ret = propName[atIdx + 1:]
        ret = ret[:1].lower() + ret[1:]
        return ret
    return propName[:1].lower() + propName[1:]

def FormatPrivatePropName(propName):
    atIdx = propName.find('@')
    if (atIdx != -1):
        ret = propName[atIdx + 1:]
        return "m_" + ret
    return "m_" + propName

def CheckNeedVirtual(className, fliedName):
    absFliedName = className + "." + fliedName
    if (needVirtual.count(absFliedName) > 0):
        return True
    return False

def CheckNeedCheckIdUnique(fliedName):
    if (dontCheckIdUnique.count(fliedName) > 0):
        return False
    return True
