import os

'''
pwd = os.path.abspath(os.path.join(os.getcwd(), "../../UnityProject"))
list_dir = os.listdir(pwd)
oldProjectName =list_dir[0]
'''

'''
pwd = os.path.abspath(os.path.join(os.getcwd(), "../../"))
list_dir = pwd.split('/')
newProjectName = os.path.basename[pwd]
'''

oldProjectName = "Match3Animal"
newProjectName = "Match3DressUp"

print(oldProjectName,"-->",newProjectName)

path_csharpBat = "output_code_csharp.bat"
path_csharpSh = "output_code_csharp.sh"
path_txtBat = "output_txt.bat"
path_txtSh = "output_txt.sh"

paths = [path_csharpBat,path_csharpSh,path_txtBat,path_txtSh]

for path in paths:
	fo = open(path , "r");
	lines = fo.readlines()
	f_w = open(path , "w")
	for line in lines:
		line = line.replace(oldProjectName,newProjectName)
		f_w.write(line)
	fo.close()
	f_w.close()
