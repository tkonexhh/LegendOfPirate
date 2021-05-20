#!/usr/bin/python
# -*- coding:utf-8 -*-
import os
import sys
import shutil


def check_need_copy_extend(src_file_path, dst_file_path):
    if (not os.path.exists(dst_file_path)):
        return True
    else:
        return False


def check_need_copy_table(table_name):
    return True


def make_dir_pv(dir_path):
    if len(dir_path) == 0:
        return
    base_dir = os.path.dirname(dir_path)
    if len(base_dir) == 0:
        base_dir = "."
    if not os.path.exists(base_dir):
        make_dir_pv(base_dir)
    if not os.path.exists(dir_path):
        os.mkdir(dir_path)


def copy_file(src_file, dst_file):
    make_dir_pv(os.path.dirname(dst_file))
    if os.path.exists(src_file):
        shutil.copyfile(src_file, dst_file)


def copy_dir(src_dir, dst_dir):
    for top, dirs, nodirs in os.walk(src_dir):
        for file_name in nodirs:
            file_path = os.path.join(top, file_name)
            rel_path = os.path.relpath(top, src_dir)
            if (file_path.count("Generate") == 1):
                if (check_need_copy_table(file_name)):
                    copy_file(file_path, dst_dir + rel_path + "/" + file_name)
            else:
                if (check_need_copy_table(file_name)):
                    if (check_need_copy_extend(file_path, dst_dir +
                                               rel_path + "/" + file_name)):
                        copy_file(file_path, dst_dir +
                                  rel_path + "/" + file_name)


if __name__ == '__main__':
    src_dir = sys.argv[1]
    dst_dir = sys.argv[2]
    copy_dir(src_dir, dst_dir)
