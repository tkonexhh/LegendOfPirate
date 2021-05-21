# Dependent
   go install github.com/tealeg/xlsx
# Build
    编译xlsx2txt工具
    go build convertxlsx
    编译代码生成器工具
    go build outputcode

# Config
    可以指定某些xlsx 不输出代码

# Usage
## Usage of outputcode.exe:
    -i string
            (输入目录或文件) input a directory or file
    -o string
            (输出目录) Output to directory
## Usage of convertxlsx.exe:
    -f string
            (选择格式) Select txt format or xc format (default "txt")
    -i string
            (输入目录或文件) input a directory or file
    -o string
            (输出目录) Output to directory
    -t string
            (客户端表或者服务器表) Option: client|server| (default "client")
