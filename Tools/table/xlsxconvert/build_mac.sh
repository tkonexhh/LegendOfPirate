pyinstaller -F --onefile convetxlsx.py
mkdir -pv ./output/
mv dist/ConvetXlsx ./output/convetxlsx
rm -rf dist
rm -rf build
rm ConvetXlsx.spec
