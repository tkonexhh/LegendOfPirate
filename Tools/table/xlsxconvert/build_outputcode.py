from distutils.core import setup
import py2exe
 
setup(
        console=["outputcode.py"], 
        data_files=[ ("csharp", ["csharp/DataExtend.tmpl", "csharp/DataTableExtend.tmpl","csharp/Data.tmpl", "csharp/DataTable.tmpl", "csharp/TableParser.tmpl"] ),("cplusplus", ["cplusplus/DataItem_h.tmpl","cplusplus/DataItem_cpp.tmpl","cplusplus/DataTable_h.tmpl","cplusplus/DataTable_cpp.tmpl"])],
        options={"py2exe":{"includes":["Cheetah.DummyTransaction", "ctypes"]}}
)
