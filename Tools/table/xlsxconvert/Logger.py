#!/usr/bin/python
# -*- coding=utf-8 -*-

import os
import sys
import platform
import ctypes,sys
 
STD_INPUT_HANDLE = -10
STD_OUTPUT_HANDLE = -11
STD_ERROR_HANDLE = -12
 
# 字体颜色定义 ,关键在于颜色编码，由2位十六进制组成，分别取0~f，前一位指的是背景色，后一位指的是字体色
#由于该函数的限制，应该是只有这16种，可以前景色与背景色组合。也可以几种颜色通过或运算组合，组合后还是在这16种颜色中
 
# Windows CMD命令行 字体颜色定义 text colors
FOREGROUND_BLACK = 0x00 # black.
FOREGROUND_DARKBLUE = 0x01 # dark blue.
FOREGROUND_DARKGREEN = 0x02 # dark green.
FOREGROUND_DARKSKYBLUE = 0x03 # dark skyblue.
FOREGROUND_DARKRED = 0x04 # dark red.
FOREGROUND_DARKPINK = 0x05 # dark pink.
FOREGROUND_DARKYELLOW = 0x06 # dark yellow.
FOREGROUND_DARKWHITE = 0x07 # dark white.
FOREGROUND_DARKGRAY = 0x08 # dark gray.
FOREGROUND_BLUE = 0x09 # blue.
FOREGROUND_GREEN = 0x0a # green.
FOREGROUND_SKYBLUE = 0x0b # skyblue.
FOREGROUND_RED = 0x0c # red.
FOREGROUND_PINK = 0x0d # pink.
FOREGROUND_YELLOW = 0x0e # yellow.
FOREGROUND_WHITE = 0x0f # white.
 
 
# Windows CMD命令行 背景颜色定义 background colors
BACKGROUND_BLUE = 0x10 # dark blue.
BACKGROUND_GREEN = 0x20 # dark green.
BACKGROUND_DARKSKYBLUE = 0x30 # dark skyblue.
BACKGROUND_DARKRED = 0x40 # dark red.
BACKGROUND_DARKPINK = 0x50 # dark pink.
BACKGROUND_DARKYELLOW = 0x60 # dark yellow.
BACKGROUND_DARKWHITE = 0x70 # dark white.
BACKGROUND_DARKGRAY = 0x80 # dark gray.
BACKGROUND_BLUE = 0x90 # blue.
BACKGROUND_GREEN = 0xa0 # green.
BACKGROUND_SKYBLUE = 0xb0 # skyblue.
BACKGROUND_RED = 0xc0 # red.
BACKGROUND_PINK = 0xd0 # pink.
BACKGROUND_YELLOW = 0xe0 # yellow.
BACKGROUND_WHITE = 0xf0 # white.
 

 
class WindowLogger:
    @classmethod
    def __set_cmd_text_color(cls, color):
        std_out_handle = ctypes.windll.kernel32.GetStdHandle(STD_OUTPUT_HANDLE)
        ret = ctypes.windll.kernel32.SetConsoleTextAttribute(std_out_handle, color)
        return ret

    @classmethod
    def __resetColor(cls):
        WindowLogger.__set_cmd_text_color(FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE)
    
    @staticmethod
    def printYellow(mess):
        try:
            WindowLogger.__set_cmd_text_color(FOREGROUND_YELLOW)
            sys.stdout.write(mess)
            sys.stdout.write("\n")
        except Exception, e:
            raise e
        finally:
            WindowLogger.__resetColor()

    @staticmethod
    def printBlue(mess):
        try:
            WindowLogger.__set_cmd_text_color(FOREGROUND_BLUE)
            sys.stdout.write(str(mess))
            sys.stdout.write("\n")
        except Exception, e:
            raise e
        finally:
            WindowLogger.__resetColor()
    @staticmethod
    def printRed(mess):
        try:
            WindowLogger.__set_cmd_text_color(FOREGROUND_RED)
            sys.stdout.write(str(mess))
            sys.stdout.write("\n")
        except Exception, e:
            raise e
        finally:
            WindowLogger.__resetColor()

class Logger:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKGREEN = '\033[92m'
    WARNING = '\033[94m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'

    ERROR_TAG = 'ERROR:'
    INFO_TAG = 'INFO:'
    WARNING_TAG = 'WARNING:'

    @staticmethod
    def isWindowsSystem():
        return 'Windows' in platform.system()
        
    @staticmethod
    def i(mess):
        print Logger.INFO_TAG + mess

    @staticmethod
    def w(mess):
        if (Logger.isWindowsSystem()):
            WindowLogger.printBlue(mess)
        else:
            print Logger.WARNING  + Logger.WARNING_TAG + " " + mess + Logger.ENDC

    @staticmethod
    def e(mess):
        if (Logger.isWindowsSystem()):
            WindowLogger.printRed(mess)
        else:
            print Logger.FAIL + Logger.ERROR_TAG + " " + mess + Logger.ENDC
#try:
#    raise Exception("ERRORXXX失败")
#except Exception, e:
#    Logger.i(str(e))
#    Logger.w(str(e))
#    Logger.e(str(e))
