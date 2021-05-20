rmdir /s /q output
rmdir /s /q dist
rmdir /s /q build
python build_outputcode.py py2exe
rmdir /s /q build
move dist .\output

rmdir /s /q dist
pyinstaller -F --onefile convertxlsx.py
move dist\convertxlsx.exe .\output\convertxlsx.exe
rmdir /s /q dist
rmdir /s /q build
del convertxlsx.spec

rmdir /s /q dist
pyinstaller -F --onefile copy_csharp_code.py
move dist\copy_csharp_code.exe .\output\copy_csharp_code.exe
rmdir /s /q dist
rmdir /s /q build
del copy_csharp_code.spec


pause